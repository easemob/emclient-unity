package com.hyphenate.wrapper.util;

public class EMSDKMethod {

    public static final String runDelegateTester = "runDelegateTester";

    // client or manager
    public static final String client = "EMClient";
    public static final String chatManager = "EMChatManager";
    public static final String contactManager = "EMContactManager";
    public static final String groupManager = "EMGroupManager";
    public static final String roomManager = "EMRoomManager";
    public static final String userInfoManager = "EMUserInfoManager";
    public static final String threadManager = "EMThreadManager";
    public static final String presenceManager = "EMPresenceManager";
    public static final String pushManager = "EMPushManager";

    public static final String messageManager = "EMMessageManager";
    public static final String conversationManager = "EMConversationManager";

    /// EMClient methods
    public static final String init = "init";
    public static final String createAccount = "createAccount";
    public static final String login = "login";
    public static final String loginWithAgoraToken = "loginWithAgoraToken";
    public static final String renewToken = "renewToken";
    public static final String logout = "logout";
    public static final String changeAppKey = "changeAppKey";

    public static final String uploadLog = "uploadLog";
    public static final String compressLogs = "compressLogs";
    public static final String kickDevice = "kickDevice";
    public static final String kickAllDevices = "kickAllDevices";
    public static final String getLoggedInDevicesFromServer = "getLoggedInDevicesFromServer";

    public static final String getToken = "getToken";
    public static final String getCurrentUser = "getCurrentUser";
    public static final String isLoggedInBefore = "isLoggedInBefore";
    public static final String isConnected = "isConnected";
    public static final String kickDeviceWithToken = "kickDeviceWithToken";
    public static final String kickAllDevicesWithToken = "kickAllDevicesWithToken";
    public static final String getLoggedInDevicesFromServerWithToken = "getLoggedInDevicesFromServerWithToken";


    /// EMContactManager methods
    public static final String addContact = "addContact";
    public static final String deleteContact = "deleteContact";
    public static final String getAllContactsFromServer = "getAllContactsFromServer";
    public static final String getAllContactsFromDB = "getAllContactsFromDB";
    public static final String addUserToBlockList = "addUserToBlockList";
    public static final String removeUserFromBlockList = "removeUserFromBlockList";
    public static final String getBlockListFromServer = "getBlockListFromServer";
    public static final String getBlockListFromDB = "getBlockListFromDB";
    public static final String acceptInvitation = "acceptInvitation";
    public static final String declineInvitation = "declineInvitation";
    public static final String getSelfIdsOnOtherPlatform = "getSelfIdsOnOtherPlatform";
    public static final String setContactRemark = "setContactRemark";
    public static final String fetchContactFromLocal = "fetchContactFromLocal";
    public static final String fetchAllContactsFromLocal = "fetchAllContactsFromLocal";
    public static final String fetchAllContactsFromServer = "fetchAllContactsFromServer";
    public static final String fetchAllContactsFromServerByPage = "fetchAllContactsFromServerByPage";



    /// EMChatManager methods
    public static final String sendMessage = "sendMessage";
    public static final String resendMessage = "resendMessage";
    public static final String ackMessageRead = "ackMessageRead";
    public static final String ackGroupMessageRead = "ackGroupMessageRead";
    public static final String ackConversationRead = "ackConversationRead";
    public static final String recallMessage = "recallMessage";
    public static final String getConversation = "getConversation";
    public static final String getThreadConversation = "getThreadConversation";
    public static final String markAllChatMsgAsRead = "markAllChatMsgAsRead";
    public static final String getUnreadMessageCount = "getUnreadMessageCount";
    public static final String updateChatMessage = "updateChatMessage";
    public static final String downloadAttachment = "downloadAttachment";
    public static final String downloadThumbnail = "downloadThumbnail";
    public static final String importMessages = "importMessages";
    public static final String loadAllConversations = "loadAllConversations";
    public static final String getConversationsFromServer = "getConversationsFromServer";
    public static final String getConversationsFromServerWithCursor = "getConversationsFromServerWithCursor";
    public static final String getConversationsFromServerWithCursorAndMark = "getConversationsFromServerWithCursorAndMark";
    public static final String pinConversation = "pinConversation";
    public static final String deleteConversation = "deleteConversation";
    public static final String fetchHistoryMessages = "fetchHistoryMessages";
    public static final String fetchHistoryMessagesBy = "fetchHistoryMessagesBy";
    public static final String searchChatMsgFromDB = "searchChatMsgFromDB";
    public static final String searchChatMsgFromDBWithScope = "searchChatMsgFromDBWithScope";
    public static final String getMessage = "getMessage";
    public static final String asyncFetchGroupAcks = "asyncFetchGroupAcks";
    public static final String deleteRemoteConversation = "deleteRemoteConversation";
    public static final String deleteMessagesBeforeTimestamp = "deleteMessagesBeforeTimestamp";
    public static final String translateMessage = "translateMessage";
    public static final String fetchSupportedLanguages = "fetchSupportLanguages";
    public static final String addReaction = "addReaction";
    public static final String removeReaction = "removeReaction";
    public static final String fetchReactionList = "fetchReactionList";
    public static final String fetchReactionDetail = "fetchReactionDetail";
    public static final String reportMessage = "reportMessage";

