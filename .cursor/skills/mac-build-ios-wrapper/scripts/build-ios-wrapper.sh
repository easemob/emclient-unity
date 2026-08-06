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
WRAPPER_IOS_DIR="$REPO_ROOT/AgoraChatSDK/CWrapper/Wrapper/Wrapper_iOS"
EMCLIENT_UNITY_WRAPPER_IOS_BUILD_PROJ_FILE="$WRAPPER_IOS_DIR/wrapper/wrapper.xcodeproj"
PBXPROJ_FILE="$WRAPPER_IOS_DIR/wrapper/wrapper.xcodeproj/project.pbxproj"
PBXPROJ_RELATIVE_PATH="AgoraChatSDK/CWrapper/Wrapper/Wrapper_iOS/wrapper/wrapper.xcodeproj/project.pbxproj"
REPLACE_FRAMEWORK_SCRIPT="$SCRIPT_DIR/replace_framework.rb"
OUTPUT_DIR="$WRAPPER_IOS_DIR/build/Release-iphoneos"
WORK_DIR="$(mktemp -d "${TMPDIR:-/tmp}/mac-ios-wrapper.XXXXXX")"
RESTORE_PROJECT=0

cleanup() {
  if [[ "$RESTORE_PROJECT" == "1" ]]; then
    if git -C "$REPO_ROOT" rev-parse --is-inside-work-tree >/dev/null 2>&1; then
      echo "[INFO] Restoring modified Xcode project file..."
      git -C "$REPO_ROOT" checkout -- "$PBXPROJ_RELATIVE_PATH" || true
    fi
  fi
  rm -rf "$WORK_DIR"
}
trap cleanup EXIT

if [[ ! -d "$WRAPPER_IOS_DIR" ]]; then
  echo "[ERROR] Wrapper_iOS directory not found: $WRAPPER_IOS_DIR"
  exit 1
fi

if [[ "$COMPILE_TYPE" == "shengwang" ]]; then
  if ! command -v ruby >/dev/null 2>&1; then
    echo "[ERROR] Ruby is required for shengwang builds but is not installed."
    echo "Install Ruby with Homebrew: brew install ruby"
    exit 1
  fi

  if ! ruby -e "require 'xcodeproj'" >/dev/null 2>&1; then
    echo "[ERROR] Ruby gem xcodeproj is required for shengwang builds but is not installed."
    echo "Install it with: gem install xcodeproj"
    exit 1
  fi
fi

if [[ -z "${IOS_SDK_DOWNLOAD_URL:-}" ]]; then
  echo "[ERROR] IOS_SDK_DOWNLOAD_URL is not set."
  echo "Set it for the current shell:"
  echo "export IOS_SDK_DOWNLOAD_URL=\"https://example.com/your-ios-sdk.zip\""
  echo
  echo "Persist it in zsh:"
  echo "echo 'export IOS_SDK_DOWNLOAD_URL=\"https://example.com/your-ios-sdk.zip\"' >> ~/.zshrc"
  echo "source ~/.zshrc"
  exit 1
fi

echo "[INFO] Compile type: $COMPILE_TYPE"
echo "[INFO] IOS_SDK_DOWNLOAD_URL=$IOS_SDK_DOWNLOAD_URL"
cd "$WORK_DIR"
curl -O "$IOS_SDK_DOWNLOAD_URL"

DOWNLOADED_FILE="$(basename "${IOS_SDK_DOWNLOAD_URL%%\?*}")"
if [[ ! -f "$DOWNLOADED_FILE" ]]; then
  for candidate in *; do
    if [[ -f "$candidate" ]]; then
      DOWNLOADED_FILE="$candidate"
      break
    fi
  done
fi
if [[ -z "${DOWNLOADED_FILE:-}" || ! -f "$DOWNLOADED_FILE" ]]; then
  echo "[ERROR] Failed to locate downloaded zip package."
  exit 1
fi

UNZIP_DIR="$WORK_DIR/unzipped"
mkdir -p "$UNZIP_DIR"
echo "[INFO] Unzipping package: $DOWNLOADED_FILE"
unzip -q "$DOWNLOADED_FILE" -d "$UNZIP_DIR"

if [[ "$COMPILE_TYPE" == "shengwang" ]]; then
  IOS_SDK_FRAMEWORK_NAME="AgoraChat.framework"
  IOS_SDK_OLD_FRAMEWORK_NAME="HyphenateChat.framework"
else
  IOS_SDK_FRAMEWORK_NAME="HyphenateChat.framework"
  IOS_SDK_OLD_FRAMEWORK_NAME=""
fi

SOURCE_FRAMEWORK_PATH="$(find "$UNZIP_DIR" -type d -name "$IOS_SDK_FRAMEWORK_NAME" -print -quit)"
if [[ -z "$SOURCE_FRAMEWORK_PATH" ]]; then
  echo "[ERROR] Failed to find $IOS_SDK_FRAMEWORK_NAME in extracted files."
  exit 1
fi

echo "[INFO] Using framework: $SOURCE_FRAMEWORK_PATH"
TARGET_FRAMEWORK_PATH="$WRAPPER_IOS_DIR/$IOS_SDK_FRAMEWORK_NAME"
rm -rf "$TARGET_FRAMEWORK_PATH"
cp -R "$SOURCE_FRAMEWORK_PATH" "$TARGET_FRAMEWORK_PATH"

cd "$WRAPPER_IOS_DIR"
rm -rf build

if [[ "$COMPILE_TYPE" == "shengwang" ]]; then
  echo "[INFO] Replacing framework reference in Xcode project..."
  ruby "$REPLACE_FRAMEWORK_SCRIPT" "$EMCLIENT_UNITY_WRAPPER_IOS_BUILD_PROJ_FILE" "$IOS_SDK_OLD_FRAMEWORK_NAME" "$IOS_SDK_FRAMEWORK_NAME"
  RESTORE_PROJECT=1

  xcodebuild -workspace OCWrapperDemo.xcworkspace -scheme ChatCWrapper -configuration Release -sdk iphoneos BUILD_DIR="$(pwd)/build" BUILD_ROOT="$(pwd)/build" ARCHS="arm64" VALID_ARCHS="arm64" CLANG_DEBUG_INFORMATION_LEVEL="line-tables-only" GCC_OPTIMIZATION_LEVEL=s CODE_SIGNING_ALLOWED=NO ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES=NO GCC_PREPROCESSOR_DEFINITIONS="AgoraChat" clean build
else
  xcodebuild -workspace OCWrapperDemo.xcworkspace -scheme ChatCWrapper -configuration Release -sdk iphoneos BUILD_DIR="$(pwd)/build" BUILD_ROOT="$(pwd)/build" ARCHS="arm64" VALID_ARCHS="arm64" CLANG_DEBUG_INFORMATION_LEVEL="line-tables-only" GCC_OPTIMIZATION_LEVEL=s CODE_SIGNING_ALLOWED=NO ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES=NO clean build
fi

MISSING=0
for framework_name in wrapper.framework ChatCWrapper.framework; do
  if [[ -d "$OUTPUT_DIR/$framework_name" ]]; then
    echo "[INFO] Found $OUTPUT_DIR/$framework_name"
  else
    echo "[ERROR] Missing $OUTPUT_DIR/$framework_name"
    MISSING=1
  fi
done

if [[ "$MISSING" != "0" ]]; then
  exit 1
fi

echo "[INFO] iOS wrapper build completed successfully."
