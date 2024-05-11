//
//  EMSDKMethod.h
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/15.
//


#import <Foundation/Foundation.h>

static NSString *const runDelegateTester = @"runDelegateTester";

// client or manager
static NSString *const client = @"EMClient";
static NSString *const chatManager = @"EMChatManager";
static NSString *const contactManager = @"EMContactManager";
static NSString *const groupManager = @"EMGroupManager";
static NSString *const roomManager = @"EMRoomManager";
static NSString *const userInfoManager = @"EMUserInfoManager";
static NSString *const threadManager = @"EMThreadManager";
static NSString *const presenceManager = @"EMPresenceManager";
static NSString *const pushManager = @"EMPushManager";

static NSString *const messageManager = @"EMMessageManager";
static NSString *const conversationManager = @"EMConversationManager";

/// EMClient methods
static NSString *const init = @"init";
static NSString *const createAccount = @"createAccount";
static NSString *const login = @"login";
static NSString *const loginWithAgoraToken = @"loginWithAgoraToken";
static NSString *const renewToken = @"renewToken";
static NSString *const logout = @"logout";
static NSString *const changeAppKey = @"changeAppKey";

static NSString *const uploadLog = @"uploadLog";
static NSString *const compressLogs = @"compressLogs";
static NSString *const kickDevice = @"kickDevice";
static NSString *const kickAllDevices = @"kickAllDevices";
static NSString *const getLoggedInDevicesFromServer = @"getLoggedInDevicesFromServer";
static NSString *const kickDeviceWithToken = @"kickDeviceWithToken";
static NSString *const kickAllDevicesWithToken = @"kickAllDevicesWithToken";
static NSString *const getLoggedInDevicesFromServerWithToken = @"getLoggedInDevicesFromServerWithToken";



static NSString *const getToken = @"getToken";
static NSString *const getCurrentUser = @"getCurrentUser";
static NSString *const isLoggedInBefore = @"isLoggedInBefore";
static NSString *const isConnected = @"isConnected";


/// EMContactManager methods
static NSString *const addContact = @"addContact";
static NSString *const deleteContact = @"deleteContact";
static NSString *const getAllContactsFromServer = @"getAllContactsFromServer";
static NSString *const getAllContactsFromDB = @"getAllContactsFromDB";
static NSString *const addUserToBlockList = @"addUserToBlockList";
static NSString *const removeUserFromBlockList = @"removeUserFromBlockList";
static NSString *const getBlockListFromServer = @"getBlockListFromServer";
static NSString *const getBlockListFromDB = @"getBlockListFromDB";
static NSString *const acceptInvitation = @"acceptInvitation";
static NSString *const declineInvitation = @"declineInvitation";
static NSString *const getSelfIdsOnOtherPlatform = @"getSelfIdsOnOtherPlatform";
static NSString *const setContactRemark = @"setContactRemark";
static NSString *const fetchContactFromLocal = @"fetchContactFromLocal";
static NSString *const fetchAllContactsFromLocal = @"fetchAllContactsFromLocal";
static NSString *const fetchAllContactsFromServer = @"fetchAllContactsFromServer";
static NSString *const fetchAllContactsFromServerByPage = @"fetchAllContactsFromServerByPage";


