using System;
namespace AgoraChat
{
    public class SDKMethod
    {
        // client or manager
        public const string client = "EMClient";
        public const string chatManager = "EMChatManager";
        public const string contactManager = "EMContactManager";
        public const string groupManager = "EMGroupManager";
        public const string roomManager = "EMRoomManager";
        public const string userInfoManager = "EMUserInfoManager";
        public const string threadManager = "EMThreadManager";
        public const string presenceManager = "EMPresenceManager";
        public const string pushManager = "EMPushManager";

        public const string messageManager = "EMMessageManager";
        public const string conversationManager = "EMConversationManager";

        /// EMClient methods
        public const string init = "init";
        public const string createAccount = "createAccount";
        public const string login = "login";
        public const string loginWithAgoraToken = "loginWithAgoraToken";
        public const string renewToken = "renewToken";
        public const string logout = "logout";
        public const string changeAppKey = "changeAppKey";

        public const string uploadLog = "uploadLog";
        public const string compressLogs = "compressLogs";
        public const string kickDevice = "kickDevice";
        public const string kickAllDevices = "kickAllDevices";
        public const string getLoggedInDevicesFromServer = "getLoggedInDevicesFromServer";

        public const string getToken = "getToken";
        public const string getCurrentUser = "getCurrentUser";
        public const string isLoggedInBefore = "isLoggedInBefore";
        public const string isConnected = "isConnected";


        /// EMContactManager methods
        public const string addContact = "addContact";
        public const string deleteContact = "deleteContact";
        public const string getAllContactsFromServer = "getAllContactsFromServer";
        public const string getAllContactsFromDB = "getAllContactsFromDB";
        public const string addUserToBlockList = "addUserToBlockList";
        public const string removeUserFromBlockList = "removeUserFromBlockList";
        public const string getBlockListFromServer = "getBlockListFromServer";
        public const string getBlockListFromDB = "getBlockListFromDB";
        public const string acceptInvitation = "acceptInvitation";
        public const string declineInvitation = "declineInvitation";
        public const string getSelfIdsOnOtherPlatform = "getSelfIdsOnOtherPlatform";



        /// EMChatManager methods
        public const string sendMessage = "sendMessage";
        public const string resendMessage = "resendMessage";
        public const string ackMessageRead = "ackMessageRead";
        public const string ackGroupMessageRead = "ackGroupMessageRead";
        public const string ackConversationRead = "ackConversationRead";
        public const string recallMessage = "recallMessage";
        public const string getConversation = "getConversation";
        public const string getThreadConversation = "getThreadConversation";
        public const string markAllChatMsgAsRead = "markAllChatMsgAsRead";
        public const string getUnreadMessageCount = "getUnreadMessageCount";
        public const string updateChatMessage = "updateChatMessage";
        public const string downloadAttachment = "downloadAttachment";
        public const string downloadThumbnail = "downloadThumbnail";
        public const string importMessages = "importMessages";
        public const string loadAllConversations = "loadAllConversations";
        public const string getConversationsFromServer = "getConversationsFromServer";
        public const string deleteConversation = "deleteConversation";
        public const string fetchHistoryMessages = "fetchHistoryMessages";
        public const string searchChatMsgFromDB = "searchChatMsgFromDB";
        public const string getMessage = "getMessage";
        public const string asyncFetchGroupAcks = "asyncFetchGroupAcks";
        public const string deleteRemoteConversation = "deleteRemoteConversation";
        public const string deleteMessagesBeforeTimestamp = "deleteMessagesBeforeTimestamp";
        public const string translateMessage = "translateMessage";
        public const string fetchSupportedLanguages = "fetchSupportLanguages";
        public const string addReaction = "addReaction";
        public const string removeReaction = "removeReaction";
        public const string fetchReactionList = "fetchReactionList";
        public const string fetchReactionDetail = "fetchReactionDetail";
        public const string reportMessage = "reportMessage";

        /// EMMessage listener
        public const string onMessageProgressUpdate = "onMessageProgressUpdate";
        public const string onMessageError = "onMessageError";
        public const string onMessageSuccess = "onMessageSuccess";
        public const string onMessageReadAck = "onMessageReadAck";
        public const string onMessageDeliveryAck = "onMessageDeliveryAck";

