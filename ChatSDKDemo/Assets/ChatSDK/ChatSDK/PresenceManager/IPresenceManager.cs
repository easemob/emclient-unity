using System.Collections.Generic;

namespace ChatSDK
{
    public abstract class IPresenceManager
    {
        public abstract void publishPresence(int presenceStatus, string ext = "", CallBack handle = null);
        public abstract void SubscribePresences(List<string> members, long expiry, ValueCallBack<List<Presence>> handle = null);
        public abstract void UnsubscribePresences(List<string> members, CallBack handle = null);
        public abstract void FetchSubscribedMembers(int pageNum, int pageSize, ValueCallBack<List<string>> handle = null);
        public abstract void FetchPresenceStatus(List<string> members, ValueCallBack<List<Presence>> handle = null);


        public void AddPresenceManagerDelegate(IPresenceManagerDelegate presenceManagerDelegate)
        {
            if (!CallbackManager.Instance().presenceManagerListener.delegater.Contains(presenceManagerDelegate))
            {
                CallbackManager.Instance().presenceManagerListener.delegater.Add(presenceManagerDelegate);
            }
        }

        public void RemovePresenceManagerDelegate(IPresenceManagerDelegate presenceManagerDelegate)
        {
            if (CallbackManager.IsQuit()) return;
            if (CallbackManager.Instance().presenceManagerListener.delegater.Contains(presenceManagerDelegate))
            {
                CallbackManager.Instance().presenceManagerListener.delegater.Remove(presenceManagerDelegate);
            }
        }

        internal void ClearDelegates()
        {
            CallbackManager.Instance().presenceManagerListener.delegater.Clear();
        }
    }
}