/// EMChatManager methods
static NSString *const sendMessage = @"sendMessage";
static NSString *const resendMessage = @"resendMessage";
static NSString *const ackMessageRead = @"ackMessageRead";
static NSString *const ackGroupMessageRead = @"ackGroupMessageRead";
static NSString *const ackConversationRead = @"ackConversationRead";
static NSString *const recallMessage = @"recallMessage";
static NSString *const getConversation = @"getConversation";
static NSString *const getThreadConversation = @"getThreadConversation";
static NSString *const markAllChatMsgAsRead = @"markAllChatMsgAsRead";
static NSString *const getUnreadMessageCount = @"getUnreadMessageCount";
static NSString *const updateChatMessage = @"updateChatMessage";
static NSString *const downloadAttachment = @"downloadAttachment";
static NSString *const downloadThumbnail = @"downloadThumbnail";
static NSString *const importMessages = @"importMessages";
static NSString *const loadAllConversations = @"loadAllConversations";
static NSString *const getConversationsFromServer = @"getConversationsFromServer";
static NSString *const getConversationsFromServerWithCursor = @"getConversationsFromServerWithCursor";
static NSString *const getConversationsFromServerWithCursorAndMark = @"getConversationsFromServerWithCursorAndMark";
static NSString *const pinConversation = @"pinConversation";
static NSString *const modifyMessage = @"modifyMessage";
static NSString *const deleteConversation = @"deleteConversation";
static NSString *const fetchHistoryMessages = @"fetchHistoryMessages";
static NSString *const fetchHistoryMessagesBy = @"fetchHistoryMessagesBy";
static NSString *const searchChatMsgFromDB = @"searchChatMsgFromDB";
static NSString *const searchChatMsgFromDBWithScope = @"searchChatMsgFromDBWithScope";
static NSString *const getMessage = @"getMessage";
static NSString *const asyncFetchGroupAcks = @"asyncFetchGroupAcks";
static NSString *const deleteRemoteConversation = @"deleteRemoteConversation";
static NSString *const deleteMessagesBeforeTimestamp = @"deleteMessagesBeforeTimestamp";
static NSString *const translateMessage = @"translateMessage";
static NSString *const fetchSupportedLanguages = @"fetchSupportLanguages";
static NSString *const addReaction = @"addReaction";
static NSString *const removeReaction = @"removeReaction";
static NSString *const fetchReactionList = @"fetchReactionList";
static NSString *const fetchReactionDetail = @"fetchReactionDetail";
static NSString *const reportMessage = @"reportMessage";
static NSString *const fetchConversationsFromServerWithPage = @"fetchConversationsFromServerWithPage";
static NSString *const removeMessagesFromServerWithMsgIds = @"removeMessagesFromServerWithMsgIds";
static NSString *const removeMessagesFromServerWithTs = @"removeMessagesFromServerWithTs";
static NSString *const downloadCombineMessages = @"downloadCombineMessages";
static NSString *const markConversations = @"markConversations";
static NSString *const deleteAllMessagesAndConversations = @"deleteAllMessagesAndConversations";
static NSString *const pinMessage = @"pinMessage";
static NSString *const getPinnedMessagesFromServer = @"getPinnedMessagesFromServer";

/// EMMessage listener
static NSString *const onMessageProgressUpdate = @"onMessageProgressUpdate";
static NSString *const onMessageError = @"onMessageError";
static NSString *const onMessageSuccess = @"onMessageSuccess";
static NSString *const onMessageReadAck = @"onMessageReadAck";
static NSString *const onMessageDeliveryAck = @"onMessageDeliveryAck";

/// EMConversation
static NSString *const getConversationUnreadMsgCount = @"getConversationUnreadMsgCount";
static NSString *const markAllMessagesAsRead = @"markAllMessagesAsRead";
static NSString *const markMessageAsRead = @"markMessageAsRead";
static NSString *const syncConversationExt = @"syncConversationExt";
static NSString *const removeMessage = @"removeMessage";
static NSString *const removeMessages = @"removeMessages";
static NSString *const getLatestMessage = @"getLatestMessage";
static NSString *const getLatestMessageFromOthers = @"getLatestMessageFromOthers";
static NSString *const clearAllMessages = @"clearAllMessages";
static NSString *const insertMessage = @"insertMessage";
static NSString *const appendMessage = @"appendMessage";
static NSString *const updateConversationMessage = @"updateConversationMessage";
static NSString *const loadMsgWithId = @"loadMsgWithId";
static NSString *const loadMsgWithStartId = @"loadMsgWithStartId";
static NSString *const loadMsgWithKeywords = @"loadMsgWithKeywords";
static NSString *const loadMsgWithMsgType = @"loadMsgWithMsgType";
static NSString *const loadMsgWithTime = @"loadMsgWithTime";
static NSString *const loadMsgWithScope = @"loadMsgWithScope";
static NSString *const messageCount = @"messageCount";
static NSString *const pinnedMessages = @"pinnedMessages";
static NSString *const marks = @"marks";

// EMMessage method
static NSString *const getReactionList = @"getReactionList";
static NSString *const groupAckCount = @"groupAckCount";
static NSString *const getChatThread = @"chatThread";
static NSString *const getHasDeliverAck = @"getHasDeliverAck";
static NSString *const getHasReadAck = @"getHasReadAck";
static NSString *const getPinnedInfo = @"pinnedInfo";


