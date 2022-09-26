using System.Collections.Generic;
using SimpleJSON;

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace AgoraChat {

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
    internal sealed class GroupManagerListener : MonoBehaviour
#else
    internal sealed class GroupManagerListener
#endif
    {

        internal List<IGroupManagerDelegate> delegater;

        internal void OnInvitationReceived(string jsonString)
        {
            if (delegater != null) {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate delegater in delegater)
                    {
                        delegater.OnInvitationReceivedFromGroup(
                            jo["groupId"].Value,
                            jo["groupName"].Value,
                            jo["inviter"].Value,
                            jo["reason"].Value
                            );
                    }
                });
            }
            
        }

        internal void OnRequestToJoinReceived(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate delegater in delegater)
                    {
                        delegater.OnRequestToJoinReceivedFromGroup(
                            jo["groupId"].Value,
                            jo["groupName"].Value,
                            jo["applicant"].Value,
                            jo["reason"].Value
                            );
                    }
                });
            }
        }

        internal void OnRequestToJoinAccepted(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate delegater in delegater)
                    {
                        delegater.OnRequestToJoinAcceptedFromGroup(
                            jo["groupId"].Value,
                            jo["groupName"].Value,
                            jo["accepter"].Value
                            );
                    }
                });
            }
        }

        internal void OnRequestToJoinDeclined(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate delegater in delegater)
                    {
                        delegater.OnRequestToJoinDeclinedFromGroup(
                            jo["groupId"].Value,
                            jo["groupName"].Value,
                            jo["decliner"].Value,
                            jo["reason"].Value
                            );
                    }
                });
            }
        }

        internal void OnInvitationAccepted(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate delegater in delegater)
                    {
                        delegater.OnInvitationAcceptedFromGroup(
                            jo["groupId"].Value,
                            jo["invitee"].Value,
                            jo["reason"].Value
                            );
                    }
                });
            }
        }

        internal void OnInvitationDeclined(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate delegater in delegater)
                    {
                        delegater.OnInvitationDeclinedFromGroup(
                            jo["groupId"].Value,
                            jo["invitee"].Value,
                            jo["inviter"].Value
                            );
                    }
                });
            }
        }

        internal void OnUserRemoved(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate delegater in delegater)
                    {
                        delegater.OnUserRemovedFromGroup(
                            jo["groupId"].Value,
                            jo["groupName"].Value
                            );
                    }
                });
            }
        }

        internal void OnGroupDestroyed(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate delegater in delegater)
                    {
                        delegater.OnDestroyedFromGroup(
                            jo["groupId"].Value,
                            jo["groupName"].Value
                            );
                    }
                });
            }
        }

        internal void OnAutoAcceptInvitationFromGroup(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate delegater in delegater)
                    {
                        delegater.OnAutoAcceptInvitationFromGroup(
                            jo["groupId"].Value,
                            jo["inviter"].Value,
                            jo["inviteMessage"].Value
                            );
                    }
                });
            }
        }

        internal void OnMuteListAdded(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate delegater in delegater)
                    {
                        delegater.OnMuteListAddedFromGroup(
                            jo["groupId"].Value,
                            TransformTool.JsonStringToStringList(jo["list"].Value),
                            jo["muteExpire"].AsInt
                            );
                    }
                });
            }
        }

        internal void OnMuteListRemoved(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate delegater in delegater)
                    {
                        delegater.OnMuteListRemovedFromGroup(
                            jo["groupId"].Value,
                            TransformTool.JsonStringToStringList(jo["list"].Value)
                            );
                    }
                });
            }
        }

        internal void OnAdminAdded(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate delegater in delegater)
                    {
                        delegater.OnAdminAddedFromGroup(
                            jo["groupId"].Value,
                            jo["admin"].Value
                            );
                    }
                });
            }
        }

        internal void OnAdminRemoved(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate delegater in delegater)
                    {
                        delegater.OnAdminRemovedFromGroup(
                            jo["groupId"].Value,
                            jo["admin"].Value
                            );
                    }
                });
            }
        }

        internal void OnOwnerChanged(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate delegater in delegater)
                    {
                        delegater.OnOwnerChangedFromGroup(
                            jo["groupId"].Value,
                            jo["newOwner"].Value,
                            jo["oldOwner"].Value
                            );
                    }
                });
            }
        }

        internal void OnMemberJoined(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate delegater in delegater)
                    {
                        delegater.OnMemberJoinedFromGroup(
                            jo["groupId"].Value,
                            jo["member"].Value
                            );
                    }
                });
            }
        }

        internal void OnMemberExited(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate delegater in delegater)
                    {
                        delegater.OnMemberExitedFromGroup(
                            jo["groupId"].Value,
                            jo["member"].Value
                            );
                    }
                });
            }
        }

        internal void OnAnnouncementChanged(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate delegater in delegater)
                    {
                        delegater.OnAnnouncementChangedFromGroup(
                            jo["groupId"].Value,
                            jo["announcement"].Value
                            );
                    }
                });
            }
        }

        internal void OnSharedFileAdded(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate delegater in delegater)
                    {
                        delegater.OnSharedFileAddedFromGroup(
                            jo["groupId"].Value,
                            new GroupSharedFile(jo["sharedFile"].Value)
                            );
                    }
                });
            }
        }

        internal void OnSharedFileDeleted(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate delegater in delegater)
                    {
                        delegater.OnSharedFileDeletedFromGroup(
                            jo["groupId"].Value,
                            jo["fileId"].Value
                            );
                    }
                });
            }
        }

        internal void OnAddWhiteListMembersFromGroup(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate delegater in delegater)
                    {
                        delegater.OnAddWhiteListMembersFromGroup(
                            jo["groupId"].Value,
                            TransformTool.JsonStringToStringList(jo["list"].Value)
                        );
                    }
                });
            }
        }

        internal void OnRemoveWhiteListMembersFromGroup(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate delegater in delegater)
                    {
                        delegater.OnRemoveWhiteListMembersFromGroup(
                            jo["groupId"].Value,
                            TransformTool.JsonStringToStringList(jo["list"].Value)
                        );
                    }
                });
            }
        }

        internal void OnAllMemberMuteChangedFromGroup(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IGroupManagerDelegate delegater in delegater)
                    {
                        delegater.OnAllMemberMuteChangedFromGroup(
                            jo["groupId"].Value,
                            jo["muted"].AsBool
                        );
                    }
                });
            }
        }
    }

}

