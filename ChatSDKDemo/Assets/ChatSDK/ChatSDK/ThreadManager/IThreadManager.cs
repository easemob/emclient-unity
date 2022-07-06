using System.Collections.Generic;

namespace ChatSDK
{
    public abstract class IThreadManager
    {
        public abstract void GetThreadWithThreadId(string threadId, ValueCallBack<ThreadEvent> handle = null);
	    public abstract void CreateThread(string threadName, string msgId, string groupId, ValueCallBack<ThreadEvent> handle = null);
        public abstract void JoinThread(string threadId, ValueCallBack<ThreadEvent> handle = null);
        public abstract void LeaveThread(string threadId, CallBack handle = null);
        public abstract void DestroyThread(string threadId, CallBack handle = null);
        public abstract void RemoveThreadMember(string threadId, string username, CallBack handle = null);
        public abstract void ChangeThreadSubject(string threadId, string newSubject, CallBack handle = null);
        public abstract void FetchThreadMembers(string threadId, string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<string>> handle = null);
        public abstract void FetchThreadListOfGroup(string groupId, bool joined, string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<ThreadEvent>> handle = null);
        public abstract void FetchMineJoinedThreadList(string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<ThreadEvent>> handle = null);
        public abstract void GetThreadDetail(string threadId, ValueCallBack<ThreadEvent> handle = null);
        public abstract void GetLastMessageAccordingThreads(List<string> threadIds, ValueCallBack<Dictionary<string, Message>> handle = null);

        public void AddThreadManagerDelegate(IThreadManagerDelegate threadManagerDelegate)
        {
            if (!CallbackManager.Instance().threadManagerListener.delegater.Contains(threadManagerDelegate))
            {
                CallbackManager.Instance().threadManagerListener.delegater.Add(threadManagerDelegate);
            }
        }

        public void RemoveThreadManagerDelegate(IThreadManagerDelegate threadManagerDelegate)
        {
            if (CallbackManager.IsQuit()) return;
            if (CallbackManager.Instance().threadManagerListener.delegater.Contains(threadManagerDelegate))
            {
                CallbackManager.Instance().threadManagerListener.delegater.Remove(threadManagerDelegate);
            }
        }

        internal void ClearDelegates()
        {
            CallbackManager.Instance().threadManagerListener.delegater.Clear();
        }
    }
}
