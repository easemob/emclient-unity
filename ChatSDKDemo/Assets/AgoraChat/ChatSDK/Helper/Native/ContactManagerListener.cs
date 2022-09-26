using System.Collections.Generic;
using SimpleJSON;

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace AgoraChat {

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
    internal sealed class ContactManagerListener : MonoBehaviour
#else
    internal sealed class ContactManagerListener
#endif
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
