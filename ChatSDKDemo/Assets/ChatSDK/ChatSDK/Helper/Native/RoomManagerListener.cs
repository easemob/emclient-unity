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
                    delegater.OnDestroyedFromRoom(
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
                    delegater.OnMemberJoinedFromRoom(
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
                    delegater.OnMemberExitedFromRoom(
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
                    delegater.OnRemovedFromRoom(
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
                    delegater.OnMuteListAddedFromRoom(
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
                    delegater.OnMuteListRemovedFromRoom(
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
                    delegater.OnAdminAddedFromRoom(
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
                    delegater.OnAdminRemovedFromRoom(
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
                    delegater.OnOwnerChangedFromRoom(
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
                    delegater.OnAnnouncementChangedFromRoom(
                        jo["roomId"].Value,
                        jo["announcement"].Value
                        );
                }
            }
        }
    }
}