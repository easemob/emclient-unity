using System;
using System.Text;
using System.Collections.Generic;
using SimpleJSON;

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE
using UnityEngine;
#endif

namespace ChatSDK
{
    internal class TransformTool
    {

        static internal string GetUnicodeStringFromUTF8(string utf8Str)
        {
#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
            string ret = utf8Str;
#else
            if (utf8Str.Length == 0) return utf8Str;

            string ret = Encoding.UTF8.GetString(Encoding.Unicode.GetBytes(utf8Str));
            int index = ret.IndexOf('\0');
            if (index > 0)
                ret = ret.Substring(0, index);
#endif
            return ret;
        }

        static internal List<string> JsonStringToStringList(string jsonString)
        {
            if (jsonString == null) return null;

            List<string> ret = new List<string>();
            JSONNode jsonArray = JSON.Parse(jsonString);
            if (jsonArray != null && jsonArray.IsArray)
            {
                foreach (JSONNode obj in jsonArray.AsArray)
                {
                    if (obj.IsString)
                    {
                        ret.Add(obj.Value);
                    }
                }
            }
            return ret;
        }

        static internal string JsonStringFromStringList(List<string> list)
        {
            JSONArray ja = new JSONArray();
            if (list != null)
            {
                foreach (string str in list)
                {
                    ja.Add(str);
                }
            }

            return ja.ToString();
        }

        static internal List<Group> JsonStringToGroupList(string jsonString)
        {
            if (jsonString == null) return null;

            List<Group> list = new List<Group>();
            JSONNode jsonArray = JSON.Parse(jsonString);
            foreach (JSONNode obj in jsonArray.AsArray)
            {
                if (obj.IsString)
                {
                    list.Add(new Group(obj.Value));
                }
            }
            return list;
        }

        //static internal List<GroupInfo> JsonStringToGroupInfoList(string jsonString)
        //{
        //    List<GroupInfo> list = new List<GroupInfo>();
        //    if (jsonString != null)
        //    {
        //        JSONNode jsonArray = JSON.Parse(jsonString);
        //        foreach (JSONNode obj in jsonArray.AsArray)
        //        {
        //            if (obj.IsString)
        //            {
        //                list.Add(new GroupInfo(obj.Value));
        //            }
        //        }
        //    }
        //    return list;
        //}

        static internal List<GroupSharedFile> JsonStringToGroupSharedFileList(string jsonString)
        {
            List<GroupSharedFile> list = new List<GroupSharedFile>();
            if (jsonString != null)
            {
                JSONNode jsonArray = JSON.Parse(jsonString);
                foreach (JSONNode obj in jsonArray.AsArray)
                {
                    if (obj.IsString)
                    {
                        list.Add(new GroupSharedFile(obj.Value));
                    }

                }
            }
            return list;
        }