// EMChatRoomManager
static NSString *const joinChatRoom = @"joinChatRoom";
static NSString *const leaveChatRoom = @"leaveChatRoom";
static NSString *const fetchPublicChatRoomsFromServer = @"fetchPublicChatRoomsFromServer";
static NSString *const fetchChatRoomInfoFromServer = @"fetchChatRoomInfoFromServer";
static NSString *const getChatRoom = @"getChatRoom";
static NSString *const getAllChatRooms = @"getAllChatRooms";
static NSString *const createChatRoom = @"createChatRoom";
static NSString *const destroyChatRoom = @"destroyChatRoom";
static NSString *const changeChatRoomSubject = @"changeChatRoomSubject";
static NSString *const changeChatRoomDescription = @"changeChatRoomDescription";
static NSString *const fetchChatRoomMembers = @"fetchChatRoomMembers";
static NSString *const muteChatRoomMembers = @"muteChatRoomMembers";
static NSString *const unMuteChatRoomMembers = @"unMuteChatRoomMembers";
static NSString *const changeChatRoomOwner = @"changeChatRoomOwner";
static NSString *const addChatRoomAdmin = @"addChatRoomAdmin";
static NSString *const removeChatRoomAdmin = @"removeChatRoomAdmin";
static NSString *const fetchChatRoomMuteList = @"fetchChatRoomMuteList";
static NSString *const removeChatRoomMembers = @"removeChatRoomMembers";
static NSString *const blockChatRoomMembers = @"blockChatRoomMembers";
static NSString *const unBlockChatRoomMembers = @"unBlockChatRoomMembers";
static NSString *const fetchChatRoomBlockList = @"fetchChatRoomBlockList";
static NSString *const updateChatRoomAnnouncement = @"updateChatRoomAnnouncement";
static NSString *const fetchChatRoomAnnouncement = @"fetchChatRoomAnnouncement";

static NSString *const addMembersToChatRoomWhiteList = @"addMembersToChatRoomWhiteList";
static NSString *const removeMembersFromChatRoomWhiteList = @"removeMembersFromChatRoomWhiteList";
static NSString *const fetchChatRoomWhiteListFromServer = @"fetchChatRoomWhiteListFromServer";
static NSString *const isMemberInChatRoomWhiteListFromServer = @"isMemberInChatRoomWhiteListFromServer";

static NSString *const muteAllChatRoomMembers = @"muteAllChatRoomMembers";
static NSString *const unMuteAllChatRoomMembers = @"unMuteAllChatRoomMembers";
static NSString *const fetchChatRoomAttributes = @"fetchChatRoomAttributes";
static NSString *const setChatRoomAttributes = @"setChatRoomAttributes";
static NSString *const removeChatRoomAttributes = @"removeChatRoomAttributes";


/// EMGroupManager
static NSString *const getGroupWithId = @"getGroupWithId";
static NSString *const getJoinedGroups = @"getJoinedGroups";
static NSString *const getGroupsWithoutPushNotification = @"getGroupsWithoutPushNotification";
static NSString *const getJoinedGroupsFromServer = @"getJoinedGroupsFromServer";
static NSString *const getJoinedGroupsFromServerSimple = @"getJoinedGroupsFromServerSimple";
static NSString *const getPublicGroupsFromServer = @"getPublicGroupsFromServer";
static NSString *const createGroup = @"createGroup";
static NSString *const getGroupSpecificationFromServer = @"getGroupSpecificationFromServer";
static NSString *const getGroupMemberListFromServer = @"getGroupMemberListFromServer";
static NSString *const getGroupBlockListFromServer = @"getGroupBlockListFromServer";
static NSString *const getGroupMuteListFromServer = @"getGroupMuteListFromServer";
static NSString *const getGroupWhiteListFromServer = @"getGroupWhiteListFromServer";
static NSString *const isMemberInWhiteListFromServer = @"isMemberInWhiteListFromServer";
static NSString *const getGroupFileListFromServer = @"getGroupFileListFromServer";
static NSString *const getGroupAnnouncementFromServer = @"getGroupAnnouncementFromServer";
static NSString *const addMembers = @"addMembers";
static NSString *const inviterUser = @"inviterUser";
static NSString *const removeMembers = @"removeMembers";
static NSString *const blockMembers = @"blockMembers";
static NSString *const unblockMembers = @"unblockMembers";
static NSString *const updateGroupSubject = @"updateGroupSubject";
static NSString *const updateDescription = @"updateDescription";
static NSString *const leaveGroup = @"leaveGroup";
static NSString *const destroyGroup = @"destroyGroup";
static NSString *const blockGroup = @"blockGroup";
static NSString *const unblockGroup = @"unblockGroup";
static NSString *const updateGroupOwner = @"updateGroupOwner";
static NSString *const addAdmin = @"addAdmin";
static NSString *const removeAdmin = @"removeAdmin";
static NSString *const muteMembers = @"muteMembers";
static NSString *const unMuteMembers = @"unMuteMembers";
static NSString *const muteAllMembers = @"muteAllMembers";
static NSString *const unMuteAllMembers = @"unMuteAllMembers";
static NSString *const addWhiteList = @"addWhiteList";
static NSString *const removeWhiteList = @"removeWhiteList";
static NSString *const uploadGroupSharedFile = @"uploadGroupSharedFile";
static NSString *const downloadGroupSharedFile = @"downloadGroupSharedFile";
static NSString *const removeGroupSharedFile = @"removeGroupSharedFile";
static NSString *const updateGroupAnnouncement = @"updateGroupAnnouncement";
static NSString *const updateGroupExt = @"updateGroupExt";
static NSString *const joinPublicGroup = @"joinPublicGroup";
static NSString *const requestToJoinGroup = @"requestToJoinGroup";
static NSString *const acceptJoinApplication = @"acceptJoinApplication";
static NSString *const declineJoinApplication = @"declineJoinApplication";
static NSString *const acceptInvitationFromGroup = @"acceptInvitationFromGroup";
static NSString *const declineInvitationFromGroup = @"declineInvitationFromGroup";
static NSString *const fetchMemberAttributes = @"fetchMemberAttributes";
static NSString *const setMemberAttributes = @"setMemberAttributes";
static NSString *const fetchMyGroupsCount = @"fetchMyGroupsCount";


