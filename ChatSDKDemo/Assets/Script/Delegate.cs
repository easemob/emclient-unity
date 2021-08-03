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
        Debug.Log("OnReadAckForGroupMessageUpdated");
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

public class GroupManagerDelegate : IGroupManagerDelegate
{
    private GroupManagerDelegate() { }
    private static GroupManagerDelegate global;

    public static GroupManagerDelegate Global
    {
        get
        {
            if (global == null)
            {
                global = new GroupManagerDelegate();
            }
            return global;
        }
    }


    public void OnInvitationReceived(string groupId, string groupName, string inviter, string reason)
    {
        Debug.Log($"OnInvitationReceived is called, groupId:{groupId}, groupName:{groupName}, reason:{reason}");
    }

    
    public void OnRequestToJoinReceived(string groupId, string groupName, string applicant, string reason)
    {
        Debug.Log($"OnRequestToJoinReceived is called, groupId:{groupId}, groupName:{groupName}, applicant:{applicant}, reason:{reason}");
    }

    public void OnRequestToJoinAccepted(string groupId, string groupName, string accepter)
    {
        Debug.Log($"OnRequestToJoinAccepted is called, groupId:{groupId}, groupName:{groupName}, accepter:{accepter}");
    }

    public void OnRequestToJoinDeclined(string groupId, string groupName, string decliner, string reason)
    {
        Debug.Log($"OnRequestToJoinDeclined is called, groupId:{groupId}, decliner:{decliner}, reason:{reason}");
    }

    public void OnInvitationAccepted(string groupId, string invitee, string reason)
    {
        Debug.Log($"OnInvitationAccepted is called, groupId:{groupId}, invitee:{invitee}, reason:{reason}");
    }

    public void OnInvitationDeclined(string groupId, string invitee, string reason)
    {
        Debug.Log($"OnInvitationDeclined is called, groupId:{groupId}, invitee:{invitee}, reason:{reason}");
    }

    public void OnUserRemoved(string groupId, string groupName)
    {
        Debug.Log($"OnUserRemoved is called, groupId:{groupId}, groupName:{groupName}");
    }

    public void OnGroupDestroyed(string groupId, string groupName)
    {
        Debug.Log($"OnGroupDestroyed is called, groupId:{groupId}, groupName:{groupName}");
    }

    public void OnAutoAcceptInvitationFromGroup(string groupId, string inviter, string inviteMessage)
    {
        Debug.Log($"OnAutoAcceptInvitationFromGroup is called, groupId:{groupId}, inviter:{inviter}, inviteMessage:{inviteMessage}");
    }

    public void OnMuteListAdded(string groupId, List<string> mutes, int muteExpire)
    {
        Debug.Log($"OnMuteListAdded is called, groupId:{groupId}, muteExpire:{muteExpire}");
        int i = 0;
        foreach(string str in mutes)
        {
            Debug.Log($"mute item{i}: {str}");
            i++;
        }
    }

    public void OnMuteListRemoved(string groupId, List<string> mutes)
    {
        Debug.Log($"OnMuteListRemoved is called, groupId:{groupId}");
        int i = 0;
        foreach (string str in mutes)
        {
            Debug.Log($"mute item{i}: {str}");
            i++;
        }
    }

    public void OnAdminAdded(string groupId, string administrator)
    {
        Debug.Log($"OnAdminAdded is called, groupId:{groupId}, administrator:{administrator}");
    }

    public void OnAdminRemoved(string groupId, string administrator)
    {
        Debug.Log($"OnAdminRemoved is called, groupId:{groupId}, administrator:{administrator}");
    }

    public void OnOwnerChanged(string groupId, string newOwner, string oldOwner)
    {
        Debug.Log($"OnOwnerChanged is called, groupId:{groupId}, newOwner:{newOwner}, oldOwner:{oldOwner}");
    }

    public void OnMemberJoined(string groupId, string member)
    {
        Debug.Log($"OnMemberJoined is called, groupId:{groupId}, member:{member}");
    }

    public void OnMemberExited(string groupId, string member)
    {
        Debug.Log($"OnMemberExited is called, groupId:{groupId}, member:{member}");
    }

    public void OnAnnouncementChanged(string groupId, string announcement)
    {
        Debug.Log($"OnAnnouncementChanged is called, groupId:{groupId}, announcement:{announcement}");
    }

    public void OnSharedFileAdded(string groupId, GroupSharedFile sharedFile)
    {
        Debug.Log($"OnSharedFileAdded is called, groupId:{groupId}, fileId:{sharedFile.FileId}, fileName:{sharedFile.FileName}");
    }

    public void OnSharedFileDeleted(string groupId, string fileId)
    {
        Debug.Log($"OnSharedFileDeleted is called, groupId:{groupId}, fileId:{fileId}");
    }

}

public class RoomManagerDelegate : IRoomManagerDelegate
{
    private RoomManagerDelegate() { }
    private static RoomManagerDelegate global;

    public static RoomManagerDelegate Global
    {
        get
        {
            if (global == null)
            {
                global = new RoomManagerDelegate();
            }
            return global;
        }
    }


    public void OnChatRoomDestroyed(string roomId, string roomName)
    {
        Debug.Log($"OnChatRoomDestroyed is called, roomId:{roomId}, roomName:{roomName}");
    }


    public void OnMemberJoined(string roomId, string participant)
    {
        Debug.Log($"OnMemberJoined is called, roomId:{roomId}, participant:{participant}");
    }

    public void OnMemberExited(string roomId, string roomName, string participant)
    {
        Debug.Log($"OnMemberExited is called, roomId:{roomId}, roomName:{roomName}, participant:{participant}");
    }

    public void OnRemovedFromChatRoom(string roomId, string roomName, string participant)
    {
        Debug.Log($"OnRemovedFromChatRoom is called, roomId:{roomId}, roomName:{roomName}, participant:{participant}");
    }

    public void OnAdminAdded(string roomId, string admin)
    {
        Debug.Log($"OnAdminAdded is called, roomId:{roomId}, admin:{admin}");
    }

    public void OnAdminRemoved(string roomId, string admin)
    {
        Debug.Log($"OnAdminRemoved is called, roomId:{roomId}, admin:{admin}");
    }

    public void OnOwnerChanged(string roomId, string newOwner, string oldOwner)
    {
        Debug.Log($"OnOwnerChanged is called, roomId:{roomId}, newOwner:{newOwner}, oldOwner:{oldOwner}");
    }

    public void OnAnnouncementChanged(string roomId, string announcement)
    {
        Debug.Log($"OnAnnouncementChanged is called, roomId:{roomId}, announcement:{announcement}");
    }

    public void OnMuteListAdded(string roomId, List<string> mutes, long expireTime)
    {
        Debug.Log($"OnMuteListAdded is called, roomId:{roomId}, expireTime:{expireTime}");
        int i = 0;
        foreach (string str in mutes)
        {
            Debug.Log($"mute item{i}: {str}");
            i++;
        }
    }

    public void OnMuteListRemoved(string roomId, List<string> mutes)
    {
        Debug.Log($"OnMuteListRemoved is called, roomId:{roomId}");
        int i = 0;
        foreach (string str in mutes)
        {
            Debug.Log($"mute item{i}: {str}");
            i++;
        }
    }

}