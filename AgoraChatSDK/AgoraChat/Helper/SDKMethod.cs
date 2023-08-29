using System;
namespace AgoraChat
{
    internal class SDKMethod
    {

        internal const string runDelegateTester = "runDelegateTester";

        // client or manager
        internal const string client = "EMClient";
        internal const string chatManager = "EMChatManager";
        internal const string contactManager = "EMContactManager";
        internal const string groupManager = "EMGroupManager";
        internal const string roomManager = "EMRoomManager";
        internal const string userInfoManager = "EMUserInfoManager";
        internal const string threadManager = "EMThreadManager";
        internal const string presenceManager = "EMPresenceManager";
        internal const string pushManager = "EMPushManager";

        internal const string messageManager = "EMMessageManager";
        internal const string conversationManager = "EMConversationManager";

        /// EMClient methods
        internal const string init = "init";
        internal const string createAccount = "createAccount";
        internal const string login = "login";
        internal const string loginWithAgoraToken = "loginWithAgoraToken";
        internal const string renewToken = "renewToken";
        internal const string logout = "logout";
        internal const string changeAppKey = "changeAppKey";

        internal const string uploadLog = "uploadLog";
        internal const string compressLogs = "compressLogs";
        internal const string kickDevice = "kickDevice";
        internal const string kickDeviceWithToken = "kickDeviceWithToken";
        internal const string kickAllDevices = "kickAllDevices";
        internal const string kickAllDevicesWithToken = "kickAllDevicesWithToken";
        internal const string getLoggedInDevicesFromServer = "getLoggedInDevicesFromServer";
        internal const string getLoggedInDevicesFromServerWithToken = "getLoggedInDevicesFromServerWithToken";

        internal const string getToken = "getToken";
        internal const string getCurrentUser = "getCurrentUser";
        internal const string isLoggedInBefore = "isLoggedInBefore";
        internal const string isConnected = "isConnected";

        internal const string logDebug = "logDebug";
        internal const string logWarn = "logWarn";
        internal const string logError = "logError";

        /// EMContactManager methods
        internal const string addContact = "addContact";
        internal const string deleteContact = "deleteContact";
        internal const string getAllContactsFromServer = "getAllContactsFromServer";
        internal const string getAllContactsFromDB = "getAllContactsFromDB";
        internal const string addUserToBlockList = "addUserToBlockList";
        internal const string removeUserFromBlockList = "removeUserFromBlockList";
        internal const string getBlockListFromServer = "getBlockListFromServer";
        internal const string getBlockListFromDB = "getBlockListFromDB";
        internal const string acceptInvitation = "acceptInvitation";
        internal const string declineInvitation = "declineInvitation";
        internal const string getSelfIdsOnOtherPlatform = "getSelfIdsOnOtherPlatform";



