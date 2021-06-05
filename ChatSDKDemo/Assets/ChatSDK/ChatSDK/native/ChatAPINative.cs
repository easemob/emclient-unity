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
		protected delegate void OnSuccess();
		protected delegate void OnError(int error, string desc);
		protected delegate void OnProgress(int progress);

		[DllImport(MyLibName)]
		public static extern void Client_CreateAccount(IntPtr client, string username, string password);

		[DllImport(MyLibName)]
		public static extern IntPtr Client_InitWithOptions(Options options);

		[DllImport(MyLibName)]
		public static extern void Client_Login(IntPtr client, string username, string pwdOrToken, bool isToken = false);

		[DllImport(MyLibName)]
		public static extern void Client_Logout(IntPtr client, bool unbindDeviceToken);

		[DllImport(MyLibName)]
		public static extern void Client_StartLog(string logFilePath);

		[DllImport(MyLibName)]
		public static extern void Client_StopLog();


#endregion native API import
	}
}
