# NOTE: Important!!!
# In order to copy libraries to CORRECT location, some location settings in preference items of xcode must be set:
#   Xcode Preference->Tab Locations->Advanced->Build Location: 
#	Custom -- Relative to Workspace
#       	Products -- Build/Products
#		Intermediates -- Build/Intermediate.noindex


#path1
mkdir -p ChatCWrapper/build/Products/Debug/ChatCWrapper.bundle/Contents/Resources/lib
cp -a ../../../../../../emclient-linux/3rd_party/platform/darwin/depends/zlib_1.2.11_share_intel/lib/* ChatCWrapper/build/Products/Debug/ChatCWrapper.bundle/Contents/Resources/lib
#cp -a ../../../../../../emclient-linux/3rd_party/platform/darwin/depends/openssl_1.1.1l_share_intel/lib/* ChatCWrapper/build/Products/Debug/ChatCWrapper.bundle/Contents/Resources/lib
cp -a ../../../../../../emclient-linux/3rd_party/platform/darwin/depends/boringssl_1.1.1f_share_intel/lib/* ChatCWrapper/build/Products/Debug/ChatCWrapper.bundle/Contents/Resources/lib
#cp -a ../../../../../../emclient-linux/3rd_party/platform/darwin/depends/curl_7.80.0_share_intel/lib/* ChatCWrapper/build/Products/Debug/ChatCWrapper.bundle/Contents/Resources/lib
cp -a ../../../../../../emclient-linux/3rd_party/platform/darwin/depends/curl_7.80.0_share_intel_boringssl/lib/* ChatCWrapper/build/Products/Debug/ChatCWrapper.bundle/Contents/Resources/lib
cp -a ../../../../../../emclient-linux/3rd_party/platform/darwin/depends/sqlite_3.34.1_share_intel/lib/* ChatCWrapper/build/Products/Debug/ChatCWrapper.bundle/Contents/Resources/lib
#cp -a ../../../../../../emclient-linux/3rd_party/platform/darwin/depends/sqlcipher_4.4.3_share_intel/lib/* ChatCWrapper/build/Products/Debug/ChatCWrapper.bundle/Contents/Resources/lib
cp -a ../../../../../../emclient-linux/3rd_party/platform/darwin/depends/sqlcipher_4.4.3_share_intel_boringssl/lib/* ChatCWrapper/build/Products/Debug/ChatCWrapper.bundle/Contents/Resources/lib
cp -a ./lib/libc++.1.dylib ChatCWrapper/build/Products/Debug/ChatCWrapper.bundle/Contents/Resources/lib

#path2
#mkdir -p Debug/hyphenateCWrapper.bundle/Contents/Resources/lib
#cp -a build/Products/Debug/hyphenateCWrapper.bundle/Contents/Resources/lib/* Debug/hyphenateCWrapper.bundle/Contents/Resources/lib/