        /// EMChatManager methods
        internal const string sendMessage = "sendMessage";
        internal const string resendMessage = "resendMessage";
        internal const string ackMessageRead = "ackMessageRead";
        internal const string ackGroupMessageRead = "ackGroupMessageRead";
        internal const string ackConversationRead = "ackConversationRead";
        internal const string recallMessage = "recallMessage";
        internal const string getConversation = "getConversation";
        internal const string getThreadConversation = "getThreadConversation";
        internal const string markAllChatMsgAsRead = "markAllChatMsgAsRead";
        internal const string getUnreadMessageCount = "getUnreadMessageCount";
        internal const string getMessagesCount = "getMessagesCount";
        internal const string updateChatMessage = "updateChatMessage";
        internal const string downloadAttachment = "downloadAttachment";
        internal const string downloadThumbnail = "downloadThumbnail";
        internal const string importMessages = "importMessages";
        internal const string loadAllConversations = "loadAllConversations";
        internal const string getConversationsFromServer = "getConversationsFromServer";
        internal const string getConversationsFromServerWithCursor = "getConversationsFromServerWithCursor";
        internal const string getConversationsFromServerWithPage = "getConversationsFromServerWithPage";
        internal const string deleteConversation = "deleteConversation";
        internal const string fetchHistoryMessages = "fetchHistoryMessages";
        internal const string fetchHistoryMessagesBy = "fetchHistoryMessagesBy";
        internal const string searchChatMsgFromDB = "searchChatMsgFromDB";
        internal const string getMessage = "getMessage";
        internal const string asyncFetchGroupAcks = "asyncFetchGroupAcks";
        internal const string deleteRemoteConversation = "deleteRemoteConversation";
        internal const string deleteMessagesBeforeTimestamp = "deleteMessagesBeforeTimestamp";
        internal const string translateMessage = "translateMessage";
        internal const string fetchSupportedLanguages = "fetchSupportLanguages";
        internal const string addReaction = "addReaction";
        internal const string removeReaction = "removeReaction";
        internal const string fetchReactionList = "fetchReactionList";
        internal const string fetchReactionDetail = "fetchReactionDetail";
        internal const string reportMessage = "reportMessage";
        internal const string removeMessagesFromServerWithMsgIds = "removeMessagesFromServerWithMsgIds";
        internal const string removeMessagesFromServerWithTs = "removeMessagesFromServerWithTs";
        internal const string pinConversation = "pinConversation";
        internal const string removeEarlierHistoryMessages = "removeEarlierHistoryMessages";
        internal const string modifyMessage = "modifyMessage";
        internal const string downloadCombineMessages = "downloadCombineMessages";

        /// EMMessage listener
        internal const string onMessageProgressUpdate = "onMessageProgressUpdate";
        internal const string onMessageError = "onMessageError";
        internal const string onMessageSuccess = "onMessageSuccess";
        internal const string onMessageReadAck = "onMessageReadAck";
        internal const string onMessageDeliveryAck = "onMessageDeliveryAck";

        /// EMConversation
        internal const string getConversationUnreadMsgCount = "getConversationUnreadMsgCount";
        internal const string markAllMessagesAsRead = "markAllMessagesAsRead";
        internal const string markMessageAsRead = "markMessageAsRead";
        internal const string syncConversationExt = "syncConversationExt";
        internal const string removeMessage = "removeMessage";
        internal const string removeMessages = "removeMessages";
        internal const string getLatestMessage = "getLatestMessage";
        internal const string getLatestMessageFromOthers = "getLatestMessageFromOthers";
        internal const string clearAllMessages = "clearAllMessages";
        internal const string insertMessage = "insertMessage";
        internal const string appendMessage = "appendMessage";
        internal const string updateConversationMessage = "updateConversationMessage";
        internal const string loadMsgWithId = "loadMsgWithId";
        internal const string loadMsgWithStartId = "loadMsgWithStartId";
        internal const string loadMsgWithKeywords = "loadMsgWithKeywords";
        internal const string loadMsgWithMsgType = "loadMsgWithMsgType";
        internal const string loadMsgWithTime = "loadMsgWithTime";
        internal const string messageCount = "messageCount";

        // EMMessage method
        internal const string getReactionList = "getReactionList";
        internal const string groupAckCount = "groupAckCount";
        internal const string getChatThread = "chatThread";
        internal const string getHasDeliverAck = "getHasDeliverAck";
        internal const string getHasReadAck = "getHasReadAck";


