#!/bin/bash

# demo path
DEMOPATH=$(pwd)


CURRENT_PATH=$(cd $(dirname $0);pwd)
cd ${CURRENT_PATH}
cd ../CWrapper/Wrapper/Wrapper_iOS

sh download_ios_sdk.sh

timefile="build_"$(date "+%Y%m%d%H%M%S")

mkdir $timefile

#xcodebuild -workspace OCWrapperDemo.xcworkspace -scheme ChatCWrapper -configuration Release -sdk iphoneos BUILD_DIR="$(pwd)/build" BUILD_ROOT="$(pwd)/build" ARCHS="armv7 arm64" VALID_ARCHS="armv7 arm64" OTHER_CFLAGS="-fembed-bitcode" OTHER_CPLUSPLUSFLAGS="-fembed-bitcode" CLANG_DEBUG_INFORMATION_LEVEL="line-tables-only" GCC_OPTIMIZATION_LEVEL=s ENABLE_BITCODE=YES BITCODE_GENERATION_MODE=bitcode CODE_SIGNING_ALLOWED=NO clean build

xcodebuild -workspace OCWrapperDemo.xcworkspace -scheme ChatCWrapper -configuration Release -sdk iphoneos BUILD_DIR="$(pwd)/build" BUILD_ROOT="$(pwd)/build" ARCHS="arm64" VALID_ARCHS="arm64" CLANG_DEBUG_INFORMATION_LEVEL="line-tables-only" GCC_OPTIMIZATION_LEVEL=s CODE_SIGNING_ALLOWED=NO clean build


mv build/Release-iphoneos/wrapper.framework $timefile/wrapper.framework
mv build/Release-iphoneos/wrapper.framework.dSYM $timefile/wrapper.framework.dSYM

mv build/Release-iphoneos/ChatCWrapper.framework $timefile/ChatCWrapper.framework
mv build/Release-iphoneos/ChatCWrapper.framework.dSYM $timefile/ChatCWrapper.framework.dSYM

rm -rf build

echo move into demo file.



# echo $DEMOPATH >> 12.log


