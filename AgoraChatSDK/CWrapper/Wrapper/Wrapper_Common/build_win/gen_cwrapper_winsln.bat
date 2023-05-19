
@echo off

xcopy cwrapper.gyp ..\..\..\..\..\..\
cd ..\..\..\..\..\..\
call emclient-linux\tools\gyp-master\gyp --depth=. cwrapper.gyp --generator-output=emclient-unity\AgoraChatSDK\CWrapper\Wrapper\Wrapper_Common\build_win\proj -D ENABLE_CALL=0
del cwrapper.gyp
cd emclient-unity\AgoraChatSDK\CWrapper\Wrapper\Wrapper_Common\build_win
echo create project successfully, please find under .\build_win\proj