/// EMPushManager
static NSString *const getImPushConfig = @"getImPushConfig";
static NSString *const getImPushConfigFromServer = @"getImPushConfigFromServer";
static NSString *const enableOfflinePush = @"enableOfflinePush";
static NSString *const disableOfflinePush = @"disableOfflinePush";
static NSString *const updateImPushStyle = @"updateImPushStyle";
static NSString *const updatePushNickname = @"updatePushNickname";

static NSString *const updateGroupPushService = @"updateGroupPushService";
static NSString *const getNoPushGroups = @"getNoPushGroups";
static NSString *const updateUserPushService = @"updateUserPushService";
static NSString *const getNoPushUsers = @"getNoPushUsers";

static NSString *const updateHMSPushToken = @"updateHMSPushToken";
static NSString *const updateFCMPushToken = @"updateFCMPushToken";


static NSString *const reportPushAction = @"reportPushAction";
static NSString *const setConversationSilentMode = @"setConversationSilentMode";
static NSString *const removeConversationSilentMode = @"removeConversationSilentMode";
static NSString *const fetchConversationSilentMode = @"fetchConversationSilentMode";
static NSString *const setSilentModeForAll = @"setSilentModeForAll";
static NSString *const fetchSilentModeForAll = @"fetchSilentModeForAll";
static NSString *const fetchSilentModeForConversations = @"fetchSilentModeForConversations";
static NSString *const setPreferredNotificationLanguage = @"setPreferredNotificationLanguage";
static NSString *const fetchPreferredNotificationLanguage = @"fetchPreferredNotificationLanguage";
static NSString *const setPushTemplate = @"setPushTemplate";
static NSString *const getPushTemplate = @"getPushTemplate";

/// EMUserInfoManager
static NSString *const updateOwnUserInfo = @"updateOwnUserInfo";
static NSString *const updateOwnUserInfoWithType = @"updateOwnUserInfoWithType";
static NSString *const fetchUserInfoById = @"fetchUserInfoById";
static NSString *const fetchUserInfoByIdWithType = @"fetchUserInfoByIdWithType";

/// EMPresenceManager methods
static NSString *const presenceWithDescription = @"publishPresenceWithDescription";
static NSString *const presenceSubscribe = @"presenceSubscribe";
static NSString *const presenceUnsubscribe = @"presenceUnsubscribe";
static NSString *const fetchSubscribedMembersWithPageNum = @"fetchSubscribedMembersWithPageNum";
static NSString *const fetchPresenceStatus = @"fetchPresenceStatus";


