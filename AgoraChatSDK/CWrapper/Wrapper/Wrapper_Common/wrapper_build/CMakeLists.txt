cmake_minimum_required(VERSION 3.15)
project(WrapperBuilder)

# 设置编译选项
set(CMAKE_C_STANDARD 11)
set(CMAKE_CXX_STANDARD 14)
set(CMAKE_POSITION_INDEPENDENT_CODE ON)

# 设置target名称
set(easemob "easemob")
set(SdkWrapper "SdkWrapper")
set(CommonWrapper "CommonWrapper")
set(ChatCWrapper "ChatCWrapper")

if(APPLE)
    set(easemob "easemob_universal")
    set(SdkWrapper "SdkWrapper_universal")
    set(CommonWrapper "CommonWrapper_universal")
    set(ChatCWrapper "ChatCWrapper")
endif()

# 设置变量
set(EMCLIENT_LINUX_PATH "../../../../../../emclient-linux/")
set(EMCLIENT_UNITY_PATH "../../../../../../emclient-unity/")

# 设置输出目录
set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/lib)
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/lib)
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin)

#message("CMAKE_RUNTIME_OUTPUT_DIRECTORY: ${CMAKE_RUNTIME_OUTPUT_DIRECTORY}")
#message("CMAKE_LIBRARY_OUTPUT_DIRECTORY: ${CMAKE_LIBRARY_OUTPUT_DIRECTORY}")

# 设置公共定义
add_definitions(
    -DDLL_EXPORT
    -DCURL_STATICLIB
    -DRAPIDJSON_NAMESPACE=easemob
    -DUSE_SQLCIPHER=1
    -D_ITERATOR_DEBUG_LEVEL=0
    -DUNICODE
    -D_UNICODE
    -DMEDIA_SERVER_LIBRARY_SDK
    -DMEDIA_SERVER_LIBRARY_NGSDK
    -DMEDIA_SERVER_LIBRARY_EMPTY_LOG
)

if(MSVC)
    # MSVC 编译选项

    # 设置运行时库
    # MultiThreaded 对应 /MT
    # MultiThreadedDLL 对应 /MD
    # MultiThreadedDebug 对应 /MTd
    # MultiThreadedDebugDLL 对应 /MDd
    set(CMAKE_MSVC_RUNTIME_LIBRARY "MultiThreaded")

    # 获取BUILD_NO
    execute_process(
        COMMAND powershell -Command "[int64][double]::Parse((Get-Date -Date (Get-Date).ToUniversalTime() -UFormat %s))"
        OUTPUT_VARIABLE BUILD_NO
        OUTPUT_STRIP_TRAILING_WHITESPACE
    )

    add_definitions(
		-D_WIN32
		-DCONFIG_WINDOWS
		-DWIN32_LEAN_AND_MEAN
                -DBUILD_NO=${BUILD_NO}
    )
    
    # 设置全局编译选项
    add_compile_options(
        /W1
        /utf-8
        /Zi
        /Od
    )

    # 设置全局链接选项
    add_link_options(/DEBUG)    

    # 定义自定义构建类型，暂时不区分Debug和Release，这两者目前配置一样
    set(CMAKE_CONFIGURATION_TYPES "Debug;Release" CACHE STRING "" FORCE)    

else()
    # 非 MSVC 编译选项（假设是 GCC 或 Clang）
	set(CMAKE_OSX_ARCHITECTURES "arm64;x86_64")

    # 获取BUILD_NO
    execute_process(
        COMMAND date +%s
        OUTPUT_VARIABLE BUILD_NO
        OUTPUT_STRIP_TRAILING_WHITESPACE
    )

    add_compile_options(
        -Wall
        -fPIC
        -Os
    )

    add_definitions(
		-DTARGET_OS_OSX
		-DBUILD_NO=${BUILD_NO}
	)

    set(CMAKE_XCODE_ATTRIBUTE_DEBUG_INFORMATION_FORMAT "dwarf-with-dsym")
    set(CMAKE_XCODE_ATTRIBUTE_STRIP_INSTALLED_PRODUCT "No")
    set(CMAKE_XCODE_ATTRIBUTE_STRIP_INSTALLED_PRODUCT "No")
    set(CMAKE_XCODE_ATTRIBUTE_STRIPSTYLE "non-global")

    set(CMAKE_XCODE_ATTRIBUTE_CODE_SIGN_IDENTITY "")
    set(CMAKE_XCODE_ATTRIBUTE_CODE_SIGNING_REQUIRED "NO")
    set(CMAKE_XCODE_ATTRIBUTE_CODE_SIGNING_ALLOWED "NO")

    set(CMAKE_C_FLAGS_"-m64 -g")
    set(CMAKE_CXX_FLAGS_"-m64 -g")
    set(CMAKE_EXE_LINKER_FLAGS_"-m64")
    set(CMAKE_SHARED_LINKER_FLAGS_"-m64")

