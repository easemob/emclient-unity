using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace ChatSDK
{
    //IConnectionDelegate
    internal delegate void OnDisconnected(int info);

    internal delegate void OnTokenNotificationed(int info, string desc);

    //IChatManagerDelegate
    internal delegate void OnMessagesReceived([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] IntPtr[] messages,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] MessageBodyType[] types, int size);

    internal delegate void OnCmdMessagesReceived([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] IntPtr[] messages,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] MessageBodyType[] types, int size);

    internal delegate void OnMessagesRead([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] IntPtr[] messages,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] MessageBodyType[] types, int size);

    internal delegate void OnMessagesDelivered([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] IntPtr[] messages,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] MessageBodyType[] types, int size);

    internal delegate void OnMessagesRecalled([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] IntPtr[] messages,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] MessageBodyType[] types, int size);

    internal delegate void OnReadAckForGroupMessageUpdated();

    internal delegate void OnGroupMessageRead([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] IntPtr[] acks, int size);
    internal delegate void OnConversationsUpdate();
    internal delegate void OnConversationRead(string from, string to);

    //IGroupManagerDelegate
    internal delegate void OnInvitationReceived(string groupId, string groupName, string inviter, string reason);
    internal delegate void OnRequestToJoinReceived(string groupId, string groupName, string applicant, string reason);
    internal delegate void OnRequestToJoinAccepted(string groupId, string groupName, string accepter);
    internal delegate void OnRequestToJoinDeclined(string groupId, string groupName, string decliner, string reason);
    internal delegate void OnInvitationAccepted(string groupId, string invitee, string reason);
    internal delegate void OnInvitationDeclined(string groupId, string invitee, string reason);
    internal delegate void OnUserRemoved(string groupId, string groupName);
    internal delegate void OnGroupDestroyed(string groupId, string groupName);
    internal delegate void OnAutoAcceptInvitationFromGroup(string groupId, string inviter, string inviteMessage);
    internal delegate void OnMuteListAdded(string groupId, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] string[] mutes, int size, int muteExpire);
    internal delegate void OnMuteListRemoved(string groupId, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] string[] mutes, int size);
    internal delegate void OnAdminAdded(string groupId, string administrator);
    internal delegate void OnAdminRemoved(string groupId, string administrator);
    internal delegate void OnOwnerChanged(string groupId, string newOwner, string oldOwner);
    internal delegate void OnMemberJoined(string groupId, string member);
    internal delegate void OnMemberExited(string groupId, string member);
    internal delegate void OnAnnouncementChanged(string groupId, string announcement);
    internal delegate void OnSharedFileAdded(string groupId,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] IntPtr[] sharedFile, int size);
    internal delegate void OnSharedFileDeleted(string groupId, string fileId);

    //IRoomManagerDelegate, most of them are duplicated as IGroupManagerDelegate
    internal delegate void OnChatRoomDestroyed(string roomId, string roomName);
    internal delegate void OnRemovedFromChatRoom(string roomId, string roomName, string participant);
    internal delegate void OnMemberExitedFromRoom(string roomId, string roomName, string member);

    //IContactManagerDelegate
    internal delegate void OnContactAdd(string username);
    internal delegate void OnContactDeleted(string username);
    internal delegate void OnContactInvited(string username, string reason);
    internal delegate void OnFriendRequestAccepted(string username);
    internal delegate void OnFriendRequestDeclined(string username);

    //IMultiDeviceDelegate 
    internal delegate void onContactMultiDevicesEvent(MultiDevicesOperation operation, string target, string ext);
    internal delegate void onGroupMultiDevicesEvent(MultiDevicesOperation operation, string target, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] string[] usernames, int size);
    internal delegate void undisturbMultiDevicesEvent(string data);

    //IPresenceManagerDelegate
    internal delegate void OnPresenceUpdated([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] IntPtr[] presences, int size);

    internal sealed class ConnectionHub
    {
        //events handler
        internal Action OnConnected;
        internal OnDisconnected OnDisconnected;
        internal Action OnPong;
        internal OnTokenNotificationed OnTokenNotification;

        internal ConnectionHub(IClient client)
        {
            //register events
            OnConnected = () =>
            {
                Debug.Log("Connection established.");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IConnectionDelegate listener in CallbackManager.Instance().connectionListener.delegater)
                    {
                        listener.OnConnected();
                    }
                });
            };
            OnDisconnected = (int info) =>
            {
                Debug.Log("Connection discontinued.");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IConnectionDelegate listener in CallbackManager.Instance().connectionListener.delegater)
                    {
                        listener.OnDisconnected(info);
                    }
                });
            };
            OnPong = () =>
            {
                Debug.Log("Server ponged.");
                //ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                //    foreach (IConnectionDelegate listener in CallbackManager.Instance().connectionListener.delegater)
                //    {
                //        listener.OnPong();
                //    }
                //});
            };
            OnTokenNotification = (int info, string desc) =>
            {
                Debug.Log($"token notification received, with errid:{info}, desc:{desc}");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IConnectionDelegate listener in CallbackManager.Instance().connectionListener.delegater)
                    {
                        listener.OnTokenNotificationed(info, desc);
                    }
                });
            };

        }
    }

    internal sealed class MultiDevicesHub
    {
        internal onContactMultiDevicesEvent onContactMultiDevicesEvent;
        internal onGroupMultiDevicesEvent onGroupMultiDevicesEvent;
        internal undisturbMultiDevicesEvent undisturbMultiDevicesEvent;

        internal MultiDevicesHub()
        {
            onContactMultiDevicesEvent = (MultiDevicesOperation operation, string target, string ext) =>
            {
                Debug.Log("onContactMultiDevicesEvent received.");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IMultiDeviceDelegate listener in CallbackManager.Instance().multiDeviceListener.delegater)
                    {
                        listener.onContactMultiDevicesEvent(operation, target, ext);
                    }
                });
            };

            onGroupMultiDevicesEvent = (MultiDevicesOperation operation, string target, string[] usernames, int size) =>
            {
                Debug.Log("onGroupMultiDevicesEvent received.");

                var usernameList = new List<string>();
                for (int i = 0; i < size; i++)
                {
                    if(usernames[i].Length != 0)
                    {
                        usernameList.Add(usernames[i]);
                    }
                }

                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IMultiDeviceDelegate listener in CallbackManager.Instance().multiDeviceListener.delegater)
                    {
                        listener.onGroupMultiDevicesEvent(operation, target, usernameList);
                    }
                });
            };

            undisturbMultiDevicesEvent = (string data) =>
            {
                Debug.Log("undisturbMultiDevicesEvent received.");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IMultiDeviceDelegate listener in CallbackManager.Instance().multiDeviceListener.delegater)
                    {
                        listener.undisturbMultiDevicesEvent(data);
                    }
                });
            };
        }
    }

    internal sealed class ChatManagerHub
    {
        internal OnMessagesReceived OnMessagesReceived;
        internal OnCmdMessagesReceived OnCmdMessagesReceived;
        internal OnMessagesRead OnMessagesRead;
        internal OnMessagesDelivered OnMessagesDelivered;
        internal OnMessagesRecalled OnMessagesRecalled;
        internal OnReadAckForGroupMessageUpdated OnReadAckForGroupMessageUpdated;
        internal OnGroupMessageRead OnGroupMessageRead;
        internal OnConversationsUpdate OnConversationsUpdate;
        internal OnConversationRead OnConversationRead;

        internal ChatManagerHub()
        {
            OnMessagesReceived = (IntPtr[] _messages, MessageBodyType[] types, int size) =>
            {
                List<Message> messages = MessageTO.GetMessageListFromIntPtrArray(_messages, types, size);
                
                //chain-call to customer specified listeners
                Debug.Log($"Invoke customer listeners in OnMessagesReceived upon messages receiving...");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IChatManagerDelegate listener in CallbackManager.Instance().chatManagerListener.delegater)
                    {
                        listener.OnMessagesReceived(messages);
                    }
                });
            };

            OnCmdMessagesReceived = (IntPtr[] _messages, MessageBodyType[] types, int size) =>
            {
                List<Message> messages = MessageTO.GetMessageListFromIntPtrArray(_messages, types, size);

                //chain-call to customer specified listeners
                Debug.Log($"Invoke customer listeners in OnCmdMessagesReceived upon messages receiving...");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IChatManagerDelegate listener in CallbackManager.Instance().chatManagerListener.delegater)
                    {
                        listener.OnCmdMessagesReceived(messages);
                    }
                });
            };

            OnMessagesRead = (IntPtr[] _messages, MessageBodyType[] types, int size) =>
            {
                List<Message> messages = MessageTO.GetMessageListFromIntPtrArray(_messages, types, size);

                //chain-call to customer specified listeners
                Debug.Log($"Invoke customer listeners in OnMessagesRead upon messages receiving...");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IChatManagerDelegate listener in CallbackManager.Instance().chatManagerListener.delegater)
                    {
                        listener.OnMessagesRead(messages);
                    }
                });
            };

            OnMessagesDelivered = (IntPtr[] _messages, MessageBodyType[] types, int size) =>
            {
                List<Message> messages = MessageTO.GetMessageListFromIntPtrArray(_messages, types, size);

                //chain-call to customer specified listeners
                Debug.Log($"Invoke customer listeners in OnMessagesDelivered upon messages receiving...");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IChatManagerDelegate listener in CallbackManager.Instance().chatManagerListener.delegater)
                    {
                        listener.OnMessagesDelivered(messages);
                    }
                });
            };

            OnMessagesRecalled = (IntPtr[] _messages, MessageBodyType[] types, int size) =>
            {
                List<Message> messages = MessageTO.GetMessageListFromIntPtrArray(_messages, types, size);

                //chain-call to customer specified listeners
                Debug.Log($"Invoke customer listeners in OnMessagesRecalled upon messages receiving...");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IChatManagerDelegate listener in CallbackManager.Instance().chatManagerListener.delegater)
                    {
                        listener.OnMessagesRecalled(messages);
                    }
                });
            };

            OnReadAckForGroupMessageUpdated = () =>
            {
                Debug.Log($"Invoke customer listeners in OnReadAckForGroupMessageUpdated");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IChatManagerDelegate listener in CallbackManager.Instance().chatManagerListener.delegater)
                    {
                        listener.OnReadAckForGroupMessageUpdated();
                    }
                });
            };

            OnGroupMessageRead = (IntPtr[] _messages, int size) =>
            {
                Debug.Log($"Invoke customer listeners in OnGroupMessageRead upon group ack receiving...");
                var acks = new List<GroupReadAck>(size);
                GroupReadAckTO gto = null;
                for (int i=0; i<size; i++)
                {
                    gto = new GroupReadAckTO();
                    Marshal.PtrToStructure(_messages[i], gto);
                    Debug.Log($"Received group message read ackid:{gto.AckId}, msgid:{gto.MsgId}");
                }
                acks.Add(gto.Unmarshall());
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IChatManagerDelegate listener in CallbackManager.Instance().chatManagerListener.delegater)
                    {
                        listener.OnGroupMessageRead(acks);
                    }
                });
                
            };

            //to-do: no any return parameter?
            OnConversationsUpdate = () =>
            {
                Debug.Log($"Invoke customer listeners in OnConversationsUpdate when conversation updated.");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IChatManagerDelegate listener in CallbackManager.Instance().chatManagerListener.delegater)
                    {
                        listener.OnConversationsUpdate();
                    }
                });
            };

            OnConversationRead = (string from, string to) =>
            {
                Debug.Log($"Invoke customer listeners in OnConversationRead when conversation read.");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IChatManagerDelegate listener in CallbackManager.Instance().chatManagerListener.delegater)
                    {
                        listener.OnConversationRead(from, to);
                    }
                });
            };

        }
    }

    internal sealed class GroupManagerHub
    {
        internal OnInvitationReceived OnInvitationReceived;
        internal OnRequestToJoinReceived OnRequestToJoinReceived;
        internal OnRequestToJoinAccepted OnRequestToJoinAccepted;
        internal OnRequestToJoinDeclined OnRequestToJoinDeclined;
        internal OnInvitationAccepted OnInvitationAccepted;
        internal OnInvitationDeclined OnInvitationDeclined;
        internal OnUserRemoved OnUserRemoved;
        internal OnGroupDestroyed OnGroupDestroyed;
        internal OnAutoAcceptInvitationFromGroup OnAutoAcceptInvitationFromGroup;
        internal OnMuteListAdded OnMuteListAdded;
        internal OnMuteListRemoved OnMuteListRemoved;
        internal OnAdminAdded OnAdminAdded;
        internal OnAdminRemoved OnAdminRemoved;
        internal OnOwnerChanged OnOwnerChanged;
        internal OnMemberJoined OnMemberJoined;
        internal OnMemberExited OnMemberExited;
        internal OnAnnouncementChanged OnAnnouncementChanged;
        internal OnSharedFileAdded OnSharedFileAdded;
        internal OnSharedFileDeleted OnSharedFileDeleted;


        internal GroupManagerHub()
        {
            OnInvitationReceived = (string groupId, string groupName, string inviter, string reason) =>
            {
                Debug.Log($"OnInvitationReceived, group[Id={groupId},Name={groupName}] invitation received from {inviter}, reason: {reason}");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate listener in CallbackManager.Instance().groupManagerListener.delegater)
                    {
                        listener.OnInvitationReceivedFromGroup(groupId, groupName, inviter, reason);
                    }
                });
            };

            OnRequestToJoinReceived = (string groupId, string groupName, string applicant, string reason) =>
            {
                Debug.Log($"ROnRequestToJoinReceived, group[Id={groupId},Name={groupName}], applicant:{applicant}, reason: {reason}");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate listener in CallbackManager.Instance().groupManagerListener.delegater)
                    {
                        listener.OnRequestToJoinReceivedFromGroup(groupId, groupName, applicant, reason);
                    }
                });
            };

            OnRequestToJoinAccepted = (string groupId, string groupName, string accepter) =>
            {
                Debug.Log($"OnRequestToJoinAccepted, group[Id={groupId},Name={groupName}], accepter: {accepter}");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate listener in CallbackManager.Instance().groupManagerListener.delegater)
                    {
                        listener.OnRequestToJoinAcceptedFromGroup(groupId, groupName, accepter);
                    }
                });
            };

            OnRequestToJoinDeclined = (string groupId, string groupName, string decliner, string reason) =>
            {
                Debug.Log($"OnRequestToJoinDeclined, group[Id={groupId},Name={groupName}], decliner: {decliner}, reason:{reason}");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate listener in CallbackManager.Instance().groupManagerListener.delegater)
                    {
                        listener.OnRequestToJoinDeclinedFromGroup(groupId, groupName, decliner, reason);
                    }
                });
            };

            OnInvitationAccepted = (string groupId, string invitee, string reason) =>
            {
                Debug.Log($"OnInvitationAccepted, group[Id={groupId}], invitee: {invitee}, reason:{reason}");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate listener in CallbackManager.Instance().groupManagerListener.delegater)
                    {
                        listener.OnInvitationAcceptedFromGroup(groupId, invitee, reason);
                    }
                });
            };

            OnInvitationDeclined = (string groupId, string invitee, string reason) =>
            {
                Debug.Log($"OnInvitationDeclined, group[Id={groupId}], invitee: {invitee}, reason:{reason}");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate listener in CallbackManager.Instance().groupManagerListener.delegater)
                    {
                        listener.OnInvitationDeclinedFromGroup(groupId, invitee, reason);
                    }
                });
            };

            OnUserRemoved = (string groupId, string groupName) =>
            {
                Debug.Log($"OnUserRemoved, group[Id={groupId}, Name={groupName}]");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate listener in CallbackManager.Instance().groupManagerListener.delegater)
                    {
                        listener.OnUserRemovedFromGroup(groupId, groupName);
                    }
                });
            };

            OnGroupDestroyed = (string groupId, string groupName) =>
            {
                Debug.Log($"OnGroupDestroyed, group[Id={groupId}, Name={groupName}]");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate listener in CallbackManager.Instance().groupManagerListener.delegater)
                    {
                        listener.OnDestroyedFromGroup(groupId, groupName);
                    }
                });
            };

            OnAutoAcceptInvitationFromGroup = (string groupId, string inviter, string inviteMessage) =>
            {
                Debug.Log($"OnAutoAcceptInvitationFromGroup, group[Id={groupId}], inviter:{inviter}, inviteMessage:{inviteMessage}");

                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate listener in CallbackManager.Instance().groupManagerListener.delegater)
                    {
                        listener.OnAutoAcceptInvitationFromGroup(groupId, inviter, inviteMessage);
                    }
                });
            };

            OnMuteListAdded = (string groupId, string[] mutes, int size, int muteExpire) =>
            {
                Debug.Log($"OnMuteListAdded, group[Id={groupId}], mute member num:{size}, muteExpire:{muteExpire}");

                var acks = new List<string>(size);
                for (int i = 0; i < size; i++)
                {
                    acks.Add(mutes[i]);
                }

                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate listener in CallbackManager.Instance().groupManagerListener.delegater)
                    {
                        listener.OnMuteListAddedFromGroup(groupId, acks, muteExpire);
                    }
                });
            };

            OnMuteListRemoved = (string groupId, string[] mutes, int size) =>
            {
                Debug.Log($"OnMuteListRemoved, group[Id={groupId}], mute member num:{size}");

                var acks = new List<string>(size);
                for (int i = 0; i < size; i++)
                {
                    acks.Add(mutes[i]);
                }

                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate listener in CallbackManager.Instance().groupManagerListener.delegater)
                    {
                        listener.OnMuteListRemovedFromGroup(groupId, acks);
                    }
                });
            };

            OnAdminAdded = (string groupId, string administrator) =>
            {
                Debug.Log($"OnAdminAdded, group[Id={groupId}], admin:{administrator}");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate listener in CallbackManager.Instance().groupManagerListener.delegater)
                    {
                        listener.OnAdminAddedFromGroup(groupId, administrator);
                    }
                });
            };

            OnAdminRemoved = (string groupId, string administrator) =>
            {
                Debug.Log($"OnAdminRemoved, group[Id={groupId}], admin:{administrator}");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate listener in CallbackManager.Instance().groupManagerListener.delegater)
                    {
                        listener.OnAdminRemovedFromGroup(groupId, administrator);
                    }
                });
            };

            OnOwnerChanged = (string groupId, string newOwner, string oldOwner) =>
            {
                Debug.Log($"OnOwnerChanged, group[Id={groupId}], newOwner:{newOwner}, oldOwner:{oldOwner}");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate listener in CallbackManager.Instance().groupManagerListener.delegater)
                    {
                        listener.OnOwnerChangedFromGroup(groupId, newOwner, oldOwner);
                    }
                });
            };

            OnMemberJoined = (string groupId, string member) =>
            {
                Debug.Log($"OnMemberJoined, group[Id={groupId}], member:{member}");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate listener in CallbackManager.Instance().groupManagerListener.delegater)
                    {
                        listener.OnMemberJoinedFromGroup(groupId, member);
                    }
                });
            };

            OnMemberExited = (string groupId, string member) =>
            {
                Debug.Log($"OnMemberExited, group[Id={groupId}], member:{member}");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate listener in CallbackManager.Instance().groupManagerListener.delegater)
                    {
                        listener.OnMemberExitedFromGroup(groupId, member);
                    }
                });
            };

            OnAnnouncementChanged = (string groupId, string announcement) =>
            {
                Debug.Log($"OnAnnouncementChanged, group[Id={groupId}], announcement:{announcement}");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate listener in CallbackManager.Instance().groupManagerListener.delegater)
                    {
                        listener.OnAnnouncementChangedFromGroup(groupId, announcement);
                    }
                });
            };

            OnSharedFileAdded = (string groupId, IntPtr[] sharedFile, int size) =>
            {
                GroupSharedFile groupSharedFile = new GroupSharedFile();
                Marshal.PtrToStructure(sharedFile[0], groupSharedFile);

                Debug.Log($"OnSharedFileAdded, group[Id={groupId}], fileName:{groupSharedFile.FileName}, fileId:{groupSharedFile.FileId}");

                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate listener in CallbackManager.Instance().groupManagerListener.delegater)
                    {
                        listener.OnSharedFileAddedFromGroup(groupId, groupSharedFile);
                    }
                });
            };

            OnSharedFileDeleted = (string groupId, string fileId) =>
            {
                Debug.Log($"OnSharedFileDeleted, group[Id={groupId}], fileId:{fileId}");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate listener in CallbackManager.Instance().groupManagerListener.delegater)
                    {
                        listener.OnSharedFileDeletedFromGroup(groupId, fileId);
                    }
                });
            };
        }
    }

    internal sealed class RoomManagerHub
    {
        internal OnChatRoomDestroyed OnChatRoomDestroyed;
        internal OnMemberJoined OnMemberJoined;
        internal OnMemberExitedFromRoom OnMemberExited;
        internal OnRemovedFromChatRoom OnRemovedFromChatRoom;
        internal OnMuteListAdded OnMuteListAdded;
        internal OnMuteListRemoved OnMuteListRemoved;
        internal OnAdminAdded OnAdminAdded;
        internal OnAdminRemoved OnAdminRemoved;
        internal OnOwnerChanged OnOwnerChanged;
        internal OnAnnouncementChanged OnAnnouncementChanged;

        internal RoomManagerHub()
        {

            OnChatRoomDestroyed = (string roomId, string roomName) =>
            {
                Debug.Log($"OnChatRoomDestroyed, roomId {roomId}, roomName {roomName}.");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IRoomManagerDelegate listener in CallbackManager.Instance().roomManagerListener.delegater)
                    {
                        listener.OnDestroyedFromRoom(roomId, roomName);
                    }
                });
            };

            OnMemberJoined = (string roomId, string member) =>
            {
                Debug.Log($"Member {member} just joined room {roomId}.");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IRoomManagerDelegate listener in CallbackManager.Instance().roomManagerListener.delegater)
                    {
                        listener.OnMemberJoinedFromRoom(roomId, member);
                    }
                });
            };

            OnMemberExited = (string roomId, string roomName, string member) =>
            {
                Debug.Log($"OnMemberExited, roomId {roomId}, member {member}.");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IRoomManagerDelegate listener in CallbackManager.Instance().roomManagerListener.delegater)
                    {
                        listener.OnMemberExitedFromRoom(roomId, roomName, member);
                    }
                });
            };

            OnRemovedFromChatRoom = (string roomId, string roomName, string participant) =>
            {
                Debug.Log($"OnRemovedFromChatRoom, roomId {roomId}, roomName {roomName}, paticipant {participant}.");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IRoomManagerDelegate listener in CallbackManager.Instance().roomManagerListener.delegater)
                    {
                        listener.OnRemovedFromRoom(roomId, roomName, participant);
                    }
                });
            };

            OnMuteListAdded = (string roomId, string[] mutes, int size, int muteExpire) =>
            {
                Debug.Log($"OnMuteListAdded, roomId {roomId}, mute member num {size}, muteExpire {muteExpire}.");
                var acks = new List<string>(size);
                for (int i = 0; i < size; i++)
                {
                    acks.Add(mutes[i]);
                }
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IRoomManagerDelegate listener in CallbackManager.Instance().roomManagerListener.delegater)
                    {
                        listener.OnMuteListAddedFromRoom(roomId, acks, muteExpire);
                    }
                });
            };

            OnMuteListRemoved = (string roomId, string[] mutes, int size) =>
            {
                Debug.Log($"OnMuteListRemoved, roomId {roomId}, mute member num {size}");
                var acks = new List<string>(size);
                for (int i = 0; i < size; i++)
                {
                    acks.Add(mutes[i]);
                }
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IRoomManagerDelegate listener in CallbackManager.Instance().roomManagerListener.delegater)
                    {
                        listener.OnMuteListRemovedFromRoom(roomId, acks);
                    }
                });
            };

            OnAdminAdded = (string roomId, string administrator) =>
            {
                Debug.Log($"OnAdminAdded, roomId {roomId}, admin {administrator}.");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IRoomManagerDelegate listener in CallbackManager.Instance().roomManagerListener.delegater)
                    {
                        listener.OnAdminAddedFromRoom(roomId, administrator);
                    }
                });
            };

            OnAdminRemoved = (string roomId, string administrator) =>
            {
                Debug.Log($"OnAdminRemoved, roomId {roomId}, admin {administrator}.");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IRoomManagerDelegate listener in CallbackManager.Instance().roomManagerListener.delegater)
                    {
                        listener.OnAdminRemovedFromRoom(roomId, administrator);
                    }
                });
            };

            OnOwnerChanged = (string roomId, string newOwner, string oldOwner) =>
            {
                Debug.Log($"OnOwnerChanged, roomId {roomId}, newOwner {newOwner}, oldOwner {oldOwner}.");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IRoomManagerDelegate listener in CallbackManager.Instance().roomManagerListener.delegater)
                    {
                        listener.OnOwnerChangedFromRoom(roomId, newOwner, oldOwner);
                    }
                });
            };

            OnAnnouncementChanged = (string roomId, string announcement) =>
            {
                Debug.Log($"OnAnnouncementChanged, roomId {roomId}, announcement {announcement}");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IRoomManagerDelegate listener in CallbackManager.Instance().roomManagerListener.delegater)
                    {
                        listener.OnAnnouncementChangedFromRoom(roomId, announcement);
                    }
                });
            };
        }
    }

    internal sealed class ContactManagerHub
    {
        internal OnContactAdd OnContactAdd;
        internal OnContactDeleted OnContactDeleted;
        internal OnContactInvited OnContactInvited;
        internal OnFriendRequestAccepted OnFriendRequestAccepted;
        internal OnFriendRequestDeclined OnFriendRequestDeclined;

        internal ContactManagerHub()
        {

            OnContactAdd = (string username) =>
            {
                Debug.Log($"Name={username}] add you.");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IContactManagerDelegate listener in CallbackManager.Instance().contactManagerListener.delegater)
                    {
                        listener.OnContactAdded(username);
                    }
                });  
            };

            OnContactDeleted = (string username) =>
            {
                Debug.Log($"Name={username}] delete you.");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IContactManagerDelegate listener in CallbackManager.Instance().contactManagerListener.delegater)
                    {
                        listener.OnContactDeleted(username);
                    }
                });
            };

            OnContactInvited = (string username, string reason) =>
            {
                Debug.Log($"Name={username}] invite you with reason:{reason}.");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IContactManagerDelegate listener in CallbackManager.Instance().contactManagerListener.delegater)
                    {
                        listener.OnContactInvited(username, reason);
                    }
                });
            };

            OnFriendRequestAccepted = (string username) =>
            {
                Debug.Log($"Name={username}] accept your invitation.");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IContactManagerDelegate listener in CallbackManager.Instance().contactManagerListener.delegater)
                    {
                        listener.OnFriendRequestAccepted(username);
                    }
                });
            };

            OnFriendRequestDeclined = (string username) =>
            {
                Debug.Log($"Name={username}] decline your invitation.");
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IContactManagerDelegate listener in CallbackManager.Instance().contactManagerListener.delegater)
                    {
                        listener.OnFriendRequestDeclined(username);
                    }
                });
            };
        }
    }

    internal sealed class PresenceManagerHub
    {
        internal OnPresenceUpdated onPresenceUpdated;

        internal PresenceManagerHub()
        {
            onPresenceUpdated = (IntPtr[] presences, int size) =>
            {
                Debug.Log("onPresenceUpdated received.");

                List<Presence> list = new List<Presence>();
                for (int i=0; i<size; i++)
                {
                    PresenceTO pto = Marshal.PtrToStructure<PresenceTO>(presences[i]);
                    Presence presence = pto.Unmarshall();
                    list.Add(presence);
                }

                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IPresenceManagerDelegate listener in CallbackManager.Instance().presenceManagerListener.delegater)
                    {
                        listener.OnPresenceUpdated(list);
                    }
                });
            };

        }
    }
}