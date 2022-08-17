Build steps:

1. Make sure the follow directory exist:
	.\lib\net48
	.\lib\net452
	.\lib\net462
	.\lib\net472
	.\runtimes\x64\native
	.\runtimes\x86\native

2. Make sure chatsdk.dll is built with Release mode.
3. Make sure hyphenateCWrapper.dll is built with Release mode.
4. Run bat script: build.bat
5. Check related nuget package is generated in current directory.

Releases:

agora_chat_sdk.1.0.2-beta.nupkg:
--  created based on Unity stable 3.9.0
--  support .Net FrameWork 4.5.2, 4.6.2, 4.7.2 and 4.8. (Note: All chatsdk.dll are compiled in .Net Framework 4.5.2)

agora_chat_sdk.1.0.5.nupkg:
--  created based on Unity stable 3.9.3
--  support .Net FrameWork 4.5.2, 4.6.2, 4.7.2 and 4.8. (Note: All chatsdk.dll are compiled in .Net Framework 4.5.2)
--  support new features: moderation, reaction, threading and translation.
