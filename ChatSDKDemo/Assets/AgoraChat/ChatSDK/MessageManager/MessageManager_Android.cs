using System.Collections.Generic;
using UnityEngine;

namespace AgoraChat
{
    internal class MessageManager_Android : IMessageManager
    {

        private AndroidJavaObject wrapper;

        public MessageManager_Android()
        {
            using (AndroidJavaClass aj = new AndroidJavaClass("com.hyphenate.unity_chat_sdk.EMMessageManager"))
            {
                wrapper = aj.CallStatic<AndroidJavaObject>("wrapper");
            }
        }

        internal override int GetGroupAckCount(string messageId)
        {
            return wrapper.Call<int>("getGroupAckCount", messageId);
        }

        internal override bool GetHasDeliverAck(string messageId)
        {
            return wrapper.Call<bool>("getHasDeliverAck", messageId);
        }

        internal override bool GetHasReadAck(string messageId)
        {
            return wrapper.Call<bool>("getHasReadAck", messageId);
        }

        internal override List<MessageReaction> GetReactionList(string MessageId)
        {
            string jsonString = wrapper.Call<string>("getReactionList", MessageId);

            if (jsonString == null || jsonString.Length == 0)
            {
                return null;
            }

            return MessageReaction.ListFromJson(jsonString);
        }

        internal override ChatThread GetChatThread(string messageId)
        {
            string jsonString = wrapper.Call<string>("getChatThread", messageId);

            if (jsonString == null || jsonString.Length == 0)
            {
                return null;
            }

            return ChatThread.FromJson(jsonString);
        }

        
    }

}
