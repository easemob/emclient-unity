using System;
using UnityEngine;
using ChatSDK;
using System.Collections.Generic;

public class ConnectionDelegate : IConnectionDelegate
{ 

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
            if(message.Body is ChatSDK.MessageBody.TextBody tb)
            {
                Debug.Log($"Content: {tb.Text}");
            }else if(message.Body is ChatSDK.MessageBody.LocationBody lb)
            {
                Debug.Log($"Latitude: {lb.Latitude} \t Longtitude: {lb.Longitude}");
                Debug.Log($"Address: {lb.Address}");
            }else if(message.Body is ChatSDK.MessageBody.CmdBody cb)
            {
                Debug.Log($"Action: {cb.Action} DeliverOnlineOnly: {cb.DeliverOnlineOnly}");
            }
        }
    }

    public void OnReadAckForGroupMessageUpdated()
    {
        throw new NotImplementedException();
    }
}
