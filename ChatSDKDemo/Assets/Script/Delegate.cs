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
            Debug.Log(message);
        }
    }

    public void OnConversationRead(string from, string to)
    {
        throw new NotImplementedException();
    }

    public void OnConversationsUpdate()
    {
        throw new NotImplementedException();
    }

    public void OnGroupMessageRead(List<GroupReadAck> list)
    {
        throw new NotImplementedException();
    }

    public void OnMessagesDelivered(List<Message> messages)
    {
        throw new NotImplementedException();
    }

    public void OnMessagesRead(List<Message> messages)
    {
        throw new NotImplementedException();
    }

    public void OnMessagesRecalled(List<Message> messages)
    {
        throw new NotImplementedException();
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
