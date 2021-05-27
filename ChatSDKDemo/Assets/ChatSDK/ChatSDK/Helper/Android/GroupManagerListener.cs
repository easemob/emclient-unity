using SimpleJSON;
using UnityEngine;

namespace ChatSDK {
    internal class GroupManagerListener : MonoBehaviour
    {

        internal WeakDelegater<IGroupManagerDelegate> groupManagerDelegater;

        internal void OnInvitatiOnReceived(string jsonString)
        {
            if (groupManagerDelegater != null) {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in groupManagerDelegater.List) {
                    delegater.OnInvitatiOnReceived(
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
            if (groupManagerDelegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in groupManagerDelegater.List)
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
            if (groupManagerDelegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in groupManagerDelegater.List)
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
            if (groupManagerDelegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in groupManagerDelegater.List)
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
            if (groupManagerDelegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in groupManagerDelegater.List)
                {
                    delegater.OnInvitatiOnAccepted(
                        jo["groupId"].Value,
                        jo["invitee"].Value,
                        jo["reason"].Value
                        );
                }
            }
        }

        internal void OnInvitatiOnDeclined(string jsonString)
        {
            if (groupManagerDelegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in groupManagerDelegater.List)
                {
                    delegater.OnInvitatiOnDeclined(
                        jo["groupId"].Value,
                        jo["invitee"].Value,
                        jo["inviter"].Value
                        );
                }
            }
        }

        internal void OnUserRemoved(string jsonString)
        {
            if (groupManagerDelegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in groupManagerDelegater.List)
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
            if (groupManagerDelegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in groupManagerDelegater.List)
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
            if (groupManagerDelegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in groupManagerDelegater.List)
                {
                    delegater.OnAutoAcceptInvitatiOnFromGroup(
                        jo["groupId"].Value,
                        jo["inviter"].Value,
                        jo["inviteMessage"].Value
                        );
                }
            }
        }

        internal void OnMuteListAdded(string jsonString)
        {
            if (groupManagerDelegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in groupManagerDelegater.List)
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
            if (groupManagerDelegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in groupManagerDelegater.List)
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
            if (groupManagerDelegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in groupManagerDelegater.List)
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
            if (groupManagerDelegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in groupManagerDelegater.List)
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
            if (groupManagerDelegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in groupManagerDelegater.List)
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
            if (groupManagerDelegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in groupManagerDelegater.List)
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
            if (groupManagerDelegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in groupManagerDelegater.List)
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
            if (groupManagerDelegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in groupManagerDelegater.List)
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
            if (groupManagerDelegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in groupManagerDelegater.List)
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
            if (groupManagerDelegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IGroupManagerDelegate delegater in groupManagerDelegater.List)
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

