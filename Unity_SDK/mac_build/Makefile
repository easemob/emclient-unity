UNAME := $(shell sh -c 'uname 2>/dev/null || echo not')

ifeq ($(UNAME),Darwin)
    COMPILE := xcodebuild -project build/easemob.xcodeproj -configuration Debug -target easemob
    COMPILE_DEPEND :=build/easemob.xcodeproj
endif

<<<<<<< HEAD
COMPILE_DEPTH = ../../../emclient-linux/tools/gyp/gyp --depth=. easemob.gyp --generator-output=./build -D ENABLE_CALL=0 -D RUN_IN_ARM=0 -D USE_SQLCIPHER=0
=======
COMPILE_DEPTH = ../../../../easemob/emclient-linux/tools/gyp/gyp --depth=. easemob.gyp --generator-output=./build -D ENABLE_CALL=0 -D RUN_IN_ARM=0 -D USE_SQLCIPHER=0
>>>>>>> bingo/c_wrapper_bingo


all: compile

<<<<<<< HEAD
$(COMPILE_DEPEND):easemob.gyp ../../../emclient-linux/tools/gyp/gyp
=======
$(COMPILE_DEPEND):easemob.gyp ../../../../easemob/emclient-linux/tools/gyp/gyp
>>>>>>> bingo/c_wrapper_bingo
	 $(COMPILE_DEPTH)

compile: $(COMPILE_DEPEND)
	$(COMPILE)

.PHONY: clean

clean:
	rm -rf build
