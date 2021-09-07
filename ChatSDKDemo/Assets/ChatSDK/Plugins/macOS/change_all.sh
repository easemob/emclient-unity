#!/bin/sh

install_name_tool -change /usr/lib/libc++.1.dylib /Users/unity/Easemob/lib/libc++.1.dylib hyphenateCWrapper.bundle/Contents/MacOS/hyphenateCWrapper
install_name_tool -change /usr/lib/libSystem.B.dylib /Users/unity/Easemob/lib/libSystem.B.dylib hyphenateCWrapper.bundle/Contents/MacOS/hyphenateCWrapper
install_name_tool -change /usr/lib/libsqlite3.dylib /Users/unity/Easemob/lib/libsqlite3.dylib hyphenateCWrapper.bundle/Contents/Resources/easemob.framework/easemob
install_name_tool -change /usr/lib/libcurl.4.dylib /Users/unity/Easemob/lib/libcurl.4.dylib hyphenateCWrapper.bundle/Contents/Resources/easemob.framework/easemob
install_name_tool -change /usr/lib/libz.1.dylib /Users/unity/Easemob/lib/libz.1.dylib hyphenateCWrapper.bundle/Contents/Resources/easemob.framework/easemob
install_name_tool -change /usr/lib/libc++.1.dylib /Users/unity/Easemob/lib/libc++.1.dylib hyphenateCWrapper.bundle/Contents/Resources/easemob.framework/easemob
install_name_tool -change /usr/lib/libSystem.B.dylib /Users/unity/Easemob/lib/libSystem.B.dylib hyphenateCWrapper.bundle/Contents/Resources/easemob.framework/easemob
install_name_tool -change /usr/local/opt/openssl/lib/libcrypto.1.0.0.dylib /Users/unity/Easemob/lib/libcrypto.1.0.0.dylib hyphenateCWrapper.bundle/Contents/Resources/easemob.framework/easemob
