using System.Runtime.InteropServices;

namespace AgoraChat
{
    public sealed class ChatAPIIOS
    {
        #region DllImport

#if UNITY_IPHONE

        public const string MyLibName = "__Internal";

#else

        public const string MyLibName = "hyphenateCWrapper";

#endif
        [DllImport(MyLibName)]
        internal extern static void Client_HandleMethodCall(string methodName, string jsonString = null, string callbackId = null);

        [DllImport(MyLibName)]
        internal extern static string Client_GetMethodCall(string methodName, string jsonString = null, string callbackId = null);

        [DllImport(MyLibName)]
        internal extern static void ChatManager_HandleMethodCall(string methodName, string jsonString = null, string callbackId = null);

        [DllImport(MyLibName)]
        internal extern static string ChatManager_GetMethodCall(string methodName, string jsonString = null, string callbackId = null);

        [DllImport(MyLibName)]
        internal extern static void Conversation_HandleMethodCall(string methodName, string jsonString = null, string callbackId = null);

        [DllImport(MyLibName)]
        internal extern static string Conversation_GetMethodCall(string methodName, string jsonString = null, string callbackId = null);

        [DllImport(MyLibName)]
        internal extern static void ContactManager_HandleMethodCall(string methodName, string jsonString = null, string callbackId = null);

        [DllImport(MyLibName)]
        internal extern static string ContactManager_GetMethodCall(string methodName, string jsonString = null, string callbackId = null);

        [DllImport(MyLibName)]
        internal extern static void GroupManager_HandleMethodCall(string methodName, string jsonString = null, string callbackId = null);

        [DllImport(MyLibName)]
        internal extern static string GroupManager_GetMethodCall(string methodName, string jsonString = null, string callbackId = null);

        [DllImport(MyLibName)]
        internal extern static void RoomManager_HandleMethodCall(string methodName, string jsonString = null, string callbackId = null);

        [DllImport(MyLibName)]
        internal extern static string RoomManager_GetMethodCall(string methodName, string jsonString = null, string callbackId = null);

        [DllImport(MyLibName)]
        internal extern static void UserInfoManager_MethodCall(string methodName, string jsonString = null, string callbackId = null);

        [DllImport(MyLibName)]
        internal extern static string UserInfoManager_GetMethodCall(string methodName, string jsonString = null, string callbackId = null);

        [DllImport(MyLibName)]
        internal extern static void PushManager_HandleMethodCall(string methodName, string jsonString = null, string callbackId = null);

        [DllImport(MyLibName)]
        internal extern static string PushManager_GetMethodCall(string methodName, string jsonString = null, string callbackId = null);

        [DllImport(MyLibName)]
        internal extern static void PresenceManager_HandleMethodCall(string methodName, string jsonString = null, string callbackId = null);

        [DllImport(MyLibName)]
        internal extern static void ChatThreadManager_HandleMethodCall(string methodName, string jsonString = null, string callbackId = null);

        [DllImport(MyLibName)]
        internal extern static string MessageManager_GetMethodCall(string methodName, string jsonString = null, string callbackId = null);

        #endregion iOS API import
    }
}


