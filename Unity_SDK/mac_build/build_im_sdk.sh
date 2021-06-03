echo Remove old build
rm -rf Release
rm -rf ../../ChatSDKDemo/Assets/ChatSDK/Plugins/macOS/hyphenateCWrapper.bundle

echo Build Linux SDK
make

echo Relink libcrypto
install_name_tool -change /usr/local/opt/openssl/lib/libcrypto.1.0.0.dylib @rpath/easemob.framework/Resources/libcrypto.1.0.0.dylib build/Debug/easemob.framework/easemob

echo Build Bundle
xcodebuild -workspace mac.xcworkspace -scheme hyphenateCWrapper -configuration Release  BUILD_DIR="$(pwd)" BUILD_ROOT="$(pwd)" ARCHS="x86_64" VALID_ARCHS="x86_64" CLANG_DEBUG_INFORMATION_LEVEL="line-tables-only" GCC_OPTIMIZATION_LEVEL=s CODE_SIGNING_REQUIRED=NO CODE_SIGNING_ALLOWED=NO clean build

echo Move macOS to Demo Path
mv Release/hyphenateCWrapper.bundle ../../ChatSDKDemo/Assets/ChatSDK/Plugins/macOS/