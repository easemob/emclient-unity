using System.Collections.Generic;
using UnityEngine;

namespace ChatSDK
{
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE
    internal sealed class ThreadManagerListener : MonoBehaviour
#else
    internal sealed class ThreadManagerListener
#endif
    {
        internal List<IThreadManagerDelegate> delegater;

        //TODO: Add code for following callback functions
        internal void OnCreatThread(ThreadEvent threadEvent)
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
        void OnMemberJoinedThread(ThreadEvent evthreadEventent)
        {

        }
        void OnMemberLeaveThread(ThreadEvent threadEvent)
        {

        }
    }
}