        /// EMConversation
        public const string getConversationUnreadMsgCount = "getConversationUnreadMsgCount";
        public const string markAllMessagesAsRead = "markAllMessagesAsRead";
        public const string markMessageAsRead = "markMessageAsRead";
        public const string syncConversationExt = "syncConversationExt";
        public const string removeMessage = "removeMessage";
        public const string getLatestMessage = "getLatestMessage";
        public const string getLatestMessageFromOthers = "getLatestMessageFromOthers";
        public const string clearAllMessages = "clearAllMessages";
        public const string insertMessage = "insertMessage";
        public const string appendMessage = "appendMessage";
        public const string updateConversationMessage = "updateConversationMessage";
        public const string loadMsgWithId = "loadMsgWithId";
        public const string loadMsgWithStartId = "loadMsgWithStartId";
        public const string loadMsgWithKeywords = "loadMsgWithKeywords";
        public const string loadMsgWithMsgType = "loadMsgWithMsgType";
        public const string loadMsgWithTime = "loadMsgWithTime";
        public const string messageCount = "messageCount";

        // EMMessage method
        public const string getReactionList = "getReactionList";
        public const string groupAckCount = "groupAckCount";
        public const string getChatThread = "chatThread";
        public const string getHasDeliverAck = "getHasDeliverAck";
        public const string getHasReadAck = "getHasReadAck";


        // EMChatRoomManager
        public const string joinChatRoom = "joinChatRoom";
        public const string leaveChatRoom = "leaveChatRoom";
        public const string fetchPublicChatRoomsFromServer = "fetchPublicChatRoomsFromServer";
        public const string fetchChatRoomInfoFromServer = "fetchChatRoomInfoFromServer";
        public const string getChatRoom = "getChatRoom";
        public const string getAllChatRooms = "getAllChatRooms";
        public const string createChatRoom = "createChatRoom";
        public const string destroyChatRoom = "destroyChatRoom";
        public const string changeChatRoomSubject = "changeChatRoomSubject";
        public const string changeChatRoomDescription = "changeChatRoomDescription";
        public const string fetchChatRoomMembers = "fetchChatRoomMembers";
        public const string muteChatRoomMembers = "muteChatRoomMembers";
        public const string unMuteChatRoomMembers = "unMuteChatRoomMembers";
        public const string changeChatRoomOwner = "changeChatRoomOwner";
        public const string addChatRoomAdmin = "addChatRoomAdmin";
        public const string removeChatRoomAdmin = "removeChatRoomAdmin";
        public const string fetchChatRoomMuteList = "fetchChatRoomMuteList";
        public const string removeChatRoomMembers = "removeChatRoomMembers";
        public const string blockChatRoomMembers = "blockChatRoomMembers";
        public const string unBlockChatRoomMembers = "unBlockChatRoomMembers";
        public const string fetchChatRoomBlockList = "fetchChatRoomBlockList";
        public const string updateChatRoomAnnouncement = "updateChatRoomAnnouncement";
        public const string fetchChatRoomAnnouncement = "fetchChatRoomAnnouncement";

        public const string addMembersToChatRoomWhiteList = "addMembersToChatRoomWhiteList";
        public const string removeMembersFromChatRoomWhiteList = "removeMembersFromChatRoomWhiteList";
        public const string fetchChatRoomWhiteListFromServer = "fetchChatRoomWhiteListFromServer";
        public const string isMemberInChatRoomWhiteListFromServer = "isMemberInChatRoomWhiteListFromServer";

        public const string muteAllChatRoomMembers = "muteAllChatRoomMembers";
        public const string unMuteAllChatRoomMembers = "unMuteAllChatRoomMembers";
        public const string fetchChatRoomAttributes = "fetchChatRoomAttributes";
        public const string setChatRoomAttributes = "setChatRoomAttributes";
        public const string removeChatRoomAttributes = "removeChatRoomAttributes";


