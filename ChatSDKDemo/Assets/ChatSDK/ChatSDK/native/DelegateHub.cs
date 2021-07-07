using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace ChatSDK
{
    //IConnectionDelegate
    public delegate void OnDisconnected(int info);
    //IChatManagerDelegate
    public delegate void OnMessagesReceived([MarshalAs(UnmanagedType.LPArray, SizeParamIndex =2)]IntPtr[] messages,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]MessageBodyType[] types, int size);
    public delegate void OnCmdMessagesReceived(MessageTO[] messages, int size);
    public delegate void OnMessagesRead(MessageTO[] messages, int size);
    public delegate void OnMessagesDelivered(MessageTO[] messages, int size);
    public delegate void OnMessagesRecalled(MessageTO[] messages, int size);
    public delegate void OnReadAckForGroupMessageUpdated();
    public delegate void OnGroupMessageRead(GroupReadAck[] acks, int size);
    public delegate void OnConversationsUpdate();
    public delegate void OnConversationRead(string from, string to);

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
            if(_listeners == null)
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
                    }
                    Debug.Log($"Message {mto.MsgId} received from {mto.From}");
                    messages.Add(mto.Unmarshall());
                    //_messages[i] memory released at unmanaged side!
                }
                //chain-call to customer specified listeners
                Debug.Log($"Invoke customer listeners upon messages receiving...");
                foreach (IChatManagerDelegate listener in listeners?.List)
                {
                    listener.OnMessagesReceived(messages);
                }
            };
        }

        public void OnCmdMessagesReceived(MessageTO[] _messages, int size)
        {
            List<Message> messages = MessageTO.ConvertToMessageList(_messages, size);
            //invoke each listener in list
            Debug.Log($"ChatManagerHub.OnCmdMessagesReceived() invoked with messages={messages}!");
            foreach (IChatManagerDelegate listener in listeners.List)
            {
                listener.OnCmdMessagesReceived(messages);
            }
        }

        public void OnMessagesRead(MessageTO[] _messages, int size)
        {
            List<Message> messages = MessageTO.ConvertToMessageList(_messages, size);
            //invoke each listener in list
            Debug.Log($"ChatManagerHub.OnMessagesRead() invoked with messages={messages}!");
            foreach (IChatManagerDelegate listener in listeners.List)
            {
                listener.OnMessagesRead(messages);
            }
        }

        public void OnMessagesDelivered(MessageTO[] _messages, int size)
        {
            List<Message> messages = MessageTO.ConvertToMessageList(_messages, size);
            //invoke each listener in list
            Debug.Log($"ChatManagerHub.OnMessagesDelivered() invoked with messages={messages}!");
            foreach (IChatManagerDelegate listener in listeners.List)
            {
                listener.OnMessagesDelivered(messages);
            }
        }

        public void OnMessagesRecalled(MessageTO[] _messages, int size)
        {
            List<Message> messages = MessageTO.ConvertToMessageList(_messages, size);
            //invoke each listener in list
            Debug.Log($"ChatManagerHub.OnMessagesRecalled() invoked with messages={messages}!");
            foreach (IChatManagerDelegate listener in listeners.List)
            {
                listener.OnMessagesRecalled(messages);
            }
        }

        public void OnReadAckForGroupMessageUpdated()
        {
            //TODO: to be implemented
        }

        public void OnGroupMessageRead(GroupReadAck[] acks, int size)
        {
            //TODO: to be implemented
        }


        public void OnConversationsUpdate()
        {
            //invoke each listener in list
            Debug.Log($"ChatManagerHub.OnConversationUpdate() invoked!");
            foreach (IChatManagerDelegate listener in listeners.List)
            {
                listener.OnConversationsUpdate();
            }
        }

        public void OnConversationRead(string from, string to)
        {
            //invoke each listener in list
            Debug.Log($"ChatManagerHub.OnConversationRead(from={from}, to={to}) invoked!");
            foreach (IChatManagerDelegate listener in listeners.List)
            {
                listener.OnConversationRead(from, to);
            }
        }
    }
}