        // EMChatRoomManager
        internal const string joinChatRoom = "joinChatRoom";
        internal const string leaveChatRoom = "leaveChatRoom";
        internal const string fetchPublicChatRoomsFromServer = "fetchPublicChatRoomsFromServer";
        internal const string fetchChatRoomInfoFromServer = "fetchChatRoomInfoFromServer";
        internal const string getChatRoom = "getChatRoom";
        internal const string getAllChatRooms = "getAllChatRooms";
        internal const string createChatRoom = "createChatRoom";
        internal const string destroyChatRoom = "destroyChatRoom";
        internal const string changeChatRoomSubject = "changeChatRoomSubject";
        internal const string changeChatRoomDescription = "changeChatRoomDescription";
        internal const string fetchChatRoomMembers = "fetchChatRoomMembers";
        internal const string muteChatRoomMembers = "muteChatRoomMembers";
        internal const string unMuteChatRoomMembers = "unMuteChatRoomMembers";
        internal const string changeChatRoomOwner = "changeChatRoomOwner";
        internal const string addChatRoomAdmin = "addChatRoomAdmin";
        internal const string removeChatRoomAdmin = "removeChatRoomAdmin";
        internal const string fetchChatRoomMuteList = "fetchChatRoomMuteList";
        internal const string removeChatRoomMembers = "removeChatRoomMembers";
        internal const string blockChatRoomMembers = "blockChatRoomMembers";
        internal const string unBlockChatRoomMembers = "unBlockChatRoomMembers";
        internal const string fetchChatRoomBlockList = "fetchChatRoomBlockList";
        internal const string updateChatRoomAnnouncement = "updateChatRoomAnnouncement";
        internal const string fetchChatRoomAnnouncement = "fetchChatRoomAnnouncement";

        internal const string addMembersToChatRoomAllowList = "addMembersToChatRoomWhiteList";
        internal const string removeMembersFromChatRoomAllowList = "removeMembersFromChatRoomWhiteList";
        internal const string fetchChatRoomAllowListFromServer = "fetchChatRoomWhiteListFromServer";
        internal const string isMemberInChatRoomAllowListFromServer = "isMemberInChatRoomWhiteListFromServer";

        internal const string muteAllChatRoomMembers = "muteAllChatRoomMembers";
        internal const string unMuteAllChatRoomMembers = "unMuteAllChatRoomMembers";
        internal const string fetchChatRoomAttributes = "fetchChatRoomAttributes";
        internal const string setChatRoomAttributes = "setChatRoomAttributes";
        internal const string removeChatRoomAttributes = "removeChatRoomAttributes";


        /// EMGroupManager
        internal const string getGroupWithId = "getGroupWithId";
        internal const string getJoinedGroups = "getJoinedGroups";
        internal const string getGroupsWithoutPushNotification = "getGroupsWithoutPushNotification";
        internal const string getJoinedGroupsFromServer = "getJoinedGroupsFromServer";
        internal const string getJoinedGroupsFromServerSimple = "getJoinedGroupsFromServerSimple";
        internal const string getPublicGroupsFromServer = "getPublicGroupsFromServer";
        internal const string createGroup = "createGroup";
        internal const string getGroupSpecificationFromServer = "getGroupSpecificationFromServer";
        internal const string getGroupMemberListFromServer = "getGroupMemberListFromServer";
        internal const string getGroupBlockListFromServer = "getGroupBlockListFromServer";
        internal const string getGroupMuteListFromServer = "getGroupMuteListFromServer";
        internal const string getGroupAllowListFromServer = "getGroupWhiteListFromServer";
        internal const string isMemberInAllowListFromServer = "isMemberInWhiteListFromServer";
        internal const string getGroupFileListFromServer = "getGroupFileListFromServer";
        internal const string getGroupAnnouncementFromServer = "getGroupAnnouncementFromServer";
        internal const string addMembers = "addMembers";
        internal const string inviterUser = "inviterUser";
        internal const string removeMembers = "removeMembers";
        internal const string blockMembers = "blockMembers";
        internal const string unblockMembers = "unblockMembers";
        internal const string updateGroupSubject = "updateGroupSubject";
        internal const string updateDescription = "updateDescription";
        internal const string leaveGroup = "leaveGroup";
        internal const string destroyGroup = "destroyGroup";
        internal const string blockGroup = "blockGroup";
        internal const string unblockGroup = "unblockGroup";
        internal const string updateGroupOwner = "updateGroupOwner";
        internal const string addAdmin = "addAdmin";
        internal const string removeAdmin = "removeAdmin";
        internal const string muteMembers = "muteMembers";
        internal const string unMuteMembers = "unMuteMembers";
        internal const string muteAllMembers = "muteAllMembers";
        internal const string unMuteAllMembers = "unMuteAllMembers";
        internal const string addAllowList = "addWhiteList";
        internal const string removeAllowList = "removeWhiteList";
        internal const string uploadGroupSharedFile = "uploadGroupSharedFile";
        internal const string downloadGroupSharedFile = "downloadGroupSharedFile";
        internal const string removeGroupSharedFile = "removeGroupSharedFile";
        internal const string updateGroupAnnouncement = "updateGroupAnnouncement";
        internal const string updateGroupExt = "updateGroupExt";
        internal const string joinPublicGroup = "joinPublicGroup";
        internal const string requestToJoinGroup = "requestToJoinGroup";
        internal const string acceptJoinApplication = "acceptJoinApplication";
        internal const string declineJoinApplication = "declineJoinApplication";
        internal const string acceptInvitationFromGroup = "acceptInvitationFromGroup";
        internal const string declineInvitationFromGroup = "declineInvitationFromGroup";
        internal const string fetchMemberAttributes = "fetchMemberAttributes";
        internal const string setMemberAttributes = "setMemberAttributes";


