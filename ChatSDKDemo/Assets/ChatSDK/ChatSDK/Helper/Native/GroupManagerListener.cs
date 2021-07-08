using SimpleJSON;
using UnityEngine;

namespace ChatSDK {
    internal class GroupManagerListener : MonoBehaviour
    {

        internal WeakDelegater<IGroupManagerDelegate> delegater;

        internal void OnInvitatiOnReceived(string jsonString)
        {
            if (delegater != null) {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in delegater.List) {
                    delegater.OnInvitationReceived(
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
                    delegater.OnRequestToJoinReceived(
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
                    delegater.OnRequestToJoinAccepted(
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
                    delegater.OnRequestToJoinDeclined(
                        jo["groupId"].Value,
                        jo["groupName"].Value,
                        jo["decliner"].Value,
                        jo["reason"].Value
                        );
                }
            }
        }

        internal void OnInvitatiOnAccepted(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in delegater.List)
                {
                    delegater.OnInvitationAccepted(
                        jo["groupId"].Value,
                        jo["invitee"].Value,
                        jo["reason"].Value
                        );
                }
            }
        }

        internal void OnInvitatiOnDeclined(string jsonString)
        {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in delegater.List)
                {
                    delegater.OnInvitationDeclined(
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
                    delegater.OnUserRemoved(
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
                    delegater.OnGroupDestroyed(
                        jo["groupId"].Value,
                        jo["groupName"].Value
                        );
                }
            }
        }

        internal void OnAutoAcceptInvitatiOnFromGroup(string jsonString)
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
                    delegater.OnMuteListAdded(
                        jo["groupId"].Value,
                        TransformTool.JsonStringToStringList(jo["mutes"].Value),
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
                    delegater.OnMuteListRemoved(
                        jo["groupId"].Value,
                        TransformTool.JsonStringToStringList(jo["mutes"].Value)
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
                    delegater.OnAdminAdded(
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
                    delegater.OnAdminRemoved(
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
                    delegater.OnOwnerChanged(
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
                    delegater.OnMemberJoined(
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
                    delegater.OnMemberExited(
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
                    delegater.OnAnnouncementChanged(
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
                    delegater.OnSharedFileAdded(
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
                    delegater.OnSharedFileDeleted(
                        jo["groupId"].Value,
                        jo["fileId"].Value
                        );
                }
            }
        }
    }

}

