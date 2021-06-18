using System;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

namespace ChatSDK {
    internal class RoomManagerListener : MonoBehaviour
    {

        internal WeakDelegater<IRoomManagerDelegate> delegater;

        internal void OnChatRoomDestroyed(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IRoomManagerDelegate delegater in delegater.List)
                {
                    delegater.OnChatRoomDestroyed(
                        jo["roomId"].Value,
                        jo["roomName"].Value
                        );
                }
            }
        }


        internal void OnMemberJoined(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IRoomManagerDelegate delegater in delegater.List)
                {
                    delegater.OnMemberJoined(
                        jo["roomId"].Value,
                        jo["participant"].Value
                        );
                }
            }
        }


        internal void OnMemberExited(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IRoomManagerDelegate delegater in delegater.List)
                {
                    delegater.OnMemberExited(
                        jo["roomId"].Value,
                        jo["roomName"].Value,
                        jo["participant"].Value
                        );
                }
            }
        }


        internal void OnRemovedFromChatRoom(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IRoomManagerDelegate delegater in delegater.List)
                {
                    delegater.OnRemovedFromChatRoom(
                        jo["roomId"].Value,
                        jo["roomName"].Value,
                        jo["participant"].Value
                        );
                }
            }
        }


        internal void OnMuteListAdded(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IRoomManagerDelegate delegater in delegater.List)
                {
                    delegater.OnMuteListAdded(
                        jo["roomId"].Value,
                        TransformTool.JsonStringToStringList(jo["list"].Value),
                        jo["expireTime"].AsInt
                        );
                }
            }
        }


        internal void OnMuteListRemoved(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IRoomManagerDelegate delegater in delegater.List)
                {
                    delegater.OnMuteListRemoved(
                        jo["roomId"].Value,
                        TransformTool.JsonStringToStringList(jo["list"].Value)
                        );
                }
            }
        }


        internal void OnAdminAdded(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IRoomManagerDelegate delegater in delegater.List)
                {
                    delegater.OnAdminAdded(
                        jo["roomId"].Value,
                        jo["admin"].Value
                        );
                }
            }
        }


        internal void OnAdminRemoved(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IRoomManagerDelegate delegater in delegater.List)
                {
                    delegater.OnAdminRemoved(
                        jo["roomId"].Value,
                        jo["admin"].Value
                        );
                }
            }
        }


        internal void OnOwnerChanged(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IRoomManagerDelegate delegater in delegater.List)
                {
                    delegater.OnOwnerChanged(
                        jo["roomId"].Value,
                        jo["newOwner"].Value,
                        jo["oldOwner"].Value
                        );
                }
            }
        }


        internal void OnAnnouncementChanged(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IRoomManagerDelegate delegater in delegater.List)
                {
                    delegater.OnAnnouncementChanged(
                        jo["roomId"].Value,
                        jo["announcement"].Value
                        );
                }
            }
        }
    }
}