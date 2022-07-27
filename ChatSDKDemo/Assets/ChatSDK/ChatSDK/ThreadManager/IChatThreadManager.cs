using System.Collections.Generic;

namespace ChatSDK
{
    public abstract class IChatThreadManager
    {
        public abstract void GetThreadWithThreadId(string threadId, ValueCallBack<ChatThread> handle = null);
	    public abstract void CreateThread(string threadName, string msgId, string groupId, ValueCallBack<ChatThread> handle = null);
        public abstract void JoinThread(string threadId, ValueCallBack<ChatThread> handle = null);
        public abstract void LeaveThread(string threadId, CallBack handle = null);
        public abstract void DestroyThread(string threadId, CallBack handle = null);
        public abstract void RemoveThreadMember(string threadId, string username, CallBack handle = null);
        public abstract void ChangeThreadName(string threadId, string newName, CallBack handle = null);
        public abstract void FetchThreadMembers(string threadId, string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<string>> handle = null);
        public abstract void FetchThreadListOfGroup(string groupId, bool joined, string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<ChatThread>> handle = null);
        public abstract void FetchMineJoinedThreadList(string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<ChatThread>> handle = null);
        public abstract void GetThreadDetail(string threadId, ValueCallBack<ChatThread> handle = null);
        public abstract void GetLastMessageAccordingThreads(List<string> threadIds, ValueCallBack<Dictionary<string, Message>> handle = null);

        public void AddThreadManagerDelegate(IChatThreadManagerDelegate threadManagerDelegate)
        {
            if (!CallbackManager.Instance().threadManagerListener.delegater.Contains(threadManagerDelegate))
            {
                CallbackManager.Instance().threadManagerListener.delegater.Add(threadManagerDelegate);
            }
        }

        public void RemoveThreadManagerDelegate(IChatThreadManagerDelegate threadManagerDelegate)
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
