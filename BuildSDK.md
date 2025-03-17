# 目录介绍

`emclient-unity` 仓库包含以下四个目录：
- `AgoraChatSDK`：C# 端 API 及封装代码
- `UnityChatDemo`：Unity API 及示例代码
- `Win_SDK`：Windows SDK 打包及测试项目
- `backup`：部分移除代码的备份

## 目录细节介绍

### AgoraChatSDK 目录
- `AgoraChatSDK/AgoraChat`：C# 端 API 接口、封装代码及 DLL 编译工程
- `AgoraChatSDK/buildScript/build_ios.sh`：iOS wrapper 编译脚本（用于衔接 C# 和 iOS SDK）
- `AgoraChatSDK/AgoraChat.sln`：C# 端 DLL 编译工程
- `AgoraChatSDK/CWrapper`：对接 iOS、Android 及 PC 端的各个 wrapper 代码
- `AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/CMakeLists.txt`：PC 端工程构建脚本

### UnityChatDemo 目录
- `UnityChatDemo/Assets/AgoraChat/AgoraChat`：C# 端 API 接口、封装代码
- `UnityChatDemo/Assets/AgoraChat/Plugins`：Unity 各端的 SDK 存放位置
- `UnityChatDemo/Assets/Scenes`：Unity 测试示例场景
- `UnityChatDemo/Assets/Script`：Unity 测试示例 C# 脚本

### Win_SDK 目录
- `Win_SDK/Dependency`：Windows SDK 打包依赖库
- `Win_SDK/Distribute`：Windows SDK 打包工具目录（含打包脚本、打包目录框架等）
- `Win_SDK/FrameworkTest`：Windows SDK 测试程序（编译生成控制台执行程序）

# 编译 Windows SDK

编译 Windows SDK 依赖两个 GitHub 仓库：`emclient-linux`（C++ 部分）和 `emclient-unity`（C# 部分）。

- `emclient-linux`：[https://github.com/easemob/emclient-linux/tree/unity_dev](https://github.com/easemob/emclient-linux/tree/unity_dev)
- `emclient-unity`：[https://github.com/easemob/emclient-unity/tree/new](https://github.com/easemob/emclient-unity/tree/new)

## 准备工作

1. 下载仓库：
   - 克隆 `emclient-linux` 并切换到 `unity_dev` 分支。
   - 克隆 `emclient-unity` 并切换到 `new` 分支。
   
2. 注意：需要将 `emclient-linux` 和 `emclient-unity` 存放至同一级目录。

## 编译 C++ SDK 部分

### 生成 VS 编译工程

1. 打开命令行界面（Developer Command Prompt for VS 2019）。
2. 在 `emclient-unity` 目录下，切换到 `AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/wrapper_build/proj`。
3. 执行命令：`cmake -G "Visual Studio 16 2019" ..`

### 工程说明

打开生成的 VS 工程：`WrapperBuilder.sln`。此工程包含以下内容：
- `easemob`：C++ SDK 静态库工程
- `ChatCWrapper`：C# 与 C++ 的接口 wrapper
- `CommonWrapper`：PC 端 C++ 衔接 wrapper
- `SdkWrapper`：easemob 静态库的封装 wrapper

其他部分项目可忽略：
- `ALL_BUILD`：CMake 自动生成的全编译项目
- `ZERO_CHECK`：CMake 自动生成的文件更新检测项目

### 编译工程

1. 打开生成的 VS 工程：`WrapperBuilder.sln`。
2. 选择配置模式为 `Release`，平台为 `x64`。
3. 在 `SdkWrapper` 的属性页面里，为 `SdkWrapper` 添加宏定义 `_PURE_WIN32`（Windows SDK 需要此宏来转换 Unicode 和 UTF-8，而 Unity Windows 不需要）。
4. 编译工程。
   - 说明：C++ 代码中有一处 log 会使用到 `BUILD_NO` 宏，而 `CMakeLists.txt` 已将其注释，所以编译出错时，直接在 C++ 中注释该行 log 代码即可。

最终会在 `AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/wrapper_build/proj/bin/Release` 目录下生成 3 个 DLL 文件：`ChatCWrapper.dll`、`CommonWrapper.dll` 和 `SdkWrapper.dll`。

## 编译 C# 部分

在 `emclient-unity` 目录下，切换到 `AgoraChatSDK` 目录，打开 `AgoraChat.sln`，直接编译生成 `AgoraChat.dll`。

## 打包 Windows SDK

1. 在 `emclient-unity` 目录下，切换到 `Win_SDK/Distribute` 目录。
2. 修改 nuspec 文件中的发布版本号，并执行打包脚本：
   - 如果是 easemob 打包，则修改 `chat_sdk.nuspec`，并执行 `easemob_build.bat`。
   - 如果是 agora 打包，则修改 `agora_chat_sdk.nuspec`，并执行 `agora_build.bat`。
   - 如果是 shengwang 打包，则修改 `shengwang_chat_sdk.nuspec`，并执行 `shengwang_build.bat`。