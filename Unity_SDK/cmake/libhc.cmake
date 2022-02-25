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
    if(MAC_DYNAMIC_GENERATE_TYPE STREQUAL "Dynamic")
      # 不生效 set_target_properties( libhyphenateCWrapper PROPERTIES BUNDLE TRUE
      # MACOSX_BUNDLE_INFO_PLIST ${HC_ROOT_DIR}/cmake/macos/Bundle.plist.in
      # MACOSX_BUNDLE_BUNDLE_NAME "hyphenateCWrapper"
      # MACOSX_BUNDLE_BUNDLE_VERSION 1.0.0 MACOSX_BUNDLE_COPYRIGHT "Copyright ©
      # 2021 easemob. All rights reserved." MACOSX_BUNDLE_GUI_IDENTIFIER
      # "com.easemob.hyphenateCWrapper" MACOSX_BUNDLE_INFO_STRING
      # "https://www.easemob.com/" INSTALL_RPATH "." INSTALL_NAME_DIR @rpath
      # PUBLIC_HEADER "${hyphenateCWrapper_HEADERS_FILES}" VERSION
      # ${PROJECT_VERSION} OUTPUT_NAME hyphenateCWrapper DEBUG_POSTFIX
      # "${HC_POSTFIX}")
      set_target_properties(
        libhyphenateCWrapper
        PROPERTIES VERSION ${PROJECT_VERSION}
                   OUTPUT_NAME hyphenateCWrapper
                   DEBUG_POSTFIX "${HC_POSTFIX}")
    elseif(MAC_DYNAMIC_GENERATE_TYPE STREQUAL "Framework")
      set_target_properties(
        libhyphenateCWrapper
        PROPERTIES FRAMEWORK TRUE
                   MACOSX_FRAMEWORK_INFO_PLIST
                   ${HC_ROOT_DIR}/cmake/macos/Framework.plist.in
                   INSTALL_RPATH "."
                   FRAMEWORK_VERSION A
                   MACOSX_FRAMEWORK_IDENTIFIER "com.easemob.hyphenateCWrapper"
                   MACOSX_FRAMEWORK_NAME "hyphenateCWrapper"
                   MACOSX_BUNDLE_COPYRIGHT
                   "Copyright © 2021 easemob. All rights reserved."
                   INSTALL_NAME_DIR @rpath
                   PUBLIC_HEADER "${hyphenateCWrapper_HEADERS_FILES}"
                   VERSION ${PROJECT_VERSION}
                   OUTPUT_NAME hyphenateCWrapper
                   DEBUG_POSTFIX "${HC_POSTFIX}")
    else()
      message(FATAL_ERROR "Not support this type. ${MAC_DYNAMIC_GENERATE_TYPE}")
    endif(MAC_DYNAMIC_GENERATE_TYPE STREQUAL "Dynamic")
  else(HC_BUILD_SHARED_LIBS)
    target_compile_definitions(libhyphenateCWrapper PUBLIC HYPHENATE_API)
    set_target_properties(
      libhyphenateCWrapper
      PROPERTIES VERSION ${PROJECT_VERSION}
                 OUTPUT_NAME hyphenateCWrapper
                 DEBUG_POSTFIX "${HC_POSTFIX}")
  endif(HC_BUILD_SHARED_LIBS)
endif(MACOS)

add_library(emclient::libhyphenateCWrapper ALIAS libhyphenateCWrapper)