endif()

# 设置公共包含目录
include_directories(
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/CWrapper/include
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/common_wrapper
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper
    ${EMCLIENT_LINUX_PATH}/protocol/generated
    ${EMCLIENT_LINUX_PATH}/protocol
    ${EMCLIENT_LINUX_PATH}/include/message
    ${EMCLIENT_LINUX_PATH}/include/utils
    ${EMCLIENT_LINUX_PATH}/include
    ${EMCLIENT_LINUX_PATH}/internal/include
    ${EMCLIENT_LINUX_PATH}/3rd_party/protobuf
    ${EMCLIENT_LINUX_PATH}/3rd_party/aosl
    ${EMCLIENT_LINUX_PATH}/3rd_party/rapidjson/include
    ${EMCLIENT_LINUX_PATH}/3rd_party/facility/include
    ${EMCLIENT_LINUX_PATH}/3rd_party/curlcpp/include
    ${EMCLIENT_LINUX_PATH}/3rd_party/facility/include/agora_facilities
    ${EMCLIENT_LINUX_PATH}/3rd_party/facility/include/agora_facilities/internal
    ${EMCLIENT_LINUX_PATH}/3rd_party/facility/include/agora_facilities/inc
    ${EMCLIENT_LINUX_PATH}/3rd_party/facility/include/agora_utils/inc
    ${EMCLIENT_LINUX_PATH}/3rd_party/facility/include/aut
)

