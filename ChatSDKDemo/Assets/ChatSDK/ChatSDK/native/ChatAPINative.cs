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

		/** ChatManager Stub **/
		[DllImport(MyLibName)]
		internal static extern void ChatManager_SendMessage(IntPtr client, Action onSuccess, OnError onError, IntPtr mto, MessageBodyType type);
		
		[DllImport(MyLibName)]
		internal static extern void ChatManager_AddListener(IntPtr client, OnMessagesReceived onMessagesReceived,
				OnCmdMessagesReceived onCmdMessagesReceived, OnMessagesRead onMessagesRead, OnMessagesDelivered onMessagesDelivered,
				OnMessagesRecalled onMessagesRecalled, OnReadAckForGroupMessageUpdated onReadAckForGroupMessageUpdated, OnGroupMessageRead onGroupMessageRead,
				OnConversationsUpdate onConversationsUpdate, OnConversationRead onConversationRead);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_FetchHistoryMessages(IntPtr client, string conversationId, ConversationType type, string startMessageId, int count, OnSuccessResult onSuccessResult, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_GetConversationsFromServer(IntPtr client, OnSuccessResult onSuccessResult, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_RemoveConversation(IntPtr client, string conversationId, bool isRemoveMessages);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_DownloadMessageAttachments(IntPtr client, string messageId, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_DownloadMessageThumbnail(IntPtr client, string messageId, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern int ChatManager_ConversationWithType(IntPtr client, string conversationId, ConversationType type, bool createIfNotExist);

		[DllImport(MyLibName)]
		internal static extern int ChatManager_GetUnreadMessageCount(IntPtr client);

		[DllImport(MyLibName)]
		internal static extern int ChatManager_InsertMessages(IntPtr client, 
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.AsAny, SizeParamIndex = 3)] IntPtr[] messageArray,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4, SizeParamIndex = 3)] MessageBodyType[] typeArray,
			int size);

		[DllImport(MyLibName)]
		internal static extern int ChatManager_LoadAllConversationsFromDB(IntPtr client,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.AsAny, SizeParamIndex = 2)] IntPtr[] toArray,
			int size);

		[DllImport(MyLibName)]
		internal static extern int ChatManager_GetMessage(IntPtr client, string messageId,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.AsAny, SizeParamIndex = 3)] IntPtr[] toArray,
			int size);

		[DllImport(MyLibName)]
		internal static extern int ChatManager_MarkAllConversationsAsRead(IntPtr client);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_RecallMessage(IntPtr client, string messageId, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern int ChatManager_ResendMessage(IntPtr client, string messageId,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.AsAny, SizeParamIndex = 3)] IntPtr[] toArray,
			int size, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern int ChatManager_LoadMoreMessages(IntPtr client, 
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.AsAny, SizeParamIndex = 3)] IntPtr[] toArray,
			int size, string keywords, long timestamp, int maxcount, string from, MessageSearchDirection direction);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_SendReadAckForConversation(IntPtr client, string conversationId, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_SendReadAckForMessage(IntPtr client, string messageId, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_UpdateMessage(IntPtr client, Action onSuccess, OnError onError, IntPtr mto, MessageBodyType type);

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
		internal static extern void GroupManager_DestoryGroup(IntPtr client, string groupId, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_AddMembers(IntPtr client, string groupId,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 3)] string[] memberArray, int size,
			Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_RemoveMembers(IntPtr client, string groupId,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 3)] string[] memberArray, int size,
			Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_AddAdmin(IntPtr client, string groupId, string memberId,
				OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_GetGroupWithId(IntPtr client, string groupId, OnSuccessResult onSuccessResult = null, OnError onError = null);

		/** RoomManager Stub **/
		[DllImport(MyLibName)]
		internal static extern void RoomManager_AddListener(IntPtr client, OnChatRoomDestroyed onChatRoomDestroyed, OnMemberJoined onMemberJoined,
				OnMemberExited onMemberExited, OnRemovedFromChatRoom onRemovedFromChatRoom, OnMuteListAdded onMuteListAdded, OnMuteListRemoved onMuteListRemoved,
				OnAdminAdded onAdminAdded, OnAdminRemoved onAdminRemoved, OnOwnerChanged onOwnerChanged, OnAnnouncementChanged onAnnouncementChanged);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_CreateRoom(IntPtr client, string subject, string descriptionsc, string welcomeMsg, int maxUserCount = 300,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 3)] string[] memberArray = null, int size = 0,
			OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_ChangeRoomSubject(IntPtr client, string roomId, string newSubject, OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_AddRoomAdmin(IntPtr client, string roomId, string memberId, OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_RemoveRoomMembers(IntPtr client, string roomId, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 3)] string[] memberArray = null,
			int size = 0, Action onSuccess = null, OnError onError = null);

		/** ContactManager Stub **/
		[DllImport(MyLibName)]
		internal static extern void ContactManager_AddListener(IntPtr client, OnContactAdd onContactAdd,OnContactDeleted onContactDeleted,
			OnContactInvited onContactInvited, OnFriendRequestAccepted onFriendRequestAccepted,OnFriendRequestDeclined onFriendRequestDeclined);

		[DllImport(MyLibName)]
		internal static extern void ContactManager_AddContact(IntPtr client, string username, string reason, Action onSuccess = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void ContactManager_DeleteContact(IntPtr client, string username, bool keepConversation = false, Action onSuccess = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void ContactManager_GetContactsFromServer(IntPtr client, OnSuccessResult onSuccessResult, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void ContactManager_GetContactsFromDB(IntPtr client, OnSuccessResult onSuccessResult, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void ContactManager_AddToBlackList(IntPtr client, string username, bool both, Action onSuccess = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void ContactManager_RemoveFromBlackList(IntPtr client, string username, Action onSuccess = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void ContactManager_GetBlackListFromServer(IntPtr client, OnSuccessResult onSuccessResult, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void ContactManager_AcceptInvitation(IntPtr client, string username, Action onSuccess = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void ContactManager_DeclineInvitation(IntPtr client, string username, Action onSuccess = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void ContactManager_GetSelfIdsOnOtherPlatform(IntPtr client, OnSuccessResult onSuccessResult, OnError onError);

		#endregion native API import
	}
}