/// EMChatThreadManager methods
static NSString *const fetchChatThreadDetail = @"fetchChatThreadDetail";
static NSString *const fetchJoinedChatThreads = @"fetchJoinedChatThreads";
static NSString *const fetchChatThreadsWithParentId = @"fetchChatThreadsWithParentId";
static NSString *const fetchChatThreadMember = @"fetchChatThreadMember";
static NSString *const fetchLastMessageWithChatThreads = @"fetchLastMessageWithChatThreads";
static NSString *const removeMemberFromChatThread = @"removeMemberFromChatThread";
static NSString *const updateChatThreadSubject = @"updateChatThreadSubject";
static NSString *const createChatThread = @"createChatThread";
static NSString *const joinChatThread = @"joinChatThread";
static NSString *const leaveChatThread = @"leaveChatThread";
static NSString *const destroyChatThread = @"destroyChatThread";



/// HandleAction ?
static NSString *const startCallback = @"startCallback";



// Listeners name
static NSString *const connectionListener = @"connectionListener";
static NSString *const multiDeviceListener = @"multiDeviceListener";
static NSString *const chatListener = @"chatListener";
static NSString *const contactListener = @"contactListener";
static NSString *const groupListener = @"groupListener";
static NSString *const chatRoomListener = @"chatRoomListener";
static NSString *const chatThreadListener = @"chatThreadListener";
static NSString *const presenceListener = @"presenceListener";
static NSString *const callback = @"callback";
static NSString *const callbackProgress = @"callbackProgress";

// ChatManagerDelegate
static NSString *const onMessagesReceived = @"onMessagesReceived";
static NSString *const onCmdMessagesReceived = @"onCmdMessagesReceived";
static NSString *const onMessagesRead = @"onMessagesRead";
static NSString *const onGroupMessageRead = @"onGroupMessageRead";
static NSString *const onReadAckForGroupMessageUpdated = @"onReadAckForGroupMessageUpdated";
static NSString *const onMessagesDelivered = @"onMessagesDelivered";
static NSString *const onMessagesRecalled = @"onMessagesRecalled";
static NSString *const onConversationsUpdate = @"onConversationsUpdate";
static NSString *const onConversationRead = @"onConversationRead";
static NSString *const onMessageReactionDidChange = @"messageReactionDidChange";
static NSString *const onMessageContentChanged = @"onMessageContentChanged";
static NSString *const onMessagePinChanged = @"onMessagePinChanged";

// ChatThreadManagerDelegate
static NSString *const onChatThreadCreate = @"onChatThreadCreate";
static NSString *const onChatThreadUpdate = @"onChatThreadUpdate";
static NSString *const onChatThreadDestroy = @"onChatThreadDestroy";
static NSString *const onUserKickOutOfChatThread = @"onUserKickOutOfChatThread";

// ContactManagerDelegate
static NSString *const onContactAdded = @"onContactAdded";
static NSString *const onContactDeleted = @"onContactDeleted";
static NSString *const onContactInvited = @"onContactInvited";
static NSString *const onFriendRequestAccepted = @"onFriendRequestAccepted";
static NSString *const onFriendRequestDeclined = @"onFriendRequestDeclined";

// MultiDeviceDelegate
static NSString *const onContactMultiDevicesEvent = @"onContactMultiDevicesEvent";
static NSString *const onGroupMultiDevicesEvent = @"onGroupMultiDevicesEvent";
static NSString *const onUnDisturbMultiDevicesEvent = @"onUnDisturbMultiDevicesEvent";
static NSString *const onThreadMultiDevicesEvent = @"onThreadMultiDevicesEvent";
static NSString *const onRoamDeleteMultiDevicesEvent = @"onRoamDeleteMultiDevicesEvent";
static NSString *const onConversationMultiDevicesEvent = @"onConversationMultiDevicesEvent";

// PresenceManagerDelegate
static NSString *const onPresenceUpdated = @"onPresenceUpdated";

// ConnectionDelegate
static NSString *const onConnected = @"onConnected";
static NSString *const onDisconnected = @"onDisconnected";
static NSString *const onLoggedOtherDevice = @"onLoggedOtherDevice";
static NSString *const onRemovedFromServer = @"onRemovedFromServer";
static NSString *const onForbidByServer = @"onForbidByServer";
static NSString *const onChangedImPwd = @"onChangedImPwd";
static NSString *const onLoginTooManyDevice = @"onLoginTooManyDevice";
static NSString *const onKickedByOtherDevice = @"onKickedByOtherDevice";
static NSString *const onAuthFailed = @"onAuthFailed";
static NSString *const onTokenExpired = @"onTokenExpired";
static NSString *const onTokenWillExpire = @"onTokenWillExpire";
static NSString *const onAppActiveNumberReachLimitation = @"onAppActiveNumberReachLimitation";

