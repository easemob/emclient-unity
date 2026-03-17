#!/bin/bash

# Script to verify if SO libraries support 16KB page size
# Usage: ./verify_16kb.sh

echo "=========================================="
echo "Verifying 16KB Page Size Support"
echo "=========================================="
echo ""

# Find NDK path
if [ -n "$ANDROID_NDK_HOME" ]; then
    NDK_PATH="$ANDROID_NDK_HOME"
elif [ -n "$ANDROID_NDK_ROOT" ]; then
    NDK_PATH="$ANDROID_NDK_ROOT"
elif [ -n "$NDK_ROOT" ]; then
    NDK_PATH="$NDK_ROOT"
else
    # Try to find ndk-build in PATH
    NDK_BUILD=$(which ndk-build 2>/dev/null)
    if [ -n "$NDK_BUILD" ]; then
        NDK_PATH=$(dirname "$NDK_BUILD")
        echo "Found NDK via ndk-build: $NDK_PATH"
    else
        echo "⚠️  Warning: NDK path not found in environment variables"
        echo "Trying system readelf/llvm-readelf..."
    fi
fi

# Find readelf tool
READELF=""
if [ -n "$NDK_PATH" ]; then
    # Try to find llvm-readelf in NDK
    if [ -f "$NDK_PATH/toolchains/llvm/prebuilt/darwin-x86_64/bin/llvm-readelf" ]; then
        READELF="$NDK_PATH/toolchains/llvm/prebuilt/darwin-x86_64/bin/llvm-readelf"
        echo "✓ Using NDK llvm-readelf: $READELF"
    elif [ -f "$NDK_PATH/toolchains/llvm/prebuilt/linux-x86_64/bin/llvm-readelf" ]; then
        READELF="$NDK_PATH/toolchains/llvm/prebuilt/linux-x86_64/bin/llvm-readelf"
        echo "✓ Using NDK llvm-readelf: $READELF"
    fi
fi

# Fallback to system tools
if [ -z "$READELF" ]; then
    if command -v llvm-readelf &> /dev/null; then
        READELF="llvm-readelf"
        echo "✓ Using system llvm-readelf"
    elif command -v readelf &> /dev/null; then
        READELF="readelf"
        echo "✓ Using system readelf"
    else
        echo "❌ Error: Cannot find readelf or llvm-readelf!"
        echo ""
        echo "Please either:"
        echo "  1. Set ANDROID_NDK_HOME environment variable"
        echo "  2. Install binutils (brew install binutils)"
        echo "  3. Manually run: /path/to/ndk/toolchains/llvm/prebuilt/darwin-x86_64/bin/llvm-readelf -l your.so"
        exit 1
    fi
fi

echo ""

LIBS_DIR="./libs"

if [ ! -d "$LIBS_DIR" ]; then
    echo "❌ Error: libs directory not found!"
    echo "Please run build.sh first to generate the libraries."
    exit 1
fi

# Find all .so files
SO_FILES=$(find "$LIBS_DIR" -name "*.so")

if [ -z "$SO_FILES" ]; then
    echo "❌ Error: No .so files found in $LIBS_DIR"
    exit 1
fi

echo "Found SO libraries:"
echo "$SO_FILES"
echo ""
echo "=========================================="
echo ""

# Check each SO file
for so_file in $SO_FILES; do
    echo "📦 Checking: $so_file"
    echo "----------------------------------------"
    
    # Get alignment using the found readelf tool
    ALIGN=$("$READELF" -Wl "$so_file" | grep "LOAD" | head -1 | awk '{print $NF}')
    
    if [ "$ALIGN" = "0x4000" ]; then
        echo "✅ PASS: Alignment is 0x4000 (16KB)"
    elif [ "$ALIGN" = "0x1000" ]; then
        echo "⚠️  WARNING: Alignment is 0x1000 (4KB) - NOT 16KB!"
    else
        echo "❓ UNKNOWN: Alignment is $ALIGN"
    fi
    
    echo ""
    echo "Full LOAD segments info:"
    "$READELF" -Wl "$so_file" | grep -A 1 "LOAD"
    
    echo ""
    echo "=========================================="
    echo ""
done

echo "✨ Verification complete!"
echo ""
echo "Legend:"
echo "  0x4000 = 16KB (16384 bytes) ✅ Required for Android 15+"
echo "  0x1000 = 4KB  (4096 bytes)  ⚠️  Legacy page size"

