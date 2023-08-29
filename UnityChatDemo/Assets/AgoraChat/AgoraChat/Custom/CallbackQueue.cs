using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

#if !_WIN32
using UnityEngine;
#endif

namespace AgoraChat
{

#if _WIN32

    internal class CallbackQueue_ThreadPoolMode
    {
        private static CallbackQueue_ThreadPoolMode instance;
        internal static CallbackQueue_ThreadPoolMode Instance()
        {
            if (null == instance)
            {
                instance = new CallbackQueue_ThreadPoolMode();
                //ThreadPool.SetMaxThreads(5,5);
            }
            return instance;
        }

        internal void EnQueue(Action action)
        {
            ThreadPool.QueueUserWorkItem(callBack:(obj)=> {
                action();
            },null);
        }

        internal void ClearQueue()
        {
        }
    }

#else
    internal sealed class CallbackQueue_UnityMode
    {
        private Queue<Action> queue = new Queue<Action>();

        private static CallbackQueue_UnityMode instance;
        internal static CallbackQueue_UnityMode Instance()
        {
            if (null == instance)
            {
                instance = new CallbackQueue_UnityMode();
            }
            return instance;
        }

        internal void ClearQueue()
        {
            lock (queue)
            {
                queue.Clear();
            }
        }

        internal void EnQueue(Action action)
        {
            lock (queue)
            {
                if (queue.Count >= 250)
                {
                    DeQueue();
                }
                queue.Enqueue(action);
            }
        }

        internal Action DeQueue()
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

        internal void Process()
        {
            Action action = DeQueue();
            action?.Invoke();
        }
    }
#endif

    internal class CallbackQueue_Worker
    {
#if _WIN32
        CallbackQueue_ThreadPoolMode callback_queue_ = CallbackQueue_ThreadPoolMode.Instance();
#else
        CallbackQueue_UnityMode callback_queue_ = CallbackQueue_UnityMode.Instance();
#endif

        private static CallbackQueue_Worker instance;
        internal static CallbackQueue_Worker Instance()
        {
            if (null == instance)
            {
                instance = new CallbackQueue_Worker();
            }
            return instance;
        }

        internal void EnQueue(Action action)
        {
            callback_queue_.EnQueue(action);
        }

        internal void ClearQueue()
        {
            callback_queue_.ClearQueue();
        }
    }
}
