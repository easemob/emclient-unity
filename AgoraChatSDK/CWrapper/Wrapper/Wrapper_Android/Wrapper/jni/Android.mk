LOCAL_PATH := $(call my-dir)
 
include $(CLEAR_VARS)
#LOCAL_MODULE_CLASS := SHARED_LIBRARIES
LOCAL_LDLIBS := -llog
LOCAL_MODULE := CWrapper
LOCAL_C_INCLUDES := ../../../../CWrapper/include
LOCAL_SRC_FILES := \
				../src/common_cwrapper.cpp\
				../src/jni_gloable.cpp\
				../src/jni_methods.cpp
				
include $(BUILD_SHARED_LIBRARY)
