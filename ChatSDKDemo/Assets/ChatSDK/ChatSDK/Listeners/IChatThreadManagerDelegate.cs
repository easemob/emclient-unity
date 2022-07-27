using System.Collections.Generic;

namespace ChatSDK
{
    public interface IChatThreadManagerDelegate
    {
        void OnChatThreadCreate(ChatThreadEvent threadEvent);
        void OnChatThreadUpdate(ChatThreadEvent threadEvent);
        void OnChatThreadDestroy(ChatThreadEvent threadEvent);
        void OnUserKickOutOfChatThread(ChatThreadEvent threadEvent);
    }
}