        /// EMPushManager
        internal const string getImPushConfig = "getImPushConfig";
        internal const string getImPushConfigFromServer = "getImPushConfigFromServer";
        internal const string enableOfflinePush = "enableOfflinePush";
        internal const string disableOfflinePush = "disableOfflinePush";
        internal const string updateImPushStyle = "updateImPushStyle";
        internal const string updatePushNickname = "updatePushNickname";

        internal const string updateGroupPushService = "updateGroupPushService";
        internal const string getNoPushGroups = "getNoPushGroups";
        internal const string updateUserPushService = "updateUserPushService";
        internal const string getNoPushUsers = "getNoPushUsers";

        internal const string updateHMSPushToken = "updateHMSPushToken";
        internal const string updateFCMPushToken = "updateFCMPushToken";


        internal const string reportPushAction = "reportPushAction";
        internal const string setConversationSilentMode = "setConversationSilentMode";
        internal const string removeConversationSilentMode = "removeConversationSilentMode";
        internal const string fetchConversationSilentMode = "fetchConversationSilentMode";
        internal const string setSilentModeForAll = "setSilentModeForAll";
        internal const string fetchSilentModeForAll = "fetchSilentModeForAll";
        internal const string fetchSilentModeForConversations = "fetchSilentModeForConversations";
        internal const string setPreferredNotificationLanguage = "setPreferredNotificationLanguage";
        internal const string fetchPreferredNotificationLanguage = "fetchPreferredNotificationLanguage";
        internal const string setPushTemplate = "setPushTemplate";
        internal const string getPushTemplate = "getPushTemplate";

        /// EMUserInfoManager 
        internal const string updateOwnUserInfo = "updateOwnUserInfo";
        internal const string updateOwnUserInfoWithType = "updateOwnUserInfoWithType";
        internal const string fetchUserInfoById = "fetchUserInfoById";
        internal const string fetchUserInfoByIdWithType = "fetchUserInfoByIdWithType";

        /// EMPresenceManager methods
        internal const string presenceWithDescription = "publishPresenceWithDescription";
        internal const string presenceSubscribe = "presenceSubscribe";
        internal const string presenceUnsubscribe = "presenceUnsubscribe";
        internal const string fetchSubscribedMembersWithPageNum = "fetchSubscribedMembersWithPageNum";
        internal const string fetchPresenceStatus = "fetchPresenceStatus";


        /// EMChatThreadManager methods
        internal const string fetchChatThreadDetail = "fetchChatThreadDetail";
        internal const string fetchJoinedChatThreads = "fetchJoinedChatThreads";
        internal const string fetchChatThreadsWithParentId = "fetchChatThreadsWithParentId";
        internal const string fetchChatThreadMember = "fetchChatThreadMember";
        internal const string fetchLastMessageWithChatThreads = "fetchLastMessageWithChatThreads";
        internal const string removeMemberFromChatThread = "removeMemberFromChatThread";
        internal const string updateChatThreadSubject = "updateChatThreadSubject";
        internal const string createChatThread = "createChatThread";
        internal const string joinChatThread = "joinChatThread";
        internal const string leaveChatThread = "leaveChatThread";
        internal const string destroyChatThread = "destroyChatThread";



