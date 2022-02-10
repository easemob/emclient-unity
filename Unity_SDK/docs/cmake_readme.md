# cmake构建介绍
    cmake是C类程序通用构建系统，可以生成产品，也可以生成开发和调试工程。

## 构建环境
    cmake 3.15版本以上
    构建需要`emclient-linux`项目（`CMakelists.txt`的`CORE_ROOT_DIRS`）

## 构建产品
    ```
    mkdir build && cd build
    cmake ..
    make
    make install
    ```
### 可调整构建参数
介绍如下：
    **`HC_BUILD_SHARED_LIBS`**：默认构建动态库（Windows是动态库，MacOS是framework），可以设置为`OFF`则构建静态库（Windows是`.dll`，MacOS是`.a`）  
    **`CMAKE_BUILD_TYPE`**: 默认构建调试版本，可选值为`Debug` 和`Release`  
    **`HC_ENABLE_COMPILE_WARNING`**： 默认禁止警告，可以设置`ON`打开警告  
    **`MAC_DYNAMIC_GENERATE_TYPE`**: 默认`Dynamic`，可以设置`Framework`

## 构建工程
    ```
    mkdir build && cd build
    cmake .. -G Xcode # (macOS)
    cmake .. -G "Visual Studio 16 2019" # (Windows)
    ```
## 构建bundle

    首先，生成动态库版本，而不是framework版本。
    其次，目前构建bundle需要额外处理，需要执行`Unity_SDK/mac_build/build_bundle.sh`脚本才能正常生成bundle。
    **注意** 执行的时候需要在脚本所在目录执行脚本。

## 建议构建方式
**采用以下方式进行构建，省去手动输入命令。**

安装`visual studio code`软件, 简称`vscode`  
安装`vscode`插件：
  * [CMake](https://marketplace.visualstudio.com/items?itemName=twxs.cmake)
  * [CMake Tools](https://marketplace.visualstudio.com/items?itemName=ms-vscode.cmake-tools)
  
当通过`vscode`打开工程之后，cmake插件自动提示这是cmake构建系统（根目录存在`CMakeLists.txt`），自动完成对应提示，就可以编译产品或者生成开发工程。
在`MacOS`下默认是生成非`Xcode`工程，需要手动设置一下。
  * 在项目中添加文件夹`.vscode`
  * 在`.vscode`文件夹下创建`settings.json`文件
  * 在`settings.json`添加如下内容`{"cmake.generator": "Xcode"}`

## 注意事项

    目前，仅支持windows平台和macOS平台。
    其他平台，后续开放。

## QA

**使用cmake命令遇到报错："STREQUAL" Debug or Release等字样**
> 需要在cmake命令后面添加 `-DCMAKE_BUILD_TYPE=Debug` 参数

**Windows下并没有make命令，除了使用VS构建之外，还有别的方法吗？**
> 有的，需要执行类似命令`"C:\Program Files\CMake\bin\cmake.EXE" --build d:/codes/easemob/yq/emclient-unity/Unity_SDK/build --config Debug --target ALL_BUILD -j 10 --`

**为什么使用vscode打开项目，没有cmake工具提示呢，我已经安装了cmake插件**
> cmake插件只有检测到当前项目根目录中有`CMakeLists.txt`文件才能生效。