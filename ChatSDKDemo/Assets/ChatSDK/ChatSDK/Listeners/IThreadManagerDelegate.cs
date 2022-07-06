using System.Collections.Generic;

namespace ChatSDK
{
    public interface IThreadManagerDelegate
    {
        void OnCreatThread(ThreadEvent threadEvent);
        void OnUpdateMyThread(ThreadEvent threadEvent);
        void OnThreadNotifyChange(ThreadEvent threadEvent);
        void OnLeaveThread(ThreadEvent threadEvent, ThreadLeaveReason reason);
        void OnMemberJoinedThread(ThreadEvent evthreadEventent);
        void OnMemberLeaveThread(ThreadEvent threadEvent);
    }
}