    public static final String getConversationsFromServerWithPage = "getConversationsFromServerWithPage";

    public static final String removeMessagesFromServerWithMsgIds = "removeMessagesFromServerWithMsgIds";

    public static final String removeMessagesFromServerWithTs = "removeMessagesFromServerWithTs";
    public static final String modifyMessage = "modifyMessage";
    public static final String downloadCombineMessages = "downloadCombineMessages";
    public static final String markConversations = "markConversations";
    public static final String deleteAllMessagesAndConversations = "deleteAllMessagesAndConversations";
    public static final String pinMessage = "pinMessage";
    public static final String getPinnedMessagesFromServer = "getPinnedMessagesFromServer";


    /// EMMessage listener
    public static final String onMessageProgressUpdate = "onMessageProgressUpdate";
    public static final String onMessageError = "onMessageError";
    public static final String onMessageSuccess = "onMessageSuccess";
    public static final String onMessageReadAck = "onMessageReadAck";
    public static final String onMessageDeliveryAck = "onMessageDeliveryAck";

    /// EMConversation
    public static final String getConversationUnreadMsgCount = "getConversationUnreadMsgCount";
    public static final String markAllMessagesAsRead = "markAllMessagesAsRead";
    public static final String markMessageAsRead = "markMessageAsRead";
    public static final String syncConversationExt = "syncConversationExt";
    public static final String removeMessage = "removeMessage";
    public static final String removeMessages = "removeMessages";
    public static final String getLatestMessage = "getLatestMessage";
    public static final String getLatestMessageFromOthers = "getLatestMessageFromOthers";
    public static final String clearAllMessages = "clearAllMessages";
    public static final String insertMessage = "insertMessage";
    public static final String appendMessage = "appendMessage";
    public static final String updateConversationMessage = "updateConversationMessage";
    public static final String loadMsgWithId = "loadMsgWithId";
    public static final String loadMsgWithStartId = "loadMsgWithStartId";
    public static final String loadMsgWithKeywords = "loadMsgWithKeywords";
    public static final String loadMsgWithMsgType = "loadMsgWithMsgType";
    public static final String loadMsgWithTime = "loadMsgWithTime";
    public static final String loadMsgWithScope = "loadMsgWithScope";
    public static final String messageCount = "messageCount";
    public static final String pinnedMessages = "pinnedMessages";
    public static final String marks = "marks";

    // EMMessage method
    public static final String getReactionList = "getReactionList";
    public static final String groupAckCount = "groupAckCount";
    public static final String getChatThread = "chatThread";
    public static final String getHasDeliverAck = "getHasDeliverAck";
    public static final String getHasReadAck = "getHasReadAck";
    public static final String pinnedInfo = "pinnedInfo";


