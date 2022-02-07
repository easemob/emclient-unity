set(hyphenateCWrapper_HEADERS_DIRS ${HC_ROOT_DIR}/c_wrapper)
set(hyphenateCWrapper_SOURCES_DIRS ${HC_ROOT_DIR}/c_wrapper)
file(GLOB_RECURSE hyphenateCWrapper_HEADERS_FILES
     "${hyphenateCWrapper_HEADERS_DIRS}/*.h")
message(hyphenateCWrapper_HEADERS_DIRS=${hyphenateCWrapper_HEADERS_DIRS})
message(hyphenateCWrapper_SOURCES_DIRS=${hyphenateCWrapper_SOURCES_DIRS})

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
    ${hyphenateCWrapper_SOURCES_DIRS}/userinfo_manager.cpp)

add_library(
  libhyphenateCWrapper
  ${HC_SHARED_OR_STATIC} ${hyphenateCWrapper_SOURCES_FILES}
  ${hyphenateCWrapper_HEADERS_FILES})

target_include_directories(libhyphenateCWrapper
                           PUBLIC ${hyphenateCWrapper_HEADERS_DIRS})
target_include_directories(libhyphenateCWrapper PRIVATE ${sdkcore_HEADERS_DIRS})
target_include_directories(libhyphenateCWrapper
                           PRIVATE ${sdkcore_HEADERS_DIRS}/message)
target_link_libraries(libhyphenateCWrapper libsdkcore)

if(WINDOWS AND MSVC)
  if(HC_BUILD_SHARED_LIBS)
    target_compile_definitions(
      libhyphenateCWrapper
      PUBLIC AGORACHAT_USE_DLLS
      PUBLIC AGORACHAT_EXPORT)
  else(HC_BUILD_SHARED_LIBS)
    target_compile_definitions(libhyphenateCWrapper PUBLIC HYPHENATE_API)
  endif(HC_BUILD_SHARED_LIBS)
  add_definitions(-DUNICODE -D_UNICODE)
  set_target_properties(
    libhyphenateCWrapper
    PROPERTIES VERSION ${PROJECT_VERSION}
               OUTPUT_NAME libhyphenateCWrapper
               DEBUG_POSTFIX "${hyphenateCWrapper_POSTFIX}")
endif(WINDOWS AND MSVC)
if(MACOS)
  target_link_libraries(libhyphenateCWrapper "-framework LDAP")
  if(HC_BUILD_SHARED_LIBS)
    target_compile_definitions(
      libhyphenateCWrapper
      PUBLIC AGORACHAT_USE_DLLS
      PUBLIC AGORACHAT_EXPORT)
    set_property(TARGET libhyphenateCWrapper PROPERTY POSITION_INDEPENDENT_CODE
                                                      TRUE)
    set_target_properties(
      libhyphenateCWrapper
      PROPERTIES FRAMEWORK TRUE
                 FRAMEWORK_VERSION A
                 MACOSX_FRAMEWORK_IDENTIFIER com.easemob.unity
                 MACOSX_FRAMEWORK_INFO_PLIST
                 ${HC_ROOT_DIR}/cmake/macos/Info.plist
                 INSTALL_RPATH "."
                 INSTALL_NAME_DIR @rpath
                 PUBLIC_HEADER "${hyphenateCWrapper_HEADERS_FILES}"
                 VERSION ${PROJECT_VERSION}
                 OUTPUT_NAME HyphenateCWrapper
                 DEBUG_POSTFIX "${HC_POSTFIX}")
  else(HC_BUILD_SHARED_LIBS)
    target_compile_definitions(libhyphenateCWrapper PUBLIC HYPHENATE_API)
    set_target_properties(
      libhyphenateCWrapper
      PROPERTIES VERSION ${PROJECT_VERSION}
                 OUTPUT_NAME libhyphenateCWrapper
                 DEBUG_POSTFIX "${HC_POSTFIX}")
  endif(HC_BUILD_SHARED_LIBS)
endif(MACOS)

add_library(emclient::libhyphenateCWrapper ALIAS libhyphenateCWrapper)
