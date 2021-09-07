using System;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

namespace ChatSDK {
    internal sealed class ContactManagerListener : MonoBehaviour
    {
        internal List<IContactManagerDelegate> delegater;


        internal void OnContactAdded(string jsonString) {
            if (delegater != null) {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IContactManagerDelegate contactManagerDelegate in delegater)
                    {
                        contactManagerDelegate.OnContactAdded(jo["username"].Value);
                    }
                });
                
            }
        }

        
        internal void OnContactDeleted(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IContactManagerDelegate contactManagerDelegate in delegater)
                    {
                        contactManagerDelegate.OnContactDeleted(jo["username"].Value);
                    }
                });
                
            }
        }

        
        internal void OnContactInvited(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IContactManagerDelegate contactManagerDelegate in delegater)
                    {
                        contactManagerDelegate.OnContactInvited(jo["username"].Value, jo["reason"].Value);
                    }
                });
            }
        }

        
        internal void OnFriendRequestAccepted(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IContactManagerDelegate contactManagerDelegate in delegater)
                    {
                        contactManagerDelegate.OnFriendRequestAccepted(jo["username"].Value);
                    }
                });
            }
        }

        
        internal void OnFriendRequestDeclined(string jsonString) {
            if (delegater != null)
            {
                JSONNode jo = JSON.Parse(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IContactManagerDelegate contactManagerDelegate in delegater)
                    {
                        contactManagerDelegate.OnFriendRequestDeclined(jo["username"].Value);
                    }
                });
            }
        }
    }
}
