using System;
using SimpleJSON;
using UnityEngine;

namespace ChatSDK {
    internal class ContactManagerListener : MonoBehaviour
    {
        internal WeakDelegater<IContactManagerDelegate> managerDelegater;


        internal void OnContactAdded(string jsonString) {
            if (managerDelegater != null) {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IContactManagerDelegate contactManagerDelegate in managerDelegater.List) {
                    contactManagerDelegate.OnContactAdded(jo["username"].Value);
                }
            }
        }

        
        internal void OnContactDeleted(string jsonString) {
            if (managerDelegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IContactManagerDelegate contactManagerDelegate in managerDelegater.List)
                {
                    contactManagerDelegate.OnContactDeleted(jo["username"].Value);
                }
            }
        }

        
        internal void OnContactInvited(string jsonString) {
            if (managerDelegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IContactManagerDelegate contactManagerDelegate in managerDelegater.List)
                {
                    contactManagerDelegate.OnContactInvited(jo["username"].Value, jo["reason"].Value);
                }
            }
        }

        
        internal void OnFriendRequestAccepted(string jsonString) {
            if (managerDelegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IContactManagerDelegate contactManagerDelegate in managerDelegater.List)
                {
                    contactManagerDelegate.OnFriendRequestAccepted(jo["username"].Value);
                }
            }
        }

        
        internal void OnFriendRequestDeclined(string jsonString) {
            if (managerDelegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IContactManagerDelegate contactManagerDelegate in managerDelegater.List)
                {
                    contactManagerDelegate.OnFriendRequestDeclined(jo["username"].Value);
                }
            }
        }
    }
}
