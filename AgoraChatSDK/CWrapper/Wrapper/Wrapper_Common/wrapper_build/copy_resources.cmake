
# 打印环境变量
execute_process(COMMAND ${CMAKE_COMMAND} -E echo "RESOURCE_FILES: $ENV{RESOURCE_FILES}")
execute_process(COMMAND ${CMAKE_COMMAND} -E echo "TARGET_BUNDLE_DIR: $ENV{TARGET_BUNDLE_DIR}")

# 定义目标目录变量
set(TARGET_RESOURCES_DIR "$ENV{TARGET_BUNDLE_DIR}/Contents/Resources")
set(TARGET_RESOURCES_LIB_DIR "$ENV{TARGET_BUNDLE_DIR}/Contents/Resources/lib")
set(DEPEND_DIR "$ENV{EMCLIENT_LINUX_ABSOLUTE_PATH}/3rd_party/platform/darwin/depends_universal")

execute_process(COMMAND ${CMAKE_COMMAND} -E echo "DEPEND_DIR: ${DEPEND_DIR}")
execute_process(COMMAND ${CMAKE_COMMAND} -E echo "TARGET_RESOURCES_LIB_DIR: ${TARGET_RESOURCES_LIB_DIR}")

# 将环境变量转换为 CMake 列表
string(REPLACE " " ";" RESOURCE_FILES_LIST "$ENV{RESOURCE_FILES}")

# 循环处理资源文件
foreach(RESOURCE IN LISTS RESOURCE_FILES_LIST)
    execute_process(COMMAND ${CMAKE_COMMAND} -E echo "Processing resource: ${RESOURCE}")

    if("${RESOURCE}" MATCHES "${easemob}.framework")
        # 获取easemob.framework的路径
        string(REGEX REPLACE "/Versions/A/.*" "" FRAMEWORK_DIR "${RESOURCE}")
        file(TO_CMAKE_PATH "${FRAMEWORK_DIR}" FRAMEWORK_DIR)
        execute_process(COMMAND ${CMAKE_COMMAND} -E echo "easemob_universal framework root directory: ${FRAMEWORK_DIR}")

	# 查找并删除框架目录中的所有软链接
	execute_process(
    		COMMAND find ${FRAMEWORK_DIR} -type l -exec rm -f {} +
    		RESULT_VARIABLE result
    		OUTPUT_VARIABLE output
    		ERROR_VARIABLE error
	)

	# 将easemob.framework拷贝到指定目录
	file(COPY "${FRAMEWORK_DIR}" DESTINATION "${TARGET_RESOURCES_DIR}")

    elseif(IS_DIRECTORY ${RESOURCE})
        execute_process(COMMAND ${CMAKE_COMMAND} -E echo "Copying directory: ${RESOURCE}")
	file(COPY "${RESOURCE}" DESTINATION "${TARGET_RESOURCES_DIR}")

    else()
        execute_process(COMMAND ${CMAKE_COMMAND} -E echo "Copying file: ${RESOURCE}")
        execute_process(COMMAND ${CMAKE_COMMAND} -E copy "${RESOURCE}" "${TARGET_RESOURCES_DIR}")
    endif()
endforeach()

# 拷贝其他依赖库文件
execute_process(COMMAND ${CMAKE_COMMAND} -E copy_directory "${DEPEND_DIR}/zlib_1.2.11_share_universal/lib" "${TARGET_RESOURCES_LIB_DIR}")
execute_process(COMMAND ${CMAKE_COMMAND} -E copy_directory "${DEPEND_DIR}/boringssl_1.1.1f_share_universal/lib" "${TARGET_RESOURCES_LIB_DIR}")
execute_process(COMMAND ${CMAKE_COMMAND} -E copy_directory "${DEPEND_DIR}/curl_7.80.0_share_universal_boringssl/lib" "${TARGET_RESOURCES_LIB_DIR}")
execute_process(COMMAND ${CMAKE_COMMAND} -E copy_directory "${DEPEND_DIR}/sqlcipher_4.4.3_share_universal_boringssl/lib" "${TARGET_RESOURCES_LIB_DIR}")
execute_process(COMMAND ${CMAKE_COMMAND} -E copy_directory "${DEPEND_DIR}/libevent_2.1.10_share_universal/lib" "${TARGET_RESOURCES_LIB_DIR}")
execute_process(COMMAND ${CMAKE_COMMAND} -E copy_directory "${DEPEND_DIR}/libzip_1.11.2_share_universal/lib" "${TARGET_RESOURCES_LIB_DIR}")
execute_process(COMMAND ${CMAKE_COMMAND} -E copy_directory "${DEPEND_DIR}/aosl_1.2.2_release" "${TARGET_RESOURCES_DIR}")
