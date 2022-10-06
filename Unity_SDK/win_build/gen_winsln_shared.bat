@echo off

set xtype=%1%

if "%xtype%"=="" (

	echo please input build type: x64 or x86
	
) else (

	if "%xtype%"=="x86" (
	
		xcopy hyphenate_shared_win.gyp ..\..\..\
		cd ..\..\..\
		call emclient-linux\tools\gyp-master\gyp --depth=. hyphenate_shared_win.gyp --generator-output=emclient-unity\Unity_SDK\win_build\build_shared -D ENABLE_CALL=0 -D is_x64=0
		del hyphenate_shared_win.gyp
		cd emclient-unity\Unity_SDK\win_build\
		echo create x86 project successfully, please find under .\build_shared
	
	) else if "%xtype%"=="x64" (
	
		xcopy hyphenate_shared_win.gyp ..\..\..\
		cd ..\..\..\
		call emclient-linux\tools\gyp-master\gyp --depth=. hyphenate_shared_win.gyp --generator-output=emclient-unity\Unity_SDK\win_build\build_shared -D ENABLE_CALL=0 -D is_x64=1
		del hyphenate_shared_win.gyp
		cd emclient-unity\Unity_SDK\win_build\
		echo create x64 project successfully, please find under .\build_shared
	
	) else (
		echo please input build type: x64 or x86
	)
)

