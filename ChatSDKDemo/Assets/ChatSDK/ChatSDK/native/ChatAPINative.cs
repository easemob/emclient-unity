using System.Runtime.InteropServices;
using System;

namespace ChatSDK{
	public class ChatAPINative
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
		internal static extern bool ChatManager_ConversationWithType(IntPtr client, string conversationId, ConversationType type, bool createIfNotExist);

		[DllImport(MyLibName)]
		internal static extern int ChatManager_GetUnreadMessageCount(IntPtr client);

		[DllImport(MyLibName)]
		internal static extern bool ChatManager_InsertMessages(IntPtr client, 
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.AsAny, SizeParamIndex = 3)] IntPtr[] messageArray,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4, SizeParamIndex = 3)] MessageBodyType[] typeArray,
			int size);

		[DllImport(MyLibName)]
		internal static extern int ChatManager_LoadAllConversationsFromDB(IntPtr client,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.AsAny, SizeParamIndex = 2)] IntPtr[] toArray,
			int size);

		[DllImport(MyLibName)]
		internal static extern int ChatManager_ReleaseConversationList(
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.AsAny, SizeParamIndex = 1)] IntPtr[] toArray,
			int size);

		[DllImport(MyLibName)]
		internal static extern int ChatManager_GetMessage(IntPtr client, string messageId,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.AsAny, SizeParamIndex = 3)] IntPtr[] toArray,
			int size);

		[DllImport(MyLibName)]
		internal static extern int ChatManager_ReleaseMessageList(
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.AsAny, SizeParamIndex = 1)] IntPtr[] toArray,
			int size);

		[DllImport(MyLibName)]
		internal static extern bool ChatManager_MarkAllConversationsAsRead(IntPtr client);

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
		internal static extern bool ChatManager_UpdateMessage(IntPtr client, IntPtr mto, MessageBodyType type);

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
		internal static extern void GroupManager_GetGroupWithId(IntPtr client, string groupId,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 3)] IntPtr[] memberArray, int size);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_AcceptInvitationFromGroup(IntPtr client, string groupId, string inviter, OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_AcceptJoinGroupApplication(IntPtr client, string groupId, string username, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_AddWhiteListMembers(IntPtr client, string groupId,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 3)] string[] memberArray, int size,
			Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_BlockGroupMessage(IntPtr client, string groupId, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_BlockGroupMembers(IntPtr client, string groupId,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 3)] string[] memberArray, int size,
			Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_ChangeGroupDescription(IntPtr client, string groupId, string desc, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_TransferGroupOwner(IntPtr client, string groupId, string newOwner,
			OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_FetchIsMemberInWhiteList(IntPtr client, string groupId, 
			OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_DeclineInvitationFromGroup(IntPtr client, string groupId, string username, string reason, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_DeclineJoinGroupApplication(IntPtr client, string groupId, string username, string reason, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_DownloadGroupSharedFile(IntPtr client, string groupId, string filePath, string fileId, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_FetchGroupAnnouncement(IntPtr client, string groupId,
			OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_FetchGroupBans(IntPtr client, string groupId, int pageNum, int pageSize,
			OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_FetchGroupSharedFiles(IntPtr client, string groupId, int pageNum, int pageSize,
			OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_FetchGroupMembers(IntPtr client, string groupId, int pageSize, string cursor, OnSuccessResult onSuccessResult, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_FetchGroupMutes(IntPtr client, string groupId, int pageNum, int pageSize,
			OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_FetchGroupSpecification(IntPtr client, string groupId,
			OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_GetGroupsWithoutNotice(IntPtr client,
			OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_FetchGroupWhiteList(IntPtr client, string groupId,
			OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_LoadAllMyGroupsFromDB(IntPtr client,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 2)] IntPtr[] array, int size);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_FetchAllMyGroupsWithPage(IntPtr client, int pageNum, int pageSize, 
			OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_FetchPublicGroupsWithCursor(IntPtr client, int pageSize, string cursor,
			OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_JoinPublicGroup(IntPtr client, string groupId, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_LeaveGroup(IntPtr client, string groupId, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_MuteAllGroupMembers(IntPtr client, string groupId, OnSuccessResult onSuccessResult, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_MuteGroupMembers(IntPtr client, string groupId,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 3)] string[] memberArray, int size, int muteDuration,
			OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_RemoveGroupAdmin(IntPtr client, string groupId, string admin, OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_DeleteGroupSharedFile(IntPtr client, string groupId, string fileId, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_RemoveWhiteListMembers(IntPtr client, string groupId,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 3)] string[] memberArray, int size,
			Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_ApplyJoinPublicGroup(IntPtr client, string groupId, string nickName, string message, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_UnblockGroupMessage(IntPtr client, string groupId, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_UnblockGroupMembers(IntPtr client, string groupId,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 3)] string[] memberArray, int size,
			Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_UnMuteAllMembers(IntPtr client, string groupId, OnSuccessResult onSuccessResult, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_UnmuteGroupMembers(IntPtr client, string groupId,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 3)] string[] memberArray, int size,
			OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_UpdateGroupAnnouncement(IntPtr client, string groupId, string newAnnouncement, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_ChangeGroupExtension(IntPtr client, string groupId, string newExtension, OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void GroupManager_UploadGroupSharedFile(IntPtr client, string groupId, string filePath, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern int GroupManager_ReleaseGroupList(
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.AsAny, SizeParamIndex = 1)] IntPtr[] toArray,
			int size);

		/** RoomManager Stub **/
		[DllImport(MyLibName)]
		internal static extern void RoomManager_AddListener(IntPtr client, OnChatRoomDestroyed onChatRoomDestroyed, OnMemberJoined onMemberJoined,
				OnMemberExitedFromRoom onMemberExited, OnRemovedFromChatRoom onRemovedFromChatRoom, OnMuteListAdded onMuteListAdded, OnMuteListRemoved onMuteListRemoved,
				OnAdminAdded onAdminAdded, OnAdminRemoved onAdminRemoved, OnOwnerChanged onOwnerChanged, OnAnnouncementChanged onAnnouncementChanged);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_AddRoomAdmin(IntPtr client, string roomId, string memberId, OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_BlockChatroomMembers(IntPtr client, string roomId, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 3)] string[] memberArray = null,
			int size = 0, OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_CreateRoom(IntPtr client, string subject, string descriptionsc, string welcomeMsg, int maxUserCount = 300,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 3)] string[] memberArray = null, int size = 0,
			OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_ChangeRoomSubject(IntPtr client, string roomId, string newSubject, OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_RemoveRoomMembers(IntPtr client, string roomId, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 3)] string[] memberArray = null,
			int size = 0, Action onSuccess = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_TransferChatroomOwner(IntPtr client, string roomId, string newOwner, OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_ChangeChatroomDescription(IntPtr client, string roomId, string newDescription, OnSuccessResult onSuccessResult = null, OnError onError = null);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_DestroyChatroom(IntPtr client, string roomId, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_FetchChatroomsWithPage(IntPtr client, int pageNum, int pageSize, OnSuccessResult onSuccessResult, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_FetchChatroomAnnouncement(IntPtr client, string roomId, OnSuccessResult onSuccessResult, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_FetchChatroomBans(IntPtr client, string roomId, int pageNum, int pageSize, OnSuccessResult onSuccessResult, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_FetchChatroomSpecification(IntPtr client, string roomId, OnSuccessResult onSuccessResult, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_FetchChatroomMembers(IntPtr client, string roomId, string cursor, int pageSize, OnSuccessResult onSuccessResult, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_FetchChatroomMutes(IntPtr client, string roomId, int pageNum, int pageSize, OnSuccessResult onSuccessResult, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_JoinedChatroomById(IntPtr client, string roomId, OnSuccessResult onSuccessResult, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_JoinChatroom(IntPtr client, string roomId, OnSuccessResult onSuccessResult, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_LeaveChatroom(IntPtr client, string roomId, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_MuteChatroomMembers(IntPtr client, string roomId, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 3)] string[] memberArray,
			int size, int muteDuration, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_RemoveChatroomAdmin(IntPtr client, string roomId, OnSuccessResult onSuccessResult, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_UnblockChatroomMembers(IntPtr client, string roomId, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 3)] string[] memberArray,
			int size, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_UnmuteChatroomMembers(IntPtr client, string roomId, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 3)] string[] memberArray,
			int size, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void RoomManager_UpdateChatroomAnnouncement(IntPtr client, string roomId, string newAnnouncement, Action onSuccess, OnError onError);

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
		internal static extern void ContactManager_GetContactsFromDB(IntPtr client, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.AsAny, SizeParamIndex = 2)] IntPtr[] array = null,
			int size = 0);

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

		[DllImport(MyLibName)]
		internal static extern int ContactManager_ReleaseStringList(
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.AsAny, SizeParamIndex = 1)] IntPtr[] toArray,
			int size);

		/** ConversationManager Stub **/
		[DllImport(MyLibName)]
		internal static extern bool ConversationManager_AppendMessage(IntPtr client, string conversationId, ConversationType converationType, IntPtr mto, MessageBodyType type);

		[DllImport(MyLibName)]
		internal static extern bool ConversationManager_ClearAllMessages(IntPtr client, string conversationId, ConversationType converationType);

		[DllImport(MyLibName)]
		internal static extern bool ConversationManager_RemoveMessage(IntPtr client, string conversationId, ConversationType converationType, string messageId);

		[DllImport(MyLibName)]
		internal static extern bool ConversationManager_ExtField(IntPtr client, string conversationId, ConversationType converationType,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.AsAny, SizeParamIndex = 4)] IntPtr[] extField = null,
			int size = 0);

		[DllImport(MyLibName)]
		internal static extern bool ConversationManager_InsertMessage(IntPtr client, string conversationId, ConversationType converationType, IntPtr mto, MessageBodyType type);

		[DllImport(MyLibName)]
		internal static extern void ConversationManager_LatestMessage(IntPtr client, string conversationId, ConversationType converationType,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.AsAny, SizeParamIndex = 4)] IntPtr[] extField = null,
			int size = 0);

		[DllImport(MyLibName)]
		internal static extern void ConversationManager_LatestMessageFromOthers(IntPtr client, string conversationId, ConversationType converationType,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.AsAny, SizeParamIndex = 4)] IntPtr[] extField = null,
			int size = 0);

		[DllImport(MyLibName)]
		internal static extern void ConversationManager_LoadMessage(IntPtr client, string conversationId, ConversationType converationType, string messageId,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.AsAny, SizeParamIndex = 5)] IntPtr[] extField = null,
			int size = 0);

		[DllImport(MyLibName)]
		internal static extern void ConversationManager_LoadMessages(IntPtr client, string conversationId, ConversationType converationType,
			string startMessageId, int count,MessageSearchDirection direction, OnSuccessResult onSuccessResult, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void ConversationManager_LoadMessagesWithKeyword(IntPtr client, string conversationId, ConversationType converationType,
			string keywords, string sender, long timestamp, int count, MessageSearchDirection direction,
			OnSuccessResult onSuccessResult, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void ConversationManager_LoadMessagesWithMsgType(IntPtr client, string conversationId, ConversationType converationType,
			MessageBodyType bodyType, long timestamp, int count, string sender, MessageSearchDirection direction,
			OnSuccessResult onSuccessResult, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void ConversationManager_LoadMessagesWithTime(IntPtr client, string conversationId, ConversationType converationType,
			long startTimeStamp, long endTimeStamp, int count,
			OnSuccessResult onSuccessResult, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void ConversationManager_MarkAllMessagesAsRead(IntPtr client, string conversationId, ConversationType converationType);

		[DllImport(MyLibName)]
		internal static extern void ConversationManager_MarkMessageAsRead(IntPtr client, string conversationId, ConversationType converationType, string messageId);

		[DllImport(MyLibName)]
		internal static extern void ConversationManager_SetExtField(IntPtr client, string conversationId, ConversationType converationType, string ext);

		[DllImport(MyLibName)]
		internal static extern int ConversationManager_UnreadMessagesCount(IntPtr client, string conversationId, ConversationType converationType);

		[DllImport(MyLibName)]
		internal static extern bool ConversationManager_UpdateMessage(IntPtr client, string conversationId, ConversationType converationType, IntPtr mto, MessageBodyType type);

		[DllImport(MyLibName)]
		internal static extern int ConversationManager_ReleaseStringList(
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.AsAny, SizeParamIndex = 1)] IntPtr[] toArray,
			int size);

		[DllImport(MyLibName)]
		internal static extern int ConversationManager_ReleaseMessageList(
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.AsAny, SizeParamIndex = 1)] IntPtr[] toArray,
			int size);

		//PushManager
		[DllImport(MyLibName)]
		internal static extern void PushManager_GetIgnoredGroupIds(IntPtr client,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.AsAny, SizeParamIndex = 2)] IntPtr[] toArray,int size);

		[DllImport(MyLibName)]
		internal static extern void PushManager_GetPushConfig(IntPtr client,
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.AsAny, SizeParamIndex = 2)] IntPtr[] toArray, int size);

		[DllImport(MyLibName)]
		internal static extern void PushManager_GetUserConfigsFromServer(IntPtr client, OnSuccessResult onSuccessResult, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void PushManager_IgnoreGroupPush(IntPtr client, string groupId, bool noDisturb, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void PushManager_UpdatePushNoDisturbing(IntPtr client, bool noDisturb, int startTime, int endTime, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void PushManager_UpdatePushDisplayStyle(IntPtr client, PushStyle pushStyle, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void PushManager_UpdateFCMPushToken(IntPtr client, string token, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void PushManager_UpdateHMSPushToken(IntPtr client, string token, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern void PushManager_UpdatePushNickName(IntPtr client, string nickname, Action onSuccess, OnError onError);

		[DllImport(MyLibName)]
		internal static extern int PushManager_ReleaseStringList(
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.AsAny, SizeParamIndex = 1)] IntPtr[] toArray,
			int size);

		[DllImport(MyLibName)]
		internal static extern int ConversationManager_ReleasePushConfigList(
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.AsAny, SizeParamIndex = 1)] IntPtr[] toArray,
			int size);

		#endregion native API import
	}
}
