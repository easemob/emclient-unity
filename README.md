# unity-im-sdk

### 结构说明

`SDKClient` sdk单例，主要用于初始化，注册，登录，退出等操作。

`Options` 用户配置sdk基本信息，如appkey，推送证书等；

**Appkey是您在环信的唯一标识，需要[去环信官网申请](https://www.easemob.com/) **

示例:

```
// 初始化sdk，并设置appkey
Options options = new Options("easemob-demo#chatdemoui");
SDKClient.Instance.InitWithOptions(options);


// CallBack, 用于接受返回结果
CallBack callback = new CallBack();
callback.Success = () => {
		Debug.Log("登录成功");
};

callback.Error = (int code, string desc) => {
		Debug.Log("登录失败 error code " + code + " desc " + desc);
};

// 登录，username和password是环信的id和对应的密码，需要在环信console注册。
SDKClient.Instance.Login(username, password, handle: callback) ;
```

`IChatManager` 消息管理类，用于获取/删除会话，发送消息，撤回消息，重发消息等操作；

`Conversation` 会话类，用于管理消息，如删除/获取/插入消息等，收发消息后会自动创建会话，会话id为对方id。

```
// 创建/获取与du003的会话
Conversation conversation = SDKClient.Instance.ChatManager.GetConversation("du003");

// 从会话中获取200条消息
List<Message> msgs = conversation.LoadMessages(null, 200);

// 获取所有有消息的会话
List<Conversation>list = SDKClient.Instance.ChatManager.LoadAllConversations();
```





`Message`消息类，用户描述消息方向和包装消息体;

`MessageBody` 消息体，描述消息类型和消息内容；

示例:

```
CallBack callBack = new CallBack(
		onSuccess: () => {
				Debug.Log("unity 消息发送成功");
    },
		onError: (code, desc)=> {
				Debug.Log("unity 消息发送失败");
		}
);

// 发送文字，接收方id是du003，环信发消息时只需要知道对方的id就可以发送。
Message msg = Message.CreateTextSendMessage("du003", "文字消息");
SDKClient.Instance.ChatManager.SendMessage(msg, callBack);
```

`IContactManager`通讯录管理类，用于管理好友管理，如添加/删除好友，获取好友列表，添加/移除黑名单等；

示例:

```
CallBack callBack = new CallBack(
		onSuccess: () => {
				Debug.Log("发送加好友请求成功");
		},
		onError: (code, desc) => {
				Debug.Log("发送加好友请求失败 " + code + " desc " + desc);
		}
);
// 添加du003为好友
SDKClient.Instance.ContactManager.AddContact("du003", handle: callBack);
```

`IGroupManager` 群组管理类，用于管理群组，如创建/销毁/申请加入群组，获取已加入群组列表，以及管理群管理员，成员，禁言，群黑名单等操作；

`GroupOptions` 创建群时的群设置配置信息；

`Group` 群组类，用于描述群的各种属性；

示例：

```
// 带有参数的callback，返回创建的群信息
ValueCallBack<Group> callBack = new ValueCallBack<Group>(
		onSuccess: (group) => {
				Debug.Log("创建群组成功，群组id " + group.GroupId);
    },
		onError:(code, desc) => {
				Debug.Log("创建群组失败 " + code + " desc " + desc);
		}
);
// 设置群组为私有群，群成员可以邀请
GroupOptions groupOptions = new GroupOptions(GroupStyle.PrivateMemberCanInvite);
SDKClient.Instance.GroupManager.CreateGroup("群组名称", groupOptions, desc: "群描述", handle: callBack);
```



`IRoomManager` 聊天室管理类，用于创建/销毁/加入聊天室；(默认客户端不支持创建和销毁聊天室，需要通过服务器操作，如需客户端操作需单独开通)；

`Room` 聊天室类，用于描述聊天室的各种属性；

示例:

```
ValueCallBack<PageResult<Room>> callback = new ValueCallBack<PageResult<Room>>();
callback.OnSuccessValue = (PageResult<Room> result) => {
		foreach (var room in result.Data) {
				Debug.Log("聊天室id " + room.RoomId);
		}
};
callback.Error = (int code, string desc) => {
		Debug.Log("获取列表失败 " + code + " desc " + desc);
};
// 从服务器获取聊天室列表
SDKClient.Instance.RoomManager.FetchPublicRoomsFromServer(handle:callback);
```



```
ValueCallBack<Room> callback = new ValueCallBack<Room>();
callback.OnSuccessValue = (Room room) => {
		Debug.Log("加入成功");
};

callback.Error = (int code, string desc) =>
{
		Debug.Log("加入聊天室失败 " + code + " desc " + desc);
};
// 加入roomId的聊天室
SDKClient.Instance.RoomManager.JoinRoom(roomId, callback);
```



`IPushManager` 推送管理类，用于设置推送昵称，设置推送免打扰等操作；

`PushConfig` 推送配置对象，用于获取当前推送状态；

示例：

```
ValueCallBack<PushConfig> callback = new ValueCallBack<PushConfig>();
callback.OnSuccessValue = (PushConfig config) => {
		Debug.Log("获取推送配置成功");
};
callback.Error = (int code, string desc) =>
{
		Debug.Log("获取推送配置失败 " + code + " desc " + desc);
};
SDKClient.Instance.PushManager.GetPushConfigFromServer(callback);
```

```
CallBack callBack = new CallBack();
callBack.Success = () => {
		Debug.Log("设置免打扰成功");
};
callBack.Error = (int code, string desc) =>
{
		Debug.Log("设置免打扰失败 " + code + " desc " + desc);
};
// 设置晚10点到早8点不接收推送
SDKClient.Instance.PushManager.SetNoDisturb(true, 22, 8, callBack);
```



### 实现监听

`IConnectionDelegate` 用于监听链接状态

示例:

```
public class Main : MonoBehaviour, IConnectionDelegate
{
		private void Awake()
    {
        SDKClient.Instance.AddConnectionDelegate(this);
    }

		public void OnConnected()
    {
        Debug.Log("连接恢复-------");
    }

    public void OnDisconnected(int i)
    {
        Debug.Log("连接断开------- " + i);
    }
}
```



`IChatManagerDelegate` 用于监听消息相关回调

示例:

```
public class Main : MonoBehaviour, IChatManagerDelegate
{
		private void Awake()
    {
        SDKClient.Instance.AddChatManagerDelegate(this);
    }

		public void OnMessagesReceived(List<Message> messages)
    {
        foreach (var msg in messages) {

            switch (msg.Body.Type) {
                case MessageBodyType.TXT:
                    {
                        ChatSDK.MessageBody.TextBody body = (ChatSDK.MessageBody.TextBody)msg.Body;
                        Debug.Log("unity ---- 文字消息 " + body.Text);

                    }
                    break;
                case MessageBodyType.IMAGE:
                    {
                        ChatSDK.MessageBody.ImageBody body = (ChatSDK.MessageBody.ImageBody)msg.Body;
                        Debug.Log("unity ---- 图片消息remote path " + body.RemotePath);
                        Debug.Log("unity ---- 图片消息thumbnai remote path " + body.ThumbnaiRemotePath);
                    }
                    break;
                case MessageBodyType.VOICE:
                    {
                        ChatSDK.MessageBody.VoiceBody body = (ChatSDK.MessageBody.VoiceBody)msg.Body;
                        Debug.Log("unity ---- 短音频消息remote path " + body.RemotePath);
                        Debug.Log("unity ---- 短音频消息时长 " + body.Duration);
                    }
                    break;
                case MessageBodyType.VIDEO:
                    {
                        ChatSDK.MessageBody.VideoBody body = (ChatSDK.MessageBody.VideoBody)msg.Body;
                        Debug.Log("unity ---- 短视频消息remote path " + body.RemotePath);
                        Debug.Log("unity ---- 短视频消息时长 " + body.Duration);
                    }
                    break;
                case MessageBodyType.FILE:
                    {
                        Debug.Log("unity ---- 文件消息");
                    }
                    break;
                case MessageBodyType.CUSTOM:
                    {
                        Debug.Log("unity ---- 自定义消息");
                    }
                    break;
                case MessageBodyType.LOCATION:
                    {
                        ChatSDK.MessageBody.LocationBody body = (ChatSDK.MessageBody.LocationBody)msg.Body;
                        Debug.Log("unity ---- 位置消息 " + body.Address);
                        Debug.Log("unity ---- 位置消息 " + body.Latitude);
                        Debug.Log("unity ---- 位置消息 " + body.Longitude);
                    }
                    break;
            }
        }
    }

    public void OnCmdMessagesReceived(List<Message> messages)
    {
        Debug.Log("unity ---- 收到cmd消息");

        foreach (var msg in messages) {
            ChatSDK.MessageBody.CmdBody cmdBody = (ChatSDK.MessageBody.CmdBody)msg.Body;
            Debug.Log("cmd action -- " + cmdBody.Action);
        }

    }

    public void OnMessagesRead(List<Message> messages)
    {
        Debug.Log("unity ---- 收到消息已读");

        foreach (var msg in messages)
        {
            Debug.Log("已读消息id -- " + msg.MsgId);
        }
    }

    public void OnMessagesDelivered(List<Message> messages)
    {
        Debug.Log("unity ---- 收到消息已送达回执");

        foreach (var msg in messages)
        {
            Debug.Log("送达消息id -- " + msg.MsgId);
        }
    }

    public void OnMessagesRecalled(List<Message> messages)
    {
        foreach (var msg in messages)
        {
            Debug.Log("撤回消息id -- " + msg.MsgId);
        }
    }

    public void OnReadAckForGroupMessageUpdated()
    {
        Debug.Log("群消息已读数量变化");
    }

    public void OnGroupMessageRead(List<GroupReadAck> list)
    {
        foreach (var ack in list) {
            Debug.Log("群消息已读id -- " + ack.MsgId);
        }
    }

    public void OnConversationsUpdate()
    {
        Debug.Log("unity ---- 会话列表变化");
        List<Conversation> list = SDKClient.Instance.ChatManager.LoadAllConversations();
        foreach (var conv in list) {
            Debug.Log("unity ---- 会话id ---- " + conv.Id);
        }
    }

    public void OnConversationRead(string from, string to)
    {
        Debug.Log("会话已读");
    }
}
```



`IContactManagerDelegate` 通讯录变化回调

示例:

```
public class Main : MonoBehaviour, IContactManagerDelegate
{
		private void Awake()
    {
        SDKClient.Instance.AddContactManagerDelegate(this);
    }

		public void OnContactAdded(string username)
    {
        Debug.Log("添加通讯录 ---- username " + username);
    }

    public void OnContactDeleted(string username)
    {
        Debug.Log("删除通讯录 ---- username " + username);
    }

    public void OnContactInvited(string username, string reason)
    {
        Debug.Log("通讯录申请 ---- username " + username);
    }

    public void OnFriendRequestAccepted(string username)
    {
        Debug.Log("通讯录申请被同意 ---- username " + username);
    }

    public void OnFriendRequestDeclined(string username)
    {
        Debug.Log("通讯录申请被拒绝 ---- username " + username);
    }
}
```

`IGroupManagerDelegate` 群变化回调

```
public class Main : MonoBehaviour, IGroupManagerDelegate
{
		private void Awake()
    {
        SDKClient.Instance.AddGroupManagerDelegate(this);
    }

		public void OnInvitationReceivedFromGroup(string groupId, string groupName, string inviter, string reason)
    {
        this.groupId = groupId;
        Debug.Log("收到群组邀请 " + groupId +  " " + groupName);
    }

    public void OnRequestToJoinReceivedFromGroup(string groupId, string groupName, string applicant, string reason)
    {
        Debug.Log("收到加群申请 " + groupId + " " + groupName);
    }

    public void OnRequestToJoinAcceptedFromGroup(string groupId, string groupName, string accepter)
    {
        Debug.Log("加群申请被同意 " + groupId + " " + groupName);
    }

    public void OnRequestToJoinDeclinedFromGroup(string groupId, string groupName, string decliner, string reason)
    {
        Debug.Log("加群申请被拒绝 " + groupId + " " + groupName);
    }

    public void OnInvitationAcceptedFromGroup(string groupId, string invitee, string reason)
    {
        Debug.Log("收到群组邀请被同意 " + groupId + " " + invitee);
    }

    public void OnInvitationDeclinedFromGroup(string groupId, string invitee, string reason)
    {
        Debug.Log("收到群组邀请被拒绝 " + groupId + " " + invitee);
    }

    public void OnUserRemovedFromGroup(string groupId, string groupName)
    {
        Debug.Log("被群组移除 " + groupId + " " + groupName);
    }

    public void OnDestroyedFromGroup(string groupId, string groupName)
    {
        Debug.Log("群组解散 " + groupId + " " + groupName);
    }

    public void OnAutoAcceptInvitationFromGroup(string groupId, string inviter, string inviteMessage)
    {
        Debug.Log("自动同意群组邀请 " + groupId + " " + inviter);
    }

    public void OnMuteListAddedFromGroup(string groupId, List<string> mutes, int muteExpire)
    {
        foreach (var username in mutes) {
            Debug.Log("被加入到禁言列表 " + groupId + " " + username);
        }
    }

    public void OnMuteListRemovedFromGroup(string groupId, List<string> mutes)
    {
        foreach (var username in mutes)
        {
            Debug.Log("从禁言列表中移除 " + groupId + " " + username);
        }
    }

    public void OnAdminAddedFromGroup(string groupId, string administrator)
    {
        Debug.Log("被添加为管理员 " + groupId + " " + administrator);
    }

    public void OnAdminRemovedFromGroup(string groupId, string administrator)
    {
        Debug.Log("被移除管理员 " + groupId + " " + administrator);
    }

    public void OnOwnerChangedFromGroup(string groupId, string newOwner, string oldOwner)
    {
        Debug.Log("群主变化 " + groupId + " 新群主 " + newOwner + "  旧群主 " + oldOwner);
    }

    public void OnMemberJoinedFromGroup(string groupId, string member)
    {
        Debug.Log("进入群 " + groupId + " " + member);
    }

    public void OnMemberExitedFromGroup(string groupId, string member)
    {
        Debug.Log("离开群 " + groupId + " " + member);
    }

    public void OnAnnouncementChangedFromGroup(string groupId, string announcement)
    {
        Debug.Log("群描述变化 " + groupId + " " + announcement);
    }

    public void OnSharedFileAddedFromGroup(string groupId, GroupSharedFile sharedFile)
    {
        Debug.Log("群文件添加 " + groupId + " " + sharedFile.FileId);
    }

    public void OnSharedFileDeletedFromGroup(string groupId, string fileId)
    {
        Debug.Log("群文件移除 " + groupId + " " + fileId);
    }
}
```



`IRoomManagerDelegate` 聊天室变化回调

示例：

```
public class Main : MonoBehaviour, IRoomManagerDelegate
{
		private void Awake()
    {
        SDKClient.Instance.AddRoomManagerDelegate(this);
    }

		public void OnDestroyedFromRoom(string roomId, string roomName)
    {
        Debug.Log("聊天室解散 " + roomId + " " + roomName);
    }

    public void OnMemberJoinedFromRoom(string roomId, string participant)
    {
        Debug.Log("用户加入聊天室 " + roomId + " " + participant);
    }

    public void OnMemberExitedFromRoom(string roomId, string roomName, string participant)
    {
        Debug.Log("用户离开聊天室 " + roomId + " " + participant);
    }

    public void OnRemovedFromRoom(string roomId, string roomName, string participant)
    {
        Debug.Log("用户被从聊天室删除 " + roomId + " " + roomName + " " + participant);
    }

    public void OnMuteListAddedFromRoom(string roomId, List<string> mutes, long expireTime)
    {
        foreach (var s in mutes) {
            Debug.Log("用户被加入禁言 " + roomId + " " + s + " " + expireTime.ToString());
        }
    }

    public void OnMuteListRemovedFromRoom(string roomId, List<string> mutes)
    {
        foreach (var s in mutes)
        {
            Debug.Log("用户被移除禁言 " + roomId + " " + s);
        }
    }

    public void OnAdminAddedFromRoom(string roomId, string admin)
    {
        Debug.Log("聊天室添加管理员 " + roomId + " " + admin);
    }

    public void OnAdminRemovedFromRoom(string roomId, string admin)
    {
        Debug.Log("聊天室移除管理员 " + roomId + " " + admin);
    }

    public void OnOwnerChangedFromRoom(string roomId, string newOwner, string oldOwner)
    {
        Debug.Log("聊天室更换房主 " + roomId + " 新房主 " + newOwner + " 旧房主 " + oldOwner);
    }

    public void OnAnnouncementChangedFromRoom(string roomId, string announcement)
    {
        Debug.Log("聊天室描述更新 " + roomId + " " + announcement);
    
}
```

