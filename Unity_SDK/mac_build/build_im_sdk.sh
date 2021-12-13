echo Remove old build
rm -rf Debug
rm -rf ../../ChatSDKDemo/Assets/ChatSDK/Plugins/macOS/hyphenateCWrapper.bundle

echo Build Linux SDK

make

echo Build Bundle
xcodebuild -workspace mac.xcworkspace -scheme hyphenateCWrapper -configuration Debug  BUILD_DIR="$(pwd)" BUILD_ROOT="$(pwd)" ARCHS="x86_64" VALID_ARCHS="x86_64" CLANG_DEBUG_INFORMATION_LEVEL="line-tables-only" GCC_OPTIMIZATION_LEVEL=s CODE_SIGNING_REQUIRED=NO CODE_SIGNING_ALLOWED=NO clean build

mkdir Debug/hyphenateCWrapper.bundle/Contents/lib
cp -fr ./lib/* Debug/hyphenateCWrapper.bundle/Contents/lib/

install_name_tool -change /usr/lib/libc++.1.dylib Assets/ChatSDK/Plugins/macOS/hyphenateCWrapper.bundle/Contents/lib/libc++.1.dylib Debug/hyphenateCWrapper.bundle/Contents/MacOS/hyphenateCWrapper
install_name_tool -change /usr/lib/libSystem.B.dylib Assets/ChatSDK/Plugins/macOS/hyphenateCWrapper.bundle/Contents/lib/libSystem.B.dylib Debug/hyphenateCWrapper.bundle/Contents/MacOS/hyphenateCWrapper
install_name_tool -change /usr/lib/libsqlite3.dylib Assets/ChatSDK/Plugins/macOS/hyphenateCWrapper.bundle/Contents/lib/libsqlite3.dylib Debug/hyphenateCWrapper.bundle/Contents/Resources/easemob.framework/easemob
install_name_tool -change /usr/lib/libcurl.4.dylib Assets/ChatSDK/Plugins/macOS/hyphenateCWrapper.bundle/Contents/lib/libcurl.4.dylib Debug/hyphenateCWrapper.bundle/Contents/Resources/easemob.framework/easemob
install_name_tool -change /usr/lib/libz.1.dylib Assets/ChatSDK/Plugins/macOS/hyphenateCWrapper.bundle/Contents/lib/libz.1.dylib Debug/hyphenateCWrapper.bundle/Contents/Resources/easemob.framework/easemob
install_name_tool -change /usr/lib/libc++.1.dylib Assets/ChatSDK/Plugins/macOS/hyphenateCWrapper.bundle/Contents/lib/libc++.1.dylib Debug/hyphenateCWrapper.bundle/Contents/Resources/easemob.framework/easemob
install_name_tool -change /usr/lib/libSystem.B.dylib Assets/ChatSDK/Plugins/macOS/hyphenateCWrapper.bundle/Contents/lib/libSystem.B.dylib Debug/hyphenateCWrapper.bundle/Contents/Resources/easemob.framework/easemob
install_name_tool -change /usr/local/opt/openssl/lib/libcrypto.1.0.0.dylib Assets/ChatSDK/Plugins/macOS/hyphenateCWrapper.bundle/Contents/lib/libcrypto.1.0.0.dylib Debug/hyphenateCWrapper.bundle/Contents/Resources/easemob.framework/easemob

echo Move macOS to Demo Path
mv Debug/hyphenateCWrapper.bundle ../../ChatSDKDemo/Assets/ChatSDK/Plugins/macOS/
