xcopy /Y ..\Dependency\*.dll runtimes\x64\native\
xcopy /Y ..\Dependency\*.dll runtimes\x86\native\

xcopy /Y ..\..\..\emclient-unity\Unity_SDK\win_build\build_shared\Release\hyphenateCWrapper.dll runtimes\x64\native\
xcopy /Y ..\..\..\emclient-unity\Unity_SDK\win_build\build_shared\Release_32\hyphenateCWrapper.dll runtimes\x86\native\

xcopy /Y ..\..\Win_SDK\FrameworkLib\bin\Release\chatsdk.dll lib\net48\
xcopy /Y ..\..\Win_SDK\FrameworkLib\bin\Release\chatsdk.dll lib\net452\
xcopy /Y ..\..\Win_SDK\FrameworkLib\bin\Release\chatsdk.dll lib\net462\
xcopy /Y ..\..\Win_SDK\FrameworkLib\bin\Release\chatsdk.dll lib\net472\

Nuget.exe pack agora_chat_sdk.nuspec