        /// EMGroupManager
        public const string getGroupWithId = "getGroupWithId";
        public const string getJoinedGroups = "getJoinedGroups";
        public const string getGroupsWithoutPushNotification = "getGroupsWithoutPushNotification";
        public const string getJoinedGroupsFromServer = "getJoinedGroupsFromServer";
        public const string getPublicGroupsFromServer = "getPublicGroupsFromServer";
        public const string createGroup = "createGroup";
        public const string getGroupSpecificationFromServer = "getGroupSpecificationFromServer";
        public const string getGroupMemberListFromServer = "getGroupMemberListFromServer";
        public const string getGroupBlockListFromServer = "getGroupBlockListFromServer";
        public const string getGroupMuteListFromServer = "getGroupMuteListFromServer";
        public const string getGroupWhiteListFromServer = "getGroupWhiteListFromServer";
        public const string isMemberInWhiteListFromServer = "isMemberInWhiteListFromServer";
        public const string getGroupFileListFromServer = "getGroupFileListFromServer";
        public const string getGroupAnnouncementFromServer = "getGroupAnnouncementFromServer";
        public const string addMembers = "addMembers";
        public const string inviterUser = "inviterUser";
        public const string removeMembers = "removeMembers";
        public const string blockMembers = "blockMembers";
        public const string unblockMembers = "unblockMembers";
        public const string updateGroupSubject = "updateGroupSubject";
        public const string updateDescription = "updateDescription";
        public const string leaveGroup = "leaveGroup";
        public const string destroyGroup = "destroyGroup";
        public const string blockGroup = "blockGroup";
        public const string unblockGroup = "unblockGroup";
        public const string updateGroupOwner = "updateGroupOwner";
        public const string addAdmin = "addAdmin";
        public const string removeAdmin = "removeAdmin";
        public const string muteMembers = "muteMembers";
        public const string unMuteMembers = "unMuteMembers";
        public const string muteAllMembers = "muteAllMembers";
        public const string unMuteAllMembers = "unMuteAllMembers";
        public const string addWhiteList = "addWhiteList";
        public const string removeWhiteList = "removeWhiteList";
        public const string uploadGroupSharedFile = "uploadGroupSharedFile";
        public const string downloadGroupSharedFile = "downloadGroupSharedFile";
        public const string removeGroupSharedFile = "removeGroupSharedFile";
        public const string updateGroupAnnouncement = "updateGroupAnnouncement";
        public const string updateGroupExt = "updateGroupExt";
        public const string joinPublicGroup = "joinPublicGroup";
        public const string requestToJoinGroup = "requestToJoinGroup";
        public const string acceptJoinApplication = "acceptJoinApplication";
        public const string declineJoinApplication = "declineJoinApplication";
        public const string acceptInvitationFromGroup = "acceptInvitationFromGroup";
        public const string declineInvitationFromGroup = "declineInvitationFromGroup";


        /// EMPushManager
        public const string getImPushConfig = "getImPushConfig";
        public const string getImPushConfigFromServer = "getImPushConfigFromServer";
        public const string enableOfflinePush = "enableOfflinePush";
        public const string disableOfflinePush = "disableOfflinePush";
        public const string updateImPushStyle = "updateImPushStyle";
        public const string updatePushNickname = "updatePushNickname";

        public const string updateGroupPushService = "updateGroupPushService";
        public const string getNoPushGroups = "getNoPushGroups";
        public const string updateUserPushService = "updateUserPushService";
        public const string getNoPushUsers = "getNoPushUsers";

        public const string updateHMSPushToken = "updateHMSPushToken";
        public const string updateFCMPushToken = "updateFCMPushToken";


        public const string reportPushAction = "reportPushAction";
        public const string setConversationSilentMode = "setConversationSilentMode";
        public const string removeConversationSilentMode = "removeConversationSilentMode";
        public const string fetchConversationSilentMode = "fetchConversationSilentMode";
        public const string setSilentModeForAll = "setSilentModeForAll";
        public const string fetchSilentModeForAll = "fetchSilentModeForAll";
        public const string fetchSilentModeForConversations = "fetchSilentModeForConversations";
        public const string setPreferredNotificationLanguage = "setPreferredNotificationLanguage";
        public const string fetchPreferredNotificationLanguage = "fetchPreferredNotificationLanguage";
        public const string setPushTemplate = "setPushTemplate";
        public const string getPushTemplate = "getPushTemplate";

        /// EMUserInfoManager 
        public const string updateOwnUserInfo = "updateOwnUserInfo";
        public const string updateOwnUserInfoWithType = "updateOwnUserInfoWithType";
        public const string fetchUserInfoById = "fetchUserInfoById";
        public const string fetchUserInfoByIdWithType = "fetchUserInfoByIdWithType";

        /// EMPresenceManager methods
        public const string presenceWithDescription = "publishPresenceWithDescription";
        public const string presenceSubscribe = "presenceSubscribe";
        public const string presenceUnsubscribe = "presenceUnsubscribe";
        public const string fetchSubscribedMembersWithPageNum = "fetchSubscribedMembersWithPageNum";
        public const string fetchPresenceStatus = "fetchPresenceStatus";


        /// EMChatThreadManager methods
        public const string fetchChatThreadDetail = "fetchChatThreadDetail";
        public const string fetchJoinedChatThreads = "fetchJoinedChatThreads";
        public const string fetchChatThreadsWithParentId = "fetchChatThreadsWithParentId";
        public const string fetchChatThreadMember = "fetchChatThreadMember";
        public const string fetchLastMessageWithChatThreads = "fetchLastMessageWithChatThreads";
        public const string removeMemberFromChatThread = "removeMemberFromChatThread";
        public const string updateChatThreadSubject = "updateChatThreadSubject";
        public const string createChatThread = "createChatThread";
        public const string joinChatThread = "joinChatThread";
        public const string leaveChatThread = "leaveChatThread";
        public const string destroyChatThread = "destroyChatThread";



        /// HandleAction
        public const string startCallback = "startCallback";



