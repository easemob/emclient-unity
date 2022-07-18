using System.Collections.Generic;
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace ChatSDK
{
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
    internal sealed class ReactionManagerListener : MonoBehaviour
#else
    internal sealed class ReactionManagerListener
#endif
    {
        internal List<IReactionManagerDelegate> delegater;

        internal void MessageReactionDidChange(List<MessageReactionChange> list)
        {
            //TODO: Add code for processing json string from IOS/Android SDK
        }
    }
}