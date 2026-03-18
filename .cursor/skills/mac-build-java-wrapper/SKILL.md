---
name: mac-build-java-wrapper
description: Build the Android Java wrapper on macOS by validating Java 17, downloading the Android SDK jar package from ANDROID_SDK_DOWNLOAD_URL, updating wrapper/build.gradle, and running gradlew makeJar. Use when the user asks to compile the Java wrapper, generate javaWrapper.jar, or refresh hyphenatechat jar dependencies on Mac.
---

# Mac Build Java Wrapper

## Purpose

Build the Java wrapper on macOS and produce:

- `AgoraChatSDK/CWrapper/Wrapper/Wrapper_Android/JavaWrapperDemo/wrapper/build/libs/build/libs/javaWrapper.jar`

## Trigger

Apply this skill when the user asks to:

- build Java wrapper on macOS
- generate `javaWrapper.jar`
- refresh the `hyphenatechat*.jar` dependency before wrapper build

## Execution

Use the script:

- `scripts/build-java-wrapper.sh`

The script performs:

1. Verifies `java --version` is Java 17
2. Checks whether `ANDROID_SDK_DOWNLOAD_URL` is set
3. If not set, prints example export commands and exits
4. If set, downloads the zip package with `curl -O`
5. Unzips the package
6. Recursively finds `libs` directories and locates `hyphenatechat*.jar`
7. Copies the jar into `AgoraChatSDK/CWrapper/Wrapper/Wrapper_Android/JavaWrapperDemo/wrapper/libs`
8. Replaces the jar reference in `AgoraChatSDK/CWrapper/Wrapper/Wrapper_Android/JavaWrapperDemo/wrapper/build.gradle` while keeping `implementation files('libs/classes.jar')` unchanged
9. Runs `./gradlew makeJar` in `AgoraChatSDK/CWrapper/Wrapper/Wrapper_Android/JavaWrapperDemo`
10. Verifies `javaWrapper.jar` exists at the expected output path

## Required Environment Variable

- `ANDROID_SDK_DOWNLOAD_URL`

If it is not set, print one of these examples:

```bash
export ANDROID_SDK_DOWNLOAD_URL="https://example.com/your-android-sdk.zip"
```

```zsh
echo 'export ANDROID_SDK_DOWNLOAD_URL="https://example.com/your-android-sdk.zip"' >> ~/.zshrc
source ~/.zshrc
```

## Example

```bash
./.cursor/skills/mac-build-java-wrapper/scripts/build-java-wrapper.sh
```
