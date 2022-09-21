using System.Runtime.InteropServices;
using System;
using System.Text;

namespace ChatSDK{

	internal delegate void OnSuccessResultV2(IntPtr header, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] IntPtr[] data, DataType dType, int size, int callbackId);
	internal delegate void OnSuccessResult([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] IntPtr[] data, DataType dType, int size, int callbackId);
	internal delegate void OnErrorV2(int code, string desc, int callbackId);
	internal delegate void OnProgressV2(int progress, int callbackId);
	internal delegate void OnSuccess(int callbackId);


	public sealed class ChatAPINative
	{

#region DllImport
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
		public const string MyLibName = "hyphenateCWrapper";
#else

//#if UNITY_IPHONE
//		public const string MyLibName = "__Internal";
//#else
        public const string MyLibName = "hyphenateCWrapper";
//#endif
#endif

		/** Client Stub **/
		[DllImport(MyLibName)]

		internal static extern void Client_CreateAccount(IntPtr client, int callbackId, OnSuccess onSuccess, OnErrorV2 onError, string username, string password);

		[DllImport(MyLibName)]
		internal static extern IntPtr Client_InitWithOptions(Options options, Action onConnect, OnDisconnected onDisconnect, Action onPong, OnTokenNotificationed onTokenNotify);

		[DllImport(MyLibName)]
		internal static extern void Client_AddMultiDeviceListener(onContactMultiDevicesEvent mdContactEvent, onGroupMultiDevicesEvent mdGroupEvent, undisturbMultiDevicesEvent mdUbEvent, onThreadMultiDevicesEvent mdThreadEvent);

		[DllImport(MyLibName)]
		internal static extern void Client_Login(IntPtr client, int callbackId, OnSuccess onSuccess, OnErrorV2 onError, string username, string pwdOrToken, bool isToken = false);

		[DllImport(MyLibName)]
		internal static extern void Client_Logout(IntPtr client, int callbackId, OnSuccess onSuccess, bool unbindDeviceToken);

		[DllImport(MyLibName)]
		internal static extern void Client_StartLog(string logFilePath);

		[DllImport(MyLibName)]
		internal static extern void Client_StopLog();

		[DllImport(MyLibName)]
		internal static extern void Client_LoginToken(IntPtr client, OnSuccessResult onSuccess);

		[DllImport(MyLibName)]
		internal static extern bool Client_isConnected(IntPtr client);

		[DllImport(MyLibName)]
		internal static extern bool Client_isLoggedIn(IntPtr client);

		[DllImport(MyLibName)]
		internal static extern void Client_ClearResource(IntPtr client);

		[DllImport(MyLibName)]
		internal static extern void Client_LoginWithAgoraToken(IntPtr client, int callbackId, OnSuccess onSuccess, OnErrorV2 onError, string username, string agoraToken);

		[DllImport(MyLibName)]
		internal static extern void Client_RenewAgoraToken(IntPtr client, string agoraToken);

		[DllImport(MyLibName)]
		internal static extern void Client_AutoLogin(IntPtr client, int callbackId, OnSuccessResult onSuccess, OnErrorV2 onError);

		/** ChatManager Stub **/
		[DllImport(MyLibName)]
		internal static extern void ChatManager_SendMessage(IntPtr client, int callbackId, OnSuccess onSuccess, OnErrorV2 onError, OnProgressV2 onProgress ,IntPtr mto, MessageBodyType type);
		
		[DllImport(MyLibName)]
		internal static extern void ChatManager_AddListener(IntPtr client, OnMessagesReceived onMessagesReceived,
				OnCmdMessagesReceived onCmdMessagesReceived, OnMessagesRead onMessagesRead, OnMessagesDelivered onMessagesDelivered,
				OnMessagesRecalled onMessagesRecalled, OnReadAckForGroupMessageUpdated onReadAckForGroupMessageUpdated, OnGroupMessageRead onGroupMessageRead,
				OnConversationsUpdate onConversationsUpdate, OnConversationRead onConversationRead, MessageReactionDidChange messageReactionDidChange);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_FetchHistoryMessages(IntPtr client, int callbackId, string conversationId, 
			ConversationType type, string startMessageId, int count, MessageSearchDirection direction, OnSuccessResultV2 onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_GetConversationsFromServer(IntPtr client, int callbackId, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_RemoveConversation(IntPtr client, string conversationId, bool isRemoveMessages, bool isThread);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_DownloadMessageAttachments(IntPtr client, int callbackId, string messageId, OnSuccess onSuccess, OnErrorV2 onError, OnProgressV2 onProgress);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_DownloadMessageThumbnail(IntPtr client, int callbackId, string messageId, OnSuccess onSuccess, OnErrorV2 onError, OnProgressV2 onProgress);

		[DllImport(MyLibName)]
		internal static extern bool ChatManager_ConversationWithType(IntPtr client, string conversationId, ConversationType type, bool createIfNotExist, bool isThread);

		[DllImport(MyLibName)]
		internal static extern int ChatManager_GetUnreadMessageCount(IntPtr client);

		[DllImport(MyLibName)]
		internal static extern bool ChatManager_InsertMessages(IntPtr client, 
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.AsAny, SizeParamIndex = 3)] IntPtr[] messageArray,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4, SizeParamIndex = 3)] MessageBodyType[] typeArray,
			int size);

		[DllImport(MyLibName)]
		internal static extern int ChatManager_LoadAllConversationsFromDB(IntPtr client, OnSuccessResult onSuccessResult);

		[DllImport(MyLibName)]
		internal static extern int ChatManager_GetMessage(IntPtr client, string messageId,OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern bool ChatManager_MarkAllConversationsAsRead(IntPtr client);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_RecallMessage(IntPtr client, int callbackId, string messageId, OnSuccess onSuccess, OnErrorV2 onError, OnProgressV2 onProgress);

		[DllImport(MyLibName)]
		internal static extern int ChatManager_ResendMessage(IntPtr client, int callbackId, string messageId, OnSuccessResult onSuccessResult, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern int ChatManager_LoadMoreMessages(IntPtr client,
			OnSuccessResult onSuccessResult, string keywords, long timestamp, int maxcount, string from, MessageSearchDirection direction);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_SendReadAckForConversation(IntPtr client, int callbackId, string conversationId, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_SendReadAckForMessage(IntPtr client, int callbackId, string messageId, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_SendReadAckForGroupMessage(IntPtr client, int callbackId, string messageId, [In, MarshalAs(UnmanagedType.LPTStr)] string ackContent, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern bool ChatManager_UpdateMessage(IntPtr client, IntPtr mto, MessageBodyType type);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_RemoveMessagesBeforeTimestamp(IntPtr client, int callbackId, long timestamp, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_DeleteConversationFromServer(IntPtr client, int callbackId, string conversationId, ConversationType conversationType, bool isDeleteServerMessages, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_FetchSupportLanguages(IntPtr client, int callbackId, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_TranslateMessage(IntPtr client, int callbackId, IntPtr mto, MessageBodyType type, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 5)] string[] targetLanguages, int size, OnSuccessResult onSuccessResult, OnErrorV2 onError);
		
		[DllImport(MyLibName)]
		internal static extern void ChatManager_FetchGroupReadAcks(IntPtr client, int callbackId, string messageId,
			string groupId, int pageSize, string startAckId, OnSuccessResultV2 onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_ReportMessage(IntPtr client, int callbackId, string messageId,
			[In, MarshalAs(UnmanagedType.LPTStr)] string tag, [In, MarshalAs(UnmanagedType.LPTStr)] string reason, 
			OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_AddReaction(IntPtr client, int callbackId, string messageId,
			[In, MarshalAs(UnmanagedType.LPTStr)] string reaction, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_RemoveReaction(IntPtr client, int callbackId, string messageId,
			[In, MarshalAs(UnmanagedType.LPTStr)] string reaction, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_GetReactionList(IntPtr client, int callbackId, string messageIdList,
			string messageType, string groupId, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_GetReactionDetail(IntPtr client, int callbackId, string messageId, 
			[In, MarshalAs(UnmanagedType.LPTStr)] string reaction, string cursor, long pageSize, OnSuccessResultV2 onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern int ChatManager_GetGroupAckCount(string messageId);

		[DllImport(MyLibName)]
		internal static extern bool ChatManager_GetHasDeliverAck(string messageId);

		[DllImport(MyLibName)]
		internal static extern bool ChatManager_GetHasReadAck(string messageId);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_GetReactionListForMsg(string messageId, [Out,MarshalAs(UnmanagedType.LPTStr)]StringBuilder buf, int len);

		[DllImport(MyLibName)]
		internal static extern void ChatManager_GetChatThreadForMsg(string messageId, [Out, MarshalAs(UnmanagedType.LPTStr)] StringBuilder buf, int len);

		/** GroupManager Stub **/
		[DllImport(MyLibName)]
		internal static extern void GroupManager_AddListener(IntPtr client, OnInvitationReceived onInvitationReceived,
				OnRequestToJoinReceived onRequestToJoinReceived, OnRequestToJoinAccepted onRequestToJoinAccepted, OnRequestToJoinDeclined onRequestToJoinDeclined,
				OnInvitationAccepted onInvitationAccepted, OnInvitationDeclined onInvitationDeclined, OnUserRemoved onUserRemoved,
				OnGroupDestroyed onGroupDestroyed, OnAutoAcceptInvitationFromGroup onAutoAcceptInvitationFromGroup, OnMuteListAdded onMuteListAdded,
				OnMuteListRemoved onMuteListRemoved, OnAdminAdded onAdminAdded, OnAdminRemoved onAdminRemoved, OnOwnerChanged onOwnerChanged,
				OnMemberJoined onMemberJoined, OnMemberExited onMemberExited, OnAnnouncementChanged onAnnouncementChanged, OnSharedFileAdded onSharedFileAdded,
				OnSharedFileDeleted onSharedFileDeleted, OnAddWhiteListMembersFromGroup onAddWhiteListMembersFromGroup,
				OnRemoveWhiteListMembersFromGroup onRemoveWhiteListMembersFromGroup, OnAllMemberMuteChangedFromGroup onAllMemberMuteChangedFromGroup);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_CreateGroup(IntPtr client, int callbackId, 
			[In, MarshalAs(UnmanagedType.LPTStr)] string groupName, GroupOptions options, 
			[In, MarshalAs(UnmanagedType.LPTStr)] string desc,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 6)]string[] inviteMembers = null, 
			int size = 0, [In, MarshalAs(UnmanagedType.LPTStr)] string inviteReason = null, 
			OnSuccessResult onSuccessResult = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_ChangeGroupName(IntPtr client, int callbackId, string groupId, [In, MarshalAs(UnmanagedType.LPTStr)] string groupName, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_DestoryGroup(IntPtr client, int callbackId, string groupId, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_AddMembers(IntPtr client, int callbackId, string groupId,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 4)] string[] memberArray, int size,
			OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_RemoveMembers(IntPtr client, int callbackId, string groupId,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 4)] string[] memberArray, int size,
			OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_AddAdmin(IntPtr client, int callbackId, string groupId, string memberId,
				OnSuccessResult onSuccessResult = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_GetGroupWithId(IntPtr client, string groupId, OnSuccessResult onSuccessResult = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_AcceptInvitationFromGroup(IntPtr client, int callbackId, string groupId, string inviter, OnSuccessResult onSuccessResult = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_AcceptJoinGroupApplication(IntPtr client, int callbackId, string groupId, string username, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_AddWhiteListMembers(IntPtr client, int callbackId, string groupId,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 4)] string[] memberArray, int size,
			OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_BlockGroupMessage(IntPtr client, int callbackId, string groupId, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_BlockGroupMembers(IntPtr client, int callbackId, string groupId,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 4)] string[] memberArray, int size,
			OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_ChangeGroupDescription(IntPtr client, int callbackId, string groupId, [In, MarshalAs(UnmanagedType.LPTStr)] string desc, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_TransferGroupOwner(IntPtr client, int callbackId, string groupId, string newOwner, OnSuccessResult onSuccessResult = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_FetchIsMemberInWhiteList(IntPtr client, int callbackId, string groupId, OnSuccessResult onSuccessResult = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_DeclineInvitationFromGroup(IntPtr client, int callbackId, string groupId, string username, [In, MarshalAs(UnmanagedType.LPTStr)] string reason, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_DeclineJoinGroupApplication(IntPtr client, int callbackId, string groupId, string username, [In, MarshalAs(UnmanagedType.LPTStr)] string reason, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_DownloadGroupSharedFile(IntPtr client, int callbackId, string groupId, string filePath, string fileId, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_FetchGroupAnnouncement(IntPtr client, int callbackId, string groupId, OnSuccessResult onSuccessResult = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_FetchGroupBans(IntPtr client, int callbackId, string groupId, int pageNum, int pageSize, OnSuccessResult onSuccessResult = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_FetchGroupSharedFiles(IntPtr client, int callbackId, string groupId, int pageNum, int pageSize, OnSuccessResult onSuccessResult = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_FetchGroupMembers(IntPtr client, int callbackId, string groupId, int pageSize, string cursor, OnSuccessResultV2 onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_FetchGroupMutes(IntPtr client, int callbackId, string groupId, int pageNum, int pageSize, OnSuccessResult onSuccessResult = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_FetchGroupSpecification(IntPtr client, int callbackId, string groupId, OnSuccessResult onSuccessResult = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_GetGroupsWithoutNotice(IntPtr client, OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_FetchGroupWhiteList(IntPtr client, int callbackId, string groupId, OnSuccessResult onSuccessResult = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_LoadAllMyGroupsFromDB(IntPtr client, OnSuccessResult onSuccessResult);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_FetchAllMyGroupsWithPage(IntPtr client, int callbackId, int pageNum, int pageSize, OnSuccessResult onSuccessResult = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_FetchPublicGroupsWithCursor(IntPtr client, int callbackId, int pageSize, string cursor, OnSuccessResultV2 onSuccessResult = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_JoinPublicGroup(IntPtr client, int callbackId, string groupId, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_LeaveGroup(IntPtr client, int callbackId, string groupId, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_MuteAllGroupMembers(IntPtr client, int callbackId, string groupId, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_MuteGroupMembers(IntPtr client, int callbackId, string groupId,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 4)] string[] memberArray, int size, int muteDuration,
			OnSuccessResult onSuccessResult = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_RemoveGroupAdmin(IntPtr client, int callbackId, string groupId, string admin, OnSuccessResult onSuccessResult = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_DeleteGroupSharedFile(IntPtr client, int callbackId, string groupId, string fileId, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_RemoveWhiteListMembers(IntPtr client, int callbackId, string groupId,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 4)] string[] memberArray, int size,
			OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_ApplyJoinPublicGroup(IntPtr client, int callbackId, string groupId, [In, MarshalAs(UnmanagedType.LPTStr)] string nickName, [In, MarshalAs(UnmanagedType.LPTStr)] string message, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_UnblockGroupMessage(IntPtr client, int callbackId, string groupId, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_UnblockGroupMembers(IntPtr client, int callbackId, string groupId,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 4)] string[] memberArray, int size,
			OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_UnMuteAllMembers(IntPtr client, int callbackId, string groupId, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_UnmuteGroupMembers(IntPtr client, int callbackId, string groupId,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 4)] string[] memberArray, int size,
			OnSuccessResult onSuccessResult = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_UpdateGroupAnnouncement(IntPtr client, int callbackId, string groupId, [In, MarshalAs(UnmanagedType.LPTStr)] string newAnnouncement, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_ChangeGroupExtension(IntPtr client, int callbackId, string groupId, string newExtension, OnSuccessResult onSuccessResult = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_UploadGroupSharedFile(IntPtr client, int callbackId, string groupId, string filePath, OnSuccess onSuccess, OnErrorV2 onError, OnProgressV2 onProgress = null);

		/** RoomManager Stub **/
		[DllImport(MyLibName)]
		internal static extern void RoomManager_AddListener(IntPtr client, OnChatRoomDestroyed onChatRoomDestroyed, OnMemberJoined onMemberJoined,
				OnMemberExitedFromRoom onMemberExited, OnRemovedFromChatRoom onRemovedFromChatRoom, OnMuteListAdded onMuteListAdded, OnMuteListRemoved onMuteListRemoved,
				OnAdminAdded onAdminAdded, OnAdminRemoved onAdminRemoved, OnOwnerChanged onOwnerChanged, OnAnnouncementChanged onAnnouncementChanged,
				OnChatroomAttributesChanged onChatroomAttributesChanged, OnChatroomAttributesRemoved onChatroomAttributesRemoved);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_AddRoomAdmin(IntPtr client, int callbackId, string roomId, string memberId, OnSuccessResult onSuccessResult = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_BlockChatroomMembers(IntPtr client, int callbackId, string roomId, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 4)] string[] memberArray = null,
			int size = 0, OnSuccessResult onSuccessResult = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_CreateRoom(IntPtr client, int callbackId, 
			[In, MarshalAs(UnmanagedType.LPTStr)] string subject, 
			[In, MarshalAs(UnmanagedType.LPTStr)] string descriptionsc, 
			[In, MarshalAs(UnmanagedType.LPTStr)] string welcomeMsg, int maxUserCount = 300,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 7)] string[] memberArray = null, 
			int size = 0,
			OnSuccessResult onSuccessResult = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_ChangeRoomSubject(IntPtr client, int callbackId, string roomId, [In, MarshalAs(UnmanagedType.LPTStr)] string newSubject, OnSuccessResult onSuccessResult = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_RemoveRoomMembers(IntPtr client, int callbackId, string roomId, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 4)] string[] memberArray = null,
			int size = 0, OnSuccess onSuccess = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_TransferChatroomOwner(IntPtr client, int callbackId, string roomId, string newOwner, OnSuccessResult onSuccessResult = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_ChangeChatroomDescription(IntPtr client, int callbackId, string roomId, [In, MarshalAs(UnmanagedType.LPTStr)] string newDescription, OnSuccessResult onSuccessResult = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_DestroyChatroom(IntPtr client, int callbackId, string roomId, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_FetchChatroomsWithPage(IntPtr client, int callbackId, int pageNum, int pageSize, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_FetchChatroomAnnouncement(IntPtr client, int callbackId, string roomId, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_FetchChatroomBans(IntPtr client, int callbackId, string roomId, int pageNum, int pageSize, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_FetchChatroomSpecification(IntPtr client, int callbackId, string roomId, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_FetchChatroomMembers(IntPtr client, int callbackId, string roomId, string cursor, int pageSize, OnSuccessResultV2 onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_FetchChatroomMutes(IntPtr client, int callbackId, string roomId, int pageNum, int pageSize, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_JoinedChatroomById(IntPtr client, string roomId, OnSuccessResult onSuccessResult, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_JoinChatroom(IntPtr client, int callbackId, string roomId, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_LeaveChatroom(IntPtr client, int callbackId, string roomId, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_MuteChatroomMembers(IntPtr client, int callbackId, string roomId, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 4)] string[] memberArray,
			int size, int muteDuration, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_AddWhiteListMembers(IntPtr client, int callbackId, string roomId, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 4)] string[] memberArray,
			int size, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_RemoveWhiteListMembers(IntPtr client, int callbackId, string roomId, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 4)] string[] memberArray,
			int size, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_RemoveChatroomAdmin(IntPtr client, int callbackId, string roomId, string adminId, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_UnblockChatroomMembers(IntPtr client, int callbackId, string roomId, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 4)] string[] memberArray,
			int size, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_UnmuteChatroomMembers(IntPtr client, int callbackId, string roomId, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 4)] string[] memberArray,
			int size, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_UpdateChatroomAnnouncement(IntPtr client, int callbackId, string roomId, [In, MarshalAs(UnmanagedType.LPTStr)] string newAnnouncement, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_MuteAllChatroomMembers(IntPtr client, int callbackId, string roomId, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_UnMuteAllChatroomMembers(IntPtr client, int callbackId, string roomId, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_AddChatRoomMetaData(IntPtr client, int callbackId, string roomId, [In, MarshalAs(UnmanagedType.LPTStr)] string extJson, bool forced, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_FetchChatRoomMetaFromSever(IntPtr client, int callbackId, string roomId, [In, MarshalAs(UnmanagedType.LPTStr)] string keys, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_RemoveChatRoomMetaFromSever(IntPtr client, int callbackId, string roomId, [In, MarshalAs(UnmanagedType.LPTStr)] string keys, bool forced, OnSuccessResult onSuccessResult, OnErrorV2 onError);


		/** ContactManager Stub **/
		[DllImport(MyLibName)]
		internal static extern void ContactManager_AddListener(IntPtr client, OnContactAdd onContactAdd,OnContactDeleted onContactDeleted,
			OnContactInvited onContactInvited, OnFriendRequestAccepted onFriendRequestAccepted,OnFriendRequestDeclined onFriendRequestDeclined);

		[DllImport(MyLibName)]
		internal static extern void ContactManager_AddContact(IntPtr client, int callbackId, string username, string reason, OnSuccess onSuccess = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void ContactManager_DeleteContact(IntPtr client, int callbackId, string username, bool keepConversation = false, OnSuccess onSuccess = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void ContactManager_GetContactsFromServer(IntPtr client, int callbackId, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ContactManager_GetContactsFromDB(IntPtr client, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ContactManager_AddToBlackList(IntPtr client, int callbackId, string username, bool both, OnSuccess onSuccess = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void ContactManager_RemoveFromBlackList(IntPtr client, int callbackId, string username, OnSuccess onSuccess = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void ContactManager_GetBlackListFromServer(IntPtr client, int callbackId, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ContactManager_AcceptInvitation(IntPtr client, int callbackId, string username, OnSuccess onSuccess = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void ContactManager_DeclineInvitation(IntPtr client, int callbackId, string username, OnSuccess onSuccess = null, OnErrorV2 onError = null);

		[DllImport(MyLibName)]
		internal static extern void ContactManager_GetSelfIdsOnOtherPlatform(IntPtr client, int callbackId, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		/** ConversationManager Stub **/
		[DllImport(MyLibName)]
		internal static extern bool ConversationManager_AppendMessage(IntPtr client, string conversationId, ConversationType converationType, IntPtr mto, MessageBodyType type);

		[DllImport(MyLibName)]
		internal static extern bool ConversationManager_ClearAllMessages(IntPtr client, string conversationId, ConversationType converationType);

		[DllImport(MyLibName)]
		internal static extern bool ConversationManager_RemoveMessage(IntPtr client, string conversationId, ConversationType converationType, string messageId);

		[DllImport(MyLibName)]
		internal static extern bool ConversationManager_ExtField(IntPtr client, string conversationId, ConversationType converationType, OnSuccessResult onSuccessResult);

		[DllImport(MyLibName)]
		internal static extern bool ConversationManager_InsertMessage(IntPtr client, string conversationId, ConversationType converationType, IntPtr mto, MessageBodyType type);

		[DllImport(MyLibName)]
		internal static extern void ConversationManager_LatestMessage(IntPtr client, string conversationId, ConversationType converationType, OnSuccessResult onSuccessResult);

		[DllImport(MyLibName)]
		internal static extern void ConversationManager_LatestMessageFromOthers(IntPtr client, string conversationId, ConversationType converationType, OnSuccessResult onSuccessResult);

		[DllImport(MyLibName)]
		internal static extern void ConversationManager_LoadMessage(IntPtr client, string conversationId, ConversationType converationType, string messageId, OnSuccessResult onSuccessResult);

		[DllImport(MyLibName)]
		internal static extern void ConversationManager_LoadMessages(IntPtr client, int callbackId, string conversationId, ConversationType converationType,
			string startMessageId, int count,MessageSearchDirection direction, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ConversationManager_LoadMessagesWithKeyword(IntPtr client, int callbackId, string conversationId, ConversationType converationType,
			string keywords, string sender, long timestamp, int count, MessageSearchDirection direction, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ConversationManager_LoadMessagesWithMsgType(IntPtr client, int callbackId, string conversationId, ConversationType converationType,
			MessageBodyType bodyType, long timestamp, int count, string sender, MessageSearchDirection direction, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ConversationManager_LoadMessagesWithTime(IntPtr client, int callbackId, string conversationId, ConversationType converationType,
			long startTimeStamp, long endTimeStamp, int count, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ConversationManager_MarkAllMessagesAsRead(IntPtr client, string conversationId, ConversationType converationType);

		[DllImport(MyLibName)]
		internal static extern void ConversationManager_MarkMessageAsRead(IntPtr client, string conversationId, ConversationType converationType, string messageId);

		[DllImport(MyLibName)]
		internal static extern void ConversationManager_SetExtField(IntPtr client, string conversationId, ConversationType converationType, string ext);

		[DllImport(MyLibName)]
		internal static extern int ConversationManager_UnreadMessagesCount(IntPtr client, string conversationId, ConversationType converationType);

		[DllImport(MyLibName)]
		internal static extern int ConversationManager_MessagesCount(IntPtr client, string conversationId, ConversationType converationType);

		[DllImport(MyLibName)]
		internal static extern bool ConversationManager_UpdateMessage(IntPtr client, string conversationId, ConversationType converationType, IntPtr mto, MessageBodyType type);

		[DllImport(MyLibName)]
		internal static extern bool ConversationManager_IsThread(IntPtr client, string conversationId, ConversationType converationType);

		//PushManager
		[DllImport(MyLibName)]
		internal static extern void PushManager_GetIgnoredGroupIds(IntPtr client, OnSuccessResult onSuccessResult);

		[DllImport(MyLibName)]
		internal static extern void PushManager_GetPushConfig(IntPtr client, OnSuccessResult onSuccessResult);

		[DllImport(MyLibName)]
		internal static extern void PushManager_GetUserConfigsFromServer(IntPtr client, int callbackId, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void PushManager_IgnoreGroupPush(IntPtr client, int callbackId, string groupId, bool noDisturb, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void PushManager_UpdatePushNoDisturbing(IntPtr client, int callbackId, bool noDisturb, int startTime, int endTime, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void PushManager_UpdatePushDisplayStyle(IntPtr client, int callbackId, PushStyle pushStyle, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void PushManager_UpdateFCMPushToken(IntPtr client, int callbackId, string token, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void PushManager_UpdateHMSPushToken(IntPtr client, int callbackId, string token, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void PushManager_UpdatePushNickName(IntPtr client, int callbackId, [In, MarshalAs(UnmanagedType.LPTStr)] string nickname, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void PushManager_ReportPushAction(IntPtr client, int callbackId, string parameters, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void PushManager_SetSilentModeForAll(IntPtr client, int callbackId, string parameters, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void PushManager_GetSilentModeForAll(IntPtr client, int callbackId, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void PushManager_SetSilentModeForConversation(IntPtr client, int callbackId, string convId, ConversationType converationType, string parameters, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void PushManager_GetSilentModeForConversation(IntPtr client, int callbackId, string convId, ConversationType converationType, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void PushManager_GetSilentModeForConversations(IntPtr client, int callbackId, string parameters, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void PushManager_SetPreferredNotificationLanguage(IntPtr client, int callbackId, string parameters, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void PushManager_GetPreferredNotificationLanguage(IntPtr client, int callbackId, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		//UserInfoManager
		[DllImport(MyLibName)]
		internal static extern void UserInfoManager_UpdateOwnInfo(IntPtr client, int callbackId, IntPtr userInfo, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void UserInfoManager_UpdateOwnInfoByAttribute(IntPtr client, int callbackId, int userInfoType, [In, MarshalAs(UnmanagedType.LPTStr)] string value, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void UserInfoManager_FetchUserInfoByUserId(IntPtr client, int callbackId, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 3)] string[] users,
			int size, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void UserInfoManager_FetchUserInfoByAttribute(IntPtr client, int callbackId, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 3)] string[] users,
			int userSize, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 5)] int[] userinfoTypes,
			int typeSize, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		//PresenceManager
		[DllImport(MyLibName)]
		internal static extern void PresenceManager_AddListener(IntPtr client, OnPresenceUpdated onPresenceUpdated);

		[DllImport(MyLibName)]
		internal static extern void PresenceManager_PublishPresence(IntPtr client, int callbackId, int presenceStatus, [In, MarshalAs(UnmanagedType.LPTStr)] string ext, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void PresenceManager_SubscribePresences(IntPtr client, int callbackId,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 3)] string[] members, int size, long expiry, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void PresenceManager_UnsubscribePresences(IntPtr client, int callbackId,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 3)] string[] members, int size, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void PresenceManager_FetchSubscribedMembers(IntPtr client, int callbackId, int pageNum, int pageSize, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void PresenceManager_FetchPresenceStatus(IntPtr client, int callbackId,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 3)] string[] members, int size, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		//ThreadManager

		[DllImport(MyLibName)]
		internal static extern void ThreadManager_AddListener(IntPtr client, OnChatThreadCreate onChatThreadCreate,
		OnChatThreadUpdate onChatThreadUpdate, OnChatThreadDestroy onChatThreadDestroy, OnUserKickOutOfChatThread nnUserKickOutOfChatThread);

		[DllImport(MyLibName)]
		internal static extern void ThreadManager_GetThreadWithThreadId(IntPtr client, int callbackId, string threadId, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ThreadManager_CreateThread(IntPtr client, int callbackId, [In, MarshalAs(UnmanagedType.LPTStr)] string threadName, string msgId, string groupId, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ThreadManager_JoinThread(IntPtr client, int callbackId, string threadId, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ThreadManager_LeaveThread(IntPtr client, int callbackId, string threadId, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ThreadManager_DestroyThread(IntPtr client, int callbackId, string threadId, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ThreadManager_RemoveThreadMember(IntPtr client, int callbackId, string threadId, string username, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ThreadManager_ChangeThreadSubject(IntPtr client, int callbackId, string threadId, [In, MarshalAs(UnmanagedType.LPTStr)] string newSubject, OnSuccess onSuccess, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ThreadManager_FetchThreadMembers(IntPtr client, int callbackId, string threadId, string cursor, int pageSize, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ThreadManager_FetchThreadListOfGroup(IntPtr client, int callbackId, string cursor, int pageSize, string groupId, bool joined, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ThreadManager_FetchMineJoinedThreadList(IntPtr client, int callbackId, string cursor, int pageSize, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ThreadManager_GetThreadDetail(IntPtr client, int callbackId, string threadId, OnSuccessResult onSuccessResult, OnErrorV2 onError);

		[DllImport(MyLibName)]
		internal static extern void ThreadManager_GetLastMessageAccordingThreads(IntPtr client, int callbackId, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 3)] string[] threadIds, int size, OnSuccessResult onSuccessResult, OnErrorV2 onError);
		#endregion native API import
	}
}