    // EMChatRoomManager
    public static final String joinChatRoom = "joinChatRoom";
    public static final String leaveChatRoom = "leaveChatRoom";
    public static final String fetchPublicChatRoomsFromServer = "fetchPublicChatRoomsFromServer";
    public static final String fetchChatRoomInfoFromServer = "fetchChatRoomInfoFromServer";
    public static final String getChatRoom = "getChatRoom";
    public static final String getAllChatRooms = "getAllChatRooms";
    public static final String createChatRoom = "createChatRoom";
    public static final String destroyChatRoom = "destroyChatRoom";
    public static final String changeChatRoomSubject = "changeChatRoomSubject";
    public static final String changeChatRoomDescription = "changeChatRoomDescription";
    public static final String fetchChatRoomMembers = "fetchChatRoomMembers";
    public static final String muteChatRoomMembers = "muteChatRoomMembers";
    public static final String unMuteChatRoomMembers = "unMuteChatRoomMembers";
    public static final String changeChatRoomOwner = "changeChatRoomOwner";
    public static final String addChatRoomAdmin = "addChatRoomAdmin";
    public static final String removeChatRoomAdmin = "removeChatRoomAdmin";
    public static final String fetchChatRoomMuteList = "fetchChatRoomMuteList";
    public static final String removeChatRoomMembers = "removeChatRoomMembers";
    public static final String blockChatRoomMembers = "blockChatRoomMembers";
    public static final String unBlockChatRoomMembers = "unBlockChatRoomMembers";
    public static final String fetchChatRoomBlockList = "fetchChatRoomBlockList";
    public static final String updateChatRoomAnnouncement = "updateChatRoomAnnouncement";
    public static final String fetchChatRoomAnnouncement = "fetchChatRoomAnnouncement";

    public static final String addMembersToChatRoomWhiteList = "addMembersToChatRoomWhiteList";
    public static final String removeMembersFromChatRoomWhiteList = "removeMembersFromChatRoomWhiteList";
    public static final String fetchChatRoomWhiteListFromServer = "fetchChatRoomWhiteListFromServer";
    public static final String isMemberInChatRoomWhiteListFromServer = "isMemberInChatRoomWhiteListFromServer";

    public static final String muteAllChatRoomMembers = "muteAllChatRoomMembers";
    public static final String unMuteAllChatRoomMembers = "unMuteAllChatRoomMembers";
    public static final String fetchChatRoomAttributes = "fetchChatRoomAttributes";
    public static final String setChatRoomAttributes = "setChatRoomAttributes";
    public static final String removeChatRoomAttributes = "removeChatRoomAttributes";


    /// EMGroupManager
    public static final String getGroupWithId = "getGroupWithId";
    public static final String getJoinedGroups = "getJoinedGroups";
    public static final String getGroupsWithoutPushNotification = "getGroupsWithoutPushNotification";
    public static final String getJoinedGroupsFromServer = "getJoinedGroupsFromServer";
    public static final String getJoinedGroupsFromServerSimple = "getJoinedGroupsFromServerSimple";
    public static final String getPublicGroupsFromServer = "getPublicGroupsFromServer";
    public static final String createGroup = "createGroup";
    public static final String getGroupSpecificationFromServer = "getGroupSpecificationFromServer";
    public static final String getGroupMemberListFromServer = "getGroupMemberListFromServer";
    public static final String getGroupBlockListFromServer = "getGroupBlockListFromServer";
    public static final String getGroupMuteListFromServer = "getGroupMuteListFromServer";
    public static final String getGroupWhiteListFromServer = "getGroupWhiteListFromServer";
    public static final String isMemberInWhiteListFromServer = "isMemberInWhiteListFromServer";
    public static final String getGroupFileListFromServer = "getGroupFileListFromServer";
    public static final String getGroupAnnouncementFromServer = "getGroupAnnouncementFromServer";
    public static final String addMembers = "addMembers";
    public static final String inviterUser = "inviterUser";
    public static final String removeMembers = "removeMembers";
    public static final String blockMembers = "blockMembers";
    public static final String unblockMembers = "unblockMembers";
    public static final String updateGroupSubject = "updateGroupSubject";
    public static final String updateDescription = "updateDescription";
    public static final String leaveGroup = "leaveGroup";
    public static final String destroyGroup = "destroyGroup";
    public static final String blockGroup = "blockGroup";
    public static final String unblockGroup = "unblockGroup";
    public static final String updateGroupOwner = "updateGroupOwner";
    public static final String addAdmin = "addAdmin";
    public static final String removeAdmin = "removeAdmin";
    public static final String muteMembers = "muteMembers";
    public static final String unMuteMembers = "unMuteMembers";
    public static final String muteAllMembers = "muteAllMembers";
    public static final String unMuteAllMembers = "unMuteAllMembers";
    public static final String addWhiteList = "addWhiteList";
    public static final String removeWhiteList = "removeWhiteList";
    public static final String uploadGroupSharedFile = "uploadGroupSharedFile";
    public static final String downloadGroupSharedFile = "downloadGroupSharedFile";
    public static final String removeGroupSharedFile = "removeGroupSharedFile";
    public static final String updateGroupAnnouncement = "updateGroupAnnouncement";
    public static final String updateGroupExt = "updateGroupExt";
    public static final String joinPublicGroup = "joinPublicGroup";
    public static final String requestToJoinGroup = "requestToJoinGroup";
    public static final String acceptJoinApplication = "acceptJoinApplication";
    public static final String declineJoinApplication = "declineJoinApplication";
    public static final String acceptInvitationFromGroup = "acceptInvitationFromGroup";
    public static final String declineInvitationFromGroup = "declineInvitationFromGroup";
    public static final String fetchMemberAttributes = "fetchMemberAttributes";
    public static final String setMemberAttributes = "setMemberAttributes";
    public static final String fetchMyGroupsCount = "fetchMyGroupsCount";


