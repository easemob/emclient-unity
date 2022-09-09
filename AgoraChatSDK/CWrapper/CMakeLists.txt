cmake_minimum_required(VERSION 3.8)

project(CWrapper LANGUAGES CXX)


if(NOT CMAKE_BUILD_TYPE)
    set(CMAKE_BUILD_TYPE "Release")
endif()


SET(CMAKE_EXPORT_COMPILE_COMMANDS ON)


# 设置头文件
set(Headers 
    CWrapper/include/CWrapper.h
)


# 添加源文件路径
aux_source_directory(CWrapper/src DIR_LIB_SOURCES)

# 生成静态库
add_library(CWrapper STATIC
    ${DIR_LIB_SOURCES}
    ${Headers}
)

# 生成Sample 项目
add_executable(Sample main.cpp)

# 将CWrapper/include 添加到Sample
target_include_directories(Sample PUBLIC CWrapper/include)


