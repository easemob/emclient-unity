# 环信IM Unity SDK

文章主要讲解环信 IM Unity SDK 如何使用。

[环信官网](https://www.easemob.com/)

[环信Unity集成文档](https://doc.easemob.com/document/unity/quickstart.html)

[环信Windows集成文档](https://doc.easemob.com/document/windows/quickstart.html)

[环信iOS集成文档](http://docs-im.easemob.com/im/ios/sdk/prepare)

[环信Android集成文档](http://docs-im.easemob.com/im/android/sdk/import)

源码地址: [Github](https://github.com/easemob/emclient-unity)  
任何问题可以通过 [Github Issues](https://github.com/easemob/emclient-unity/issues) 提问

**QQ群: 891289413**

## 前期准备

如果您还没有 Appkey，可以前往[环信官网](https://www.easemob.com/)注册[即时通讯云](https://console.easemob.com/user/register)。

进入 console -> 添加应用 -> Appkey 获取 `Appkey`。

### 下载 SDK

[UnitySDK下载页面](https://www.easemob.com/download/im)

#### 导入SDK 到 Unity

`Assets` -> `Import Package` -> `Custom Package..`  -> `imUnitySDK.unitypackage`

## SDK讲解

- `SDKClient` 用于管理sdk各个模块和一些账号相关的操作，如注册，登录，退出;
- `IChatManager`用于管理聊天相关操作，如发送消息，接收消息，发送已读回执，获取会话列表等;
- `IContactManager` 用于管理通讯录相关操作，如获取通讯录列表，添加好友，删除好友等;
- `IGroupManager`用于群组相关操作，如获取群组列表，加入群组，离开群组等;
- `IRoomManager`用于管理聊天室，如获取聊天室列表;


### SDKClient

#### 初始化

```C#
// 设置 Appkey
Options options = new Options(appKey: "easemob-demo#easeim");

// 初始化 sdk
SDKClient.Instance.InitWithOptions(options);
```


#### 注册

```C#
SDKClient.Instance.CreateAccount("username", "password", new CallBack(
    onSuccess: () => {
        Debug.Log("执行成功");
    },
    onError: (code, desc) => {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

>客户端注册需要将注册方式设置为`开放注册`，具体说明请参考文档[用户管理](http://docs-im.easemob.com/im/server/ready/user#%E7%94%A8%E6%88%B7%E7%AE%A1%E7%90%86)。

#### 登录

```C#
SDKClient.Instance.Login("username", "password", handle: new CallBack(
    onSuccess: () => {
        Debug.Log("执行成功");
    },
    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
}
```

#### 获取当前登录环信id

```C#
String currentUsername = SDKClient.Instance.CurrentUsername();
```

#### 退出

```C#
SDKClient.Instance.Logout(true, new CallBack(
    onSuccess: () => {
        Debug.Log("执行成功");
    },
    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

>退出也有失败的情况，需要确定是否失败。  
>注册环信id详细说明请参考文档[用户体系集成](http://docs-im.easemob.com/im/server/ready/user)。


#### 监听服务器连接状态

```C#
public class TestSDK : MonoBehaviour, IConnectionDelegate
{

    private void Start()
    {
        // 添加监听，需在sdk 初始化后调用
        SDKClient.Instance.AddConnectionDelegate(this);
    }

    private void OnDestroy()
    {
        // 移除监听
        SDKClient.Instance.DeleteConnectionDelegate(this);
    }

    // 成功连接回调
    public void OnConnected()
    {

    }

    // 服务器断开连接时触发的回调
    public void OnDisconnected()
    {

    }

    // 当前登录账号在其它设备登录时会接收到此回调
    public void OnLoggedOtherDevice(string deviceName)
    {

    }

    // 当前登录账号已经被从服务器端删除时会收到该回调
    public void OnRemovedFromServer()
    {

    }

    // 当前用户账号被禁用时会收到该回调
    public void OnForbidByServer()
    {

    }

    // 当前登录账号因密码被修改被强制退出
    public void OnChangedIMPwd()
    {

    }

    // 当前登录账号因达到登录设备数量上限被强制退出
    public void OnLoginTooManyDevice()
    {

    }

    // 当前登录设备账号被登录其他设备的同账号踢下线
    public void OnKickedByOtherDevice()
    {

    }

    // 当前登录设备账号因鉴权失败强制退出
    public void OnAuthFailed()
    {

    }

    // Token 过期回调
    public void OnTokenExpired()
    {

    }

    // Token 即将过期回调
    public void OnTokenWillExpire()
    {

    }

    // App 激活数量已达到上限值
    public void OnAppActiveNumberReachLimitation()
    {

    }
}

```

#### 获取当前连接状态

```C#
bool isConnected = SDKClient.Instance.IsConnected;
```

#### 获取 unity sdk 版本号

```C#
string version = SDKClient.Instance.SdkVersion;
```

### IChatManager

#### 获取会话列表	

```C#
List<Conversation>conversations = SDKClient.Instance.ChatManager.LoadAllConversations();
```

>会话列表是存在本地的一种消息管理对象，如果您会话中没有消息则表示会话不存在。


#### 创建会话

```C#
// emId: 会话对应环信id, 如果是群组或者聊天室，则为群组id或者聊天室id
  Conversation conv = SDKClient.Instance.ChatManager.GetConversation("emId");
```

#### 获取会话中的消息

```C#
Conversation conv = SDKClient.Instance.ChatManager.GetConversation("emId");
conv.LoadMessages(handle:new ValueCallBack<List<Message>>(
    onSuccess: (messages) => {
        Debug.Log("获取成功");
    },
    onError:(code, desc) => {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```
#### 获取会话中未读消息数

```C#
Conversation conv = SDKClient.Instance.ChatManager.GetConversation("conversationId");
int unreadCount = conv.UnReadCount;
```

#### 设置单条消息为已读

```C#
Conversation conv = SDKClient.Instance.ChatManager.GetConversation("emId");
conv.MarkMessageAsRead("messageId");
```

#### 设置所有消息为已读

```C#
Conversation conv = SDKClient.Instance.ChatManager.GetConversation("emId");
conv.MarkAllMessageAsRead();
```

#### 发送消息已读状态

```C#
SDKClient.Instance.ChatManager.SendMessageReadAck("messageId", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },
    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

#### 删除会话中的消息

```C#
Conversation conv = SDKClient.Instance.ChatManager.GetConversation("emId");
conv.DeleteMessage("messageId");
```

#### 删除会话中所有的消息
```C#
Conversation conv = SDKClient.Instance.ChatManager.GetConversation("conversationId");
conv.DeleteAllMessages();
```

#### 插入消息

```C#
Conversation conv = SDKClient.Instance.ChatManager.GetConversation("emId");
conv.InsertMessage(message);
```

> SDK在您发送和接收消息(_cmd类型消息除外_)后会自动将消息插入数据库中，并不需要您自己将消息插入数据库，但如果您需要自己插入一条消息时可以调用该api。

#### 更新消息

```C#
Conversation conv = SDKClient.Instance.ChatManager.GetConversation("emId");
conv.UpdateMessage(message);
```


#### 删除会话

```C#
SDKClient.Instance.ChatManager.DeleteConversation("emId");
```
​	
#### 构建要发送的消息

```C#
// 文本消息
Message textMessage = Message.CreateTextSendMessage("接收方id", "消息内容");

// 图片消息
Message imgMessage = Message.CreateImageSendMessage("接收方id", localPath: "图片路径", displayName: "图片名称.xx");

// 视频消息
Message videoMessage = Message.CreateVideoSendMessage("接收方id", localPath: "适配路径", displayName: "适配文件名称.xx");

// 音频消息
Message voiceMessage = Message.CreateVoiceSendMessage("接收方id", localPath: "音频文件路径", displayName: "音频文件名称.xx", duration: 10);

// 文件消息
Message fileMessage = Message.CreateFileSendMessage("接收方id", localPath:"文件路径", displayName:"文件名称.xx");

// 位置消息
Message localMessage = Message.CreateLocationSendMessage("接收方id", address:"位置名称", latitude:0.11, longitude:0.31);

// cmd消息
Message cmdMessage = Message.CreateCmdSendMessage("接收方id", action: "自定义事件");

// 自定义消息
Message customMessage = Message.CreateCustomSendMessage("接收方id", customEvent: "自定义事件");
```

> 如果您是要构建群组或者聊天室消息，需要修改消息的`MessageType`属性, 如:   
> `message.MessageType = MessageType.Group;`

#### 发送消息

```C#
SDKClient.Instance.ChatManager.SendMessage(ref message, new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    },
    // 只有带有附件的消息才会回调进度
    onProgress: (progress) => { }
));
```
>环信收发消息并不需要对方是您通讯录中的成员，只要知道对方的环信id就可以发送消息。

```
#### 撤回消息

```C#
SDKClient.Instance.ChatManager.RecallMessage("messageId", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 消息撤回为增值服务，您只能撤回2分钟内的消息，如需开通，请[咨询商务](https://www.easemob.com/pricing/im#p08)。


#### 收消息监听

```C#

public class TestSDK : MonoBehaviour, IChatManagerDelegate
{

    private void Start()
    {
        // 添加监听，需在sdk 初始化后调用
        SDKClient.Instance.ChatManager.AddChatManagerDelegate(this);
    }

    private void OnDestroy()
    {
        // 移除监听
        SDKClient.Instance.ChatManager.AddChatManagerDelegate(this);
    }

    // 收到消息
    public void OnMessagesReceived(List<Message> messages)
    {

    }

    // 收到CMD消息
    public void OnCmdMessagesReceived(List<Message> messages)
    {

    }

    // 发出的消息已读回调
    public void OnMessagesRead(List<Message> messages)
    {

    }

    // 发出的消息已送达回调
    public void OnMessagesDelivered(List<Message> messages)
    {

    }

    // 消息被撤回回调
    public void OnMessagesRecalled(List<Message> messages)
    {

    }

    // 发出的群消息已读更新
    public void OnReadAckForGroupMessageUpdated()
    {

    }

    // 发出的群消息已读
    public void OnGroupMessageRead(List<GroupReadAck> list)
    {

    }

    // 会话列表变化
    public void OnConversationsUpdate()
    {

    }

    // 会话已读
    public void OnConversationRead(string from, string to)
    {

    }

    // 消息Reaction发生变更
    public void MessageReactionDidChange(List<MessageReactionChange> list)
    {

    }

    // 消息内容发生变更
    public void OnMessageContentChanged(Message msg, string operatorId, long operationTime)
    {

    }
}


```

### IContactManager

#### 从服务器获取通讯录中的用户列表

```C#
SDKClient.Instance.ContactManager.GetAllContactsFromServer(new ValueCallBack<List<string>>(
    onSuccess: (list) => {
        Debug.Log("执行成功");
    },

    onError:(code, desc) => {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```


#### 发送添加申请

```C#
SDKClient.Instance.ContactManager.AddContact("emId", "hello", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 添加申请不会发推送，如果用户不在线，等上线后会收到。

#### 删除通讯录中的成员

```C#
SDKClient.Instance.ContactManager.DeleteContact("emId", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

#### 从服务器获取黑名单

```C#
SDKClient.Instance.ContactManager.GetBlockListFromServer(new ValueCallBack<List<string>>(
    onSuccess: (list) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

#### 添加用户到黑名单中

```C#
SDKClient.Instance.ContactManager.AddUserToBlockList("emId", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 黑名单和通讯录是独立的，被添加人不需要在您的通讯录中，如果是通讯录中成员被加入到黑名单后，他仍然会出现在您的通讯录名单中，同时他也会出现在您的黑名单中。被添加到黑名单后，您双方均无法收发对方的消息。

#### 将用户从黑名单中删除

```C#
SDKClient.Instance.ContactManager.RemoveUserFromBlockList("emId", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```


#### 通讯录监听

```C#
public class TestSDK : MonoBehaviour, IContactManagerDelegate
{

    private void Start()
    {
        // 添加监听，需在sdk 初始化后调用
        SDKClient.Instance.ContactManager.AddContactManagerDelegate(this);
    }

    private void OnDestroy()
    {
        // 移除监听
        SDKClient.Instance.ContactManager.RemoveContactManagerDelegate(this);
    }

    // 联系人增加回调
    public void OnContactAdded(string username)
    {

    }

    // 联系人减少回调
    public void OnContactDeleted(string username)
    {

    }

    // 收到添加好友请求
    public void OnContactInvited(string username, string reason)
    {

    }

    // 发出的添加好友请求被同意
    public void OnFriendRequestAccepted(string username)
    {

    }

    // 发出的添加好友请求被解决
    public void OnFriendRequestDeclined(string username)
    {

    }
}

```

#### 同意添加申请

```C#
SDKClient.Instance.ContactManager.AcceptInvitation("emId", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

#### 拒绝添加申请

```C#
SDKClient.Instance.ContactManager.DeclineInvitation("emId", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

### IGroupManager

#### 从服务器获取已加入群组列表

```C#
SDKClient.Instance.GroupManager.FetchJoinedGroupsFromServer(handle: new ValueCallBack<List<Group>>(
    onSuccess: (list) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

#### 从缓存中获取已加入群组列表

```C#
List<Group>groups = SDKClient.Instance.G服务器获取公开群组列表

```C#
SDKClient.Instance.GroupManchPublicGrouomServer(new ValueCallBack<CursorResult<GroupInfo>>(
    onSuccess: (result) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

#### 创建群组

```C#
SDKClient.Instance.GroupManager.CreateGroup("群组名称", options: new GroupOptions(GroupStyle.PrivateMemberCanInvite), new ValueCallBack<Group>(
    onSuccess: (group) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> GroupOptions `GroupStyle` 等参数进行设置，群组有四种，分别是:
 `PrivateOnlyOwnerInvite` 私有群，只有群主和管理员能邀请他人进群，被邀请人会收到邀请信息，同意后可入群;
 `PrivateMemberCanInvite` 私有群，所有人都可以邀请他人进群，被邀请人会收到邀请信息，同意后可入群;
 `PublicJoinNeedApproval` 公开群，可以通过获取公开群列表api取的，申请加入时需要群主或管理员同意;
 `PublicOpenJoin` 公开群，可以通过获取公开群列表api取，可以直接进入;


#### 获取群组详情

```C#
SDKClient.Instance.GroupManager.GetGroupSpecificationFromServer("groupId", new ValueCallBack<Group>(
    onSuccess: (group) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

#### 获取群成员列表

```C#
SDKClient.Instance.GroupManager.GetGroupMemberListFromServer("groupId", handle: new ValueCallBack<CursorResult<string>>(
    onSuccess: (result) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

#### 加入公开群组

```C#
SDKClient.Instance.GroupManager.JoinPublicGroup("groupId", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 需要群组类型是 `PublicOpenJoin` ,调用后直接加入群组。

#### 申请加入公开群

```C#
SDKClient.Instance.GroupManager.applyJoinToGroup("groupId", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 需要群组类型是 `PublicJoinNeedApproval` ,申请后，群主和管理员会收到加群邀请，同意后入群。

#### 邀请用户入群

```C#
SDKClient.Instance.GroupManager.AddGroupMembers("groupId", members, new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 需要群组类型是 `PrivateOnlyOwnerInvite` 或 `PrivateMemberCanInvite` ,
 `PrivateOnlyOwnerInvite` 时，群主和管理员可以调用；
 `PrivateMemberCanInvite ` 是，群中任何人都可以调用；
被邀请方会收到邀请通知，同意后进群。邀请通知并不会以推送的形式发出，如果用户不在线，等上线后会收到，用户同意后入群。

#### 从群组中删除用户

```C#
SDKClient.Instance.GroupManager.DeleteGroupMembers("emId", members, new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 群主和管理员可以调用。

#### 添加管理员

```C#
SDKClient.Instance.GroupManager.AddGroupAdmin("emId", "memberId", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 群主可以调用。被操作人会收到被添加为管理员回调，该回调无推送，如用户不在线，上线后会收到。


#### 移除管理员

```C#
SDKClient.Instance.GroupManager.RemoveGroupAdmin("emId", "adminId", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 群主可以调用。被操作人会收到被移除管理员回调，该回调无推送，如用户不在线，上线后会收到。

#### 退出群组

```C#
SDKClient.Instance.GroupManager.LeaveGroup("emId", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

#### 解散群组

```C#
SDKClient.Instance.GroupManager.DestroyGroup("emId", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 只有群主可以调用。

#### 转移群组

```C#
SDKClient.Instance.GroupManager.ChangeGroupOwner("emId", "newOwnerId", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 只有群主可以调用。

#### 获取群组黑名单列表

```C#
SDKClient.Instance.GroupManager.GetGroupBlockListFromServer("emId", new ValueCallBack<List<string>>(
    onSuccess: (list) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

#### 将群成员添加到群黑名单

```C#
SDKClient.Instance.GroupManager.BlockGroupMembers("groupId", members, new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 该方法只有群主和管理员可以调用，被操作用户当前必须是群成员，当用户被加入到群黑名单后，该用户将从群成员中移除并加入到当前群的黑名单中。同时该用户将无法再进入该群。

#### 将用户从黑名单移除

```C#
SDKClient.Instance.GroupManager.UnBlockGroupMembers("groupId", members, new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 该方法只有群主和管理员可以调用，当账号从黑名单中移除后可以再允许申请加群。

#### 获取群禁言列表

```C#
SDKClient.Instance.GroupManager.GetGroupMuteListFromServer("groupId", handle: new ValueCallBack<List<string>>(
    onSuccess: (list) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));

```

#### 对成员禁言

```C#
SDKClient.Instance.GroupManager.MuteGroupMembers("groupId", members, new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 该方法只有群主和管理员可以调用，被禁言的用户仍然可以收到群中的消息，但是无法发出消息， 白名单中的用户即使被加入到禁言列表中也不受影响。

#### 对成员解除禁言

```C#
SDKClient.Instance.GroupManager.UnMuteGroupMembers("groupId", members, new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 该方法只有群主和管理员可以调用。

#### 对所有成员禁言

```C#
SDKClient.Instance.GroupManager.MuteGroupAllMembers("groupId", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

>该方法只有群主和管理员可以调用，对群主，管理员，白名单中的成员无效，且针对所有人的`禁言`操作与 `muteMembers` 、 `unMuteMembers` 接口不冲突，该接口的操作并不会导致 `getGroupMuteListFromServer` 接口的返回的数据变化。

#### 对所有成员解除禁言

```C#
SDKClient.Instance.GroupManager.UnMuteGroupAllMembers("groupId", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 该方法只有群主和管理员可以调用，且针对所有人的`解除禁言`操作与`muteMembers`、`unMuteMembers`接口不冲突，该接口的操作并不会导致`getGroupMuteListFromServer`接口的返回的数据变化。当调用该方法后，之前在禁言列表中的用户仍在禁言列表中，且仍处于禁言状态。

#### 获取白名单列表

```C#
SDKClient.Instance.GroupManager.GetGroupAllowListFromServer("groupId", new ValueCallBack<List<string>>(
    onSuccess: (list) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

#### 将用户添加到白名单中

```C#
SDKClient.Instance.GroupManager.AddGroupAllowList("groupId", members, new ValueCallBack<List<string>>(
    onSuccess: (list) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 该方法只有群主和管理员可以调用，当用户被加入到白名单后，当群组全部禁言或者被添加到禁言列表后仍可以发言。

#### 将用户从白名单中移除

```C#
SDKClient.Instance.GroupManager.RemoveGroupAllowList("groupId", members, new ValueCallBack<List<string>>(
    onSuccess: (list) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 该方法只有群主和管理员可以调用。

#### 判断自己是否在白名单中

```C#
SDKClient.Instance.GroupManager.CheckIfInGroupAllowList("groupId",  new ValueCallBack<bool>(
    onSuccess: (result) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

#### 不接收群消息

```C#
SDKClient.Instance.GroupManager.BlockGroup("groupId",  new ValueCallBack<bool>(
    onSuccess: (result) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

>设置后群组中的所有消息都无法收到，用户不在线时也不会有推送告知。

#### 恢复接收群消息

```C#
SDKClient.Instance.GroupManager.UnBlockGroup("groupId",  new ValueCallBack<bool>(
    onSuccess: (result) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

#### 更新群名称

```C#
SDKClient.Instance.GroupManager.ChangeGroupName("groupId", "new name", new ValueCallBack<bool>(
    onSuccess: (result) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 群主或管理员可以调用。

#### 更新群描述

```C#
SDKClient.Instance.GroupManager.ChangeGroupDescription("groupId", "new description", new ValueCallBack<bool>(
    onSuccess: (result) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 群主或管理员可以调用。


#### 获取群组公告

```C#
SDKClient.Instance.GroupManager.GetGroupAnnouncementFromServer("groupId", new ValueCallBack<string>(
    onSuccess: (result) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

#### 更新群公告

```C#
SDKClient.Instance.GroupManager.UpdateGroupAnnouncement("groupId", "new announcement",new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 群主或管理员可以调用。

#### 获取群共享文件列表

```C#
SDKClient.Instance.GroupManager.GetGroupFileListFromServer("groupId", new ValueCallBack<List<GroupSharedFile>>(
    onSuccess: (list) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

#### 上传群共享文件

```C#
SDKClient.Instance.GroupManager.UploadGroupSharedFile("groupId", "file path", handle: new ValueCallBack<List<GroupSharedFile>>(
    onSuccess: (list) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

#### 下载群共享文件

```C#
SDKClient.Instance.GroupManager.DownloadGroupSharedFile("groupId", "fileId", savePath, new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

#### 删除群共享文件

```C#
SDKClient.Instance.GroupManager.DeleteGroupSharedFile("groupId", "fileId", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 群主，管理员，文件上传者可以调用。

#### 群回调监听

```C#
public class TestSDK : MonoBehaviour, IGroupManagerDelegate
{

    private void Start()
    {
        // 添加监听，需在sdk 初始化后调用
        SDKClient.Instance.GroupManager.AddGroupManagerDelegate(this);
    }

    private void OnDestroy()
    {
        // 移除监听
        SDKClient.Instance.GroupManager.RemoveGroupManagerDelegate(this);
    }

    // 收到加群邀请
    public void OnInvitationReceivedFromGroup(string groupId, string groupName, string inviter, string reason)
    {

    }

    // 收到加群申请
    public void OnRequestToJoinReceivedFromGroup(string groupId, string groupName, string applicant, string reason)
    {

    }

    // 发出的加群申请被同意
    public void OnRequestToJoinAcceptedFromGroup(string groupId, string groupName, string accepter)
    {

    }

    // 发出的加群申请被拒绝
    public void OnRequestToJoinDeclinedFromGroup(string groupId, string groupName, string decliner, string reason)
    {

    }

    // 发出的加群邀请被同意
    public void OnInvitationAcceptedFromGroup(string groupId, string invitee)
    {

    }

    // 发出的加群邀请被拒绝
    public void OnInvitationDeclinedFromGroup(string groupId, string invitee, string reason)
    {

    }

    // 群成员被删除
    public void OnUserRemovedFromGroup(string groupId, string groupName)
    {

    }

    // 群解散
    public void OnDestroyedFromGroup(string groupId, string groupName)
    {

    }

    // 自动同意加群邀请
    public void OnAutoAcceptInvitationFromGroup(string groupId, string inviter, string inviteMessage)
    {

    }

    // 收到禁言列表增加回调
    public void OnMuteListAddedFromGroup(string groupId, List<string> mutes, long muteExpire)
    {

    }

    // 收到禁言列表减少回调
    public void OnMuteListRemovedFromGroup(string groupId, List<string> mutes)
    {

    }

    // 收到管理员列表增加回调
    public void OnAdminAddedFromGroup(string groupId, string administrator)
    {

    }

    // 收到管理员列表减少回调
    public void OnAdminRemovedFromGroup(string groupId, string administrator)
    {

    }

    // 收到群主变更回调
    public void OnOwnerChangedFromGroup(string groupId, string newOwner, string oldOwner)
    {

    }

    // 收到群成员增加回调
    public void OnMemberJoinedFromGroup(string groupId, string member)
    {

    }

    // 收到群成员减少回调
    public void OnMemberExitedFromGroup(string groupId, string member)
    {

    }

    // 收到群公告变更回调
    public void OnAnnouncementChangedFromGroup(string groupId, string announcement)
    {

    }

    // 收到群文件增加回调
    public void OnSharedFileAddedFromGroup(string groupId, GroupSharedFile sharedFile)
    {

    }

    // 收到群文件减少回调
    public void OnSharedFileDeletedFromGroup(string groupId, string fileId)
    {

    }

    // 收到允许列表添加回调
    public void OnAddAllowListMembersFromGroup(string groupId, List<string> allowList)
    {

    }

    // 收到允许列表删除回调
    public void OnRemoveAllowListMembersFromGroup(string groupId, List<string> allowList)
    {

    }

    // 收到全体成员禁言状态变化回调
    public void OnAllMemberMuteChangedFromGroup(string groupId, bool isAllMuted)
    {

    }

    // 收到群组禁用状态发生变化回调
    public void OnStateChangedFromGroup(string groupId, bool isDisable)
    {

    }

    // 收到群详情发生了变更回调
    public void OnSpecificationChangedFromGroup(Group group)
    {

    }

    // 收到群成员自定义属性发生了变更回调
    public void OnUpdateMemberAttributesFromGroup(string groupId, string userId, Dictionary<string, string> attributes, string from)
    {

    }
}

```

#### 同意加群申请

```C#
SDKClient.Instance.GroupManager.AcceptGroupJoinApplication("groupId", "memberId", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 群主和管理员可以调用。

#### 拒绝加群申请

```C#
SDKClient.Instance.GroupManager.DeclineGroupJoinApplication("groupId", "memberId", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 群主和管理员可以调用。

#### 同意加群邀请

```C#
SDKClient.Instance.GroupManager.AcceptGroupInvitation("groupId", new ValueCallBack<Group>(
    onSuccess: (group) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

#### 拒绝加群邀请

```C#
SDKClient.Instance.GroupManager.DeclineGroupInvitation("groupId", handle: new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

### IRoomManager

#### 从服务器获取聊天室列表

```C#
SDKClient.Instance.RoomManager.FetchPublicRoomsFromServer(handle: new ValueCallBack<PageResult<Room>>(
    onSuccess: (result) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

#### 创建聊天室

```C#
SDKClient.Instance.RoomManager.CreateRoom("RoomName", new ValueCallBack<Room>(
    onSuccess: (room) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

>聊天室创建需要单独拥有权限，具体可以参考文档[聊天室管理](http://docs-im.easemob.com/im/server/basics/chatroom)。

#### 加入聊天室

```C#
SDKClient.Instance.RoomManager.JoinRoom("roomId", new ValueCallBack<Room>(
    onSuccess: (result) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

#### 离开聊天室

```C#
SDKClient.Instance.RoomManager.LeaveRoom("roomId", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

#### 销毁聊天室

```C#
SDKClient.Instance.RoomManager.DestroyRoom("roomId", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

>聊天室销毁需要单独拥有权限，具体可以参考文档[聊天室管理](http://docs-im.easemob.com/im/server/basics/chatroom)。

#### 转移聊天室

```C#
SDKClient.Instance.RoomManager.ChangeRoomOwner("roomId", "newOwnerId", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

>聊天室转移需要单独拥有权限，具体可以参考文档[聊天室管理](http://docs-im.easemob.com/im/server/basics/chatroom)。

#### 获取聊天室详情

```C#
SDKClient.Instance.RoomManager.FetchRoomInfoFromServer("roomId", new ValueCallBack<Room>(
    onSuccess: (room) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

#### 获取聊天室成员

```C#
SDKClient.Instance.RoomManager.FetchRoomMembers("roomId", new ValueCallBack<CursorResult<string>>(
    onSuccess: (result) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

#### 移除聊天室成员

```C#
SDKClient.Instance.RoomManager.DeleteRoomMembers("roomId", members, new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 创建者或管理员调用。

#### 添加管理员

```C#
SDKClient.Instance.RoomManager.AddRoomAdmin("roomId", "memberId", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));

```

> 创建者调用，被操作者会收到回调。

#### 移除管理员

```C#
SDKClient.Instance.RoomManager.RemoveRoomAdmin("roomId", "adminId", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 创建者调用，被操作者会收到回调。

#### 获取禁言列表

```C#
SDKClient.Instance.RoomManager.FetchRoomMuteList("roomId",  new ValueCallBack<List<string>>(
    onSuccess: (list) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

#### 设置禁言

```C#
SDKClient.Instance.RoomManager.MuteRoomMembers("roomId", members, new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 创建者或者管理员调用。

#### 解除禁言

```C#
SDKClient.Instance.RoomManager.UnMuteRoomMembers("roomId", members, new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 创建者或者管理员调用。

#### 获取黑名单列表

```C#
SDKClient.Instance.RoomManager.FetchRoomBlockList("roomId", new ValueCallBack<List<string>>(
    onSuccess: (list) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

#### 添加黑名单

```C#
SDKClient.Instance.RoomManager.BlockRoomMembers("roomId", members, new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 创建者或管理员调用。

#### 移除黑名单

```C#
SDKClient.Instance.RoomManager.UnBlockRoomMembers("roomId", members, new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 创建者或管理员调用。


#### 修改聊天室名称

```C#
SDKClient.Instance.RoomManager.ChangeRoomName("roomId", "new name", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 创建者或管理员调用。

#### 修改聊天室描述

```C#
SDKClient.Instance.RoomManager.ChangeRoomDescription("roomId", "new description", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 创建者或管理员调用。

#### 获取聊天室公告

```C#
SDKClient.Instance.RoomManager.FetchRoomAnnouncement("roomId", new ValueCallBack<string>(
    onSuccess: (result) =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

#### 修改聊天室公告

```C#
SDKClient.Instance.RoomManager.UpdateRoomAnnouncement("roomId", "new announcement", new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
```

> 创建者或管理员调用

#### 添加聊天室监听

```C#
public class TestSDK : MonoBehaviour, IRoomManagerDelegate
{

    private void Start()
    {
        // 添加监听，需在sdk 初始化后调用
        SDKClient.Instance.RoomManager.AddRoomManagerDelegate(this);
    }

    private void OnDestroy()
    {
        // 移除监听
        SDKClient.Instance.RoomManager.RemoveRoomManagerDelegate(this);
    }


    // 聊天室被销毁回调
    public void OnDestroyedFromRoom(string roomId, string roomName)
    {

    }

    // 有用户加入聊天室回调
    public void OnMemberJoinedFromRoom(string roomId, string participant)
    {

    }

    // 有用户离开聊天室回调
    public void OnMemberExitedFromRoom(string roomId, string roomName, string participant)
    {

    }

    // 当前账号被移出聊天室
    public void OnRemovedFromRoom(string roomId, string roomName, string participant)
    {

    }

    // 被禁言用户增加
    public void OnMuteListAddedFromRoom(string roomId, List<string> mutes, long expireTime)
    {

    }

    // 被禁言用户减少
    public void OnMuteListRemovedFromRoom(string roomId, List<string> mutes)
    {

    }

    // 管理员增加
    public void OnAdminAddedFromRoom(string roomId, string admin)
    {

    }

    // 管理员减少
    public void OnAdminRemovedFromRoom(string roomId, string admin)
    {

    }

    // 聊天室拥有者变更
    public void OnOwnerChangedFromRoom(string roomId, string newOwner, string oldOwner)
    {

    }

    // 聊天室公告变更
    public void OnAnnouncementChangedFromRoom(string roomId, string announcement)
    {

    }

    // 聊天室成员因为离线被移除
    public void OnRemoveFromRoomByOffline(string roomId, string roomName)
    {
    }
}

```
