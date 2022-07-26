using System;
using System.Collections.Generic;

namespace ChatSDK
{
    internal sealed class ChatThreadManager_iOS : IChatThreadManager
    {
        public override void ChangeThreadSubject(string threadId, string newSubject, CallBack handle = null)
        {
            throw new NotImplementedException();
        }

        public override void CreateThread(string threadName, string msgId, string groupId, ValueCallBack<ChatThread> handle = null)
        {
            throw new NotImplementedException();
        }

        public override void DestroyThread(string threadId, CallBack handle = null)
        {
            throw new NotImplementedException();
        }

        public override void FetchMineJoinedThreadList(string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<ChatThread>> handle = null)
        {
            throw new NotImplementedException();
        }

        public override void FetchThreadListOfGroup(string groupId, bool joined, string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<ChatThread>> handle = null)
        {
            throw new NotImplementedException();
        }

        public override void FetchThreadMembers(string threadId, string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<string>> handle = null)
        {
            throw new NotImplementedException();
        }

        public override void GetLastMessageAccordingThreads(List<string> threadIds, ValueCallBack<Dictionary<string, Message>> handle = null)
        {
            throw new NotImplementedException();
        }

        public override void GetThreadDetail(string threadId, ValueCallBack<ChatThread> handle = null)
        {
            throw new NotImplementedException();
        }

        public override void GetThreadWithThreadId(string threadId, ValueCallBack<ChatThread> handle = null)
        {
            throw new NotImplementedException();
        }

        public override void JoinThread(string threadId, ValueCallBack<ChatThread> handle = null)
        {
            throw new NotImplementedException();
        }

        public override void LeaveThread(string threadId, CallBack handle = null)
        {
            throw new NotImplementedException();
        }

        public override void RemoveThreadMember(string threadId, string username, CallBack handle = null)
        {
            throw new NotImplementedException();
        }
    }
}