        static internal CursorResult<GroupInfo> JsonStringToGroupInfoResult(string jsonString)
        {
            CursorResult<GroupInfo> result = null;
            if (jsonString != null)
            {
                result = new CursorResult<GroupInfo>();

                JSONNode jsonObject = JSON.Parse(jsonString);

                result.Cursor = jsonObject["cursor"].Value;

                List<GroupInfo> list = new List<GroupInfo>();

                if (jsonObject["list"].IsString)
                {

                    JSONArray jsonArray = JSON.Parse(jsonObject["list"].Value).AsArray;

                    foreach (JSONNode obj in jsonArray)
                    {
                        if (obj.IsString)
                        {
                            list.Add(new GroupInfo(obj.Value));
                        }
                    }
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

                if (jsonObject["list"].IsString)
                {

                    JSONArray jsonArray = JSON.Parse(jsonObject["list"].Value).AsArray;

                    foreach (JSONNode obj in jsonArray)
                    {
                        if (obj.IsString)
                        {
                            list.Add(new Room(obj.Value));
                        }
                    }
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
                    if (obj.IsString)
                    {
                        list.Add(new Room(obj.Value));
                    }
                }
            }
            return list;
        }

        static internal CursorResult<string> JsonStringToStringResult(string jsonString)
        {
            if (jsonString == null || jsonString.Length == 0) return null;
            CursorResult<string> result = null;
            if (jsonString != null)
            {
                result = new CursorResult<string>();

                JSONNode jsonObject = JSON.Parse(jsonString);

                result.Cursor = jsonObject["cursor"].Value;

                List<string> list = new List<string>();

                JSONArray jsonArray = JSON.Parse(jsonObject["list"].Value).AsArray;

                foreach (JSONNode obj in jsonArray)
                {
                    if (obj.IsString)
                    {
                        list.Add(obj.Value);
                    }

                }

                result.Data = list;
            }
            return result;
        }

        static internal CursorResult<Message> JsonStringToMessageResult(string jsonString)
        {
            if (jsonString == null || jsonString.Length == 0) return null;
            CursorResult<Message> result = null;
            if (jsonString != null)
            {
                result = new CursorResult<Message>();

                JSONNode jsonObject = JSON.Parse(jsonString);

                result.Cursor = jsonObject["cursor"].Value;

                List<Message> list = new List<Message>();

                JSONArray jsonArray = JSON.Parse(jsonObject["list"].Value).AsArray;

                foreach (JSONNode obj in jsonArray)
                {
                    if (obj.IsString)
                    {
                        list.Add(new Message(obj.Value));
                    }
                }

                result.Data = list;
            }
            return result;
        }

        static internal string JsonStringFromDictionary(Dictionary<string, string> dictionary)
        {

            JSONObject jo = new JSONObject();
            if (dictionary != null)
            {
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
            }

            return jo.ToString();
        }

        /*
        static internal string JsonStringFromAttributes(Dictionary<string, AttributeValue> attributes = null)
        {
            
            JSONObject jo = new JSONObject();
            if (attributes != null)
            {
                var keys = attributes.Keys;
                foreach (var key in keys)
                {
                    if (!attributes.TryGetValue(key, out AttributeValue value)) {
                        value = new AttributeValue();
                    }
                    jo[key] = value.ToJsonString();
                }
            }
                
            return jo.ToString();
            
        }*/

        static internal string JsonStringFromAttributes(Dictionary<string, AttributeValue> attributes = null)
        {
            if (null == attributes || 0 == attributes.Count)
                return "";

            JSONObject jo = new JSONObject();
            foreach (var item in attributes)
            {
                jo[item.Key] = item.Value.ToJsonObject();
            }
            return jo.ToString();
        }

        static internal Dictionary<string, string> JsonStringToDictionary(string jsonString)
        {
            if (jsonString == null || jsonString.Length == 0) return null;
            Dictionary<string, string> ret = new Dictionary<string, string>();
            JSONObject jo = JSON.Parse(jsonString).AsObject;
            foreach (string s in jo.Keys)
            {
                ret.Add(s, jo[s]);
            }

            return ret;
        }

        /*
        static internal Dictionary<string, AttributeValue> JsonStringToAttributes(string jsonString)
        {
            
            if (jsonString == null || jsonString.Length == 0) return null;
            Dictionary<string, AttributeValue> ret = new Dictionary<string, AttributeValue>();
            JSONObject jo = JSON.Parse(jsonString).AsObject;
            foreach (string key in jo.Keys)
            {
                ret.Add(key, AttributeValue.FromJsonString(jo[key]));
            }
            return ret;
            
        }*/

        static internal Dictionary<string, AttributeValue> JsonStringToAttributes(string jsonString)
        {
            Dictionary<string, AttributeValue> ret = new Dictionary<string, AttributeValue>();

            // Json at least has { and } two characters
            if (null == jsonString || jsonString.Length <= 2) return ret;
            JSONNode jn = JSON.Parse(jsonString);
            if (null == jn) return ret;
            JSONNode jo = jn.AsObject;
            foreach (string k in jo.Keys)
            {
                ret.Add(k, AttributeValue.FromJsonObject(jo[k]));
            }
            return ret;
        }

        static internal List<Message> JsonStringToMessageList(string jsonString)
        {
            if (jsonString == null) return null;
            List<Message> list = new List<Message>();
            if (jsonString.Length == 0)
            {
                return list;
            }
            JSONNode jsonArray = JSON.Parse(jsonString);
            if (jsonArray != null && jsonArray.IsArray)
            {
                foreach (JSONNode v in jsonArray.AsArray)
                {
                    if (v.IsString)
                    {
                        Message conv = new Message(v.Value);
                        list.Add(conv);
                    }
                }
            }

            return list;
        }

        static internal JSONArray JsonObjectFromMessageList(List<Message> list)
        {
            if (list == null) return null;
            JSONArray ja = new JSONArray();
            foreach (Message msg in list)
            {
                ja.Add(msg.ToJson());
            }
            return ja;
        }

        static internal List<Conversation> JsonStringToConversationList(string jsonString)
        {
            if (jsonString == null) return null;
            List<Conversation> list = new List<Conversation>();
            if (jsonString.Length == 0)
            {
                return list;
            }
            JSONNode jsonArray = JSON.Parse(jsonString);
            if (jsonArray != null && jsonArray.IsArray)
            {
                foreach (JSONNode v in jsonArray.AsArray)
                {
                    if (v.IsString)
                    {
                        Conversation conv = new Conversation(v.Value);
                        list.Add(conv);
                    }
                }
            }

            return list;
        }

        static internal List<GroupReadAck> JsonStringToGroupReadAckList(string jsonString)
        {
            if (jsonString == null || jsonString.Length == 0) return null;
            List<GroupReadAck> list = new List<GroupReadAck>();
            JSONNode jsonArray = JSON.Parse(jsonString);
            if (jsonArray != null && jsonArray.IsArray)
            {
                foreach (JSONNode v in jsonArray.AsArray)
                {
                    if (v.IsString)
                    {
                        GroupReadAck ack = new GroupReadAck(v.Value);
                        list.Add(ack);
                    }

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
            switch (intType)
            {
                case 0: ret = MessageType.Chat; break;
                case 1: ret = MessageType.Group; break;
                case 2: ret = MessageType.Room; break;
            }
            return ret;
        }

        static internal int MessageTypeToInt(MessageType type)
        {
            int ret = 0;
            switch (type)
            {
                case MessageType.Chat: ret = 0; break;
                case MessageType.Group: ret = 1; break;
                case MessageType.Room: ret = 2; break;
            }
            return ret;
        }

        static internal string MessageBodyTypeToString(MessageBodyType type)
        {
            string ret = "txt";
            switch (type)
            {
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

        static internal string JsonStringFromUserInfo(UserInfo info)
        {
            return info.ToJson().ToString();
            
        }

        static internal UserInfo JsonStringToUserInfo(string jsonString)
        {
            return new UserInfo(jsonString);
        }


        static internal List<string> StringListFromStringArray(string[] ary) {
            List<string> list = new List<string>();
            for (int i = 0; i < ary.Length; i++) {
                list.Add(ary[i]);
            }

            return list;
        }
    }
}