        /// HandleAction ?
        internal const string startCallback = "startCallback";



        // Listeners name
        internal const string connectionListener = "connectionListener";
        internal const string multiDeviceListener = "multiDeviceListener";
        internal const string chatListener = "chatListener";
        internal const string contactListener = "contactListener";
        internal const string groupListener = "groupListener";
        internal const string chatRoomListener = "chatRoomListener";
        internal const string chatThreadListener = "chatThreadListener";
        internal const string presenceListener = "presenceListener";
        internal const string callback = "callback";
        internal const string callbackProgress = "callbackProgress";

        // ChatManagerDelegate
        internal const string onMessagesReceived = "onMessagesReceived";
        internal const string onCmdMessagesReceived = "onCmdMessagesReceived";
        internal const string onMessagesRead = "onMessagesRead";
        internal const string onGroupMessageRead = "onGroupMessageRead";
        internal const string onReadAckForGroupMessageUpdated = "onReadAckForGroupMessageUpdated";
        internal const string onMessagesDelivered = "onMessagesDelivered";
        internal const string onMessagesRecalled = "onMessagesRecalled";
        internal const string onConversationsUpdate = "onConversationsUpdate";
        internal const string onConversationRead = "onConversationRead";
        internal const string onMessageReactionDidChange = "messageReactionDidChange";
        internal const string onMessageIdChanged = "onMessageIdChanged";
        internal const string onMessageContentChanged = "onMessageContentChanged";

        // ChatThreadManagerDelegate
        internal const string onChatThreadCreate = "onChatThreadCreate";
        internal const string onChatThreadUpdate = "onChatThreadUpdate";
        internal const string onChatThreadDestroy = "onChatThreadDestroy";
        internal const string onUserKickOutOfChatThread = "onUserKickOutOfChatThread";

        // ContactManagerDelegate
        internal const string onContactAdded = "onContactAdded";
        internal const string onContactDeleted = "onContactDeleted";
        internal const string onContactInvited = "onContactInvited";
        internal const string onFriendRequestAccepted = "onFriendRequestAccepted";
        internal const string onFriendRequestDeclined = "onFriendRequestDeclined";

        // MultiDeviceDelegate
        internal const string onContactMultiDevicesEvent = "onContactMultiDevicesEvent";
        internal const string onGroupMultiDevicesEvent = "onGroupMultiDevicesEvent";
        internal const string onUnDisturbMultiDevicesEvent = "onUnDisturbMultiDevicesEvent";
        internal const string onThreadMultiDevicesEvent = "onThreadMultiDevicesEvent";
        internal const string onRoamDeleteMultiDevicesEvent = "onRoamDeleteMultiDevicesEvent";
        internal const string onConversationMultiDevicesEvent = "onConversationMultiDevicesEvent";

        // PresenceManagerDelegate
        internal const string onPresenceUpdated = "onPresenceUpdated";

        // ConnectionDelegate
        internal const string onConnected = "onConnected";
        internal const string onDisconnected = "onDisconnected";
        internal const string onLoggedOtherDevice = "onLoggedOtherDevice";
        internal const string onRemovedFromServer = "onRemovedFromServer";
        internal const string onForbidByServer = "onForbidByServer";
        internal const string onChangedImPwd = "onChangedImPwd";
        internal const string onLoginTooManyDevice = "onLoginTooManyDevice";
        internal const string onKickedByOtherDevice = "onKickedByOtherDevice";
        internal const string onAuthFailed = "onAuthFailed";
        internal const string onTokenExpired = "onTokenExpired";
        internal const string onTokenWillExpire = "onTokenWillExpire";
        internal const string onAppActiveNumberReachLimitation = "onAppActiveNumberReachLimitation";

