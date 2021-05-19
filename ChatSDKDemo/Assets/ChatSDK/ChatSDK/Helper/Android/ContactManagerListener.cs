using System;
using SimpleJSON;
using UnityEngine;

namespace ChatSDK {
    internal class ContactManagerListener : MonoBehaviour
    {
        internal WeakDelegater<IContactManagerDelegate> contactManagerDelegater;


        internal void OnContactAdded(string jsonString) {
            if (contactManagerDelegater != null) {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IContactManagerDelegate contactManagerDelegate in contactManagerDelegater.List) {
                    contactManagerDelegate.OnContactAdded(jo["username"].Value);
                }
            }
        }

        
        internal void OnContactDeleted(string jsonString) {
            if (contactManagerDelegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IContactManagerDelegate contactManagerDelegate in contactManagerDelegater.List)
                {
                    contactManagerDelegate.OnContactDeleted(jo["username"].Value);
                }
            }
        }

        
        internal void OnContactInvited(string jsonString) {
            if (contactManagerDelegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IContactManagerDelegate contactManagerDelegate in contactManagerDelegater.List)
                {
                    contactManagerDelegate.OnContactInvited(jo["username"].Value, jo["reason"].Value);
                }
            }
        }

        
        internal void OnFriendRequestAccepted(string jsonString) {
            if (contactManagerDelegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IContactManagerDelegate contactManagerDelegate in contactManagerDelegater.List)
                {
                    contactManagerDelegate.OnFriendRequestAccepted(jo["username"].Value);
                }
            }
        }

        
        internal void OnFriendRequestDeclined(string jsonString) {
            if (contactManagerDelegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IContactManagerDelegate contactManagerDelegate in contactManagerDelegater.List)
                {
                    contactManagerDelegate.OnFriendRequestDeclined(jo["username"].Value);
                }
            }
        }
    }
}