// GroupManagerDeleagate
static NSString *const onInvitationReceivedFromGroup = @"onInvitationReceivedFromGroup";
static NSString *const onRequestToJoinReceivedFromGroup = @"onRequestToJoinReceivedFromGroup";
static NSString *const onRequestToJoinAcceptedFromGroup = @"onRequestToJoinAcceptedFromGroup";
static NSString *const onRequestToJoinDeclinedFromGroup = @"onRequestToJoinDeclinedFromGroup";
static NSString *const onInvitationAcceptedFromGroup = @"onInvitationAcceptedFromGroup";
static NSString *const onInvitationDeclinedFromGroup = @"onInvitationDeclinedFromGroup";
static NSString *const onUserRemovedFromGroup = @"onUserRemovedFromGroup";
static NSString *const onDestroyedFromGroup = @"onDestroyedFromGroup";
static NSString *const onAutoAcceptInvitationFromGroup = @"onAutoAcceptInvitationFromGroup";
static NSString *const onMuteListAddedFromGroup = @"onMuteListAddedFromGroup";
static NSString *const onMuteListRemovedFromGroup = @"onMuteListRemovedFromGroup";
static NSString *const onAdminAddedFromGroup = @"onAdminAddedFromGroup";
static NSString *const onAdminRemovedFromGroup = @"onAdminRemovedFromGroup";
static NSString *const onOwnerChangedFromGroup = @"onOwnerChangedFromGroup";
static NSString *const onMemberJoinedFromGroup = @"onMemberJoinedFromGroup";
static NSString *const onMemberExitedFromGroup = @"onMemberExitedFromGroup";
static NSString *const onAnnouncementChangedFromGroup = @"onAnnouncementChangedFromGroup";
static NSString *const onSharedFileAddedFromGroup = @"onSharedFileAddedFromGroup";
static NSString *const onSharedFileDeletedFromGroup = @"onSharedFileDeletedFromGroup";
static NSString *const onAddAllowListMembersFromGroup = @"onAddWhiteListMembersFromGroup";
static NSString *const onRemoveAllowListMembersFromGroup = @"onRemoveWhiteListMembersFromGroup";
static NSString *const onAllMemberMuteChangedFromGroup = @"onAllMemberMuteChangedFromGroup";
static NSString *const onSpecificationChangedFromGroup = @"onSpecificationChangedFromGroup";
static NSString *const onStateChangedFromGroup = @"onStateChangedFromGroup";
static NSString *const onUpdateMemberAttributesFromGroup = @"onUpdateMemberAttributesFromGroup";
// RoomManagerDelegate
static NSString *const onDestroyedFromRoom = @"onDestroyedFromRoom";
static NSString *const onMemberJoinedFromRoom = @"onMemberJoinedFromRoom";
static NSString *const onMemberExitedFromRoom = @"onMemberExitedFromRoom";
static NSString *const onRemovedFromRoom = @"onRemovedFromRoom";
static NSString *const onRemoveFromRoomByOffline = @"onRemoveFromRoomByOffline";
static NSString *const onMuteListAddedFromRoom = @"onMuteListAddedFromRoom";
static NSString *const onMuteListRemovedFromRoom = @"onMuteListRemovedFromRoom";
static NSString *const onAdminAddedFromRoom = @"onAdminAddedFromRoom";
static NSString *const onAdminRemovedFromRoom = @"onAdminRemovedFromRoom";
static NSString *const onOwnerChangedFromRoom = @"onOwnerChangedFromRoom";
static NSString *const onAnnouncementChangedFromRoom = @"onAnnouncementChangedFromRoom";
static NSString *const onAttributesChangedFromRoom = @"onAttributesChangedFromRoom";
static NSString *const onAttributesRemovedFromRoom = @"onAttributesRemovedFromRoom";
static NSString *const onSpecificationChangedFromRoom = @"onSpecificationChangedFromRoom";
static NSString *const onAddAllowListMembersFromRoom = @"onAddWhiteListMembersFromRoom";
static NSString *const onRemoveAllowListMembersFromRoom = @"onRemoveWhiteListMembersFromRoom";
static NSString *const onAllMemberMuteChangedFromRoom = @"onAllMemberMuteChangedFromRoom";