        // GroupManagerDeleagate
        internal const string onInvitationReceivedFromGroup = "onInvitationReceivedFromGroup";
        internal const string onRequestToJoinReceivedFromGroup = "onRequestToJoinReceivedFromGroup";
        internal const string onRequestToJoinAcceptedFromGroup = "onRequestToJoinAcceptedFromGroup";
        internal const string onRequestToJoinDeclinedFromGroup = "onRequestToJoinDeclinedFromGroup";
        internal const string onInvitationAcceptedFromGroup = "onInvitationAcceptedFromGroup";
        internal const string onInvitationDeclinedFromGroup = "onInvitationDeclinedFromGroup";
        internal const string onUserRemovedFromGroup = "onUserRemovedFromGroup";
        internal const string onDestroyedFromGroup = "onDestroyedFromGroup";
        internal const string onAutoAcceptInvitationFromGroup = "onAutoAcceptInvitationFromGroup";
        internal const string onMuteListAddedFromGroup = "onMuteListAddedFromGroup";
        internal const string onMuteListRemovedFromGroup = "onMuteListRemovedFromGroup";
        internal const string onAdminAddedFromGroup = "onAdminAddedFromGroup";
        internal const string onAdminRemovedFromGroup = "onAdminRemovedFromGroup";
        internal const string onOwnerChangedFromGroup = "onOwnerChangedFromGroup";
        internal const string onMemberJoinedFromGroup = "onMemberJoinedFromGroup";
        internal const string onMemberExitedFromGroup = "onMemberExitedFromGroup";
        internal const string onAnnouncementChangedFromGroup = "onAnnouncementChangedFromGroup";
        internal const string onSharedFileAddedFromGroup = "onSharedFileAddedFromGroup";
        internal const string onSharedFileDeletedFromGroup = "onSharedFileDeletedFromGroup";
        internal const string onAddAllowListMembersFromGroup = "onAddWhiteListMembersFromGroup";
        internal const string onRemoveAllowListMembersFromGroup = "onRemoveWhiteListMembersFromGroup";
        internal const string onAllMemberMuteChangedFromGroup = "onAllMemberMuteChangedFromGroup";
        internal const string onSpecificationChangedFromGroup = "onSpecificationChangedFromGroup";
        internal const string onStateChangedFromGroup = "onStateChangedFromGroup";
        internal const string onUpdateMemberAttributesFromGroup = "onUpdateMemberAttributesFromGroup";

        // RoomManagerDelegate
        internal const string onDestroyedFromRoom = "onDestroyedFromRoom";
        internal const string onMemberJoinedFromRoom = "onMemberJoinedFromRoom";
        internal const string onMemberExitedFromRoom = "onMemberExitedFromRoom";
        internal const string onRemovedFromRoom = "onRemovedFromRoom";
        internal const string onRemoveFromRoomByOffline = "onRemoveFromRoomByOffline";
        internal const string onMuteListAddedFromRoom = "onMuteListAddedFromRoom";
        internal const string onMuteListRemovedFromRoom = "onMuteListRemovedFromRoom";
        internal const string onAdminAddedFromRoom = "onAdminAddedFromRoom";
        internal const string onAdminRemovedFromRoom = "onAdminRemovedFromRoom";
        internal const string onOwnerChangedFromRoom = "onOwnerChangedFromRoom";
        internal const string onAnnouncementChangedFromRoom = "onAnnouncementChangedFromRoom";
        internal const string onAttributesChangedFromRoom = "onAttributesChangedFromRoom";
        internal const string onAttributesRemovedFromRoom = "onAttributesRemovedFromRoom";
        internal const string onSpecificationChangedFromRoom = "onSpecificationChangedFromRoom";
        internal const string onAddAllowListMembersFromRoom = "onAddWhiteListMembersFromRoom";
        internal const string onRemoveAllowListMembersFromRoom = "onRemoveWhiteListMembersFromRoom";
        internal const string onAllMemberMuteChangedFromRoom = "onAllMemberMuteChangedFromRoom";
    }
}
