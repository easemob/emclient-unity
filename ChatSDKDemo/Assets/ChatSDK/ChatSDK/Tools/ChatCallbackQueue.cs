using System;
using System.Collections.Generic;

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE
using UnityEngine;
#endif

namespace ChatSDK
{
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE
    internal sealed class ChatCallbackQueue : MonoBehaviour
#else
    internal sealed class ChatCallbackQueue
#endif
    {

        private Queue<Action> queue = new Queue<Action>();

        public void ClearQueue()
        {
            lock (queue)
            {
                queue.Clear();
            }
        }

        public void EnQueue(Action action)
        {
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE
            lock (queue)
            {
                if (queue.Count >= 250)
                {
                    queue.Dequeue();
                }
                queue.Enqueue(action);
            }
#else
            if (action != null)
            {
                action();
            }
            action = null;
#endif
        }

        private Action DeQueue()
        {
            Action action = null;
            lock (queue)
            {
                if (queue.Count > 0)
                {
                    action = queue.Dequeue();
                }
            }
            return action;
        }

        void Awake()
        {

        }
        // Update is called once per frame
        void Update()
        {
            var action = DeQueue();

            if (action != null)
            {
                action();
            }
            action = null;
        }

        void OnDestroy()
        {
            ClearQueue();
        }
    }
}