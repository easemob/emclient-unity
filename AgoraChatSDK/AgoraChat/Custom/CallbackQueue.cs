using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AgoraChat
{
 
    internal class CallbackQueue
    {
        private Queue<Action> queue = new Queue<Action>();

        public void ClearQueue()
        {
            lock(queue)
            {
                queue.Clear();
            }
        }

        public void EnQueue(Action action)
        {
            lock (queue)
            {
                if (queue.Count >= 250)
                {
                    queue.Dequeue();
                }
                queue.Enqueue(action);
            }
        }

        public Action DeQueue()
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
    }

    internal class CallbackQueue_ThreadMode
    {
        Task worker;
        bool turn_on;

        CallbackQueue queue = new CallbackQueue();

        public void EnQueue(Action action)
        {
            queue.EnQueue(action);
        }

        public void Start()
        {
            queue.ClearQueue();
            turn_on = true;
            worker = new Task(Process);
            worker.Start();
        }

        public void Process()
        {
            while(turn_on)
            {
                Action action = queue.DeQueue();
                if (null != action)
                {
                    action();
                }
                action = null;
                Thread.Sleep(20); // avoid running too fast
            }
        }

        public void Stop()
        {
            turn_on = false;
            queue.ClearQueue();
        }
    }

#if Unity
    internal class CallbackQueue_UnityMode : MonoBehaviour
    {
        CallbackQueue queue = new CallbackQueue();

        public void EnQueue(Action action)
        {
            queue.EnQueue(action);
        }

        public void Start()
        {
            queue.ClearQueue();
        }

        public void Process()
        {
            Action action = queue.DeQueue();
            if (null != action)
            {
                action();
            }
            action = null;
        }

        public void Stop()
        {
            queue.ClearQueue();
        }

        void Awake()
        {

        }

        void Update()
        {
            Process();
        }

        void OnDestory()
        {
            queue.ClearQueue();
        }
    }
#endif 
}
