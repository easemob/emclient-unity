set(hypernateCWrapper_HEADERS_DIRS ${HC_ROOT_DIR}/../c_wrapper)
set(hypernateCWrapper_SOURCES_DIRS ${HC_ROOT_DIR}/../c_wrapper)
file(GLOB_RECURSE hypernateCWrapper_HEADERS_FILES "${hypernateCWrapper_HEADERS_DIRS}/*.h")

set(hypernateCWrapper_SOURCES_FILES
  ${hypernateCWrapper_SOURCES_DIRS}/chat_manager.cpp
  ${hypernateCWrapper_SOURCES_DIRS}/client.cpp
  ${hypernateCWrapper_SOURCES_DIRS}/contact_manager.cpp
  ${hypernateCWrapper_SOURCES_DIRS}/conversation_manager.cpp
  ${hypernateCWrapper_SOURCES_DIRS}/group_manager.cpp
  ${hypernateCWrapper_SOURCES_DIRS}/hyphenateCWrapper.cpp
  ${hypernateCWrapper_SOURCES_DIRS}/models.cpp
  ${hypernateCWrapper_SOURCES_DIRS}/push_manager.cpp
  ${hypernateCWrapper_SOURCES_DIRS}/room_manager.cpp
  ${hypernateCWrapper_SOURCES_DIRS}/tool.cpp
  ${hypernateCWrapper_SOURCES_DIRS}/userinfo_manager.cpp
)

add_definitions(-DAGORACHAT_EXPORT)

add_library(libhypernateCWrapper ${HC_SHARED_OR_STATIC}
  ${hypernateCWrapper_SOURCES_FILES}
  ${hypernateCWrapper_HEADERS_FILES}
)

target_include_directories(libhypernateCWrapper PUBLIC ${hypernateCWrapper_HEADERS_DIRS})
target_include_directories(libhypernateCWrapper PRIVATE ${sdkcore_HEADERS_DIRS})
target_include_directories(libhypernateCWrapper PRIVATE ${sdkcore_HEADERS_DIRS}/message)
# target_include_directories(libhypernateCWrapper PRIVATE ${sdkcore_ROOT_DIR}/protocol/generated)
# target_include_directories(libhypernateCWrapper PRIVATE ${HC_ROOT_DIR}/protocol)
# target_include_directories(libhypernateCWrapper PRIVATE ${ZLIB_INCLUDE_DIRS})
# target_include_directories(libhypernateCWrapper PRIVATE ${OPENSSL_INCLUDE_DIRS})
# target_include_directories(libhypernateCWrapper PRIVATE ${CURL_INCLUDE_DIRS})
# target_include_directories(libhypernateCWrapper PRIVATE ${SQLITE3_INCLUDE_DIRS})
# target_include_directories(libhypernateCWrapper PRIVATE ${PROTOBUF_INCLUDE_DIRS})
# target_include_directories(libhypernateCWrapper PRIVATE ${RJSON_INCLUDE_DIRS})
# target_include_directories(libhypernateCWrapper PRIVATE ${CURLCPP_INCLUDE_DIRS})
target_link_libraries(libhypernateCWrapper libsdkcore)
# target_link_libraries(libhypernateCWrapper zlib)
# target_link_libraries(libhypernateCWrapper libcrypto libssl)
# target_link_libraries(libhypernateCWrapper libcurl)
# target_link_libraries(libhypernateCWrapper libsqlite3)
# target_link_libraries(libhypernateCWrapper libprotobuf)
# target_link_libraries(libhypernateCWrapper librjson)
# target_link_libraries(libhypernateCWrapper libcurlcpp)
# target_link_directories(libhypernateCWrapper PRIVATE ${ZLIB_LIBRARY_DIRS})
# target_link_directories(libhypernateCWrapper PRIVATE ${OPENSSL_LIBRARY_DIRS})
# target_link_directories(libhypernateCWrapper PRIVATE ${CURL_LIBRARY_DIRS})
# target_link_directories(libhypernateCWrapper PRIVATE ${SQLITE3_LIBRARY_DIRS})

if(MSVC)
  add_definitions(-DUNICODE -D_UNICODE)
  set_target_properties(libhypernateCWrapper PROPERTIES
    VERSION ${PROJECT_VERSION}
    OUTPUT_NAME libhypernateCWrapper
    DEBUG_POSTFIX "${hypernateCWrapper_POSTFIX}")
endif(MSVC)

add_library(emclient::libhypernateCWrapper ALIAS libhypernateCWrapper)