        // Listeners name
        public const string connetionListener = "connetionListener";
        public const string multiDeviceListener = "multiDeviceListener";
        public const string chatListener = "chatListener";
        public const string contactListener = "contactListener";
        public const string groupListener = "groupListener";
        public const string chatRoomListener = "chatRoomListener";
        public const string chatThreadListener = "chatThreadListener";
        public const string presenceListener = "presenceListener";
        public const string callback = "callback";
        public const string callbackProgress = "callbackProgress";

        // ChatManagerDelegate
        public const string onMessagesReceived = "onMessagesReceived";
        public const string onCmdMessagesReceived = "onCmdMessagesReceived";
        public const string onMessagesRead = "onMessagesRead";
        public const string onGroupMessageRead = "onGroupMessageRead";
        public const string onReadAckForGroupMessageUpdated = "onReadAckForGroupMessageUpdated";
        public const string onMessagesDelivered = "onMessagesDelivered";
        public const string onMessagesRecalled = "onMessagesRecalled";
        public const string onConversationsUpdate = "onConversationsUpdate";
        public const string onConversationRead = "onConversationRead";
        public const string onMessageReactionDidChange = "messageReactionDidChange";

        // ChatThreadManagerDelegate
        public const string onChatThreadCreate = "onChatThreadCreate";
        public const string onChatThreadUpdate = "onChatThreadUpdate";
        public const string onChatThreadDestroy = "onChatThreadDestroy";
        public const string onUserKickOutOfChatThread = "onUserKickOutOfChatThread";

        // ContactManagerDelegate
        public const string onContactAdded = "onContactAdded";
        public const string onContactDeleted = "onContactDeleted";
        public const string onContactInvited = "onContactInvited";
        public const string onFriendRequestAccepted = "onFriendRequestAccepted";
        public const string onFriendRequestDeclined = "onFriendRequestDeclined";

        // MultiDeviceDelegate
        public const string onContactMultiDevicesEvent = "onContactMultiDevicesEvent";
        public const string onGroupMultiDevicesEvent = "onGroupMultiDevicesEvent";
        public const string onUndisturbMultiDevicesEvent = "onUndisturbMultiDevicesEvent";
        public const string onThreadMultiDevicesEvent = "onThreadMultiDevicesEvent";

        // PresenceManagerDelegate
        public const string onPresenceUpdated = "onPresenceUpdated";

        // ConnectionDelegate
        public const string onConnected = "onConnected";
        public const string onDisconnected = "onDisconnected";
        public const string onTokenExpired = "onTokenExpired";
        public const string onTokenWillExpire = "onTokenWillExpire";

        // GroupManagerDeleagate
        public const string onInvitationReceivedFromGroup = "onInvitationReceivedFromGroup";
        public const string onRequestToJoinReceivedFromGroup = "onRequestToJoinReceivedFromGroup";
        public const string onRequestToJoinAcceptedFromGroup = "onRequestToJoinAcceptedFromGroup";
        public const string onRequestToJoinDeclinedFromGroup = "onRequestToJoinDeclinedFromGroup";
        public const string onInvitationAcceptedFromGroup = "onInvitationAcceptedFromGroup";
        public const string onInvitationDeclinedFromGroup = "onInvitationDeclinedFromGroup";
        public const string onUserRemovedFromGroup = "onUserRemovedFromGroup";
        public const string onDestroyedFromGroup = "onDestroyedFromGroup";
        public const string onAutoAcceptInvitationFromGroup = "onAutoAcceptInvitationFromGroup";
        public const string onMuteListAddedFromGroup = "onMuteListAddedFromGroup";
        public const string onMuteListRemovedFromGroup = "onMuteListRemovedFromGroup";
        public const string onAdminAddedFromGroup = "onAdminAddedFromGroup";
        public const string onAdminRemovedFromGroup = "onAdminRemovedFromGroup";
        public const string onOwnerChangedFromGroup = "onOwnerChangedFromGroup";
        public const string onMemberJoinedFromGroup = "onMemberJoinedFromGroup";
        public const string onMemberExitedFromGroup = "onMemberExitedFromGroup";
        public const string onAnnouncementChangedFromGroup = "onAnnouncementChangedFromGroup";
        public const string onSharedFileAddedFromGroup = "onSharedFileAddedFromGroup";
        public const string onSharedFileDeletedFromGroup = "onSharedFileDeletedFromGroup";
        public const string onAddWhiteListMembersFromGroup = "onAddWhiteListMembersFromGroup";
        public const string onRemoveWhiteListMembersFromGroup = "onRemoveWhiteListMembersFromGroup";
        public const string onAllMemberMuteChangedFromGroup = "onAllMemberMuteChangedFromGroup";

    }
}
