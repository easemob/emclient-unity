# NOTE: Important!!!
# In order to copy libraries to CORRECT location, some location settings in preference items of xcode must be set:
#   Xcode Preference->Tab Locations->Advanced->Build Location: 
#	Custom -- Relative to Workspace
#       	Products -- Build/Products
#		Intermediates -- Build/Intermediate.noindex


#path1
mkdir -p ChatCWrapper/build/Products/Debug/ChatCWrapper.bundle/Contents/Resources/lib
cp -a ../../../../../../emclient-linux/3rd_party/platform/darwin/depends_universal/zlib_1.2.11_share_universal/lib/* ChatCWrapper/build/Products/Debug/ChatCWrapper.bundle/Contents/Resources/lib
#cp -a ../../../../../../emclient-linux/3rd_party/platform/darwin/depends_universal/openssl_1.1.1l_share_universal/lib/* ChatCWrapper/build/Products/Debug/ChatCWrapper.bundle/Contents/Resources/lib
cp -a ../../../../../../emclient-linux/3rd_party/platform/darwin/depends_universal/boringssl_1.1.1f_share_universal/lib/* ChatCWrapper/build/Products/Debug/ChatCWrapper.bundle/Contents/Resources/lib
#cp -a ../../../../../../emclient-linux/3rd_party/platform/darwin/depends_universal/curl_7.80.0_share_universal/lib/* ChatCWrapper/build/Products/Debug/ChatCWrapper.bundle/Contents/Resources/lib
cp -a ../../../../../../emclient-linux/3rd_party/platform/darwin/depends_universal/curl_7.80.0_share_universal_boringssl/lib/* ChatCWrapper/build/Products/Debug/ChatCWrapper.bundle/Contents/Resources/lib
cp -a ../../../../../../emclient-linux/3rd_party/platform/darwin/depends_universal/sqlite_3.34.1_share_universal/lib/* ChatCWrapper/build/Products/Debug/ChatCWrapper.bundle/Contents/Resources/lib
#cp -a ../../../../../../emclient-linux/3rd_party/platform/darwin/depends_universal/sqlcipher_4.4.3_share_universal/lib/* ChatCWrapper/build/Products/Debug/ChatCWrapper.bundle/Contents/Resources/lib
cp -a ../../../../../../emclient-linux/3rd_party/platform/darwin/depends_universal/sqlcipher_4.4.3_share_universal_boringssl/lib/* ChatCWrapper/build/Products/Debug/ChatCWrapper.bundle/Contents/Resources/lib
cp -a ../../../../../../emclient-linux/3rd_party/platform/darwin/depends_universal/libevent_2.1.10_share_universal/lib/* ChatCWrapper/build/Products/Debug/ChatCWrapper.bundle/Contents/Resources/lib

#remove soft link from easemob.framework
./remove_lnk.sh ChatCWrapper/build/Products/Debug/easemob_universal.framework
./remove_lnk.sh ChatCWrapper/build/Products/Debug/ChatCWrapper.bundle/Contents/Resources/easemob_universal.framework
