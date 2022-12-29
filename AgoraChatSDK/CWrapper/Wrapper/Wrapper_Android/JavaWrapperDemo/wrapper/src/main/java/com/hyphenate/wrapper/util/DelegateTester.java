package com.hyphenate.wrapper.util;

import com.hyphenate.cwrapper.EMCWrapperListener;
import com.hyphenate.wrapper.EMClientWrapper;
import com.hyphenate.wrapper.EMWrapperHelper;

import org.json.JSONException;
import org.json.JSONObject;

import java.sql.Array;
import java.util.ArrayList;
import java.util.List;

public class DelegateTester {
    static DelegateTester tester;
    public static DelegateTester shared() {
        if (tester == null){
            tester = new DelegateTester();
        }
        return tester;
    }
    public void startTest (){
        connectionDelegateTest();
        multiDeviceDelegateTest();
        contactListenerTest();
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


    public void contactListenerTest() {
        EMClientWrapper.shared().contactManagerWrapper.wrapperContactListener.onContactAdded("test");
        EMClientWrapper.shared().contactManagerWrapper.wrapperContactListener.onContactDeleted("test");
        EMClientWrapper.shared().contactManagerWrapper.wrapperContactListener.onContactInvited("test", "message");
        EMClientWrapper.shared().contactManagerWrapper.wrapperContactListener.onFriendRequestAccepted("test");
        EMClientWrapper.shared().contactManagerWrapper.wrapperContactListener.onFriendRequestDeclined("test");
    }
}
