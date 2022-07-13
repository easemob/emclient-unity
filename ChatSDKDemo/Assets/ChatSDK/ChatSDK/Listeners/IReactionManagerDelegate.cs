using System.Collections.Generic;

namespace ChatSDK
{
    public interface IReactionManagerDelegate
    {
        void MessageReactionDidChange(List<MessageReactionChange> list);
    }
}
