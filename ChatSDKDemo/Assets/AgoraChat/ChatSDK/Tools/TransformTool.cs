using System;
using System.Text;
using System.Collections.Generic;
using SimpleJSON;
using System.Runtime.InteropServices;

namespace AgoraChat
{
    internal class TransformTool
    {

        static internal string PtrToString(IntPtr ptr)
        {
#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
            string ret = Marshal.PtrToStringAnsi(ptr);
            return ret;
#else
            string ret = Marshal.PtrToStringUni(ptr);
            return GetUnicodeStringFromUTF8(ret);
#endif 
        }

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

        static internal string GetUnicodeStringFromUTF8InCallBack(string utf8Str)
        {
#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX|| UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
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

        static internal string[] GetArrayFromList(List<string> list)
        {
            int size = list.Count;
            string[] a = new string[size];

            int i = 0;
            foreach (string it in list)
            {
                a[i] = it;
                i++;
            }
            return a;
        }

        static internal void DeleteEmptyStringFromList(ref List<string> list)
        {
            if (list.Count == 0) return;
            List<string> new_list = new List<string>();

            foreach (string it in list)
            {
                if (it.Length > 0)
                    new_list.Add(it);
            }
            list = new_list;
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

        /*
         * return json string looking like:
         * {
         *      "metaData":{"key":"value"},
         *      "autoDelete":"NO_DELETE"
         * }
         */
        static internal string JsonStringFromRoomAttribute(Dictionary<string, string> kvs, bool deleteWhenExit)
        {
            string json = "";
            if (null != kvs && kvs.Count > 0)
            {
                JSONObject jdict = new JSONObject();
                foreach (var kv in kvs)
                {
                    if (!string.IsNullOrEmpty(kv.Key) && !string.IsNullOrEmpty(kv.Value))
                    {
                        jdict[kv.Key] = kv.Value;
                    }
                }

                if (jdict.Count > 0)
                {
                    JSONObject jo = new JSONObject();
                    jo["metaData"] = jdict;

                    string auto_delete = "DELETE";
                    if (false == deleteWhenExit) auto_delete = "NO_DELETE";
                    jo["autoDelete"] = auto_delete;

                    json = jo.ToString();
                }
            }
            return json;
        }

        /*
         * input json string looks like:
         * {
         *  "result":
         *   {
         *      "successKeys":["xxx", "xxx"],
         *      "errorKeys":
         *      {
         *          "xxx":"desc xxx",
         *          "xxx":"desc xxx",
         *      },
         *   },
         *   "is_forced":True,
         *   "properties:"
         *   {
         *      "xxx": "xxx",
         *      "xxx": "xxx",
         *   },
         *   "operator": "xxx"
         * }
         */
        static internal Dictionary<string,string> JsonStringToRoomSuccessAttribute(string json)
        {
            Dictionary<string, string> success_props = new Dictionary<string, string>();
            List<string> successKeys = new List<string>();
            Dictionary<string, string> properties = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(json))
            {
                JSONNode jn = JSON.Parse(json);
                if(null != jn)
                {
                    //get list from "successKeys" in json
                    if (jn["result"].IsObject)
                    {
                        JSONObject j_ret = jn["result"].AsObject;
                        if (j_ret["successKeys"].IsArray)
                        {
                            JSONArray j_successkeys = j_ret["successKeys"].AsArray;
                            foreach (JSONNode it in j_successkeys)
                            {
                                successKeys.Add(it.Value);
                            }
                        }
                    }

                    // get dictionary from "properties" in json
                    if (jn["properties"].IsObject)
                    {
                        JSONObject j_prop = jn["properties"].AsObject;
                        foreach (string it in j_prop.Keys)
                        {
                            properties[it] = j_prop[it];
                        }
                    }

                    // generate success properties dictionary
                    foreach(var it in successKeys)
                    {
                        if (properties.ContainsKey(it))
                        {
                            success_props[it] = properties[it];
                        }
                    }
                }
            }
            return success_props;
        }

        /*
         * input json string looks like:
         * {
         *  "result":
         *   {
         *      "successKeys":["xxx", "xxx"],
         *      "errorKeys":
         *      {
         *          "xxx":"desc xxx",
         *          "xxx":"desc xxx",
         *      },
         *   },
         *   "keys:" ["xxx","xxx"],
         *   "is_forced": True,
         *   "auto_delete": False,
         *   "operator": "xxx"
         * }
         */
        static internal List<string> JsonStringToRoomSuccessKeys(string json)
        {
            List<string> successKeys = new List<string>();

            if (!string.IsNullOrEmpty(json))
            {
                JSONNode jn = JSON.Parse(json);
                if (null != jn)
                {
                    //get list from "successKeys" in json
                    if (jn["result"].IsObject)
                    {
                        JSONObject j_ret = jn["result"].AsObject;
                        if (j_ret["successKeys"].IsArray)
                        {
                            JSONArray j_successkeys = j_ret["successKeys"].AsArray;
                            foreach (JSONNode it in j_successkeys)
                            {
                                successKeys.Add(it.Value);
                            }
                        }
                    }
                }
            }
            return successKeys;
        }

        static internal string JsonStringFromStringList(List<string> list)
        {
            string json = "";
            if (null != list && list.Count > 0)
            {
                JSONArray ja = new JSONArray();
                foreach (string str in list)
                {
                    if (!string.IsNullOrEmpty(str))
                        ja.Add(str);
                }
                json = ja.ToString();
            }
            return json;
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

                JSONNode jn = JSON.Parse(jsonObject["list"].Value);
                if (null != jn && jn.IsArray)
                {
                    JSONArray jsonArray = jn.AsArray;

                    foreach (JSONNode obj in jsonArray)
                    {
                        if (obj.IsString)
                        {
                            list.Add(obj.Value);
                        }

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

                JSONNode jn = JSON.Parse(jsonObject["list"].Value);
                if (null != jn && jn.IsArray)
                {
                    JSONArray jsonArray = jn.AsArray;

                    foreach (JSONNode obj in jsonArray)
                    {
                        if (obj.IsString)
                        {
                            list.Add(new Message(obj.Value));
                        }
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

        static internal string JsonStringFromDictionaryStringAndInt(Dictionary<string, int> dictionary)
        {

            JSONObject jo = new JSONObject();
            if (dictionary != null)
            {
                IDictionary<string, int> sortedParams = new SortedDictionary<string, int>(dictionary);
                IEnumerator<KeyValuePair<string, int>> dem = sortedParams.GetEnumerator();

                while (dem.MoveNext())
                {
                    string key = dem.Current.Key;
                    int value = dem.Current.Value;
                    if (!string.IsNullOrEmpty(key))
                    {
                        jo[key] = value;
                    }
                }
            }

            return jo.ToString();
        }


        static internal string JsonStringFromAttributes(Dictionary<string, AttributeValue> attributes = null)
        {
            if (null == attributes || 0 == attributes.Count)
                return null;

            JSONObject jo = new JSONObject();
            foreach (var item in attributes)
            {
                jo[item.Key] = item.Value.ToJsonObject();
            }
            return jo.ToString();
        }

        static internal Dictionary<string, string> JsonStringToDictionary(string jsonString)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            if (jsonString == null || jsonString.Length == 0) return ret;

            JSONNode jn = JSON.Parse(jsonString);
            if (null == jn || jn.IsNull) return ret;

            JSONObject jo = jn.AsObject;
            foreach (string s in jo.Keys)
            {
                ret.Add(s, jo[s]);
            }

            return ret;
        }

        static internal Dictionary<string, int> JsonStringToDictionaryStringAndInt(string jsonString)
        {
            Dictionary<string, int> ret = new Dictionary<string, int>();

            if (jsonString == null || jsonString.Length == 0) return ret;

            JSONNode jn = JSON.Parse(jsonString);
            if (null == jn || jn.IsNull) return ret;

            JSONObject jo = jn.AsObject;
            foreach (string s in jo.Keys)
            {
                ret.Add(s, jo[s].AsInt);
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

        static internal List<string> JsonArrayToStringList(JSONNode jn)
        {
            List<string> list = new List<string>();
            if (null == jn) return list;

            if (!jn.IsArray) return list;

            foreach (JSONNode v in jn.AsArray)
            {
                if (v.IsString)
                {
                    list.Add(v.Value);
                }
            }
            return list;
        }

        static internal JSONArray JsonObjectFromStringList(List<string> list)
        {
            if (list == null) return null;
            JSONArray ja = new JSONArray();
            foreach (string str in list)
            {
                ja.Add(str);
            }
            return ja;
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

        static internal List<SupportLanguage> JsonStringToSupportLanguageList(string jsonString)
        {
            if (jsonString == null || jsonString.Length == 0) return null;
            List<SupportLanguage> list = new List<SupportLanguage>();
            JSONNode jsonArray = JSON.Parse(jsonString);
            if (jsonArray != null && jsonArray.IsArray)
            {
                foreach (JSONNode v in jsonArray.AsArray)
                {
                    if (v.IsString)
                    {
                        SupportLanguage language = new SupportLanguage(v.Value);
                        list.Add(language);
                    }
                }
            }

            return list;
        }

        static internal CursorResult<GroupReadAck> JsonStringToGroupReadAckResult(string jsonString) {
            CursorResult<GroupReadAck> result = null;
            if (jsonString != null)
            {
                result = new CursorResult<GroupReadAck>();

                JSONNode jsonObject = JSON.Parse(jsonString);

                result.Cursor = jsonObject["cursor"].Value;

                List<GroupReadAck> list = new List<GroupReadAck>();

                if (jsonObject["list"].IsString)
                {

                    JSONArray jsonArray = JSON.Parse(jsonObject["list"].Value).AsArray;

                    foreach (JSONNode obj in jsonArray)
                    {
                        if (obj.IsString)
                        {
                            list.Add(new GroupReadAck(obj.Value));
                        }
                    }
                }

                result.Data = list;
            }
            return result;
        }

        static internal CursorResult<MessageReaction> JsonStringToMessageReactionResult(string jsonString) {
            CursorResult<MessageReaction> result = null;
            if (jsonString != null)
            {
                result = new CursorResult<MessageReaction>();

                JSONNode jsonObject = JSON.Parse(jsonString);

                result.Cursor = jsonObject["cursor"].Value;

                List<MessageReaction> list = new List<MessageReaction>();

                if (jsonObject["list"].IsString)
                {

                    JSONArray jsonArray = JSON.Parse(jsonObject["list"].Value).AsArray;

                    foreach (JSONNode obj in jsonArray)
                    {
                        if (obj.IsString)
                        {
                            list.Add(MessageReaction.FromJson(obj.Value));
                        }
                    }
                }

                result.Data = list;
            }
            return result;
        }

        static internal Dictionary<string, List<MessageReaction>> JsonStringToReactionMap(string jsonString) {

            Dictionary<string, List<MessageReaction>> ret = new Dictionary<string, List<MessageReaction>>();

            JSONNode jn = JSON.Parse(jsonString);
            if (null == jn) return ret;

            JSONObject jo = jn.AsObject;

            foreach (string s in jo.Keys)
            {
                ret.Add(s, MessageReaction.ListFromJsonObject(jo[s]));
            }

            return ret;
        }

        static internal List<Presence> JsonStringToPresenceList(string jsonString) {
            List<Presence> ret = new List<Presence>();
            JSONNode jn = JSON.Parse(jsonString);
            if (null == jn || !jn.IsArray) return ret;

            JSONArray jAry = jn.AsArray;
            foreach (JSONNode item in jAry) {
                ret.Add(new Presence(item.AsObject));
            }
            return ret;
        }
        
    }
}