    /// EMPushManager
    public static final String getImPushConfig = "getImPushConfig";
    public static final String getImPushConfigFromServer = "getImPushConfigFromServer";
    public static final String enableOfflinePush = "enableOfflinePush";
    public static final String disableOfflinePush = "disableOfflinePush";
    public static final String updateImPushStyle = "updateImPushStyle";
    public static final String updatePushNickname = "updatePushNickname";

    public static final String updateGroupPushService = "updateGroupPushService";
    public static final String getNoPushGroups = "getNoPushGroups";
    public static final String updateUserPushService = "updateUserPushService";
    public static final String getNoPushUsers = "getNoPushUsers";

    public static final String updateHMSPushToken = "updateHMSPushToken";
    public static final String updateFCMPushToken = "updateFCMPushToken";


    public static final String reportPushAction = "reportPushAction";
    public static final String setConversationSilentMode = "setConversationSilentMode";
    public static final String removeConversationSilentMode = "removeConversationSilentMode";
    public static final String fetchConversationSilentMode = "fetchConversationSilentMode";
    public static final String setSilentModeForAll = "setSilentModeForAll";
    public static final String fetchSilentModeForAll = "fetchSilentModeForAll";
    public static final String fetchSilentModeForConversations = "fetchSilentModeForConversations";
    public static final String setPreferredNotificationLanguage = "setPreferredNotificationLanguage";
    public static final String fetchPreferredNotificationLanguage = "fetchPreferredNotificationLanguage";
    public static final String setPushTemplate = "setPushTemplate";
    public static final String getPushTemplate = "getPushTemplate";

    /// EMUserInfoManager 
    public static final String updateOwnUserInfo = "updateOwnUserInfo";
    public static final String updateOwnUserInfoWithType = "updateOwnUserInfoWithType";
    public static final String fetchUserInfoById = "fetchUserInfoById";
    public static final String fetchUserInfoByIdWithType = "fetchUserInfoByIdWithType";

    /// EMPresenceManager methods
    public static final String presenceWithDescription = "publishPresenceWithDescription";
    public static final String presenceSubscribe = "presenceSubscribe";
    public static final String presenceUnsubscribe = "presenceUnsubscribe";
    public static final String fetchSubscribedMembersWithPageNum = "fetchSubscribedMembersWithPageNum";
    public static final String fetchPresenceStatus = "fetchPresenceStatus";


