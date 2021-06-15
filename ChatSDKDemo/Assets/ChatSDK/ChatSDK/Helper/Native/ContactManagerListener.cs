using System;
using SimpleJSON;
using UnityEngine;

namespace ChatSDK {
    internal class ContactManagerListener : MonoBehaviour
    {
        internal WeakDelegater<IContactManagerDelegate> delegater;


        internal void OnContactAdded(string jsonString) {
            if (delegater != null) {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IContactManagerDelegate contactManagerDelegate in delegater.List) {
                    contactManagerDelegate.OnContactAdded(jo["username"].Value);
                }
            }
        }

        
        internal void OnContactDeleted(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IContactManagerDelegate contactManagerDelegate in delegater.List)
                {
                    contactManagerDelegate.OnContactDeleted(jo["username"].Value);
                }
            }
        }

        
        internal void OnContactInvited(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IContactManagerDelegate contactManagerDelegate in delegater.List)
                {
                    contactManagerDelegate.OnContactInvited(jo["username"].Value, jo["reason"].Value);
                }
            }
        }

        
        internal void OnFriendRequestAccepted(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IContactManagerDelegate contactManagerDelegate in delegater.List)
                {
                    contactManagerDelegate.OnFriendRequestAccepted(jo["username"].Value);
                }
            }
        }

        
        internal void OnFriendRequestDeclined(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                foreach (IContactManagerDelegate contactManagerDelegate in delegater.List)
                {
                    contactManagerDelegate.OnFriendRequestDeclined(jo["username"].Value);
                }
            }
        }
    }
}
