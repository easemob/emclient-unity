using System;
using UnityEngine;
using ChatSDK;
using ChatSDK.MessageBody;
using System.Collections.Generic;

public class ConnectionDelegate : IConnectionDelegate
{
    private ConnectionDelegate() { }
    private static ConnectionDelegate global;

    public static ConnectionDelegate Global {
        get {
            if (global == null)
            {
                global = new ConnectionDelegate();
            }
            return global;
        }
    }

    public void OnConnected()
    {
        Debug.Log("Client connected.");
    }

    public void OnDisconnected(int info)
    {
        Debug.Log($"Client disconnected with {info}.");
    }

    public void OnPong()
    {
        Debug.Log("Server ponged.");
    }
}

public class ChatManagerDelegate : IChatManagerDelegate
{
    private ChatManagerDelegate() { }
    private static ChatManagerDelegate global;

    public static ChatManagerDelegate Global
    {
        get
        {
            if (global == null)
            {
                global = new ChatManagerDelegate();
            }
            return global;
        }
    }
    public void OnCmdMessagesReceived(List<Message> messages)
    {
        foreach (Message message in messages)
        {
            Debug.Log($"Received cmd messageid:{message.MsgId}, to:{message.To}, from:{message.From}");
        }
    }

    public void OnConversationRead(string from, string to)
    {
        Debug.Log($"Conversation Read, from:{from}, to:{to}");
    }

    public void OnConversationsUpdate()
    {
        Debug.Log("Receive conversation update.");
    }

    public void OnGroupMessageRead(List<GroupReadAck> list)
    {
        foreach(GroupReadAck ack in list)
        {
            Debug.Log($"Receive group read ack, AckId:{ack.AckId}, msgId:{ack.MsgId}, from:{ack.From}");
        }
    }

    public void OnMessagesDelivered(List<Message> messages)
    {
        foreach (Message message in messages)
        {
            Debug.Log($"Delivery message, msgId:{message.MsgId}, to:{message.To}, from:{message.From}");
        }
    }

    public void OnMessagesRead(List<Message> messages)
    {
        foreach (Message message in messages)
        {
            Debug.Log($"Message read, msgId:{message.MsgId}, to:{message.To}, from:{message.From}");
        }
    }

    public void OnMessagesRecalled(List<Message> messages)
    {
        foreach (Message message in messages)
        {
            Debug.Log($"Message recalled, msgId:{message.MsgId}, to:{message.To}, from:{message.From}");
        }
    }

    public void OnMessagesReceived(List<Message> messages)
    {
        foreach(Message message in messages)
        {
            Debug.Log($"From: {message.From} \t To: {message.To}");
            if(message.Body is TextBody tb)
            {
                Debug.Log($"Content: {tb.Text}");
            }else if(message.Body is LocationBody lb)
            {
                Debug.Log($"Latitude: {lb.Latitude} \t Longtitude: {lb.Longitude}");
                Debug.Log($"Address: {lb.Address}");
            }else if(message.Body is CmdBody cb)
            {
                Debug.Log($"Action: {cb.Action} DeliverOnlineOnly: {cb.DeliverOnlineOnly}");
            }else if(message.Body is FileBody fb)
            {
                Debug.Log($"LocalPath:{fb.LocalPath}, DisplayName:{fb.DisplayName}");
            }else if (message.Body is ImageBody ib)
            {
                Debug.Log($"LocalPath:{ib.LocalPath}, DisplayName:{ib.DisplayName}");
                Debug.Log($"ThumbnailLocalPath:{ib.ThumbnailLocalPath}");
            }
        }
    }

    public void OnReadAckForGroupMessageUpdated()
    {
        throw new NotImplementedException();
    }
}

public class ContactManagerDelegate : IContactManagerDelegate
{
    private ContactManagerDelegate() { }
    private static ContactManagerDelegate global;

    public static ContactManagerDelegate Global
    {
        get
        {
            if (global == null)
            {
                global = new ContactManagerDelegate();
            }
            return global;
        }
    }
    public void OnContactAdded(string username)
    {
        Debug.Log($"{username} add you.");
    }

    public void OnContactDeleted(string username)
    {
        Debug.Log($"{username} delete you.");
    }

    public void OnContactInvited(string username, string reason)
    {
        Debug.Log($"{username} invite you with reason:{reason}.");
    }

    public void OnFriendRequestAccepted(string username)
    {
        Debug.Log($"{username} accept your invitation.");
    }

    public void OnFriendRequestDeclined(string username)
    {
        Debug.Log($"{username} declinet your invitation.");
    }
}
