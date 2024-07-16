package com.hyphenate.wrapper.util;

import com.hyphenate.chat.EMChatRoom;
import com.hyphenate.chat.EMChatThreadEvent;
import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMCursorResult;
import com.hyphenate.chat.EMGroup;
import com.hyphenate.chat.EMGroupReadAck;
import com.hyphenate.chat.EMMessage;
import com.hyphenate.chat.EMMessageReactionChange;
import com.hyphenate.chat.EMMucSharedFile;
import com.hyphenate.chat.EMPageResult;
import com.hyphenate.chat.EMPresence;
import com.hyphenate.chat.EMRecallMessageInfo;
import com.hyphenate.cwrapper.EMCWrapperListener;
import com.hyphenate.exceptions.HyphenateException;
import com.hyphenate.wrapper.EMClientWrapper;
import com.hyphenate.wrapper.EMWrapperHelper;
import com.hyphenate.wrapper.helper.EMChatRoomHelper;
import com.hyphenate.wrapper.helper.EMChatThreadHelper;
import com.hyphenate.wrapper.helper.EMGroupAckHelper;
import com.hyphenate.wrapper.helper.EMMessageHelper;
import com.hyphenate.wrapper.helper.EMMessageReactionChangeHelper;
import com.hyphenate.wrapper.helper.EMMessageReactionHelper;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.sql.Array;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class DelegateTester {
    static DelegateTester tester;

    private EMGroup _group;
    private EMChatRoom _room;

    public static DelegateTester shared() {
        if (tester == null){
            tester = new DelegateTester();
        }
        return tester;
    }
    public void startTest (){
        try {
            List<EMGroup> groups = EMClient.getInstance().groupManager().getJoinedGroupsFromServer();
            _group = groups.get(0);
            EMPageResult<EMChatRoom> result = EMClient.getInstance().chatroomManager().fetchPublicChatRoomsFromServer(0,5);
            _room = result.getData().get(0);
        }catch (HyphenateException e) {

        }


        connectionDelegateTest();
        multiDeviceDelegateTest();
        contactDelegateTest();
        groupManagerListenerTest();
        roomManagerListenerTest();
        presenceManagerListenerTest();
        threadManagerListenerTest();
        chatManagerListenerTest();
    }

    public void connectionDelegateTest() {
        EMClientWrapper.shared().wrapperConnectionListener.onConnected();
        EMClientWrapper.shared().wrapperConnectionListener.onDisconnected(216);
        EMClientWrapper.shared().wrapperConnectionListener.onDisconnected(214);
        EMClientWrapper.shared().wrapperConnectionListener.onDisconnected(217);
        EMClientWrapper.shared().wrapperConnectionListener.onDisconnected(202);
        EMClientWrapper.shared().wrapperConnectionListener.onDisconnected(206);
        EMClientWrapper.shared().wrapperConnectionListener.onDisconnected(207);
        EMClientWrapper.shared().wrapperConnectionListener.onDisconnected(305);
        EMClientWrapper.shared().wrapperConnectionListener.onDisconnected(0);
        EMClientWrapper.shared().wrapperConnectionListener.onTokenWillExpire();
        EMClientWrapper.shared().wrapperConnectionListener.onTokenExpired();
    }

    public void multiDeviceDelegateTest() {
        EMClientWrapper.shared().wrapperMultiDeviceListener.onContactEvent(3, "username", "ext");
        List<String> list = new ArrayList<>();
        list.add("userId1");
        list.add("userId2");
        EMClientWrapper.shared().wrapperMultiDeviceListener.onGroupEvent(18, "groupId", list);
        EMClientWrapper.shared().wrapperMultiDeviceListener.onChatThreadEvent(42, "threadId", list);
    }

    public void contactDelegateTest() {
        EMClientWrapper.shared().contactManagerWrapper.wrapperContactListener.onContactAdded("test");
        EMClientWrapper.shared().contactManagerWrapper.wrapperContactListener.onContactDeleted("test");
        EMClientWrapper.shared().contactManagerWrapper.wrapperContactListener.onContactInvited("test", "message");
        EMClientWrapper.shared().contactManagerWrapper.wrapperContactListener.onFriendRequestAccepted("test");
        EMClientWrapper.shared().contactManagerWrapper.wrapperContactListener.onFriendRequestDeclined("test");
    }

    public void groupManagerListenerTest() {
        List<String>users = new ArrayList<>();
        users.add("userId1");
        users.add("userId2");
        EMClientWrapper.shared().groupManagerWrapper.wrapperGroupListener.onInvitationReceived("groupId", "name", "inviter", "reason");
        EMClientWrapper.shared().groupManagerWrapper.wrapperGroupListener.onInvitationAccepted("groupId", "invitee", "reason");
        EMClientWrapper.shared().groupManagerWrapper.wrapperGroupListener.onInvitationDeclined("groupId", "invitee", "reason");
        EMClientWrapper.shared().groupManagerWrapper.wrapperGroupListener.onAutoAcceptInvitationFromGroup("groupId", "inviter", "reason");
        EMClientWrapper.shared().groupManagerWrapper.wrapperGroupListener.onGroupDestroyed("groupId", "name");
        EMClientWrapper.shared().groupManagerWrapper.wrapperGroupListener.onUserRemoved("groupId","name");
        EMClientWrapper.shared().groupManagerWrapper.wrapperGroupListener.onRequestToJoinReceived("groupId", "name", "user", "reason");
        //EMClientWrapper.shared().groupManagerWrapper.wrapperGroupListener.onRequestToJoinDeclined("groupId", "name", "user", "reason");
        EMClientWrapper.shared().groupManagerWrapper.wrapperGroupListener.onRequestToJoinDeclined("groupId", "name", "user", "reason", "applicant");
        EMClientWrapper.shared().groupManagerWrapper.wrapperGroupListener.onRequestToJoinAccepted("groupId", "name", "user");
        EMClientWrapper.shared().groupManagerWrapper.wrapperGroupListener.onMuteListAdded("groupId", users, 10000000);
        EMClientWrapper.shared().groupManagerWrapper.wrapperGroupListener.onMuteListRemoved("groupId", users);
        EMClientWrapper.shared().groupManagerWrapper.wrapperGroupListener.onWhiteListAdded("groupId", users);
        EMClientWrapper.shared().groupManagerWrapper.wrapperGroupListener.onWhiteListRemoved("groupId", users);
        EMClientWrapper.shared().groupManagerWrapper.wrapperGroupListener.onAllMemberMuteStateChanged("groupId", true);
        EMClientWrapper.shared().groupManagerWrapper.wrapperGroupListener.onAdminAdded("groupId", "user");
        EMClientWrapper.shared().groupManagerWrapper.wrapperGroupListener.onAdminRemoved("groupId", "user");
        EMClientWrapper.shared().groupManagerWrapper.wrapperGroupListener.onOwnerChanged("groupId", "newOwner", "oldOwner");
        EMClientWrapper.shared().groupManagerWrapper.wrapperGroupListener.onMemberJoined("groupId", "user");
        EMClientWrapper.shared().groupManagerWrapper.wrapperGroupListener.onMemberExited("groupId", "user");
        EMClientWrapper.shared().groupManagerWrapper.wrapperGroupListener.onAnnouncementChanged("groupId", "announcement");

        EMClientWrapper.shared().groupManagerWrapper.wrapperGroupListener.onSharedFileDeleted("groupId","fileId");
        EMClientWrapper.shared().groupManagerWrapper.wrapperGroupListener.onStateChanged(_group, true);
        EMClientWrapper.shared().groupManagerWrapper.wrapperGroupListener.onSpecificationChanged(_group);
//        无法构建EMMucSharedFile对象， 需要用json 数据测试
//        EMMucSharedFile file = new EMMucSharedFile();
        try {
            JSONObject data = new JSONObject();
            JSONObject file = new JSONObject();
            file.put("fileId", "file.getFileId()");
            file.put("name", "file.getFileName()");
            file.put("owner", "file.getFileOwner()");
            file.put("createTime", Integer.valueOf(200000000));
            file.put("fileSize", Integer.valueOf(100000000));
            data.put("file", file);
            data.put("file", file);
            EMClientWrapper.shared().groupManagerWrapper.wrapperGroupListener.onTestSharedFileAdded("groupId", data);
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }
    public void roomManagerListenerTest() {
        List<String>users = new ArrayList<>();
        users.add("userId1");
        users.add("userId1");
        EMClientWrapper.shared().roomManagerWrapper.emWrapperRoomListener.onWhiteListAdded("roomId",users);
        EMClientWrapper.shared().roomManagerWrapper.emWrapperRoomListener.onWhiteListRemoved("roomId",users);
        EMClientWrapper.shared().roomManagerWrapper.emWrapperRoomListener.onAllMemberMuteStateChanged("roomId", true);
        EMClientWrapper.shared().roomManagerWrapper.emWrapperRoomListener.onChatRoomDestroyed("roomId", "name");
        EMClientWrapper.shared().roomManagerWrapper.emWrapperRoomListener.onMemberJoined("roomId", "userId");
        EMClientWrapper.shared().roomManagerWrapper.emWrapperRoomListener.onMemberExited("roomId", "name","userId");
        EMClientWrapper.shared().roomManagerWrapper.emWrapperRoomListener.onRemovedFromChatRoom(0,"roomId", "name","userId");
        EMClientWrapper.shared().roomManagerWrapper.emWrapperRoomListener.onRemovedFromChatRoom(2,"roomId", "name","userId");
        EMClientWrapper.shared().roomManagerWrapper.emWrapperRoomListener.onMuteListAdded("roomId", users, 1000000);
        EMClientWrapper.shared().roomManagerWrapper.emWrapperRoomListener.onMuteListRemoved("roomId",users);
        EMClientWrapper.shared().roomManagerWrapper.emWrapperRoomListener.onAdminAdded("roomId", "userId");
        EMClientWrapper.shared().roomManagerWrapper.emWrapperRoomListener.onAdminRemoved("roomId", "userId");
        EMClientWrapper.shared().roomManagerWrapper.emWrapperRoomListener.onOwnerChanged("roomId", "newOwner","oldOwner");
        EMClientWrapper.shared().roomManagerWrapper.emWrapperRoomListener.onAnnouncementChanged("roomId", "announcement");
        EMClientWrapper.shared().roomManagerWrapper.emWrapperRoomListener.onSpecificationChanged(_room);
        Map<String, String> map = new HashMap<>();
        map.put("key", "value");
        EMClientWrapper.shared().roomManagerWrapper.emWrapperRoomListener.onAttributesUpdate("roomId", map, "from");
        List<String>keys = new ArrayList<>();
        keys.add("key1");
        keys.add("key2");
        EMClientWrapper.shared().roomManagerWrapper.emWrapperRoomListener.onAttributesRemoved("roomId", keys, "from");
    }


    public void presenceManagerListenerTest() {
        // 无法构建 EMPresence， 需要用假数据测试
        EMClientWrapper.shared().presenceManagerWrapper.emWrapperPresenceListener.onTestPresenceUpdated();
    }


    public void threadManagerListenerTest() {
        try{
            JSONObject jo = new JSONObject();
            jo.put("type", 0);
            jo.put("from", "from");

            JSONObject data = new JSONObject();
            data.put("threadId", "threadId");
            data.put("name", "name");
            data.put("owner", "owner");
            data.put("msgId", "msgId");
            data.put("parentId", "parentId");
            data.put("memberCount", "memberCount");
            data.put("msgCount", "msgCount");
            data.put("createAt", Integer.valueOf(100000));
            EMMessage msg = EMMessage.createTextSendMessage("hello", "username");
            data.put("lastMsg", EMMessageHelper.toJson(msg));

            jo.put("thread", data);
            EMClientWrapper.shared().chatThreadManagerWrapper.emWrapperThreadListener.onTestChatThreadCreated(jo);
            EMClientWrapper.shared().chatThreadManagerWrapper.emWrapperThreadListener.onTestChatThreadUpdated(jo);
            EMClientWrapper.shared().chatThreadManagerWrapper.emWrapperThreadListener.onTestChatThreadUserRemoved(jo);
            EMClientWrapper.shared().chatThreadManagerWrapper.emWrapperThreadListener.onTestChatThreadDestroyed(jo);
        }catch (JSONException e) {

        }
    }

    public void chatManagerListenerTest() {
        List<EMMessage> msgs = new ArrayList<>();
        EMMessage msg = EMMessage.createTextSendMessage("hello", "from");
        msgs.add(msg);



        EMClientWrapper.shared().chatManagerWrapper.emWrapperMessageListener.onMessageReceived(msgs);
        EMClientWrapper.shared().chatManagerWrapper.emWrapperMessageListener.onCmdMessageReceived(msgs);
        EMClientWrapper.shared().chatManagerWrapper.emWrapperMessageListener.onMessageRead(msgs);
        EMClientWrapper.shared().chatManagerWrapper.emWrapperMessageListener.onMessageDelivered(msgs);
        EMClientWrapper.shared().chatManagerWrapper.emWrapperMessageListener.onMessageRecalled(msgs);
        EMClientWrapper.shared().chatManagerWrapper.emWrapperMessageListener.onReadAckForGroupMessageUpdated();
        EMClientWrapper.shared().chatManagerWrapper.emWrapperMessageListener.onConversationUpdate();
        EMClientWrapper.shared().chatManagerWrapper.emWrapperMessageListener.onConversationRead("from", "to");

        List<EMRecallMessageInfo> recallInfos = new ArrayList<>();
        EMRecallMessageInfo recallInfo = new EMRecallMessageInfo("recallBy", "messageId", msg, "ext");
        recallInfos.add(recallInfo);
        EMClientWrapper.shared().chatManagerWrapper.emWrapperMessageListener.onMessageRecalledWithExt(recallInfos);

        try{
            // 无法构建EMMessageReactionChange，使用假数据
            JSONArray changeList = new JSONArray();
            JSONObject change = new JSONObject();
            change.put("convId", "convId");
            change.put("msgId", "msgId");
            JSONArray reactionList = new JSONArray();
            JSONObject reaction = new JSONObject();
            reaction.put("reaction", "reaction");
            reaction.put("count", 10);
            reaction.put("isAddedBySelf", true);
            JSONArray userList = new JSONArray();
            userList.put("userId1");
            userList.put("userId2");
            reaction.put("userList", userList);
            reactionList.put(reaction);
            change.put("reactions", reactionList);
            changeList.put(change);
            EMClientWrapper.shared().chatManagerWrapper.emWrapperMessageListener.onTestReactionChanged(changeList);


            JSONArray ackList = new JSONArray();
            JSONObject ack = new JSONObject();
            ack.put("msgId", "msgId");
            ack.put("ackId", "ackId");
            ack.put("from", "from");
            ack.put("count", 10);
            ack.put("timestamp", 10000000);
            ack.put("content", "content");
            ackList.put(ack);
            EMClientWrapper.shared().chatManagerWrapper.emWrapperMessageListener.onTestGroupMessageRead(ackList);

        }catch (JSONException e) {

        }
    }



}
