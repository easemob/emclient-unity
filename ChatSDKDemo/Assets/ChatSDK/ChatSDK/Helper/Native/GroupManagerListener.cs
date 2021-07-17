using SimpleJSON;
using UnityEngine;

namespace ChatSDK {
    internal class GroupManagerListener : MonoBehaviour
    {

        internal WeakDelegater<IGroupManagerDelegate> delegater;

        internal void OnInvitationReceived(string jsonString)
        {
            if (delegater != null) {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in delegater.List) {
                    delegater.OnInvitationReceivedFromGroup(
                        jo["groupId"].Value,
                        jo["groupName"].Value,
                        jo["inviter"].Value,
                        jo["reason"].Value
                        );
                }
            }
            
        }

        internal void OnRequestToJoinReceived(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in delegater.List)
                {
                    delegater.OnRequestToJoinReceivedFromGroup(
                        jo["groupId"].Value,
                        jo["groupName"].Value,
                        jo["applicant"].Value,
                        jo["reason"].Value
                        );
                }
            }
        }

        internal void OnRequestToJoinAccepted(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in delegater.List)
                {
                    delegater.OnRequestToJoinAcceptedFromGroup(
                        jo["groupId"].Value,
                        jo["groupName"].Value,
                        jo["accepter"].Value
                        );
                }
            }
        }

        internal void OnRequestToJoinDeclined(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in delegater.List)
                {
                    delegater.OnRequestToJoinDeclinedFromGroup(
                        jo["groupId"].Value,
                        jo["groupName"].Value,
                        jo["decliner"].Value,
                        jo["reason"].Value
                        );
                }
            }
        }

        internal void OnInvitationAccepted(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in delegater.List)
                {
                    delegater.OnInvitationAcceptedFromGroup(
                        jo["groupId"].Value,
                        jo["invitee"].Value,
                        jo["reason"].Value
                        );
                }
            }
        }

        internal void OnInvitationDeclined(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in delegater.List)
                {
                    delegater.OnInvitationDeclinedFromGroup(
                        jo["groupId"].Value,
                        jo["invitee"].Value,
                        jo["inviter"].Value
                        );
                }
            }
        }

        internal void OnUserRemoved(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in delegater.List)
                {
                    delegater.OnUserRemovedFromGroup(
                        jo["groupId"].Value,
                        jo["groupName"].Value
                        );
                }
            }
        }

        internal void OnGroupDestroyed(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in delegater.List)
                {
                    delegater.OnDestroyedFromGroup(
                        jo["groupId"].Value,
                        jo["groupName"].Value
                        );
                }
            }
        }

        internal void OnAutoAcceptInvitationFromGroup(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in delegater.List)
                {
                    delegater.OnAutoAcceptInvitationFromGroup(
                        jo["groupId"].Value,
                        jo["inviter"].Value,
                        jo["inviteMessage"].Value
                        );
                }
            }
        }

        internal void OnMuteListAdded(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in delegater.List)
                {
                    delegater.OnMuteListAddedFromGroup(
                        jo["groupId"].Value,
                        TransformTool.JsonStringToStringList(jo["list"].Value),
                        jo["muteExpire"].AsInt
                        );
                }
            }
        }

        internal void OnMuteListRemoved(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in delegater.List)
                {
                    delegater.OnMuteListRemovedFromGroup(
                        jo["groupId"].Value,
                        TransformTool.JsonStringToStringList(jo["list"].Value)
                        );
                }
            }
        }

        internal void OnAdminAdded(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in delegater.List)
                {
                    delegater.OnAdminAddedFromGroup(
                        jo["groupId"].Value,
                        jo["admin"].Value
                        );
                }
            }
        }

        internal void OnAdminRemoved(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in delegater.List)
                {
                    delegater.OnAdminRemovedFromGroup(
                        jo["groupId"].Value,
                        jo["admin"].Value
                        );
                }
            }
        }

        internal void OnOwnerChanged(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in delegater.List)
                {
                    delegater.OnOwnerChangedFromGroup(
                        jo["groupId"].Value,
                        jo["newOwner"].Value,
                        jo["oldOwner"].Value
                        );
                }
            }
        }

        internal void OnMemberJoined(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in delegater.List)
                {
                    delegater.OnMemberJoinedFromGroup(
                        jo["groupId"].Value,
                        jo["member"].Value
                        );
                }
            }
        }

        internal void OnMemberExited(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in delegater.List)
                {
                    delegater.OnMemberExitedFromGroup(
                        jo["groupId"].Value,
                        jo["member"].Value
                        );
                }
            }
        }

        internal void OnAnnouncementChanged(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in delegater.List)
                {
                    delegater.OnAnnouncementChangedFromGroup(
                        jo["groupId"].Value,
                        jo["announcement"].Value
                        );
                }
            }
        }

        internal void OnSharedFileAdded(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in delegater.List)
                {
                    delegater.OnSharedFileAddedFromGroup(
                        jo["groupId"].Value,
                        new GroupSharedFile(jo["sharedFile"])
                        );
                }
            }
        }

        internal void OnSharedFileDeleted(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in delegater.List)
                {
                    delegater.OnSharedFileDeletedFromGroup(
                        jo["groupId"].Value,
                        jo["fileId"].Value
                        );
                }
            }
        }
    }

}

