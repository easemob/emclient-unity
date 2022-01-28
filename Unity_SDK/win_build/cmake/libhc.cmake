set(hyphenateCWrapper_HEADERS_DIRS ${HC_ROOT_DIR}/../c_wrapper)
set(hyphenateCWrapper_SOURCES_DIRS ${HC_ROOT_DIR}/../c_wrapper)
file(GLOB_RECURSE hyphenateCWrapper_HEADERS_FILES "${hyphenateCWrapper_HEADERS_DIRS}/*.h")

set(hyphenateCWrapper_SOURCES_FILES
  ${hyphenateCWrapper_SOURCES_DIRS}/chat_manager.cpp
  ${hyphenateCWrapper_SOURCES_DIRS}/client.cpp
  ${hyphenateCWrapper_SOURCES_DIRS}/contact_manager.cpp
  ${hyphenateCWrapper_SOURCES_DIRS}/conversation_manager.cpp
  ${hyphenateCWrapper_SOURCES_DIRS}/group_manager.cpp
  ${hyphenateCWrapper_SOURCES_DIRS}/hyphenateCWrapper.cpp
  ${hyphenateCWrapper_SOURCES_DIRS}/models.cpp
  ${hyphenateCWrapper_SOURCES_DIRS}/push_manager.cpp
  ${hyphenateCWrapper_SOURCES_DIRS}/room_manager.cpp
  ${hyphenateCWrapper_SOURCES_DIRS}/tool.cpp
  ${hyphenateCWrapper_SOURCES_DIRS}/userinfo_manager.cpp
)

add_definitions(-DAGORACHAT_EXPORT)

add_library(libhyphenateCWrapper  ${HC_SHARED_OR_STATIC}
  ${hyphenateCWrapper_SOURCES_FILES}
  ${hyphenateCWrapper_HEADERS_FILES}
)

target_include_directories(libhyphenateCWrapper  PUBLIC ${hyphenateCWrapper_HEADERS_DIRS})
target_include_directories(libhyphenateCWrapper  PRIVATE ${sdkcore_HEADERS_DIRS})
target_include_directories(libhyphenateCWrapper  PRIVATE ${sdkcore_HEADERS_DIRS}/message)
target_link_libraries(libhyphenateCWrapper  libsdkcore)

if(MSVC)
  add_definitions(-DUNICODE -D_UNICODE)
  set_target_properties(libhyphenateCWrapper  PROPERTIES
    VERSION ${PROJECT_VERSION}
    OUTPUT_NAME libhyphenateCWrapper 
    DEBUG_POSTFIX "${hyphenateCWrapper_POSTFIX}")
endif(MSVC)

add_library(emclient::libhyphenateCWrapper  ALIAS libhyphenateCWrapper )