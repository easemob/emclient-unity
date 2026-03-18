---
name: mac-build-ios-wrapper
description: Build the iOS wrapper on macOS by downloading the iOS SDK framework from IOS_SDK_DOWNLOAD_URL, copying HyphenateChat.framework or AgoraChat.framework into Wrapper_iOS, optionally switching the Xcode project to AgoraChat.framework for shengwang builds, and running xcodebuild to produce wrapper.framework and ChatCWrapper.framework. Use when the user asks to compile the iOS wrapper, generate iPhoneOS wrapper frameworks, or build the iOS wrapper in shengwang mode.
---

# Mac Build iOS Wrapper

## Purpose

Build the iOS wrapper on macOS and verify these outputs under `AgoraChatSDK/CWrapper/Wrapper/Wrapper_iOS/build/Release-iphoneos`:

- `wrapper.framework`
- `ChatCWrapper.framework`

## Trigger

Apply this skill when the user asks to:

- build the iOS wrapper on macOS
- compile `wrapper.framework` and `ChatCWrapper.framework`
- switch the iOS wrapper between default and `shengwang` framework input

## Execution

Use the script:

- `scripts/build-ios-wrapper.sh` (default compile type)
- `scripts/build-ios-wrapper.sh shengwang` (uses `AgoraChat.framework` and adjusts the Xcode project)

The script performs:

1. If compile type is `shengwang`, checks whether `ruby` is installed and whether the `xcodeproj` gem is available, then exits with install guidance if either is missing
2. Checks whether `IOS_SDK_DOWNLOAD_URL` is set
3. If not set, prints example export commands and exits
4. Prints `IOS_SDK_DOWNLOAD_URL`, downloads the zip package with `curl -O`, and unzips it
5. Finds the required framework in the extracted files:
   - `AgoraChat.framework` for `shengwang`
   - `HyphenateChat.framework` for the default compile type
6. Copies the located framework into `AgoraChatSDK/CWrapper/Wrapper/Wrapper_iOS`
7. If compile type is `shengwang`, runs:
   - `ruby scripts/replace_framework.rb "$EMCLIENT_UNITY_WRAPPER_IOS_BUILD_PROJ_FILE" "$IOS_SDK_OLD_FRAMEWORK_NAME" "$IOS_SDK_FRAMEWORK_NAME"`
8. Runs Xcode build from `AgoraChatSDK/CWrapper/Wrapper/Wrapper_iOS`:
   - default compile type: `xcodebuild ... clean build`
   - `shengwang`: `xcodebuild ... GCC_PREPROCESSOR_DEFINITIONS="AgoraChat" clean build`
9. Verifies `wrapper.framework` and `ChatCWrapper.framework` exist in `build/Release-iphoneos`
10. If compile type is `shengwang`, restores the modified Xcode project file after the build completes

## Parameters

- `shengwang`: use `AgoraChat.framework`, modify the Xcode project, and build with `GCC_PREPROCESSOR_DEFINITIONS="AgoraChat"`
- `default` or empty: use `HyphenateChat.framework` and build without the extra preprocessor definition

## Shengwang Requirement

When compile type is `shengwang`, `ruby` and the `xcodeproj` gem must be available:

```bash
brew install ruby
gem install xcodeproj
```

## Required Environment Variable

- `IOS_SDK_DOWNLOAD_URL`

If it is not set, print one of these examples:

```bash
export IOS_SDK_DOWNLOAD_URL="https://example.com/your-ios-sdk.zip"
```

```zsh
echo 'export IOS_SDK_DOWNLOAD_URL="https://example.com/your-ios-sdk.zip"' >> ~/.zshrc
source ~/.zshrc
```

## Example

```bash
./.cursor/skills/mac-build-ios-wrapper/scripts/build-ios-wrapper.sh
./.cursor/skills/mac-build-ios-wrapper/scripts/build-ios-wrapper.sh shengwang
```
