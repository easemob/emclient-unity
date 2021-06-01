UNAME := $(shell sh -c 'uname 2>/dev/null || echo not')

ifeq ($(UNAME),Darwin)
    COMPILE := xcodebuild -project build/easemob.xcodeproj -configuration Debug -target easemob
    COMPILE_DEPEND :=build/easemob.xcodeproj
endif

COMPILE_DEPTH = ../../../emclient-linux_3.8.1/tools/gyp/gyp --depth=. easemob.gyp --generator-output=./build -D ENABLE_CALL=0 -D RUN_IN_ARM=0 -D USE_SQLCIPHER=0


all: compile

$(COMPILE_DEPEND):easemob.gyp ../../../emclient-linux_3.8.1/tools/gyp/gyp
	 $(COMPILE_DEPTH)

compile: $(COMPILE_DEPEND)
	$(COMPILE)

.PHONY: clean

clean:
	rm -rf build