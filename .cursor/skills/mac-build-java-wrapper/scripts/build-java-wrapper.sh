#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/../../../.." && pwd)"
JAVA_WRAPPER_DEMO_DIR="$REPO_ROOT/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Android/JavaWrapperDemo"
EMCLIENT_UNITY_WRAPPER_ANDROID_LIBS_DIR="$JAVA_WRAPPER_DEMO_DIR/wrapper/libs"
EMCLIENT_UNITY_WRAPPER_ANDROID_BUILD_GRADLE_FILE="$JAVA_WRAPPER_DEMO_DIR/wrapper/build.gradle"
EXPECTED_JAR_OUTPUT="$JAVA_WRAPPER_DEMO_DIR/wrapper/build/libs/build/libs/javaWrapper.jar"
WORK_DIR="$(mktemp -d "${TMPDIR:-/tmp}/mac-java-wrapper.XXXXXX")"
cleanup() {
  rm -rf "$WORK_DIR"
}
trap cleanup EXIT

echo "[INFO] Checking Java version..."
JAVA_VERSION_OUTPUT="$(java --version 2>&1 || true)"
echo "$JAVA_VERSION_OUTPUT"
JAVA_VERSION_LINE="$(printf '%s\n' "$JAVA_VERSION_OUTPUT" | awk 'NF { print; exit }')"
if [[ ! "$JAVA_VERSION_LINE" =~ (^openjdk[[:space:]]+17(\.|[[:space:]])|^java[[:space:]]+17(\.|[[:space:]])|version[[:space:]]\"17(\.|\")) ]]; then
  echo "[ERROR] Java 17 is required. Current version is not 17."
  exit 1
fi

if [[ -z "${ANDROID_SDK_DOWNLOAD_URL:-}" ]]; then
  echo "[ERROR] ANDROID_SDK_DOWNLOAD_URL is not set."
  echo "Set it for the current shell:"
  echo "export ANDROID_SDK_DOWNLOAD_URL=\"https://example.com/your-android-sdk.zip\""
  echo
  echo "Persist it in zsh:"
  echo "echo 'export ANDROID_SDK_DOWNLOAD_URL=\"https://example.com/your-android-sdk.zip\"' >> ~/.zshrc"
  echo "source ~/.zshrc"
  exit 1
fi

if [[ ! -d "$JAVA_WRAPPER_DEMO_DIR" ]]; then
  echo "[ERROR] Java wrapper demo directory not found: $JAVA_WRAPPER_DEMO_DIR"
  exit 1
fi

if [[ ! -f "$EMCLIENT_UNITY_WRAPPER_ANDROID_BUILD_GRADLE_FILE" ]]; then
  echo "[ERROR] build.gradle not found: $EMCLIENT_UNITY_WRAPPER_ANDROID_BUILD_GRADLE_FILE"
  exit 1
fi

mkdir -p "$EMCLIENT_UNITY_WRAPPER_ANDROID_LIBS_DIR"

echo "[INFO] Downloading Android SDK package from $ANDROID_SDK_DOWNLOAD_URL ..."
cd "$WORK_DIR"
curl -O "$ANDROID_SDK_DOWNLOAD_URL"

DOWNLOADED_FILE="$(basename "${ANDROID_SDK_DOWNLOAD_URL%%\?*}")"
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

echo "[INFO] Unzipping package: $DOWNLOADED_FILE"
UNZIP_DIR="$WORK_DIR/unzipped"
mkdir -p "$UNZIP_DIR"
unzip -q "$DOWNLOADED_FILE" -d "$UNZIP_DIR"

echo "[INFO] Searching for hyphenatechat jar under libs directories..."
JAR_CANDIDATES=()
while IFS= read -r jar_path; do
  [[ -n "$jar_path" ]] && JAR_CANDIDATES+=("$jar_path")
done < <(find "$UNZIP_DIR" -type d -name libs -print0 | while IFS= read -r -d '' libs_dir; do
  find "$libs_dir" -maxdepth 1 -type f -name 'hyphenatechat*.jar' -print
done)

if [[ "${#JAR_CANDIDATES[@]}" -eq 0 ]]; then
  echo "[ERROR] No hyphenatechat*.jar found under extracted libs directories."
  exit 1
fi

SOURCE_JAR="${JAR_CANDIDATES[0]}"
ANDORID_SDK_JAR_NAME="$(basename "$SOURCE_JAR")"
echo "[INFO] Using jar: $SOURCE_JAR"
if [[ "${#JAR_CANDIDATES[@]}" -gt 1 ]]; then
  echo "[WARN] Multiple hyphenatechat jars found. Using the first match: $ANDORID_SDK_JAR_NAME"
fi

echo "[INFO] Copying jar to wrapper libs directory..."
cp -f "$SOURCE_JAR" "$EMCLIENT_UNITY_WRAPPER_ANDROID_LIBS_DIR/"

echo "[INFO] Updating build.gradle dependency to $ANDORID_SDK_JAR_NAME"
sed -i "" -E "/implementation files\('libs\/classes\.jar'\)/!s|implementation files\('libs/[^/]+\.jar'\)|implementation files('libs/$ANDORID_SDK_JAR_NAME')|g" "$EMCLIENT_UNITY_WRAPPER_ANDROID_BUILD_GRADLE_FILE"

echo "[INFO] Running Gradle makeJar..."
cd "$JAVA_WRAPPER_DEMO_DIR"
./gradlew makeJar

echo "[INFO] Checking output jar..."
if [[ -f "$EXPECTED_JAR_OUTPUT" ]]; then
  echo "[INFO] javaWrapper.jar generated successfully: $EXPECTED_JAR_OUTPUT"
else
  echo "[ERROR] javaWrapper.jar was not found at expected path: $EXPECTED_JAR_OUTPUT"
  exit 1
fi
