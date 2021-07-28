using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace ChatSDK
{
    //IConnectionDelegate
    public delegate void OnDisconnected(int info);
    //IChatManagerDelegate
    public delegate void OnMessagesReceived([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] IntPtr[] messages,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] MessageBodyType[] types, int size);

    public delegate void OnCmdMessagesReceived([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] IntPtr[] messages,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] MessageBodyType[] types, int size);

    public delegate void OnMessagesRead([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] IntPtr[] messages,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] MessageBodyType[] types, int size);

    public delegate void OnMessagesDelivered([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] IntPtr[] messages,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] MessageBodyType[] types, int size);

    public delegate void OnMessagesRecalled([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] IntPtr[] messages,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] MessageBodyType[] types, int size);

    public delegate void OnReadAckForGroupMessageUpdated();

    public delegate void OnGroupMessageRead([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] IntPtr[] acks, int size);
    public delegate void OnConversationsUpdate();
    public delegate void OnConversationRead(string from, string to);

    //IGroupManagerDelegate
    public delegate void OnInvitationReceived(string groupId, string groupName, string inviter, string reason);
    public delegate void OnRequestToJoinReceived(string groupId, string groupName, string applicant, string reason);
    public delegate void OnRequestToJoinAccepted(string groupId, string groupName, string accepter);
    public delegate void OnRequestToJoinDeclined(string groupId, string groupName, string decliner, string reason);
    public delegate void OnInvitationAccepted(string groupId, string invitee, string reason);
    public delegate void OnInvitationDeclined(string groupId, string invitee, string reason);
    public delegate void OnUserRemoved(string groupId, string groupName);
    public delegate void OnGroupDestroyed(string groupId, string groupName);
    public delegate void OnAutoAcceptInvitationFromGroup(string groupId, string inviter, string inviteMessage);
    public delegate void OnMuteListAdded(string groupId, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] string[] mutes, int size, int muteExpire);
    public delegate void OnMuteListRemoved(string groupId, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] string[] mutes, int size);
    public delegate void OnAdminAdded(string groupId, string administrator);
    public delegate void OnAdminRemoved(string groupId, string administrator);
    public delegate void OnOwnerChanged(string groupId, string newOwner, string oldOwner);
    public delegate void OnMemberJoined(string groupId, string member);
    public delegate void OnMemberExited(string groupId, string member);
    public delegate void OnAnnouncementChanged(string groupId, string announcement);
    public delegate void OnSharedFileAdded(string groupId, GroupSharedFile sharedFile);
    public delegate void OnSharedFileDeleted(string groupId, string fileId);

    //IRoomManagerDelegate, most of them are duplicated as IGroupManagerDelegate
    public delegate void OnChatRoomDestroyed(string roomId, string roomName);
    public delegate void OnRemovedFromChatRoom(string roomId, string roomName, string participant);

    //IContactManagerDelegate
    public delegate void OnContactAdd(string username);
    public delegate void OnContactDeleted(string username);
    public delegate void OnContactInvited(string username, string reason);
    public delegate void OnFriendRequestAccepted(string username);
    public delegate void OnFriendRequestDeclined(string username);

    public class ConnectionHub
    {
        //events handler
        internal Action OnConnected;
        internal OnDisconnected OnDisconnected;
        internal Action OnPong;

        private WeakDelegater<IConnectionDelegate> listeners;

        public ConnectionHub(IClient client, WeakDelegater<IConnectionDelegate> _listeners)
        {
            //callbackmanager registration done in base()!
            if (_listeners == null)
            {
                listeners = new WeakDelegater<IConnectionDelegate>();
            }
            else
            {
                listeners = _listeners;
            }

            //register events
            OnConnected = () =>
            {
                Debug.Log("Connection established.");
                client.IsConnected = true;
                foreach (IConnectionDelegate listener in listeners?.List)
                {
                    listener.OnConnected();
                }
            };
            OnDisconnected = (int info) =>
            {
                Debug.Log("Connection discontinued.");
                client.IsConnected = false;
                foreach (IConnectionDelegate listener in listeners?.List)
                {
                    listener.OnDisconnected(info);
                }
            };
            OnPong = () =>
            {
                Debug.Log("Server ponged.");
                foreach (IConnectionDelegate listener in listeners?.List)
                {
                    listener.OnPong();
                }
            };
        }
    }

    public class ChatManagerHub
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

        private WeakDelegater<IChatManagerDelegate> listeners;

        public ChatManagerHub(WeakDelegater<IChatManagerDelegate> _listeners)
        {
            if (_listeners == null)
            {
                listeners = new WeakDelegater<IChatManagerDelegate>();
            }
            else
            {
                listeners = _listeners;
            }

            OnMessagesReceived = (IntPtr[] _messages, MessageBodyType[] types, int size) =>
            {
                List<Message> messages = GetMessageListFromIntPtrArray(_messages, types, size);
                
                //chain-call to customer specified listeners
                Debug.Log($"Invoke customer listeners in OnMessagesReceived upon messages receiving...");
                foreach (IChatManagerDelegate listener in listeners?.List)
                {
                    listener.OnMessagesReceived(messages);
                }
            };

            OnCmdMessagesReceived = (IntPtr[] _messages, MessageBodyType[] types, int size) =>
            {
                List<Message> messages = GetMessageListFromIntPtrArray(_messages, types, size);

                //chain-call to customer specified listeners
                Debug.Log($"Invoke customer listeners in OnCmdMessagesReceived upon messages receiving...");
                foreach (IChatManagerDelegate listener in listeners?.List)
                {
                    listener.OnCmdMessagesReceived(messages);
                }
            };

            OnMessagesRead = (IntPtr[] _messages, MessageBodyType[] types, int size) =>
            {
                List<Message> messages = GetMessageListFromIntPtrArray(_messages, types, size);

                //chain-call to customer specified listeners
                Debug.Log($"Invoke customer listeners in OnMessagesRead upon messages receiving...");
                foreach (IChatManagerDelegate listener in listeners?.List)
                {
                    listener.OnMessagesRead(messages);
                }
            };

            OnMessagesDelivered = (IntPtr[] _messages, MessageBodyType[] types, int size) =>
            {
                List<Message> messages = GetMessageListFromIntPtrArray(_messages, types, size);

                //chain-call to customer specified listeners
                Debug.Log($"Invoke customer listeners in OnMessagesDelivered upon messages receiving...");
                foreach (IChatManagerDelegate listener in listeners?.List)
                {
                    listener.OnMessagesDelivered(messages);
                }
            };

            OnMessagesRecalled = (IntPtr[] _messages, MessageBodyType[] types, int size) =>
            {
                List<Message> messages = GetMessageListFromIntPtrArray(_messages, types, size);

                //chain-call to customer specified listeners
                Debug.Log($"Invoke customer listeners in OnMessagesRecalled upon messages receiving...");
                foreach (IChatManagerDelegate listener in listeners?.List)
                {
                    listener.OnMessagesRecalled(messages);
                }
            };

            OnReadAckForGroupMessageUpdated = () =>
            {
                Debug.Log($"Invoke customer listeners in OnReadAckForGroupMessageUpdated");
                foreach (IChatManagerDelegate listener in listeners?.List)
                {
                    listener.OnReadAckForGroupMessageUpdated();
                }
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
                foreach (IChatManagerDelegate listener in listeners?.List)
                {
                    listener.OnGroupMessageRead(acks);
                }
            };

            //to-do: no any return parameter?
            OnConversationsUpdate = () =>
            {
                Debug.Log($"Invoke customer listeners in OnConversationsUpdate when conversation updated.");
                foreach (IChatManagerDelegate listener in listeners?.List)
                {
                    listener.OnConversationsUpdate();
                }
            };

            OnConversationRead = (string from, string to) =>
            {
                Debug.Log($"Invoke customer listeners in OnConversationRead when conversation read.");
                foreach (IChatManagerDelegate listener in listeners?.List)
                {
                    listener.OnConversationRead(from, to);
                }
            };

        }

        public List<Message>  GetMessageListFromIntPtrArray(IntPtr[] _messages, MessageBodyType[] types, int size)
        {
            var messages = new List<Message>(size);
            for (int i = 0; i < size; i++)
            {
                MessageTO mto = null;
                switch (types[i])
                {
                    case MessageBodyType.TXT:
                        //keep using mto
                        mto = new TextMessageTO();
                        Marshal.PtrToStructure(_messages[i], mto);
                        break;
                    case MessageBodyType.LOCATION:
                        mto = new LocationMessageTO();
                        Marshal.PtrToStructure(_messages[i], mto);
                        break;
                    case MessageBodyType.CMD:
                        mto = new CmdMessageTO();
                        Marshal.PtrToStructure(_messages[i], mto);
                        break;
                    case MessageBodyType.FILE:
                        mto = new FileMessageTO();
                        Marshal.PtrToStructure(_messages[i], mto);
                        break;
                    case MessageBodyType.IMAGE:
                        mto = new ImageMessageTO();
                        Marshal.PtrToStructure(_messages[i], mto);
                        break;
                    case MessageBodyType.VOICE:
                        mto = new VoiceMessageTO();
                        Marshal.PtrToStructure(_messages[i], mto);
                        break;
                }
                Debug.Log($"Message {mto.MsgId} received from {mto.From}");
                messages.Add(mto.Unmarshall());
                //_messages[i] memory released at unmanaged side!                
            }
            return messages;
        }
    }

    public class GroupManagerHub
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

        private WeakDelegater<IGroupManagerDelegate> listeners;

        public GroupManagerHub(WeakDelegater<IGroupManagerDelegate> _listeners)
        {
            if (_listeners == null)
            {
                listeners = new WeakDelegater<IGroupManagerDelegate>();
            }
            else
            {
                listeners = _listeners;
            }

            OnInvitationReceived = (string groupId, string groupName, string inviter, string reason) =>
            {
                Debug.Log($"Group[Id={groupId},Name={groupName}] invitation received from {inviter}, reason: {reason}");
                foreach (IGroupManagerDelegate listener in listeners?.List)
                {
                    listener.OnInvitationReceived(groupId, groupName, inviter, reason);
                }
            };
        }
    }

    public class RoomManagerHub
    {
        internal OnChatRoomDestroyed OnChatRoomDestroyed;
        internal OnMemberJoined OnMemberJoined;
        internal OnMemberExited OnMemberExited;
        internal OnRemovedFromChatRoom OnRemovedFromChatRoom;
        internal OnMuteListAdded OnMuteListAdded;
        internal OnMuteListRemoved OnMuteListRemoved;
        internal OnAdminAdded OnAdminAdded;
        internal OnAdminRemoved OnAdminRemoved;
        internal OnOwnerChanged OnOwnerChanged;
        internal OnAnnouncementChanged OnAnnouncementChanged;

        private WeakDelegater<IRoomManagerDelegate> listeners;

        public RoomManagerHub(WeakDelegater<IRoomManagerDelegate> _listeners)
        {
            if (_listeners == null)
            {
                listeners = new WeakDelegater<IRoomManagerDelegate>();
            }
            else
            {
                listeners = _listeners;
            }

            OnMemberJoined = (string groupId, string member) =>
            {
                Debug.Log($"Member {member} just joined group {groupId}.");
                foreach (IRoomManagerDelegate listener in listeners?.List)
                {
                    listener.OnMemberJoined(groupId, member);
                }
            };
        }
    }

    public class ContactManagerHub
    {
        internal OnContactAdd OnContactAdd;
        internal OnContactDeleted OnContactDeleted;
        internal OnContactInvited OnContactInvited;
        internal OnFriendRequestAccepted OnFriendRequestAccepted;
        internal OnFriendRequestDeclined OnFriendRequestDeclined;

        private WeakDelegater<IContactManagerDelegate> listeners;

        public ContactManagerHub(WeakDelegater<IContactManagerDelegate> _listeners)
        {
            if (_listeners == null)
            {
                listeners = new WeakDelegater<IContactManagerDelegate>();
            }
            else
            {
                listeners = _listeners;
            }

            OnContactAdd = (string username) =>
            {
                Debug.Log($"Name={username}] add you.");
                foreach (IContactManagerDelegate listener in listeners?.List)
                {
                    listener.OnContactAdded(username);
                }
            };

            OnContactDeleted = (string username) =>
            {
                Debug.Log($"Name={username}] delete you.");
                foreach (IContactManagerDelegate listener in listeners?.List)
                {
                    listener.OnContactDeleted(username);
                }
            };

            OnContactInvited = (string username, string reason) =>
            {
                Debug.Log($"Name={username}] invite you with reason:{reason}.");
                foreach (IContactManagerDelegate listener in listeners?.List)
                {
                    listener.OnContactInvited(username, reason);
                }
            };

            OnFriendRequestAccepted = (string username) =>
            {
                Debug.Log($"Name={username}] accept your invitation.");
                foreach (IContactManagerDelegate listener in listeners?.List)
                {
                    listener.OnFriendRequestAccepted(username);
                }
            };

            OnFriendRequestDeclined = (string username) =>
            {
                Debug.Log($"Name={username}] decline your invitation.");
                foreach (IContactManagerDelegate listener in listeners?.List)
                {
                    listener.OnFriendRequestDeclined(username);
                }
            };
        }
    }
}