using System.Collections.Generic;

namespace ChatSDK
{

    /**
     *
     * \~chinese
     * Reaction状态监听器。
     *
     * \~english
     * The reaction state listener.
     */
    public interface IReactionManagerDelegate
    {

        /**
         * \~chinese
         * Reaction发生变化。
         *
         * @param list 改变的reaction列表。
         *
         *  \~english
         * Occurs when the reactions changed.
         *
         * @param list The changed reaction list.
         */
        void MessageReactionDidChange(List<MessageReactionChange> list);
    }
}
