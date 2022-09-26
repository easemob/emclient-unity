using System.Collections.Generic;
using SimpleJSON;

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace AgoraChat {

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
    internal sealed class RoomManagerListener : MonoBehaviour
#else
    internal sealed class RoomManagerListener
#endif
    {

        internal List<IRoomManagerDelegate> delegater;

        internal void OnChatRoomDestroyed(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IRoomManagerDelegate delegater in delegater)
                    {
                        delegater.OnDestroyedFromRoom(
                            jo["roomId"].Value,
                            jo["roomName"].Value
                            );
                    }
                });
            }
        }


        internal void OnMemberJoined(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IRoomManagerDelegate delegater in delegater)
                    {
                        delegater.OnMemberJoinedFromRoom(
                            jo["roomId"].Value,
                            jo["participant"].Value
                            );
                    }
                });
            }
        }


        internal void OnMemberExited(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IRoomManagerDelegate delegater in delegater)
                    {
                        delegater.OnMemberExitedFromRoom(
                            jo["roomId"].Value,
                            jo["roomName"].Value,
                            jo["participant"].Value
                            );
                    }
                });
            }
        }


        internal void OnRemovedFromChatRoom(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IRoomManagerDelegate delegater in delegater)
                    {
                        delegater.OnRemovedFromRoom(
                            jo["roomId"].Value,
                            jo["roomName"].Value,
                            jo["participant"].Value
                            );
                    }
                });
            }
        }


        internal void OnMuteListAdded(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IRoomManagerDelegate delegater in delegater)
                    {
                        delegater.OnMuteListAddedFromRoom(
                            jo["roomId"].Value,
                            TransformTool.JsonStringToStringList(jo["list"].Value),
                            jo["expireTime"].AsInt
                            );
                    }
                });
            }
        }


        internal void OnMuteListRemoved(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IRoomManagerDelegate delegater in delegater)
                    {
                        delegater.OnMuteListRemovedFromRoom(
                            jo["roomId"].Value,
                            TransformTool.JsonStringToStringList(jo["list"].Value)
                            );
                    }
                });
            }
        }


        internal void OnAdminAdded(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IRoomManagerDelegate delegater in delegater)
                    {
                        delegater.OnAdminAddedFromRoom(
                            jo["roomId"].Value,
                            jo["admin"].Value
                            );
                    }
                });
            }
        }


        internal void OnAdminRemoved(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IRoomManagerDelegate delegater in delegater)
                    {
                        delegater.OnAdminRemovedFromRoom(
                            jo["roomId"].Value,
                            jo["admin"].Value
                            );
                    }
                });
            }
        }


        internal void OnOwnerChanged(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IRoomManagerDelegate delegater in delegater)
                    {
                        delegater.OnOwnerChangedFromRoom(
                            jo["roomId"].Value,
                            jo["newOwner"].Value,
                            jo["oldOwner"].Value
                            );
                    }
                });
            }
        }


        internal void OnAnnouncementChanged(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IRoomManagerDelegate delegater in delegater)
                    {
                        delegater.OnAnnouncementChangedFromRoom(
                            jo["roomId"].Value,
                            jo["announcement"].Value
                            );
                    }
                });
            }
        }

        internal void OnChatroomAttributesChanged(string jsonString)
        {
            //TODO: add code here.
        }

        internal void OnChatroomAttributesRemoved(string jsonString)
        {
            //TODO: add code here.
        }
    }
}