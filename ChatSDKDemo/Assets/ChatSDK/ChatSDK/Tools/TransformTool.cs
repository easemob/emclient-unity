using System;
using System.Collections.Generic;
using SimpleJSON;

namespace ChatSDK
{
    public class TransformTool
    {

        static internal List<string> JsonStringToStringList(string jsonString)
        {
            List<string> ret = new List<string>();
            if (jsonString != null)
            {
                JSONNode jsonArray = JSON.Parse(jsonString);
                if (jsonArray != null && jsonArray.IsArray)
                {
                    foreach (JSONNode v in jsonArray.AsArray)
                    {
                        ret.Add(v.Value);
                    }
                }
            }
            return ret;
        }

        static internal string JsonStringFromStringList(List<string> list)
        {
            JSONObject jo = new JSONObject();
            foreach (string str in list) {
                jo.Add(str);
            }

            return jo.ToString();
        }

        static internal List<Group> JsonStringToGroupList(string jsonString)
        {
            List<Group> list = new List<Group>();
            if (jsonString != null)
            {
                JSONNode jsonArray = JSON.Parse(jsonString);
                foreach (JSONNode obj in jsonArray.AsArray)
                {
                    list.Add(new Group(obj));
                }
            }
            return list;
        }

        static internal List<GroupInfo> JsonStringToGroupInfoList(string jsonString)
        {
            List<GroupInfo> list = new List<GroupInfo>();
            if (jsonString != null)
            {
                JSONNode jsonArray = JSON.Parse(jsonString);
                foreach (JSONNode obj in jsonArray.AsArray)
                {
                    list.Add(new GroupInfo(obj));
                }
            }
            return list;
        }

        static internal List<GroupSharedFile> JsonStringToGroupSharedFileList(string jsonString)
        {
            List<GroupSharedFile> list = new List<GroupSharedFile>();
            if (jsonString != null)
            {
                JSONNode jsonArray = JSON.Parse(jsonString);
                foreach (JSONNode obj in jsonArray.AsArray)
                {
                    list.Add(new GroupSharedFile(obj));
                }
            }
            return list;
        }

        /*
          {
            "cursor":"xxx",
            "list": [{"groupId: "","groupName":""}]
          }
        */
        static internal CursorResult<GroupInfo> JsonStringToGroupInfoResult(string jsonString)
        {
            CursorResult<GroupInfo> result = null;
            if (jsonString != null)
            {
                result = new CursorResult<GroupInfo>();

                JSONNode jsonObject = JSON.Parse(jsonString);

                result.Cursor = jsonObject["cursor"].Value;

                List<GroupInfo> list = new List<GroupInfo>();

                foreach (JSONNode obj in jsonObject["list"].AsArray)
                {
                    list.Add(new GroupInfo(obj));
                }

                result.Data = list;
            }
            return result;
        }

        static internal PageResult<Room> JsonStringToRoomPageResult(string jsonString)
        {
            PageResult<Room> result = null;
            if (jsonString != null)
            {
                result = new PageResult<Room>();

                JSONNode jsonObject = JSON.Parse(jsonString);

                result.PageCount = jsonObject["count"].AsInt;

                List<Room> list = new List<Room>();

                foreach (JSONNode obj in jsonObject["list"].AsArray)
                {
                    list.Add(new Room(obj));
                }

                result.Data = list;
            }
            return result;
        }

        static internal List<Room> JsonStringToRoomList(string jsonString)
        {
            List<Room> list = new List<Room>();
            if (jsonString != null)
            {
                JSONNode jsonArray = JSON.Parse(jsonString);
                foreach (JSONNode obj in jsonArray.AsArray)
                {
                    list.Add(new Room(obj));
                }
            }
            return list;
        }

        static internal CursorResult<string> JsonStringToStringResult(string jsonString)
        {
            if (jsonString == null) return null;
            CursorResult<string> result = null;
            if (jsonString != null)
            {
                result = new CursorResult<string>();

                JSONNode jsonObject = JSON.Parse(jsonString);

                result.Cursor = jsonObject["cursor"].Value;

                List<string> list = new List<string>();

                foreach (JSONNode obj in jsonObject["list"].AsArray)
                {
                    list.Add(obj);
                }

                result.Data = list;
            }
            return result;
        }

        static internal CursorResult<Message> JsonStringToMessageResult(string jsonString)
        {
            if (jsonString == null) return null;
            CursorResult<Message> result = null;
            if (jsonString != null)
            {
                result = new CursorResult<Message>();

                JSONNode jsonObject = JSON.Parse(jsonString);

                result.Cursor = jsonObject["cursor"].Value;

                List<Message> list = new List<Message>();

                foreach (JSONNode obj in jsonObject["list"].AsArray)
                {
                    list.Add(new Message(obj));
                }

                result.Data = list;
            }
            return result;
        }

        static internal string JsonStringFromDictionary(Dictionary<string, string> dictionary)
        {
            if (dictionary == null) return null;
            JSONObject jo = new JSONObject();
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(dictionary);
            IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();

            while (dem.MoveNext())
            {
                string key = dem.Current.Key;
                string value = dem.Current.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
                    jo[key] = value;
                }
            }

