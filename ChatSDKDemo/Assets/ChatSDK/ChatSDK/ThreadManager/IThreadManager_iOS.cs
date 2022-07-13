using System;
using System.Collections.Generic;

namespace ChatSDK
{
	internal sealed class ThreadManager_iOS : IThreadManager
	{
		private IntPtr client;

		internal ThreadManager_iOS(IClient _client)
		{
			if (_client is Client_Mac clientMac)
			{
				client = clientMac.client;
			}
		}

        public override void ChangeThreadSubject(string threadId, string newSubject, CallBack handle = null)
        {
            //TODO: add code
        }

        public override void CreateThread(string threadName, string msgId, string groupId, ValueCallBack<ThreadEvent> handle = null)
        {
            //TODO: add code
        }

        public override void DestroyThread(string threadId, CallBack handle = null)
        {
            //TODO: add code
        }

        public override void FetchMineJoinedThreadList(string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<ThreadEvent>> handle = null)
        {
            //TODO: add code
        }

        public override void FetchThreadListOfGroup(string groupId, bool joined, string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<ThreadEvent>> handle = null)
        {
            //TODO: add code
        }

        public override void FetchThreadMembers(string threadId, string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<string>> handle = null)
        {
            //TODO: add code
        }

        public override void GetLastMessageAccordingThreads(List<string> threadIds, ValueCallBack<Dictionary<string, Message>> handle = null)
        {
            //TODO: add code
        }

        public override void GetThreadDetail(string threadId, ValueCallBack<ThreadEvent> handle = null)
        {
            //TODO: add code
        }

        public override void GetThreadWithThreadId(string threadId, ValueCallBack<ThreadEvent> handle = null)
        {
            //TODO: add code
        }

        public override void JoinThread(string threadId, ValueCallBack<ThreadEvent> handle = null)
        {
            //TODO: add code
        }

        public override void LeaveThread(string threadId, CallBack handle = null)
        {
            //TODO: add code
        }

        public override void RemoveThreadMember(string threadId, string username, CallBack handle = null)
        {
            //TODO: add code
        }
    }
}
