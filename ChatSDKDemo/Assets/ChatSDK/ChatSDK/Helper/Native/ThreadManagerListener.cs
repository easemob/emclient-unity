using System.Collections.Generic;
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace ChatSDK
{
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
    internal sealed class ThreadManagerListener : MonoBehaviour
#else
    internal sealed class ThreadManagerListener
#endif
    {
        internal List<IThreadManagerDelegate> delegater;

        //TODO: Add code for following callback functions
        internal void OnCreateThread(ThreadEvent threadEvent)
        {
            
        }

        void OnUpdateMyThread(ThreadEvent threadEvent)
        {

        }
        void OnThreadNotifyChange(ThreadEvent threadEvent)
        {

        }
        void OnLeaveThread(ThreadEvent threadEvent, ThreadLeaveReason reason)
        {

        }
        void OnMemberJoinedThread(ThreadEvent threadEvent)
        {

        }
        void OnMemberLeaveThread(ThreadEvent threadEvent)
        {

        }
    }
}