            return jo.ToString();
        }

        static internal string JsonStringFromAttributes(Dictionary<string, AttributeValue> attributes = null)
        {
            //if (attributes == null) return null;
            JSONObject jo = new JSONObject();
            var keys = attributes.Keys;
            foreach (var key in keys)
            {
                if (!attributes.TryGetValue(key, out AttributeValue value))
                    value = new AttributeValue();
                jo[key] = value.ToJsonString();
            }
            return jo.ToString();
        }

        static internal Dictionary<string, string> JsonStringToDictionary(string jsonString)
        {
            if (jsonString == null) return null;
            Dictionary<string, string> ret = new Dictionary<string, string>();
            JSONObject jo = JSON.Parse(jsonString).AsObject;
            foreach (string s in jo.Keys)
            {
                ret.Add(s, jo[s]);
            }

            return ret;
        }

        static internal Dictionary<string, AttributeValue> JsonStringToAttributes(string jsonString)
        {
            if (jsonString == null) return null;
            Dictionary<string, AttributeValue> ret = new Dictionary<string, AttributeValue>();
            JSONObject jo = JSON.Parse(jsonString).AsObject;
            foreach (string key in jo.Keys)
            {
                ret.Add(key, AttributeValue.FromJsonString(jo[key]));
            }
            return ret;
        }

        static internal List<Message> JsonStringToMessageList(string jsonString)
        {
            if (jsonString == null) return null;
            List<Message> list = new List<Message>();
            JSONNode jsonArray = JSON.Parse(jsonString);
            if (jsonArray != null && jsonArray.IsArray)
            {
                foreach (JSONNode v in jsonArray.AsArray)
                {
                    Message conv = new Message(v.Value);
                    list.Add(conv);
                }
            }

            return list;
        }

        static internal string JsonStringFromMessageList(List<Message> list)
        {
            if (list == null) return null;
            JSONArray ja = new JSONArray();
            foreach (Message msg in list) {
                ja.Add(msg.ToJsonString());
            }
            return ja.ToString();
        }

        static internal List<Conversation> JsonStringToConversationList(string jsonString)
        {
            if (jsonString == null) return null;
            List<Conversation> list = new List<Conversation>();
            JSONNode jsonArray = JSON.Parse(jsonString);
            if (jsonArray != null && jsonArray.IsArray)
            {
                foreach (JSONNode v in jsonArray.AsArray)
                {
                    Conversation conv = new Conversation(v.Value);
                    list.Add(conv);
                }
            }

            return list;
        }

        static internal List<GroupReadAck> JsonStringToGroupReadAckList(string jsonString)
        {
            if (jsonString == null) return null;
            List<GroupReadAck> list = new List<GroupReadAck>();
            JSONNode jsonArray = JSON.Parse(jsonString);
            if (jsonArray != null && jsonArray.IsArray)
            {
                foreach (JSONNode v in jsonArray.AsArray)
                {
                    GroupReadAck ack = new GroupReadAck(v.Value);
                    list.Add(ack);
                }
            }

            return list;
        }

        static internal int ConversationTypeToInt(ConversationType type)
        {
            int intType = 0;
            switch (type)
            {
                case ConversationType.Chat: intType = 0; break;
                case ConversationType.Group: intType = 1; break;
                case ConversationType.Room: intType = 2; break;
            }
            return intType;
        }

        static internal ConversationType ConversationTypeFromInt(int intType)
        {
            ConversationType type = ConversationType.Chat;
            switch (intType)
            {
                case 0: type = ConversationType.Chat; break;
                case 1: type = ConversationType.Group; break;
                case 2: type = ConversationType.Room; break;
            }
            return type;
        }

        static internal MessageType MessageTypeFromInt(int intType)
        {
            MessageType ret = MessageType.Chat;
            switch (intType) {
                case 0: ret = MessageType.Chat; break;
                case 1: ret = MessageType.Group; break;
                case 2: ret = MessageType.Room; break;
            }
            return ret;
        }

        static internal int MessageTypeToInt(MessageType type) {
            int ret = 0;
            switch (type)
            {
                case MessageType.Chat: ret = 0; break;
                case MessageType.Group: ret = 1; break;
                case MessageType.Room: ret = 2; break;
            }
            return ret;
        }

        static internal string MessageBodyTypeToString(MessageBodyType type) {
            string ret = "txt";
            switch (type) {
                case MessageBodyType.TXT:
                    {
                        ret = "txt";
                    }
                    break;
                case MessageBodyType.IMAGE:
                    {
                        ret = "img";
                    }
                    break;
                case MessageBodyType.LOCATION:
                    {
                        ret = "loc";
                    }
                    break;
                case MessageBodyType.CMD:
                    {
                        ret = "cmd";
                    }
                    break;
                case MessageBodyType.CUSTOM:
                    {
                        ret = "custom";
                    }
                    break;
                case MessageBodyType.VOICE:
                    {
                        ret = "voice";
                    }
                    break;
                case MessageBodyType.VIDEO:
                    {
                        ret = "video";
                    }
                    break;
                case MessageBodyType.FILE:
                    {
                        ret = "file";
                    }
                    break;
            }
            return ret;
        }
    }
}