# 设置平台区分包含目录
if(CMAKE_GENERATOR MATCHES "Visual Studio")

    if(CMAKE_GENERATOR_PLATFORM STREQUAL "Win32")
				include_directories(
					${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends32/libevent-2.1.10-stable-x86-static-mt/include
					${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends32/zlib_1.2.11-x86-static-mt/include
					${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends32/boringssl-1.1.1f-x86-static-mt/include
					${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends32/curl_7.80.0-x86-static-md/include
					${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends32/sqlcipher_4.4.3-x86-static-mt-boringssl/include
					${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends32/libzip_1.11.2-x86-static-mt/include
				)

    elseif(CMAKE_GENERATOR_PLATFORM STREQUAL "x64" OR CMAKE_GENERATOR_PLATFORM STREQUAL "")
				include_directories(
					${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends/libevent-2.1.10-stable-x64-static-mt/include
					${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends/zlib_1.2.11-x64-static-mt/include
					${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends/boringssl-1.1.1f-x64-static-mt/include
					${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends/curl_7.80.0-x64-static-md/include
					${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends/sqlcipher_4.4.3-x64-static-mt-boringssl/include
					${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends/libzip_1.11.2-x64-static-mt/include
				)
    endif()

else()
		include_directories(
			${EMCLIENT_LINUX_PATH}/3rd_party/platform/darwin/depends_universal/libevent_2.1.10_share_universal/include
			${EMCLIENT_LINUX_PATH}/3rd_party/platform/darwin/depends_universal/libzip_1.11.2_share_universal/include
			${EMCLIENT_LINUX_PATH}/3rd_party/platform/darwin/depends_universal/boringssl_1.1.1f_share_universal/include
			${EMCLIENT_LINUX_PATH}/3rd_party/platform/darwin/depends_universal/curl_7.80.0_share_universal_boringssl/include
			${EMCLIENT_LINUX_PATH}/3rd_party/platform/darwin/depends_universal/sqlcipher_4.4.3_share_universal_boringssl/include
			${EMCLIENT_LINUX_PATH}/3rd_party/platform/darwin/depends_universal/libzip_1.11.2_share_universal/include
		)
endif()

# 设置不同架构和生成器的动态库路径
if(CMAKE_GENERATOR MATCHES "Visual Studio")

    if(CMAKE_GENERATOR_PLATFORM STREQUAL "Win32")
		find_library(libcrypto NAMES libcrypto PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends32/boringssl-1.1.1f-x86-static-mt/lib)
		find_library(libssl NAMES libssl PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends32/boringssl-1.1.1f-x86-static-mt/lib)
		find_library(zlib NAMES zlib PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends32/zlib_1.2.11-x86-static-mt/lib)
		find_library(libcurl NAMES libcurl PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends32/curl_7.80.0-x86-static-md/lib)
		find_library(libsqlcipher NAMES libsqlcipher PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends32/sqlcipher_4.4.3-x86-static-mt-boringssl/lib)
		find_library(libevent_openssl NAMES libevent_openssl PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends32/libevent-2.1.10-stable-x86-static-mt/lib)
		find_library(libevent_core NAMES libevent_core PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends32/libevent-2.1.10-stable-x86-static-mt/lib)
		find_library(libevent_extra NAMES libevent_extra PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends32/libevent-2.1.10-stable-x86-static-mt/lib)
		find_library(libevent NAMES libevent PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends32/libevent-2.1.10-stable-x86-static-mt/lib)
		find_library(agora_utils_static NAMES agora_utils_static PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends32/facility-x86-static-mt-0.0.60.11/lib)
		find_library(agora_transport_static NAMES agora_transport_static PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends32/facility-x86-static-mt-0.0.60.11/lib)
		find_library(agora_ap_static NAMES agora_ap_static PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends32/facility-x86-static-mt-0.0.60.11/lib)
		find_library(agora_edge_allocator_static NAMES agora_edge_allocator_static PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends32/facility-x86-static-mt-0.0.60.11/lib)
		find_library(agora_generic_report_static NAMES agora_generic_report_static PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends32/facility-x86-static-mt-0.0.60.11/lib)
		find_library(libaosl_chat NAMES libaosl PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends32/aosl_1.3.0-x86-release)
		find_library(libzip NAMES zip PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends32/libzip_1.11.2-x86-static-mt/lib)

    elseif(CMAKE_GENERATOR_PLATFORM STREQUAL "x64" OR CMAKE_GENERATOR_PLATFORM STREQUAL "")
		find_library(libcrypto NAMES libcrypto PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends/boringssl-1.1.1f-x64-static-mt/lib)
		find_library(libssl NAMES libssl PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends/boringssl-1.1.1f-x64-static-mt/lib)
		find_library(zlib NAMES zlib PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends/zlib_1.2.11-x64-static-mt/lib)
		find_library(libcurl NAMES libcurl PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends/curl_7.80.0-x64-static-md/lib)
		find_library(libsqlcipher NAMES libsqlcipher PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends/sqlcipher_4.4.3-x64-static-mt-boringssl/lib)
		find_library(libevent_openssl NAMES libevent_openssl PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends/libevent-2.1.10-stable-x64-static-mt/lib)
		find_library(libevent_core NAMES libevent_core PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends/libevent-2.1.10-stable-x64-static-mt/lib)
		find_library(libevent_extra NAMES libevent_extra PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends/libevent-2.1.10-stable-x64-static-mt/lib)
		find_library(libevent NAMES libevent PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends/libevent-2.1.10-stable-x64-static-mt/lib)
		find_library(agora_utils_static NAMES agora_utils_static PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends/facility-x64-static-mt-0.0.60.11/lib)
		find_library(agora_transport_static NAMES agora_transport_static PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends/facility-x64-static-mt-0.0.60.11/lib)
		find_library(agora_ap_static NAMES agora_ap_static PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends/facility-x64-static-mt-0.0.60.11/lib)
		find_library(agora_edge_allocator_static NAMES agora_edge_allocator_static PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends/facility-x64-static-mt-0.0.60.11/lib)
		find_library(agora_generic_report_static NAMES agora_generic_report_static PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends/facility-x64-static-mt-0.0.60.11/lib)
		find_library(libaosl_chat NAMES libaosl PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends/aosl_1.3.0-x64-release)
		find_library(libzip NAMES zip PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/win/depends/libzip_1.11.2-x64-static-mt/lib)
    endif()
    message(STATUS "Found zlib: ${zlib}")

else()
	find_library(libz_tbd NAMES libz.tbd PATHS /Applications/Xcode.app/Contents/Developer/Platforms/MacOSX.platform/Developer/SDKs/MacOSX.sdk/usr/lib)
	find_library(security_framework NAMES Security PATHS /Applications/Xcode.app/Contents/Developer/Platforms/MacOSX.platform/Developer/SDKs/MacOSX.sdk/System/Library/Frameworks)
	find_library(foundation_framework NAMES Foundation PATHS /Applications/Xcode.app/Contents/Developer/Platforms/MacOSX.platform/Developer/SDKs/MacOSX.sdk/System/Library/Frameworks)
	find_library(libcrypto NAMES crypto.1.1 PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/darwin/depends_universal/boringssl_1.1.1f_share_universal/lib)
	find_library(libssl NAMES ssl.1.1 PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/darwin/depends_universal/boringssl_1.1.1f_share_universal/lib)
	#find_library(zlib NAMES zlib PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/darwin/depends_universal/zlib_1.2.11_share_universal/lib)
	find_library(libcurl NAMES curl.4 PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/darwin/depends_universal/curl_7.80.0_share_universal_boringssl/lib)
	find_library(libsqlcipher NAMES sqlcipher.0 PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/darwin/depends_universal/sqlcipher_4.4.3_share_universal_boringssl/lib)
	find_library(libevent_openssl NAMES event_openssl PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/darwin/depends_universal/libevent_2.1.10_share_universal/lib)
	find_library(libevent_core NAMES event_core PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/darwin/depends_universal/libevent_2.1.10_share_universal/lib)
	find_library(libevent_extra NAMES event_extra PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/darwin/depends_universal/libevent_2.1.10_share_universal/lib)
	find_library(libevent_thread NAMES event_pthreads PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/darwin/depends_universal/libevent_2.1.10_share_universal/lib)
	find_library(libevent NAMES event PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/darwin/depends_universal/libevent_2.1.10_share_universal/lib)
	find_library(agora_utils_static NAMES agora_utils_static PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/darwin/depends_universal/facility_static_universal-0.0.60.11/lib)
	find_library(agora_transport_static NAMES agora_transport_static PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/darwin/depends_universal/facility_static_universal-0.0.60.11/lib)
	find_library(agora_ap_static NAMES agora_ap_static PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/darwin/depends_universal/facility_static_universal-0.0.60.11/lib)
	find_library(agora_edge_allocator_static NAMES agora_edge_allocator_static PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/darwin/depends_universal/facility_static_universal-0.0.60.11/lib)
	find_library(agora_generic_report_static NAMES agora_generic_report_static PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/darwin/depends_universal/facility_static_universal-0.0.60.11/lib)
	find_library(libaosl_chat NAMES aosl PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/darwin/depends_universal/aosl_1.3.0_release)
	find_library(libzip NAMES zip.5 PATHS ${EMCLIENT_LINUX_PATH}/3rd_party/platform/darwin/depends_universal/libzip_1.11.2_share_universal/lib)
	message(STATUS "Found libz.tbd: ${libz_tbd}")
	message(STATUS "Found Security.framework: ${security_framework}")
	message(STATUS "Found Foundation.framework: ${foundation_framework}")
	message(STATUS "Found libevent: ${libevent_thread}")
endif()

# 打印找到的库路径（可选）
message(STATUS "Found libcrypto: ${libcrypto}")
message(STATUS "Found libssl: ${libssl}")
#message(STATUS "Found zlib: ${zlib}")
message(STATUS "Found libcurl: ${libcurl}")
message(STATUS "Found libsqlcipher: ${libsqlcipher}")
message(STATUS "Found libevent_openssl: ${libevent_openssl}")
message(STATUS "Found libevent_core: ${libevent_core}")
message(STATUS "Found libevent_extra: ${libevent_extra}")
message(STATUS "Found libevent: ${libevent}")
message(STATUS "Found agora_utils_static: ${agora_utils_static}")
message(STATUS "Found agora_transport_static: ${agora_transport_static}")
message(STATUS "Found agora_ap_static: ${agora_ap_static}")
message(STATUS "Found agora_edge_allocator_static: ${agora_edge_allocator_static}")
message(STATUS "Found agora_generic_report_static: ${agora_generic_report_static}")
message(STATUS "Found libaosl_chat: ${libaosl_chat}")
message(STATUS "Found libzip: ${libzip}")

# 检查是否找到所有库
if(NOT libcrypto OR NOT libssl OR NOT libcurl OR NOT libsqlcipher OR
   NOT libevent_openssl OR NOT libevent_core OR NOT libevent_extra OR NOT libevent OR
   NOT agora_utils_static OR NOT agora_transport_static OR NOT agora_ap_static OR
   NOT agora_edge_allocator_static OR NOT agora_generic_report_static
   OR NOT libzip)
    message(FATAL_ERROR "Could not find one or more libraries")
endif()

set(EASEMOB_SRC
${EMCLIENT_LINUX_PATH}/3rd_party/curlcpp/src/curl_easy.cpp
    ${EMCLIENT_LINUX_PATH}/3rd_party/curlcpp/src/curl_exception.cpp
    ${EMCLIENT_LINUX_PATH}/3rd_party/curlcpp/src/curl_form.cpp
    ${EMCLIENT_LINUX_PATH}/3rd_party/curlcpp/src/curl_header.cpp
    ${EMCLIENT_LINUX_PATH}/3rd_party/curlcpp/src/curl_writer.cpp
    ${EMCLIENT_LINUX_PATH}/3rd_party/protobuf/google/protobuf/generated_message_util.cc
    ${EMCLIENT_LINUX_PATH}/3rd_party/protobuf/google/protobuf/message_lite.cc
    ${EMCLIENT_LINUX_PATH}/3rd_party/protobuf/google/protobuf/repeated_field.cc
    ${EMCLIENT_LINUX_PATH}/3rd_party/protobuf/google/protobuf/wire_format_lite.cc
    ${EMCLIENT_LINUX_PATH}/3rd_party/protobuf/google/protobuf/io/coded_stream.cc
    ${EMCLIENT_LINUX_PATH}/3rd_party/protobuf/google/protobuf/io/zero_copy_stream.cc
    ${EMCLIENT_LINUX_PATH}/3rd_party/protobuf/google/protobuf/io/zero_copy_stream_impl_lite.cc
    ${EMCLIENT_LINUX_PATH}/3rd_party/protobuf/google/protobuf/stubs/common.cc
    ${EMCLIENT_LINUX_PATH}/3rd_party/protobuf/google/protobuf/stubs/once.cc
    ${EMCLIENT_LINUX_PATH}/3rd_party/protobuf/google/protobuf/stubs/atomicops_internals_x86_msvc.cc
    ${EMCLIENT_LINUX_PATH}/protocol/basenode.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/chatclient.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/conference.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/conferencebody.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/emcompressionzlib.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/emconnectionfactory.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/emconnectiontcpbase.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/emconnectiontcpclient.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/emlogsink.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/emmutex.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/keyvalue.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/message.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/messagebody.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/messagebodycontent.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/messageconfig.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/meta.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/metaqueue.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/msync.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/mucbody.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/mucmeta.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/notice.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/protocolparser.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/provision.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/rosterbody.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/rostermeta.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/statistics.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/status.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/syncdl.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/syncul.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/testserver.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/uid.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/unreaddl.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/unreadul.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/roamconfig.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/muc.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/offlinemessagerecord.cpp
    ${EMCLIENT_LINUX_PATH}/protocol/generated/conferencebody.pb.cc
    ${EMCLIENT_LINUX_PATH}/protocol/generated/jid.pb.cc
    ${EMCLIENT_LINUX_PATH}/protocol/generated/keyvalue.pb.cc
    ${EMCLIENT_LINUX_PATH}/protocol/generated/messagebody.pb.cc
    ${EMCLIENT_LINUX_PATH}/protocol/generated/msync.pb.cc
    ${EMCLIENT_LINUX_PATH}/protocol/generated/mucbody.pb.cc
    ${EMCLIENT_LINUX_PATH}/protocol/generated/rosterbody.pb.cc
    ${EMCLIENT_LINUX_PATH}/protocol/generated/statisticsbody.pb.cc
    ${EMCLIENT_LINUX_PATH}/protocol/generated/argusdata.pb.cc
    ${EMCLIENT_LINUX_PATH}/protocol/generated/argussdkapi.pb.cc
    ${EMCLIENT_LINUX_PATH}/protocol/generated/argussdkinit.pb.cc
    ${EMCLIENT_LINUX_PATH}/protocol/generated/arguscrashevent.pb.cc
    ${EMCLIENT_LINUX_PATH}/src/b64decoder.c
    ${EMCLIENT_LINUX_PATH}/src/b64encoder.c
    ${EMCLIENT_LINUX_PATH}/src/emattributevalue.cpp
    ${EMCLIENT_LINUX_PATH}/src/emchatclient.cpp
    ${EMCLIENT_LINUX_PATH}/src/emchatclientimpl.cpp
    ${EMCLIENT_LINUX_PATH}/src/emchatconfigs.cpp
    ${EMCLIENT_LINUX_PATH}/src/emchatmanager.cpp
    ${EMCLIENT_LINUX_PATH}/src/emchatroom.cpp
    ${EMCLIENT_LINUX_PATH}/src/emchatroommanager.cpp
    ${EMCLIENT_LINUX_PATH}/src/emclient.cpp
    ${EMCLIENT_LINUX_PATH}/src/emcollector.cpp
    ${EMCLIENT_LINUX_PATH}/src/emconfigmanager.cpp
    ${EMCLIENT_LINUX_PATH}/src/emcontactmanager.cpp
    ${EMCLIENT_LINUX_PATH}/src/emconversation.cpp
    ${EMCLIENT_LINUX_PATH}/src/emconversationprivate.cpp
    ${EMCLIENT_LINUX_PATH}/src/emconversation_manager.cpp
    ${EMCLIENT_LINUX_PATH}/src/emdatabase.cpp
    ${EMCLIENT_LINUX_PATH}/src/emdatareport.cpp
    ${EMCLIENT_LINUX_PATH}/src/emdnsmanager.cpp
    ${EMCLIENT_LINUX_PATH}/src/emencryptproviderimpl.cpp
    ${EMCLIENT_LINUX_PATH}/src/emerror.cpp
    ${EMCLIENT_LINUX_PATH}/src/emgroup.cpp
    ${EMCLIENT_LINUX_PATH}/src/emgroupmanager.cpp
    ${EMCLIENT_LINUX_PATH}/src/emjsonstring.cpp
    ${EMCLIENT_LINUX_PATH}/src/emmuc.cpp
    ${EMCLIENT_LINUX_PATH}/src/emmucmanager.cpp
    ${EMCLIENT_LINUX_PATH}/src/empushconfigs.cpp
    ${EMCLIENT_LINUX_PATH}/src/empushmanager.cpp
    ${EMCLIENT_LINUX_PATH}/src/emreportevent.cpp
    ${EMCLIENT_LINUX_PATH}/src/emrtcconfigmanager.cpp
    ${EMCLIENT_LINUX_PATH}/src/emsemaphoretracker.cpp
    ${EMCLIENT_LINUX_PATH}/src/emsessionmanager.cpp
    ${EMCLIENT_LINUX_PATH}/src/emtaskqueue.cpp
    ${EMCLIENT_LINUX_PATH}/src/emuserinfomanager.cpp
    ${EMCLIENT_LINUX_PATH}/src/sqlite.cpp
    ${EMCLIENT_LINUX_PATH}/src/emtranslatemanager.cpp
    ${EMCLIENT_LINUX_PATH}/src/emtranslateresult.cpp
    ${EMCLIENT_LINUX_PATH}/src/emmessagereactionchange.cpp
    ${EMCLIENT_LINUX_PATH}/src/empresence.cpp
    ${EMCLIENT_LINUX_PATH}/src/empresencemanager.cpp
    ${EMCLIENT_LINUX_PATH}/src/emreactionmanager.cpp
    ${EMCLIENT_LINUX_PATH}/src/emreportservice.cpp
    ${EMCLIENT_LINUX_PATH}/src/emrequestreport.cpp
    ${EMCLIENT_LINUX_PATH}/src/emthread.cpp
    ${EMCLIENT_LINUX_PATH}/src/emthreadevent.cpp
    ${EMCLIENT_LINUX_PATH}/src/emthreadmanager.cpp
    ${EMCLIENT_LINUX_PATH}/src/emhttprequestmonitor.cpp
    ${EMCLIENT_LINUX_PATH}/src/emmessagestatistics.cpp
    ${EMCLIENT_LINUX_PATH}/src/emstatisticsmanager.cpp
    ${EMCLIENT_LINUX_PATH}/src/emmessagecollect.cpp
    ${EMCLIENT_LINUX_PATH}/src/emfetchmessageoption.cpp
    ${EMCLIENT_LINUX_PATH}/src/emgroupprivate.cpp
    ${EMCLIENT_LINUX_PATH}/src/emuploadinparts.cpp
    ${EMCLIENT_LINUX_PATH}/src/emcontact.cpp
    ${EMCLIENT_LINUX_PATH}/src/message/emcmdmessagebody.cpp
    ${EMCLIENT_LINUX_PATH}/src/message/emcustommessagebody.cpp
    ${EMCLIENT_LINUX_PATH}/src/message/emfilemessagebody.cpp
    ${EMCLIENT_LINUX_PATH}/src/message/emimagemessagebody.cpp
    ${EMCLIENT_LINUX_PATH}/src/message/emlocationmessagebody.cpp
    ${EMCLIENT_LINUX_PATH}/src/message/emmessage.cpp
    ${EMCLIENT_LINUX_PATH}/src/message/emmessagebody.cpp
    ${EMCLIENT_LINUX_PATH}/src/message/emmessageencoder.cpp
    ${EMCLIENT_LINUX_PATH}/src/message/emtextmessagebody.cpp
    ${EMCLIENT_LINUX_PATH}/src/message/emvideomessagebody.cpp
    ${EMCLIENT_LINUX_PATH}/src/message/emvoicemessagebody.cpp
    ${EMCLIENT_LINUX_PATH}/src/message/emmessagereaction.cpp
    ${EMCLIENT_LINUX_PATH}/src/message/emcombinemessagebody.cpp
    ${EMCLIENT_LINUX_PATH}/src/utils/emcryptoadapter.cpp
    ${EMCLIENT_LINUX_PATH}/src/utils/emencryptutils.cpp
    ${EMCLIENT_LINUX_PATH}/src/utils/emfilecompressor.cpp
    ${EMCLIENT_LINUX_PATH}/src/utils/emhttprequest.cpp
    ${EMCLIENT_LINUX_PATH}/src/utils/emlog.cpp
    ${EMCLIENT_LINUX_PATH}/src/utils/emtimer.cpp
    ${EMCLIENT_LINUX_PATH}/src/utils/emutils.cpp
    ${EMCLIENT_LINUX_PATH}/src/utils/emaestool.cpp
    ${EMCLIENT_LINUX_PATH}/src/emapclientmanager.cpp
    ${EMCLIENT_LINUX_PATH}/src/emeventengine.cpp
    ${EMCLIENT_LINUX_PATH}/src/emtcpclientimpl.cpp
    ${EMCLIENT_LINUX_PATH}/src/emtransportdelegateimpl.cpp
    ${EMCLIENT_LINUX_PATH}/src/emudpserverimpl.cpp
    ${EMCLIENT_LINUX_PATH}/src/emconversationfilter.cpp
    ${EMCLIENT_LINUX_PATH}/src/emcrashmonitor.cpp
    ${EMCLIENT_LINUX_PATH}/src/emautchattransport.cpp
    ${EMCLIENT_LINUX_PATH}/src/emtcpchattransport.cpp
)

if(APPLE)
    add_library(${easemob} SHARED ${EASEMOB_SRC})
else()
    add_library(${easemob} STATIC ${EASEMOB_SRC})
endif()

target_compile_definitions(${easemob} PRIVATE
    CHECK_HASDATA
    CURL_STATICLIB
)

if(APPLE)
        set_source_files_properties(${EMCLIENT_LINUX_PATH}/src/emapclientmanager.cpp PROPERTIES COMPILE_FLAGS "-fno-rtti")

	set_target_properties(${easemob} PROPERTIES
		FRAMEWORK TRUE
		FRAMEWORK_VERSION A
		XCODE_ATTRIBUTE_PRODUCT_BUNDLE_IDENTIFIER "com.easemob.hyphenateChat-mac"
		MACOSX_FRAMEWORK_INFO_PLIST ${CMAKE_SOURCE_DIR}/Info.plist
	)

	target_link_libraries(${easemob} PRIVATE 
		${libz_tbd}
		${security_framework}
		${foundation_framework}
		${libcrypto}
		${libssl}
		${libcurl}
		${libsqlcipher}
		${libevent_openssl}
		${libevent_core}
		${libevent_extra}
		${libevent_thread}
		${libevent}
		${agora_utils_static}
		${agora_transport_static}
		${agora_ap_static}
		${agora_edge_allocator_static}
		${agora_generic_report_static}
		${libaosl_chat}
		${libzip}
	)
endif()

# 添加目标 SdkWrapper
add_library(${SdkWrapper} SHARED
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/callbacks.h
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/models.h
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/sdk_wrapper.h
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/sdk_wrapper_internal.h
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/tool.h
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/client.cpp
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/chat_manager.cpp
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/contact_manager.cpp
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/conversation_manager.cpp
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/group_manager.cpp
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/models.cpp
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/presence_manager.cpp
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/room_manager.cpp
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/tool.cpp
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/thread_manager.cpp
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/userinfo_manager.cpp
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/core_dump.h
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/core_dump.cpp
)

target_compile_definitions(${SdkWrapper} PRIVATE
    SDK_WRAPPER_EXPORT
    _CRT_SECURE_NO_WARNINGS
    NOMINMAX
    CURL_STATICLIB
)

if(APPLE)
	target_link_libraries(${SdkWrapper} PRIVATE ${easemob})

	set_target_properties(${SdkWrapper} PROPERTIES
		INSTALL_NAME_DIR "@rpath"
		INSTALL_NAME "@rpath/$(EXECUTABLE_PATH)"
		BUILD_RPATH "@loader_path/../Resources"
	)
else()
	target_link_libraries(${SdkWrapper} PRIVATE 
		${easemob}
		${zlib}
		${libcrypto}
		${libssl}
		${libcurl}
		${libsqlcipher}
		${libevent_openssl}
		${libevent_core}
		${libevent_extra}
		${libevent}
		${agora_utils_static}
		${agora_transport_static}
		${agora_ap_static}
		${agora_edge_allocator_static}
		${agora_generic_report_static}
		${libaosl_chat}
		${libzip}
	)

	target_link_options(${SdkWrapper} PRIVATE "/NODEFAULTLIB:LIBCMT.lib")
endif()

# 添加目标 CommonWrapper
add_library(${CommonWrapper} SHARED
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/common_wrapper/common_wrapper.h
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/common_wrapper/common_wrapper_internal.h
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/sdk_wrapper.h
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/common_wrapper/common_cwrapper.cpp
)

target_compile_definitions(${CommonWrapper} PRIVATE COMMON_WRAPPER_EXPORT)

if(APPLE)
	set_target_properties(${CommonWrapper} PROPERTIES
		INSTALL_NAME_DIR "@rpath"
		INSTALL_NAME "@rpath/$(EXECUTABLE_PATH)"
		BUILD_RPATH "@loader_path/../Resources"
	)
endif()

target_link_libraries(${CommonWrapper} PRIVATE ${SdkWrapper})

set(CHATCWRAPPER_SRC
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/CWrapper/include/Api_decorator.h
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/CWrapper/include/CWrapper.h
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/CWrapper/include/CWrapper_import.h
    ${EMCLIENT_UNITY_PATH}/AgoraChatSDK/CWrapper/CWrapper/src/CWrapper.cpp
)

# 添加目标 ChatCWrapper
if(APPLE)
	add_library(${ChatCWrapper} MODULE ${CHATCWRAPPER_SRC})
else()
	add_library(${ChatCWrapper} SHARED ${CHATCWRAPPER_SRC})
endif()

target_compile_definitions(${ChatCWrapper} PRIVATE AGORACHAT_EXPORT)

target_link_libraries(${ChatCWrapper} PRIVATE ${CommonWrapper})

if(APPLE)
	set_target_properties(${ChatCWrapper} PROPERTIES BUILD_RPATH "@loader_path/../Resources")

	set_target_properties(${ChatCWrapper} PROPERTIES
    	    BUNDLE TRUE
            MACOSX_BUNDLE TRUE
	    XCODE_ATTRIBUTE_PRODUCT_BUNDLE_IDENTIFIER "com.easemob.ChatCWrapper"
            MACOSX_BUNDLE_INFO_PLIST ${CMAKE_SOURCE_DIR}/Info.plist
	)

    	# 定义需要拷贝的资源文件列表
	set(RESOURCE_FILES
        	$<TARGET_FILE:${easemob}>
        	$<TARGET_FILE:${CommonWrapper}>
        	$<TARGET_FILE:${SdkWrapper}>
    	)

	message(STATUS "RESOURCE_FILES: ${RESOURCE_FILES}")

	# 添加自定义命令来创建 Resources 目录
	add_custom_command(TARGET ${ChatCWrapper} POST_BUILD
        	COMMAND ${CMAKE_COMMAND} -E make_directory $<TARGET_BUNDLE_DIR:${ChatCWrapper}>/Contents/Resources
        	COMMAND ${CMAKE_COMMAND} -E make_directory $<TARGET_BUNDLE_DIR:${ChatCWrapper}>/Contents/Resources/lib
        	COMMENT "Creating Resources directory"
    	)

	# 将相对路径转换为绝对路径
	get_filename_component(EMCLIENT_LINUX_ABSOLUTE_PATH ${EMCLIENT_LINUX_PATH} ABSOLUTE)
	message(STATUS "EMCLIENT_LINUX_ABSOLUTE_PATH: ${EMCLIENT_LINUX_ABSOLUTE_PATH}")

	# 添加自定义命令来处理和复制资源文件
	add_custom_command(TARGET ChatCWrapper POST_BUILD
    		COMMAND ${CMAKE_COMMAND} -E env
        	RESOURCE_FILES="${RESOURCE_FILES}"
        	TARGET_BUNDLE_DIR=$<TARGET_BUNDLE_DIR:ChatCWrapper>
        	EMCLIENT_LINUX_ABSOLUTE_PATH="${EMCLIENT_LINUX_ABSOLUTE_PATH}"
        	${CMAKE_COMMAND} -P ${CMAKE_SOURCE_DIR}/copy_resources.cmake
    		COMMENT "Processing and copying resources"
	)
endif()
