
# !/bin/sh
# ref: https://developer.apple.com/library/archive/documentation/CoreFoundation/Conceptual/CFBundles/BundleTypes/BundleTypes.html
# 简要说明
# 本脚本支持参数输入 ref: https://stackoverflow.com/questions/18003370/script-parameters-in-bash
# 示例： sh build_bundle.sh --version "1.0.0" --type "Debug|Release"


# 默认版本，可以修改
DYNAMIC_VERSION="1.0.0"
# 默认Debug版本，可以修改
DYNAMIC_TYPE="Debug"
DYNAMIC_SUFFIX="d"


if test -n $2; then
    DYNAMIC_VERSION=$2
fi

if test $4 = "Release"; then
    DYNAMIC_TYPE=$4
    DYNAMIC_SUFFIX=""
fi

echo DYNAMIC_VERSION=${DYNAMIC_VERSION}
echo DYNAMIC_TYPE=${DYNAMIC_TYPE}
echo DYNAMIC_SUFFIX=${DYNAMIC_SUFFIX}

# 当前执行脚本所在目录 emclient-unity/Unity_SDK/mac_build
CURRENT_DIR=$PWD
# 当前项目所在目录 emclient-unity
ROOT_DIR=${CURRENT_DIR}/../..
LIBRARY_DIR=${ROOT_DIR}/Unity_SDK
DEMO_DIR=${ROOT_DIR}/ChatSDKDemo

echo "current directory is " ${CURRENT_DIR}
echo "root directory is " ${ROOT_DIR}
echo "library directory is " ${LIBRARY_DIR}
echo "demo directory is " ${DEMO_DIR}

# read -n 1 -p "Press any key to continue..."

if test -d ${LIBRARY_DIR}/build/${DYNAMIC_TYPE}; then
    BUILD_DIR=${LIBRARY_DIR}/build/${DYNAMIC_TYPE}
else
    BUILD_DIR=${LIBRARY_DIR}/build
fi

# 删除之前的bundle
rm -rf ${BUILD_DIR}/hyphenateCWrapper.bundle

# 创建需要的目录
mkdir -p ${BUILD_DIR}/hyphenateCWrapper.bundle/Contents/MacOS
mkdir -p ${BUILD_DIR}/hyphenateCWrapper.bundle/Contents/Frameworks
mkdir -p ${BUILD_DIR}/hyphenateCWrapper.bundle/Contents/Resources

# 拷贝目标动态库和元信息
cp ${BUILD_DIR}/libhyphenateCWrapper${DYNAMIC_SUFFIX}.${DYNAMIC_VERSION}.dylib ${BUILD_DIR}/hyphenateCWrapper.bundle/Contents/MacOS/hyphenateCWrapper

cp ${LIBRARY_DIR}/cmake/macos/Bundle.plist.in ${BUILD_DIR}/hyphenateCWrapper.bundle/Contents/Info.plist

# 拷贝动态库
cp ${LIBRARY_DIR}/mac_build/lib/libc++.1.dylib ${BUILD_DIR}/hyphenateCWrapper.bundle/Contents/Frameworks
cp ${LIBRARY_DIR}/mac_build/lib/libSystem.B.dylib ${BUILD_DIR}/hyphenateCWrapper.bundle/Contents/Frameworks
cp -R ${LIBRARY_DIR}/mac_build/lib/LDAP.framework ${BUILD_DIR}/hyphenateCWrapper.bundle/Contents/Frameworks/LDAP.framework

# 修改库依赖信息 /usr/bin/install_name_tool
install_name_tool -change /usr/lib/libc++.1.dylib Assets/ChatSDK/Plugins/macOS/hyphenateCWrapper.bundle/Contents/Frameworks/libc++.1.dylib ${BUILD_DIR}/hyphenateCWrapper.bundle/Contents/MacOS/hyphenateCWrapper
install_name_tool -change /usr/lib/libSystem.B.dylib Assets/ChatSDK/Plugins/macOS/hyphenateCWrapper.bundle/Contents/Frameworks/libSystem.B.dylib ${BUILD_DIR}/hyphenateCWrapper.bundle/Contents/MacOS/hyphenateCWrapper
install_name_tool -change /System/Library/Frameworks/LDAP.framework/Versions/A/LDAP Assets/ChatSDK/Plugins/macOS/hyphenateCWrapper.bundle/Contents/Frameworks/LDAP.framework/Versions/A/LDAP ${BUILD_DIR}/hyphenateCWrapper.bundle/Contents/MacOS/hyphenateCWrapper

# 文件签名 /usr/bin/codesign
codesign --force --sign - --timestamp\=none --preserve-metadata\=identifier,entitlements,flags ${BUILD_DIR}/hyphenateCWrapper.bundle/Contents/MacOS/hyphenateCWrapper
codesign --force --sign - --timestamp\=none ${BUILD_DIR}/hyphenateCWrapper.bundle

# 修改日期 /usr/bin/touch
touch -c ${BUILD_DIR}/hyphenateCWrapper.bundle

# 删除目标文件夹 复制新生成的bundle
rm -rf ${DEMO_DIR}/Assets/ChatSDK/Plugins/macOS
mkdir -p ${DEMO_DIR}/Assets/ChatSDK/Plugins/macOS
cp -R ${BUILD_DIR}/hyphenateCWrapper.bundle ${DEMO_DIR}/Assets/ChatSDK/Plugins/macOS