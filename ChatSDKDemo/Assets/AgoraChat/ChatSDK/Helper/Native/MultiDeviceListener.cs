using System;
using System.Collections.Generic;
using SimpleJSON;

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace AgoraChat
{
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
    internal sealed class MultiDeviceListener : MonoBehaviour
#else
    internal sealed class MultiDeviceListener
#endif
    {

        internal List<IMultiDeviceDelegate> delegater;

        internal void OnContactMultiDevicesEvent(string jsonString)
        {

            ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                foreach (IMultiDeviceDelegate deviceDelegate in delegater)
                {     
                    JSONNode jo = JSON.Parse(jsonString);
                    string operationEvent = jo["event"].Value;
                    MultiDevicesOperation operation = (MultiDevicesOperation)int.Parse(operationEvent);
                    string username = jo["username"].Value;
                    string ext = jo["ext"].Value;
                    deviceDelegate.onContactMultiDevicesEvent(operation, username, ext);
                }
            });
        }

        internal void OnGroupMultiDevicesEvent(string jsonString)
        {
            ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                foreach (IMultiDeviceDelegate deviceDelegate in delegater)
                {
                    JSONNode jo = JSON.Parse(jsonString);
                    string operationEvent = jo["event"].Value;
                    MultiDevicesOperation operation = (MultiDevicesOperation)int.Parse(operationEvent);
                    string groupId = jo["groupId"].Value;
                    List<string> usernames = TransformTool.JsonStringToStringList(jo["usernames"].Value);
                    deviceDelegate.onGroupMultiDevicesEvent(operation, groupId, usernames);
                }
            });
        }

        internal void OndisturbMultiDevicesEvent(string data)
        {
            ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                foreach (IMultiDeviceDelegate deviceDelegate in delegater)
                {
                    deviceDelegate.undisturbMultiDevicesEvent(data);
                }
            });
        }

    }
}