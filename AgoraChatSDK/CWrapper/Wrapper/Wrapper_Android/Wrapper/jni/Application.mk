APP_ABI := armeabi-v7a arm64-v8a x86 x86_64
APP_STL := c++_static
APP_PLATFORM := android-21

# Support for 16KB page size (Android 15+)
APP_LDFLAGS := -Wl,-z,max-page-size=16384
