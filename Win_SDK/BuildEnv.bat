
rd /s/q FrameworkLib\Assets
echo d|xcopy /Y /S ..\ChatSDKDemo\Assets FrameworkLib\Assets

cd FrameworkLib\Assets

rd /s/q Plugins
rd /s/q Resources
rd /s/q Scenes
rd /s/q Script
del appkey.conf /f /q /a
del token.conf /f /q /a
del *.meta /f /q /s /a
del *Android.cs /f /q /s /a
del *iOS.cs /f /q /s /a

cd ..\..