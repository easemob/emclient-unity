using System;
using UnityEngine;

namespace ChatSDK {
    public class ChatManagerListener : MonoBehaviour
    {
        internal WeakDelegater<IChatManagerDelegate> managerDelegater;

        public ChatManagerListener()
        {
        }
    }
}