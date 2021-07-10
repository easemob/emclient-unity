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
		/** Client Stub **/
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
		internal static extern void ChatManager_SendMessage(IntPtr client, Action onSuccess, OnError onError, IntPtr mto, MessageBodyType type);

		/** ChatManager Stub **/
		[DllImport(MyLibName)]
		internal static extern void ChatManager_AddListener(IntPtr client, OnMessagesReceived onMessagesReceived,
				OnCmdMessagesReceived onCmdMessagesReceived, OnMessagesRead onMessagesRead, OnMessagesDelivered onMessagesDelivered,
				OnMessagesRecalled onMessagesRecalled, OnReadAckForGroupMessageUpdated onReadAckForGroupMessageUpdated, OnGroupMessageRead onGroupMessageRead,
				OnConversationsUpdate onConversationsUpdate, OnConversationRead onConversationRead);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_FetchHistoryMessages(IntPtr client, string conversationId, ConversationType type, string startMessageId, int count, OnSuccessResult onSuccessResult, OnError onError);

		/** GroupManager Stub **/
		[DllImport(MyLibName)]
		internal static extern void GroupManager_AddListener(IntPtr client, OnInvitationReceived onInvitationReceived,
				OnRequestToJoinReceived onRequestToJoinReceived, OnRequestToJoinAccepted onRequestToJoinAccepted, OnRequestToJoinDeclined onRequestToJoinDeclined,
				OnInvitationAccepted onInvitationAccepted, OnInvitationDeclined onInvitationDeclined, OnUserRemoved onUserRemoved,
				OnGroupDestroyed onGroupDestroyed, OnAutoAcceptInvitationFromGroup onAutoAcceptInvitationFromGroup, OnMuteListAdded onMuteListAdded,
				OnMuteListRemoved onMuteListRemoved, OnAdminAdded onAdminAdded, OnAdminRemoved onAdminRemoved, OnOwnerChanged onOwnerChanged,
				OnMemberJoined onMemberJoined, OnMemberExited onMemberExited, OnAnnouncementChanged onAnnouncementChanged, OnSharedFileAdded onSharedFileAdded,
				OnSharedFileDeleted onSharedFileDeleted);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_CreateGroup(IntPtr client, string groupName, GroupOptions options, string desc,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 5)]string[] inviteMembers = null, int size = 0, string inviteReason = null, OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_ChangeGroupName(IntPtr client, string groupId, string groupName, Action onSuccess, OnError onError);


		[DllImport(MyLibName)]
		internal static extern void GroupManager_AddMembers(IntPtr client, string groupId,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 3)] string[] memberArray, int size,
			Action onSuccess, OnError onError);
		#endregion native API import
	}
}
