using System.Collections.Generic;
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace ChatSDK
{
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
    internal sealed class ChatThreadManagerListener : MonoBehaviour
#else
    internal sealed class ChatThreadManagerListener
#endif
    {
        internal List<IChatThreadManagerDelegate> delegater;

        //TODO: Add code for following callback functions
        internal void OnCreatThread(ChatThreadEvent threadEvent)
        {
            
        }

        void OnUpdateMyThread(ChatThreadEvent threadEvent)
        {

        }
        void OnThreadNotifyChange(ChatThreadEvent threadEvent)
        {

        }
        void OnLeaveThread(ChatThreadEvent threadEvent, ThreadLeaveReason reason)
        {

        }
        void OnMemberJoinedThread(ChatThreadEvent threadEvent)
        {

        }
        void OnMemberLeaveThread(ChatThreadEvent threadEvent)
        {

        }
    }
}