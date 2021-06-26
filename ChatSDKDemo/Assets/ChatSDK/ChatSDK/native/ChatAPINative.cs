using System.Runtime.InteropServices;
using System;

namespace ChatSDK{
	public class ChatAPINative
	{

#region DllImport
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
		public const string MyLibName = "hyphenateCWrapper";
#else

#if UNITY_IPHONE
		public const string MyLibName = "__Internal";
#else
        public const string MyLibName = "hyphenateCWrapper";
#endif
#endif
		[DllImport(MyLibName)]
		internal static extern void Client_CreateAccount(IntPtr client, Action onSuccess, OnError onError, string username, string password);

		[DllImport(MyLibName)]
		internal static extern IntPtr Client_InitWithOptions(Options options, Action onConnect, OnDisconnected onDisconnect, Action onPong);

		[DllImport(MyLibName)]
		internal static extern void Client_Login(IntPtr client, Action onSuccess, OnError onError, string username, string pwdOrToken, bool isToken = false);

		[DllImport(MyLibName)]
		internal static extern void Client_Logout(IntPtr client, Action onSuccess, bool unbindDeviceToken);

		[DllImport(MyLibName)]
		internal static extern void Client_StartLog(string logFilePath);

		[DllImport(MyLibName)]
		internal static extern void Client_StopLog();

		[DllImport(MyLibName)]
		internal static extern void ChatManager_SendMessage(IntPtr client, Action onSuccess, OnError onError, TextMessageTO mto, MessageBodyType type);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_SendMessage(IntPtr client, Action onSuccess, OnError onError, LocationMessageTO mto, MessageBodyType type);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_SendMessage(IntPtr client, Action onSuccess, OnError onError, CmdMessageTO mto, MessageBodyType type);

		#endregion native API import
	}
}
