using System.Collections.Generic;
using AgoraChat.SimpleJSON;
#if !_WIN32
using UnityEngine.Scripting;
#endif

namespace AgoraChat
{
    /**
     *  \~chinese
     *  从服务端查询历史消息的参数配置类。
     *
     *  \~english
     *  The parameter configuration class for pulling historical messages from the server.
     */
    [Preserve]
    public class FetchServerMessagesOption : BaseModel
    {
        /**
         *  \~chinese
         *  获取的消息是否保存到数据库：
         *  -`true`：保存到数据库；
         *  -（默认）`false`：不保存到数据库。
         *
         *  \~english
         *  Whether to save the obtained messages to the database:
         *  -`true`: Yes;
         *  -(Default) `false`：No.
         */
        public bool IsSave;

        /**
         *  \~chinese
         *  消息搜索方向，详见 {@link MessageSearchDirection}。
         *
         *  \~english
         *  The message search direction. See {@link MessageSearchDirection}.
         */
        public MessageSearchDirection Direction = MessageSearchDirection.UP;

        /**
         *  \~chinese
         *  消息发送方的用户 ID。
         *
         * 仅用于群组消息。
         *
         *  \~english
         *  The user ID of the message sender.
         *
         *  This attribute is used only for group message.
         */
        public string From;

        /**
         *  \~chinese
         *  要查询的消息类型列表，默认为空，表示返回所有类型的消息。
         *
         *  \~english
         *  The list of message types for query. The default is empty, indicating that all types of messages are retrieved.
         */
        public List<MessageBodyType> MsgTypes;

        /**
         *  \~chinese
         *  消息查询的起始时间，Unix 时间戳，单位为毫秒。默认为 `-1`，表示消息查询时会忽略该参数。
         *
         *  若起始时间设置为特定时间点，而结束时间采用默认值 `-1`，则查询起始时间至当前时间的消息。
         *
         *  若起始时间采用默认值 `-1`，而结束时间设置了特定时间，SDK 返回从会话中最早的消息到结束时间点的消息。
         *
         *  \~english
         *  The start time for message query. The time is a UNIX timestamp in milliseconds.
         *
         *  The default value is `-1`, indicating that this parameter is ignored during message query. 
         *
         *  If the start time is set to a specific time spot and the end time uses the default value `-1`, the SDK returns messages that are sent and received in the period that is from the start time to the current time.
         *
         *  If the start time uses the default value `-1` and the end time is set to a specific time spot, the SDK returns messages that are sent and received in the period that is from the timestamp of the first message to the current time.
         */
        public long StartTime = -1;

        /**
         *  \~chinese
         *  消息查询的结束时间，Unix 时间戳，单位为毫秒。默认为 `-1`，表示消息查询时会忽略该参数。
         *
         *  若起始时间设置为特定时间点，而结束时间采用默认值 `-1`，则查询起始时间至当前时间的消息。
         *
         *  若起始时间采用默认值 `-1`，而结束时间设置了特定时间，SDK 返回从会话中最早的消息到结束时间点的消息。
         *
         *  \~english
         *  The end time for message query. The time is a UNIX time stamp in milliseconds. 
         *
         *  The default value is `-1`, indicating that this parameter is ignored during message query. 
         *
         *  If the start time is set to a specific time spot and the end time uses the default value `-1`, the SDK returns messages that are sent and received in the period that is from the start time to the current time. 
         *
         *  If the start time uses the default value `-1` and the end time is set to a specific time spot, the SDK returns messages that are sent and received in the period that is from the timestamp of the first message to the current time.
         */
        public long EndTime = -1;

        [Preserve]
        public FetchServerMessagesOption() { }

        [Preserve]
        internal FetchServerMessagesOption(string jsonString) : base(jsonString) { }

        [Preserve]
        internal FetchServerMessagesOption(JSONObject jsonObject) : base(jsonObject) { }

        internal override void FromJsonObject(JSONObject jsonObject) { }

        internal List<int> GetListFromMsgTypes()
        {
            List<int> list = new List<int>();
            foreach(var it in MsgTypes)
            {
                list.Add(it.ToInt());
            }
            return list;
        }

        internal override JSONObject ToJsonObject()
        {
            JSONObject jo = new JSONObject();
            jo.AddWithoutNull("isSave", IsSave);
            jo.AddWithoutNull("direction", Direction.ToInt());
            jo.AddWithoutNull("from", From);
            jo.AddWithoutNull("types", JsonObject.JsonArrayFromIntList(GetListFromMsgTypes()));
            jo.AddWithoutNull("startTime", StartTime);
            jo.AddWithoutNull("endTime", EndTime);
            return jo;
        }
    };
}