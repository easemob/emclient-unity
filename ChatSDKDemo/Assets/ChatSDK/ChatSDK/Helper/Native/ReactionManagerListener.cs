using System.Collections.Generic;
using UnityEngine;

namespace ChatSDK
{
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE
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