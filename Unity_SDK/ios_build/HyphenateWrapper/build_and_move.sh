xcodebuild -project HyphenateWrapper.xcodeproj -scheme libHyphenateWrapper -configuration Release -sdk iphoneos BUILD_DIR="$(pwd)/build" BUILD_ROOT="$(pwd)/build" ARCHS="arm64" VALID_ARCHS="arm64" OTHER_CFLAGS="-fembed-bitcode" OTHER_CPLUSPLUSFLAGS="-fembed-bitcode" CLANG_DEBUG_INFORMATION_LEVEL="line-tables-only" GCC_OPTIMIZATION_LEVEL=s ENABLE_BITCODE=YES BITCODE_GENERATION_MODE=bitcode CODE_SIGNING_ALLOWED=NO clean build

echo "build successed."

mv build/Release-iphoneos/* ../../../ChatSDKDemo/Assets/ChatSDK/Plugins/iOS/

cp -R HyphenateChat.framework ../../../ChatSDKDemo/Assets/ChatSDK/Plugins/iOS/HyphenateChat.framework

rm -rf build

open ../../../ChatSDKDemo/Assets/ChatSDK/Plugins/iOS/