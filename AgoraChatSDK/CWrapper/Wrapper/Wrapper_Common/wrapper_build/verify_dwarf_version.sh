#!/bin/bash

# 验证 dSYM 文件是否使用 DWARF 4 格式
# 用法: ./verify_dwarf_version.sh <dSYM_path>

if [ "$#" -ne 1 ]; then
    echo "Usage: $0 <dSYM_path>"
    echo "Example: $0 ./lib/Debug/easemob_universal.framework.dSYM"
    exit 1
fi

DSYM_PATH="$1"

if [ ! -d "$DSYM_PATH" ]; then
    echo "Error: $DSYM_PATH does not exist or is not a directory"
    exit 1
fi

# 查找 DWARF 文件
DWARF_FILE=$(find "$DSYM_PATH" -type f -path "*/DWARF/*" | head -1)

if [ -z "$DWARF_FILE" ]; then
    echo "Error: No DWARF file found in $DSYM_PATH"
    exit 1
fi

echo "Checking DWARF version for: $DWARF_FILE"
echo "=========================================="

# 使用 dwarfdump 检查 DWARF 版本
DWARF_VERSION=$(dwarfdump --debug-info "$DWARF_FILE" 2>&1 | grep -m 1 "version = " | sed 's/.*version = 0x\([0-9]*\).*/\1/')

if [ -z "$DWARF_VERSION" ]; then
    echo "Error: Could not determine DWARF version"
    exit 1
fi

echo "DWARF Version: $DWARF_VERSION"

if [ "$DWARF_VERSION" = "0004" ] || [ "$DWARF_VERSION" = "4" ]; then
    echo "✅ SUCCESS: Using DWARF 4"
    exit 0
elif [ "$DWARF_VERSION" = "0005" ] || [ "$DWARF_VERSION" = "5" ]; then
    echo "❌ FAIL: Still using DWARF 5 (incompatible with old dump_syms)"
    exit 1
else
    echo "⚠️  WARNING: Unknown DWARF version: $DWARF_VERSION"
    exit 1
fi
