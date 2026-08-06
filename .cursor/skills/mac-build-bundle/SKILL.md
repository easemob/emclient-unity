---
name: mac-build-bundle
description: Build and test ChatCWrapper.bundle on macOS using CMake + Xcode, with optional shengwang easemob macros. Use when the user asks to compile mac wrapper bundle, run xcodebuild for ChatCWrapper, or verify ChatCWrapper.bundle output under wrapper_build/proj/lib/Debug.
---

# Mac Build Bundle

## Purpose

Build the macOS wrapper bundle `ChatCWrapper.bundle` in this repository.

## Trigger

Apply this skill when the user asks to:

- build wrapper bundle on macOS
- compile `ChatCWrapper.bundle`
- run CMake with Xcode generator for wrapper build

## Execution

Use the script:

- `scripts/build-chatcwrapper-bundle.sh` (default compile type)
- `scripts/build-chatcwrapper-bundle.sh shengwang` (adds `USE_SHENGWANG_DOMAIN`)

The script performs:

1. Enters `AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/wrapper_build/proj`
2. Clears all contents in that `proj` directory
3. Runs `cmake -G Xcode ..`
4. Logs CMake output to both screen and temp log, then verifies the final non-empty line includes `Build files have been written to`
5. Sets:
   - `COMPILE_SCHEMA_CHATCWRAPPER="ChatCWrapper"`
   - `BUILD_NO=$(date +%s)`
   - `EASEMOB_MACROS="$(inherited) BUILD_NO=$BUILD_NO"` by default
   - `EASEMOB_MACROS="$(inherited) BUILD_NO=$BUILD_NO USE_SHENGWANG_DOMAIN"` when compile type is `shengwang`
6. Runs:
   - `xcodebuild -project WrapperBuilder.xcodeproj -scheme $COMPILE_SCHEMA_CHATCWRAPPER -configuration Debug -destination 'generic/platform=macOS' GCC_PREPROCESSOR_DEFINITIONS="$EASEMOB_MACROS"`
7. Reports output path:
   - `AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/wrapper_build/proj/lib/Debug/ChatCWrapper.bundle`

## Parameters

- `shengwang`: build with `EASEMOB_MACROS="$(inherited) BUILD_NO=$BUILD_NO USE_SHENGWANG_DOMAIN"`
- `default` or empty: build with `EASEMOB_MACROS="$(inherited) BUILD_NO=$BUILD_NO"`

## Example

```bash
./.cursor/skills/mac-build-bundle/scripts/build-chatcwrapper-bundle.sh
./.cursor/skills/mac-build-bundle/scripts/build-chatcwrapper-bundle.sh shengwang
```