    /// EMChatThreadManager methods
    public static final String fetchChatThreadDetail = "fetchChatThreadDetail";
    public static final String fetchJoinedChatThreads = "fetchJoinedChatThreads";
    public static final String fetchChatThreadsWithParentId = "fetchChatThreadsWithParentId";
    public static final String fetchChatThreadMember = "fetchChatThreadMember";
    public static final String fetchLastMessageWithChatThreads = "fetchLastMessageWithChatThreads";
    public static final String removeMemberFromChatThread = "removeMemberFromChatThread";
    public static final String updateChatThreadSubject = "updateChatThreadSubject";
    public static final String createChatThread = "createChatThread";
    public static final String joinChatThread = "joinChatThread";
    public static final String leaveChatThread = "leaveChatThread";
    public static final String destroyChatThread = "destroyChatThread";



    /// HandleAction ?
    public static final String startCallback = "startCallback";



    // Listeners name
    public static final String connectionListener = "connectionListener";
    public static final String multiDeviceListener = "multiDeviceListener";
    public static final String chatListener = "chatListener";
    public static final String contactListener = "contactListener";
    public static final String groupListener = "groupListener";
    public static final String chatRoomListener = "chatRoomListener";
    public static final String chatThreadListener = "chatThreadListener";
    public static final String presenceListener = "presenceListener";
    public static final String callback = "callback";
    public static final String callbackProgress = "callbackProgress";

    // ChatManagerDelegate
    public static final String onMessagesReceived = "onMessagesReceived";
    public static final String onCmdMessagesReceived = "onCmdMessagesReceived";
    public static final String onMessagesRead = "onMessagesRead";
    public static final String onGroupMessageRead = "onGroupMessageRead";
    public static final String onReadAckForGroupMessageUpdated = "onReadAckForGroupMessageUpdated";
    public static final String onMessagesDelivered = "onMessagesDelivered";
    public static final String onMessagesRecalled = "onMessagesRecalled";
    public static final String onConversationsUpdate = "onConversationsUpdate";
    public static final String onConversationRead = "onConversationRead";
    public static final String onMessageReactionDidChange = "messageReactionDidChange";
    public static final String onMessageContentChanged = "onMessageContentChanged";
    public static final String onMessagePinChanged = "onMessagePinChanged";

    // ChatThreadManagerDelegate
    public static final String onChatThreadCreate = "onChatThreadCreate";
    public static final String onChatThreadUpdate = "onChatThreadUpdate";
    public static final String onChatThreadDestroy = "onChatThreadDestroy";
    public static final String onUserKickOutOfChatThread = "onUserKickOutOfChatThread";

    // ContactManagerDelegate
    public static final String onContactAdded = "onContactAdded";
    public static final String onContactDeleted = "onContactDeleted";
    public static final String onContactInvited = "onContactInvited";
    public static final String onFriendRequestAccepted = "onFriendRequestAccepted";
    public static final String onFriendRequestDeclined = "onFriendRequestDeclined";

    // MultiDeviceDelegate
    public static final String onContactMultiDevicesEvent = "onContactMultiDevicesEvent";
    public static final String onGroupMultiDevicesEvent = "onGroupMultiDevicesEvent";
    public static final String onUnDisturbMultiDevicesEvent = "onUnDisturbMultiDevicesEvent";
    public static final String onThreadMultiDevicesEvent = "onThreadMultiDevicesEvent";
    public static final String onRoamDeleteMultiDevicesEvent = "onRoamDeleteMultiDevicesEvent";
    public static final String onConversationMultiDevicesEvent = "onConversationMultiDevicesEvent";


    // PresenceManagerDelegate
    public static final String onPresenceUpdated = "onPresenceUpdated";

    // ConnectionDelegate
    public static final String onConnected = "onConnected";
    public static final String onDisconnected = "onDisconnected";
    public static final String onLoggedOtherDevice = "onLoggedOtherDevice";
    public static final String onRemovedFromServer = "onRemovedFromServer";
    public static final String onForbidByServer = "onForbidByServer";
    public static final String onAppActiveNumberReachLimitation = "onAppActiveNumberReachLimitation";
    public static final String onChangedImPwd = "onChangedImPwd";
    public static final String onLoginTooManyDevice = "onLoginTooManyDevice";
    public static final String onKickedByOtherDevice = "onKickedByOtherDevice";
    public static final String onAuthFailed = "onAuthFailed";
    public static final String onTokenExpired = "onTokenExpired";
    public static final String onTokenWillExpire = "onTokenWillExpire";

