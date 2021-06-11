using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace ChatSDK
{
    //IConnectionDelegate
    public delegate void OnDisconnected(int info);
    //IChatManagerDelegate
    public delegate void OnMessagesReceived(MessageTransferObject[] messages);
    public delegate void OnCmdMessagesReceived(MessageTransferObject[] messages);
    public delegate void OnMessagesRead(MessageTransferObject[] messages);
    public delegate void OnMessagesDelivered(MessageTransferObject[] messages);
    public delegate void OnMessagesRecalled(MessageTransferObject[] messages);
    public delegate void OnConversationsUpdate();
    public delegate void OnConversationRead(string from, string to);

    public class ConnectionHub : CallBack, IConnectionDelegate
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct Delegate
        {
            public Action Connected;
            public OnDisconnected Disconnected;
        };

        private Delegate @delegate;
        private WeakDelegater<IConnectionDelegate> listeners;

        public ConnectionHub(WeakDelegater<IConnectionDelegate> _listeners)
        {
            listeners = _listeners;
            (@delegate.Connected, @delegate.Disconnected) = (OnConnected, OnDisconnected);
            callbackId = CallbackManager.Instance().currentId.ToString();
            CallbackManager.Instance().AddCallback(CallbackManager.Instance().currentId, this);
        }

        public void OnConnected()
        {
            //invoke each listener in list
            Debug.Log("ConnectionHub.OnConnected() invoked!");
            foreach (IConnectionDelegate listener in listeners.List)
            {
                listener.OnConnected();
            }
        }

        public void OnDisconnected(int info)
        {
            //invoke each listener in list
            Debug.Log($"ConnectionHub.OnDisconnected() invoked with info={info}!");
            foreach (IConnectionDelegate listener in listeners.List)
            {
                listener.OnDisconnected(info);
            }
        }

        public Delegate Delegates()
        {
            return @delegate;
        }
    }

    public class ChatManagerHub : CallBack
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct Delegate
        {
            public OnMessagesReceived MessagesReceived;
            public OnCmdMessagesReceived CmdMessagesReceived;
            public OnMessagesRead MessagesRead;
            public OnMessagesDelivered MessagesDelivered;
            public OnMessagesRecalled MessagesRecalled;
            public OnConversationsUpdate ConversationsUpdate;
            public OnConversationRead ConversationRead;
        };

        private Delegate @delegate;
        private WeakDelegater<IChatManagerDelegate> listeners;

        public ChatManagerHub(WeakDelegater<IChatManagerDelegate> _listeners)
        {
            listeners = _listeners;
            (@delegate.CmdMessagesReceived,
                @delegate.CmdMessagesReceived,
                @delegate.MessagesRead,
                @delegate.MessagesDelivered,
                @delegate.MessagesRecalled,
                @delegate.ConversationsUpdate,
                @delegate.ConversationRead) = (OnMessagesReceived,
                                                OnCmdMessagesReceived,
                                                OnMessagesRead,
                                                OnMessagesDelivered,
                                                OnMessagesRecalled,
                                                OnConversationsUpdate,
                                                OnConversationRead);
            callbackId = CallbackManager.Instance().currentId.ToString();
            CallbackManager.Instance().AddCallback(CallbackManager.Instance().currentId, this);
        }

        public void OnMessagesReceived(MessageTransferObject[] _messages)
        {
            List<Message> messages = MessageTransferObject.ConvertToMessageList(_messages);
            //invoke each listener in list
            Debug.Log($"ChatManagerHub.OnMessagesReceived() invoked with messages={messages}!");
            foreach (IChatManagerDelegate listener in listeners.List)
            {
                listener.OnMessagesReceived(messages);
            }
        }

        public void OnCmdMessagesReceived(MessageTransferObject[] _messages)
        {
            List<Message> messages = MessageTransferObject.ConvertToMessageList(_messages);
            //invoke each listener in list
            Debug.Log($"ChatManagerHub.OnCmdMessagesReceived() invoked with messages={messages}!");
            foreach (IChatManagerDelegate listener in listeners.List)
            {
                listener.OnCmdMessagesReceived(messages);
            }
        }

        public void OnMessagesRead(MessageTransferObject[] _messages)
        {
            List<Message> messages = MessageTransferObject.ConvertToMessageList(_messages);
            //invoke each listener in list
            Debug.Log($"ChatManagerHub.OnMessagesRead() invoked with messages={messages}!");
            foreach (IChatManagerDelegate listener in listeners.List)
            {
                listener.OnMessagesRead(messages);
            }
        }

        public void OnMessagesDelivered(MessageTransferObject[] _messages)
        {
            List<Message> messages = MessageTransferObject.ConvertToMessageList(_messages);
            //invoke each listener in list
            Debug.Log($"ChatManagerHub.OnMessagesDelivered() invoked with messages={messages}!");
            foreach (IChatManagerDelegate listener in listeners.List)
            {
                listener.OnMessagesDelivered(messages);
            }
        }

        public void OnMessagesRecalled(MessageTransferObject[] _messages)
        {
            List<Message> messages = MessageTransferObject.ConvertToMessageList(_messages);
            //invoke each listener in list
            Debug.Log($"ChatManagerHub.OnMessagesRecalled() invoked with messages={messages}!");
            foreach (IChatManagerDelegate listener in listeners.List)
            {
                listener.OnMessagesRecalled(messages);
            }
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

        public Delegate Delegates()
        {
            return @delegate;
        }
    }
}