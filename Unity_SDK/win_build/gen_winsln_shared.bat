xcopy hyphenate_shared_win.gyp ..\..\..\
cd ..\..\..\
call emclient-linux\tools\gyp-master\gyp --depth=. hyphenate_shared_win.gyp --generator-output=emclient-unity\Unity_SDK\win_build\build_shared -D ENABLE_CALL=0
del hyphenate_shared_win.gyp
cd emclient-unity\Unity_SDK\win_build\