    // GroupManagerDelegate
    public static final String onInvitationReceivedFromGroup = "onInvitationReceivedFromGroup";
    public static final String onRequestToJoinReceivedFromGroup = "onRequestToJoinReceivedFromGroup";
    public static final String onRequestToJoinAcceptedFromGroup = "onRequestToJoinAcceptedFromGroup";
    public static final String onRequestToJoinDeclinedFromGroup = "onRequestToJoinDeclinedFromGroup";
    public static final String onInvitationAcceptedFromGroup = "onInvitationAcceptedFromGroup";
    public static final String onInvitationDeclinedFromGroup = "onInvitationDeclinedFromGroup";
    public static final String onUserRemovedFromGroup = "onUserRemovedFromGroup";
    public static final String onDestroyedFromGroup = "onDestroyedFromGroup";
    public static final String onAutoAcceptInvitationFromGroup = "onAutoAcceptInvitationFromGroup";
    public static final String onMuteListAddedFromGroup = "onMuteListAddedFromGroup";
    public static final String onMuteListRemovedFromGroup = "onMuteListRemovedFromGroup";
    public static final String onAdminAddedFromGroup = "onAdminAddedFromGroup";
    public static final String onAdminRemovedFromGroup = "onAdminRemovedFromGroup";
    public static final String onOwnerChangedFromGroup = "onOwnerChangedFromGroup";
    public static final String onMemberJoinedFromGroup = "onMemberJoinedFromGroup";
    public static final String onMemberExitedFromGroup = "onMemberExitedFromGroup";
    public static final String onAnnouncementChangedFromGroup = "onAnnouncementChangedFromGroup";
    public static final String onSharedFileAddedFromGroup = "onSharedFileAddedFromGroup";
    public static final String onSharedFileDeletedFromGroup = "onSharedFileDeletedFromGroup";
    public static final String onAddAllowListMembersFromGroup = "onAddWhiteListMembersFromGroup";
    public static final String onRemoveAllowListMembersFromGroup = "onRemoveWhiteListMembersFromGroup";
    public static final String onAllMemberMuteChangedFromGroup = "onAllMemberMuteChangedFromGroup";
    public static final String onSpecificationChangedFromGroup = "onSpecificationChangedFromGroup";
    public static final String onStateChangedFromGroup = "onStateChangedFromGroup";
    public static final String onUpdateMemberAttributesFromGroup = "onUpdateMemberAttributesFromGroup";
    // RoomManagerDelegate
    public static final String onDestroyedFromRoom = "onDestroyedFromRoom";
    public static final String onMemberJoinedFromRoom = "onMemberJoinedFromRoom";
    public static final String onMemberExitedFromRoom = "onMemberExitedFromRoom";
    public static final String onRemovedFromRoom = "onRemovedFromRoom";
    public static final String onRemoveFromRoomByOffline = "onRemoveFromRoomByOffline";
    public static final String onMuteListAddedFromRoom = "onMuteListAddedFromRoom";
    public static final String onMuteListRemovedFromRoom = "onMuteListRemovedFromRoom";
    public static final String onAdminAddedFromRoom = "onAdminAddedFromRoom";
    public static final String onAdminRemovedFromRoom = "onAdminRemovedFromRoom";
    public static final String onOwnerChangedFromRoom = "onOwnerChangedFromRoom";
    public static final String onAnnouncementChangedFromRoom = "onAnnouncementChangedFromRoom";
    public static final String onAttributesChangedFromRoom = "onAttributesChangedFromRoom";
    public static final String onAttributesRemovedFromRoom = "onAttributesRemovedFromRoom";
    public static final String onSpecificationChangedFromRoom = "onSpecificationChangedFromRoom";
    public static final String onAddAllowListMembersFromRoom = "onAddWhiteListMembersFromRoom";
    public static final String onRemoveAllowListMembersFromRoom = "onRemoveWhiteListMembersFromRoom";
    public static final String onAllMemberMuteChangedFromRoom = "onAllMemberMuteChangedFromRoom";
}
