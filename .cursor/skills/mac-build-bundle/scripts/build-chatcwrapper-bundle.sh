#!/usr/bin/env bash
set -euo pipefail

COMPILE_TYPE="${1:-default}"
if [[ "$COMPILE_TYPE" != "default" && "$COMPILE_TYPE" != "shengwang" ]]; then
  echo "[ERROR] Invalid compile type: $COMPILE_TYPE"
  echo "Usage: $(basename "$0") [default|shengwang]"
  exit 1
fi

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/../../../.." && pwd)"
PROJ_DIR="$REPO_ROOT/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/wrapper_build/proj"
CMAKE_LOG="$(mktemp -t chatcwrapper_cmake_XXXX.log)"

if [[ ! -d "$PROJ_DIR" ]]; then
  echo "[ERROR] Build directory not found: $PROJ_DIR"
  exit 1
fi

echo "[INFO] Compile type: $COMPILE_TYPE"
echo "[INFO] Project dir: $PROJ_DIR"
cd "$PROJ_DIR"

echo "[INFO] Cleaning project directory content..."
shopt -s dotglob nullglob
for item in *; do
  rm -rf -- "$item"
done
shopt -u dotglob nullglob

echo "[INFO] Running CMake with Xcode generator..."
cmake -G Xcode .. 2>&1 | tee "$CMAKE_LOG"

LAST_CMAKE_LINE="$(awk 'NF { line=$0 } END { print line }' "$CMAKE_LOG")"
echo "[INFO] Last CMake line: $LAST_CMAKE_LINE"
if [[ "$LAST_CMAKE_LINE" != *"Build files have been written to"* ]]; then
  echo "[ERROR] CMake did not finish with expected success message."
  exit 1
fi

COMPILE_SCHEMA_CHATCWRAPPER="ChatCWrapper"
BUILD_NO="$(date +%s)"
if [[ "$COMPILE_TYPE" == "shengwang" ]]; then
  EASEMOB_MACROS="\$(inherited) BUILD_NO=$BUILD_NO USE_SHENGWANG_DOMAIN"
else
  EASEMOB_MACROS="\$(inherited) BUILD_NO=$BUILD_NO"
fi

echo "[INFO] BUILD_NO=$BUILD_NO"
echo "[INFO] COMPILE_SCHEMA_CHATCWRAPPER=$COMPILE_SCHEMA_CHATCWRAPPER"
echo "[INFO] EASEMOB_MACROS=$EASEMOB_MACROS"
echo "[INFO] Building ChatCWrapper.bundle..."
xcodebuild \
  -project WrapperBuilder.xcodeproj \
  -scheme "$COMPILE_SCHEMA_CHATCWRAPPER" \
  -configuration Debug \
  -destination 'generic/platform=macOS' \
  GCC_PREPROCESSOR_DEFINITIONS="$EASEMOB_MACROS"

OUTPUT_BUNDLE="$PROJ_DIR/lib/Debug/ChatCWrapper.bundle"
echo "[INFO] Build finished."
echo "[INFO] Bundle output: $OUTPUT_BUNDLE"
if [[ -e "$OUTPUT_BUNDLE" ]]; then
  echo "[INFO] Bundle exists."
else
  echo "[WARN] Bundle not found at expected path. Please verify Xcode output logs."
fi
