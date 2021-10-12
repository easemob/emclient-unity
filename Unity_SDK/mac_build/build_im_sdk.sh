echo Remove old build
rm -rf Debug
rm -rf ../../ChatSDKDemo/Assets/ChatSDK/Plugins/macOS/hyphenateCWrapper.bundle

echo Build Linux SDK

make

#echo Relink libcrypto
#install_name_tool -change /usr/lib/libc++.1.dylib @rpath/easemob.framework/Resources/libc++.1.dylib build/Debug/easemob.framework/easemob
#install_name_tool -change /usr/lib/libSystem.B.dylib @rpath/easemob.framework/Resources/libSystem.B.dylib build/Debug/easemob.framework/easemob
#install_name_tool -change /usr/lib/libsqlite3.dylib @rpath/easemob.framework/Resources/libsqlite3.dylib build/Debug/easemob.framework/easemob
#install_name_tool -change /usr/lib/libcurl.4.dylib @rpath/easemob.framework/Resources/libcurl.4.dylib build/Debug/easemob.framework/easemob
#install_name_tool -change /usr/lib/libz.1.dylib @rpath/easemob.framework/Resources/libz.1.dylib build/Debug/easemob.framework/easemob
#install_name_tool -change /usr/local/opt/openssl/lib/libcrypto.1.0.0.dylib @rpath/easemob.framework/Resources/libcrypto.1.0.0.dylib build/Debug/easemob.framework/easemob

echo Build Bundle
xcodebuild -workspace mac.xcworkspace -scheme hyphenateCWrapper -configuration Debug  BUILD_DIR="$(pwd)" BUILD_ROOT="$(pwd)" ARCHS="x86_64" VALID_ARCHS="x86_64" CLANG_DEBUG_INFORMATION_LEVEL="line-tables-only" GCC_OPTIMIZATION_LEVEL=s CODE_SIGNING_REQUIRED=NO CODE_SIGNING_ALLOWED=NO clean build

#cp -fr /usr/lib/libc++.1.dylib @rpath/easemob.framework/Resources/
#cp -fr /usr/lib/libSystem.B.dylib @rpath/easemob.framework/Resources/
#cp -fr /usr/lib/libsqlite3.dylib @rpath/easemob.framework/Resources/
#cp -fr /usr/lib/libcurl.4.dylib @rpath/easemob.framework/Resources/
#cp -fr /usr/lib/libz.1.dylib @rpath/easemob.framework/Resources/
#cp -fr /usr/local/opt/openssl/lib/libcrypto.1.0.0.dylib @rpath/easemob.framework/Resources/

#cd Debug/hyphenateCWrapper.bundle/Contents/
mkdir Debug/hyphenateCWrapper.bundle/Contents/lib
cp -fr ./lib/* Debug/hyphenateCWrapper.bundle/Contents/lib/
#cp -fr /usr/lib/libSystem.B.dylib ./lib/
#cp -fr /usr/lib/libsqlite3.dylib ./lib/
#cp -fr /usr/lib/libcurl.4.dylib ./lib/
#cp -fr /usr/lib/libz.1.dylib ./lib/
#cp -fr /usr/local/opt/openssl/lib/libcrypto.1.0.0.dylib ./lib/
#cd -

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
