LOCAL_PATH := $(call my-dir)
 
include $(CLEAR_VARS)
#LOCAL_MODULE_CLASS := SHARED_LIBRARIES
LOCAL_LDLIBS := -llog
LOCAL_MODULE := ChatCWrapper
LOCAL_C_INCLUDES := ../../../../CWrapper/include

# Support for 16KB page size (Android 15+)
LOCAL_LDFLAGS := -Wl,-z,max-page-size=16384

LOCAL_SRC_FILES := \
			../src/common_cwrapper.cpp\
			../src/jni_gloable.cpp\
			../src/jni_methods.cpp\
			../../../../CWrapper/src/CWrapper.cpp\

include $(BUILD_SHARED_LIBRARY)
