xcopy /Y ..\Dependency\x86\*.dll runtimes\x86\native\

xcopy /Y ..\..\..\emclient-unity\AgoraChatSDK\CWrapper\Wrapper\Wrapper_Common\wrapper_build\proj\bin\Release\ChatCWrapper.dll runtimes\x86\native\
xcopy /Y ..\..\..\emclient-unity\AgoraChatSDK\CWrapper\Wrapper\Wrapper_Common\wrapper_build\proj\bin\Release\CommonWrapper.dll runtimes\x86\native\
xcopy /Y ..\..\..\emclient-unity\AgoraChatSDK\CWrapper\Wrapper\Wrapper_Common\wrapper_build\proj\bin\Release\SdkWrapper.dll runtimes\x86\native\

xcopy /Y ..\..\..\emclient-unity\AgoraChatSDK\AgoraChat\bin\Release\AgoraChat.dll lib\net48\
xcopy /Y ..\..\..\emclient-unity\AgoraChatSDK\AgoraChat\bin\Release\AgoraChat.dll lib\net452\
xcopy /Y ..\..\..\emclient-unity\AgoraChatSDK\AgoraChat\bin\Release\AgoraChat.dll lib\net462\
xcopy /Y ..\..\..\emclient-unity\AgoraChatSDK\AgoraChat\bin\Release\AgoraChat.dll lib\net472\

Nuget.exe pack chat_sdk.nuspec