# 使用说明 该项目依赖`emclient-linux`，两个项目需要位于同一个目录下，如果不在同一个目录则需要修改`CORE_ROOT_DIRS`值
# `-DHC_BUILD_SHARED_LIBS=ON` or OFF : 控制生成静态库还是动态库，默认是静态库 执行命令示例： "C:\Program
# Files\CMake\bin\cmake.EXE" --no-warn-unused-cli
# -DCMAKE_EXPORT_COMPILE_COMMANDS:BOOL=TRUE
# -DMAC_DYNAMIC_GENERATE_TYPE=Dynamic or Framework
# -Hd:/codes/gyp/emclient-unity/Unity_SDK/win_build
# -Bd:/codes/gyp/emclient-unity/Unity_SDK/win_build/build -G "Visual Studio 16
# 2019" -T host=x86 -A x64
# `-DCMAKE_BUILD_TYPE=Debug` or Release

cmake_minimum_required(VERSION 3.15.0 FATAL_ERROR)
project(
  hyphenateCWrapper
  VERSION 1.0.0
  LANGUAGES C CXX)

if(NOT CORE_ROOT_DIRS)
  set(CORE_ROOT_DIRS ${CMAKE_CURRENT_SOURCE_DIR}/../../emclient-linux)
endif(NOT CORE_ROOT_DIRS)
message(CORE_ROOT_DIRS=${CORE_ROOT_DIRS})

if(NOT HC_ROOT_DIR)
  set(HC_ROOT_DIR ${CMAKE_CURRENT_SOURCE_DIR})
endif(NOT HC_ROOT_DIR)
message(HC_ROOT_DIR=${HC_ROOT_DIR})

include(${CORE_ROOT_DIRS}/cmake/platform.cmake)

if(NOT WINDOWS AND NOT MACOS)
  message(
    FATAL_ERROR "Currently, only Windows or MacOs compilation is supported")
endif()

# CMake policies
cmake_policy(SET CMP0022 NEW)
# On MacOS use @rpath/ for target's install name prefix path
if(POLICY CMP0042)
  cmake_policy(SET CMP0042 NEW)
endif()
# Clear VERSION variables when no VERSION is given to project()
if(POLICY CMP0048)
  cmake_policy(SET CMP0048 NEW)
endif()
# The target_sources() command converts relative paths to absolute.
if(POLICY CMP0076)
  cmake_policy(SET CMP0076 NEW)
endif()
# WINDOWS AND MSVC runtime library flags are selected by an abstraction.
if(POLICY CMP0091)
  cmake_policy(SET CMP0091 NEW)
endif()

set(CMAKE_CXX_STANDARD 11)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)

# default Debug compile
set(CMAKE_BUILD_TYPE
    Debug
    CACHE STRING "Choose the type of build, options are: Debug Release.")

# default build dynamic library
set(HC_BUILD_SHARED_LIBS ON)

set(sdkcore_BUILD_SHARED_LIBS OFF)
set(sdkcore_SHARED_OR_STATIC "STATIC")
set(sdkcore_ROOT_DIR ${CORE_ROOT_DIRS})

set(HC_SHARED_OR_STATIC "STATIC")
if(HC_BUILD_SHARED_LIBS)
  set(HC_SHARED_OR_STATIC "SHARED")
  if(NOT MAC_DYNAMIC_GENERATE_TYPE)
    set(MAC_DYNAMIC_GENERATE_TYPE "Dynamic")
  endif(NOT MAC_DYNAMIC_GENERATE_TYPE)
else(HC_BUILD_SHARED_LIBS)
  set(HC_SHARED_OR_STATIC "STATIC")
endif(HC_BUILD_SHARED_LIBS)

# default enable compile warning
set(HC_ENABLE_COMPILE_WARNING OFF)

if(WINDOWS AND MSVC)
  if(CMAKE_CXX_COMPILER_ID STREQUAL "WINDOWS AND MSVC")
    # Build with multiple processes
    add_definitions(/MP)
  endif()
  # Set source file and execution character sets to UTF-8
  add_definitions(/utf-8)
  # WINDOWS AND MSVC warning suppressions
  if(HC_ENABLE_COMPILE_WARNING)
    list(APPEND _COMPILE_WARNING)
  else(HC_ENABLE_COMPILE_WARNING)
    list(
      APPEND
      _COMPILE_WARNING
      /wd4018 # 'expression' : signed/unsigned mismatch
      /wd4065 # switch statement contains 'default' but no 'case' labels
      /wd4146 # unary minus operator applied to unsigned type, result still
              # unsigned
      /wd4244 # 'conversion' conversion from 'type1' to 'type2', possible loss
              # of data
      /wd4251 # 'identifier' : class 'type' needs to have dll-interface to be
              # used by clients of class 'type2'
      /wd4267 # 'var' : conversion from 'size_t' to 'type', possible loss of
              # data
      /wd4305 # 'identifier' : truncation from 'type1' to 'type2'
      /wd4307 # 'operator' : integral constant overflow
      /wd4309 # 'conversion' : truncation of constant value
      /wd4334 # 'operator' : result of 32-bit shift implicitly converted to 64
              # bits (was 64-bit shift intended?)
      /wd4355 # 'this' : used in base member initializer list
      /wd4506 # no definition for inline function 'function'
      /wd4800 # 'type' : forcing value to bool 'true' or 'false' (performance
              # warning)
      /wd4996 # The compiler encountered a deprecated declaration.
    )
  endif(HC_ENABLE_COMPILE_WARNING)
  add_definitions(${_COMPILE_WARNING})

  set(CMAKE_EXE_LINKER_FLAGS "${CMAKE_EXE_LINKER_FLAGS} /ignore:4099")
  set(CMAKE_STATIC_LINKER_FLAGS "${CMAKE_STATIC_LINKER_FLAGS} /ignore:4099")
  set(CMAKE_MODULE_LINKER_FLAGS "${CMAKE_MODULE_LINKER_FLAGS} /ignore:4099")
  set(CMAKE_SHARED_LINKER_FLAGS "${CMAKE_SHARED_LINKER_FLAGS} /ignore:4099")
