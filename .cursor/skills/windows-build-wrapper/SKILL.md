---
name: windows-build-wrapper
description: Build and test the Windows wrapper DLLs with CMake + Visual Studio 2019, including SdkWrapper _PURE_WIN32 or Unity output modes and optional shengwang easemob macros. Use when the user asks to compile wrapper on Windows, run wrapper build bat scripts, or verify easemob/SdkWrapper/CommonWrapper/ChatCWrapper DLL generation.
---

# Windows Build Wrapper

## Purpose

Build the Windows wrapper in this repository using Visual Studio 2019 generator and produce:

- `easemob.dll`
- `SdkWrapper.dll`
- `CommonWrapper.dll`
- `ChatCWrapper.dll`

`SdkWrapper.dll` has two modes:

- Windows test mode: defines `_PURE_WIN32`
- Unity mode: does not define `_PURE_WIN32` (default mode)

## Trigger

Apply this skill when the user asks to:

- build wrapper on Windows
- compile test wrapper
- generate the four wrapper DLLs
- switch SdkWrapper output between Windows test and Unity

## Execution

1. Use the batch script:
   - `scripts/build-wrapper.bat` (default Unity mode)
   - `scripts/build-wrapper.bat windows-test` (Windows test mode, sets `_PURE_WIN32`)
2. The script performs:
   - cleanup of `AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/wrapper_build/proj`
   - CMake generation with `Visual Studio 16 2019`
   - CMake success verification by checking the final log line contains `Build files have been written to`
   - `BUILD_NO` setup and `easemob.dll` build with `/DBUILD_NO=<timestamp>`, or `/DBUILD_NO=<timestamp> /DUSE_SHENGWANG_DOMAIN` when compile type is `shengwang`
   - `SdkWrapper.dll` build in Unity mode (default) or Windows test mode (`/D_PURE_WIN32`)
   - `CommonWrapper.dll` and `ChatCWrapper.dll` build

## Parameters

First parameter:

- `windows-test`: build `SdkWrapper.dll` with `_PURE_WIN32`
- `unity` or empty: build `SdkWrapper.dll` without `_PURE_WIN32` (default)

Second parameter:

- `shengwang`: build `easemob.dll` with `/DUSE_SHENGWANG_DOMAIN`
- `default` or empty: build `easemob.dll` without `/DUSE_SHENGWANG_DOMAIN` (default)

## Examples

```bat
:: Default (Unity wrapper)
.\.cursor\skills\windows-build-wrapper\scripts\build-wrapper.bat

:: Explicit Unity mode
.\.cursor\skills\windows-build-wrapper\scripts\build-wrapper.bat unity

:: Windows test wrapper mode
.\.cursor\skills\windows-build-wrapper\scripts\build-wrapper.bat windows-test

:: Unity wrapper + shengwang compile type
.\.cursor\skills\windows-build-wrapper\scripts\build-wrapper.bat unity shengwang

:: Windows test wrapper + shengwang compile type
.\.cursor\skills\windows-build-wrapper\scripts\build-wrapper.bat windows-test shengwang
```
