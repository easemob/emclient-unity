xcopy /Y ..\Dependency\x86\*.dll runtimes\x86\native\

xcopy /Y ..\..\..\emclient-unity\AgoraChatSDK\CWrapper\Wrapper\Wrapper_Common\build_win\proj\Release_32\ChatCWrapper.dll runtimes\x86\native\
xcopy /Y ..\..\..\emclient-unity\AgoraChatSDK\CWrapper\Wrapper\Wrapper_Common\build_win\proj\Release_32\CommonWrapper.dll runtimes\x86\native\
xcopy /Y ..\..\..\emclient-unity\AgoraChatSDK\CWrapper\Wrapper\Wrapper_Common\build_win\proj\Release_32\SdkWrapper.dll runtimes\x86\native\

xcopy /Y ..\..\..\emclient-unity\AgoraChatSDK\AgoraChat\bin\Release\AgoraChat.dll lib\net48\
xcopy /Y ..\..\..\emclient-unity\AgoraChatSDK\AgoraChat\bin\Release\AgoraChat.dll lib\net452\
xcopy /Y ..\..\..\emclient-unity\AgoraChatSDK\AgoraChat\bin\Release\AgoraChat.dll lib\net462\
xcopy /Y ..\..\..\emclient-unity\AgoraChatSDK\AgoraChat\bin\Release\AgoraChat.dll lib\net472\

Nuget.exe pack agora_chat_sdk_86.nuspec