endif(WINDOWS AND MSVC)
if(MACOS)
  # set(CMAKE_GENERATOR "Xcode")
  if(HC_ENABLE_COMPILE_WARNING)
    list(
      APPEND
      _COMPILE_WARNING
      -Wall
      -Wextra
      -Wno-unused-parameter
      -Wstrict-aliasing
      -Wstrict-prototypes
      -fno-strict-aliasing
      -Wmissing-prototypes
      -Winit-self
      -Wmissing-field-initializers
      -Wdeclaration-after-statement
      -Waddress
      -Wnormalized=id
      -Woverride-init
      -Wlogical-op
      -Wwrite-strings
      -Wno-unused-function)
  else(HC_ENABLE_COMPILE_WARNING)
    list(APPEND _COMPILE_WARNING -w)
  endif(HC_ENABLE_COMPILE_WARNING)
  add_compile_options(${_COMPILE_WARNING})
endif(MACOS)

# https://cmake.org/cmake/help/latest/variable/CMAKE_MSVC_RUNTIME_LIBRARY.html
if(${CMAKE_BUILD_TYPE} STREQUAL Debug)
  add_definitions(-DDEBUG)
  if(WINDOWS AND MSVC)
    if(HC_BUILD_SHARED_LIBS)
      set(CMAKE_MSVC_RUNTIME_LIBRARY MultiThreaded$<$<CONFIG:Debug>:Debug>DLL)
    else(HC_BUILD_SHARED_LIBS)
      set(CMAKE_MSVC_RUNTIME_LIBRARY MultiThreaded$<$<CONFIG:Debug>:Debug>DLL)
    endif(HC_BUILD_SHARED_LIBS)
  endif(WINDOWS AND MSVC)
else()
  if(WINDOWS AND MSVC)
    if(HC_BUILD_SHARED_LIBS)
      set(CMAKE_MSVC_RUNTIME_LIBRARY MultiThreadedDLL)
    else(HC_BUILD_SHARED_LIBS)
      set(CMAKE_MSVC_RUNTIME_LIBRARY MultiThreadedDLL)
    endif(HC_BUILD_SHARED_LIBS)
  endif(WINDOWS AND MSVC)
endif()

if(${CMAKE_BUILD_TYPE} STREQUAL Debug)
  set(HC_POSTFIX
      "d"
      CACHE STRING "Default debug postfix")
else()
  set(HC_POSTFIX
      ""
      CACHE STRING "Default debug postfix")
endif()
set(sdkcore_PREFIX "lib")

# sdkcore
set(sdkcore_HEADERS_DIRS ${CORE_ROOT_DIRS}/include)

# zlib
include(${CORE_ROOT_DIRS}/cmake/libz.cmake)

# openssl
include(${CORE_ROOT_DIRS}/cmake/libopenssl.cmake)

# curl
include(${CORE_ROOT_DIRS}/cmake/libcurl.cmake)

# sqlite3
include(${CORE_ROOT_DIRS}/cmake/libsqlite3.cmake)

# protobuf
include(${CORE_ROOT_DIRS}/cmake/libprotobuf.cmake)

# rapidjson
include(${CORE_ROOT_DIRS}/cmake/librjson.cmake)

# curlcpp
include(${CORE_ROOT_DIRS}/cmake/libcurlcpp.cmake)

# sdkcore
include(${CORE_ROOT_DIRS}/cmake/libsdkcore.cmake)

# hyphenateCWrapper
include(${HC_ROOT_DIR}/cmake/libhc.cmake)

include(CTest)
enable_testing()

set(CPACK_PROJECT_NAME ${PROJECT_NAME})
set(CPACK_PROJECT_VERSION ${PROJECT_VERSION})
include(CPack)


message(STATUS CORE_ROOT_DIRS="${CORE_ROOT_DIRS}")
message(STATUS HC_ROOT_DIR="${HC_ROOT_DIR}")
message(STATUS CMAKE_BUILD_TYPE="${CMAKE_BUILD_TYPE}")
message(STATUS HC_BUILD_SHARED_LIBS="${HC_BUILD_SHARED_LIBS}")
message(STATUS sdkcore_BUILD_SHARED_LIBS="${sdkcore_BUILD_SHARED_LIBS}")
message(STATUS sdkcore_ROOT_DIR="${sdkcore_ROOT_DIR}")
message(STATUS HC_ENABLE_COMPILE_WARNING="${HC_ENABLE_COMPILE_WARNING}")
message(STATUS HC_SHARED_OR_STATIC="${HC_SHARED_OR_STATIC}")
message(STATUS MAC_DYNAMIC_GENERATE_TYPE="${MAC_DYNAMIC_GENERATE_TYPE}")
message(STATUS CMAKE_SYSTEM_NAME="${CMAKE_SYSTEM_NAME}")
