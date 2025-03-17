xcopy /Y ..\Dependency\x64\*.dll runtimes\x64\native\

xcopy /Y ..\..\..\emclient-unity\AgoraChatSDK\CWrapper\Wrapper\Wrapper_Common\wrapper_build\proj\bin\Release\ChatCWrapper.dll runtimes\x64\native\
xcopy /Y ..\..\..\emclient-unity\AgoraChatSDK\CWrapper\Wrapper\Wrapper_Common\wrapper_build\proj\bin\Release\CommonWrapper.dll runtimes\x64\native\
xcopy /Y ..\..\..\emclient-unity\AgoraChatSDK\CWrapper\Wrapper\Wrapper_Common\wrapper_build\proj\bin\Release\SdkWrapper.dll runtimes\x64\native\

xcopy /Y ..\..\..\emclient-unity\AgoraChatSDK\AgoraChat\bin\Release\AgoraChat.dll lib\net48\
xcopy /Y ..\..\..\emclient-unity\AgoraChatSDK\AgoraChat\bin\Release\AgoraChat.dll lib\net452\
xcopy /Y ..\..\..\emclient-unity\AgoraChatSDK\AgoraChat\bin\Release\AgoraChat.dll lib\net462\
xcopy /Y ..\..\..\emclient-unity\AgoraChatSDK\AgoraChat\bin\Release\AgoraChat.dll lib\net472\

Nuget.exe pack agora_chat_sdk.nuspec