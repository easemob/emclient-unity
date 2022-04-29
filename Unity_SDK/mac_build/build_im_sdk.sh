echo Remove old build
rm -rf Debug
rm -rf ../../ChatSDKDemo/Assets/ChatSDK/Plugins/macOS/hyphenateCWrapper.bundle

echo Build Linux SDK

make

echo Build Bundle
xcodebuild -workspace mac.xcworkspace -scheme hyphenateCWrapper -configuration Debug  BUILD_DIR="$(pwd)" BUILD_ROOT="$(pwd)" ARCHS="x86_64" VALID_ARCHS="x86_64" CLANG_DEBUG_INFORMATION_LEVEL="line-tables-only" GCC_OPTIMIZATION_LEVEL=s CODE_SIGNING_REQUIRED=NO CODE_SIGNING_ALLOWED=NO clean build

./cplib.sh

echo Move macOS to Demo Path
mv Debug/hyphenateCWrapper.bundle ../../ChatSDKDemo/Assets/ChatSDK/Plugins/macOS/
