using System;
using System.Collections.Generic;
using AgoraChat.MessageBody;
using AgoraChat.SimpleJSON;

namespace AgoraChat
{

    internal static class ModelHelper
    {

        internal static IMessageBody CreateBodyWithJsonObject(JSONNode jsonNode)
        {
            if (jsonNode.IsNull || !jsonNode.IsObject) return null;
            IMessageBody body = null;
            MessageBodyType type = jsonNode["type"].AsInt.ToMessageBodyType();
            switch (type)
            {
                case MessageBodyType.TXT:
                    body = CreateWithJsonObject<TextBody>(jsonNode["body"].AsObject);
                    break;
                case MessageBodyType.IMAGE:
                    body = CreateWithJsonObject<ImageBody>(jsonNode["body"]);
                    break;
                case MessageBodyType.VIDEO:
                    body = CreateWithJsonObject<VideoBody>(jsonNode["body"]);
                    break;
                case MessageBodyType.LOCATION:
                    body = CreateWithJsonObject<LocationBody>(jsonNode["body"]);
                    break;
                case MessageBodyType.VOICE:
                    body = CreateWithJsonObject<VoiceBody>(jsonNode["body"]);
                    break;
                case MessageBodyType.FILE:
                    body = CreateWithJsonObject<FileBody>(jsonNode["body"]);
                    break;
                case MessageBodyType.CUSTOM:
                    body = CreateWithJsonObject<CustomBody>(jsonNode["body"]);
                    break;
                case MessageBodyType.CMD:
                    body = CreateWithJsonObject<CmdBody>(jsonNode["body"]);
                    break;
            }

            return body;
        }

        internal static T CreateWithJsonObject<T>(JSONNode jsonNode) where T : BaseModel
        {
            if (jsonNode == null || !jsonNode.IsObject) return null;
            BaseModel bs = (T)Activator.CreateInstance(typeof(T), true);
            bs.FromJsonObject(jsonNode.AsObject);
            return (T)bs;
        }

        internal static T CreateWithJsonString<T>(string jsonString) where T : BaseModel
        {
            if (!string.IsNullOrEmpty(jsonString))
            {
                JSONNode jn = JSON.Parse(jsonString);
                return CreateWithJsonObject<T>(jn);
            }

            return null;
        }
    }

    internal static class String
    {
        internal static JSONNode GetReturnJsonNode(this string str)
        {
            JSONNode jsonNode = null;
            do
            {
                if (string.IsNullOrEmpty(str)) break;

                JSONNode jn = JSON.Parse(str);

                if (jn.IsNull || !jn.IsObject) break;

                jsonNode = jn["ret"];

            } while (false);


            return jsonNode;
        }
    }

    internal static class List
    {
        internal static List<string> StringListFromJsonArray(JSONNode jsonNode)
        {
            List<string> list = new List<string>();
            if (jsonNode != null && jsonNode.IsArray)
            {
                foreach (JSONNode item in jsonNode.AsArray)
                {
                    if (item.IsString)
                    {
                        list.Add(item.Value);
                    }
                }
            }
            return list;
        }

        internal static List<string> StringListFromJsonString(string json)
        {
            if (json == null) return new List<string>();
            JSONNode jn = JSON.Parse(json);
            return StringListFromJsonArray(jn);
        }

        internal static List<T> BaseModelListFromJsonArray<T>(JSONNode jn) where T : BaseModel
        {
            if (null != jn && jn.IsArray) return null;

            List<T> list = new List<T>();

            foreach (JSONNode it in jn.AsArray)
            {
                if (it.IsObject)
                {
                    list.Add(ModelHelper.CreateWithJsonObject<T>(it.AsObject));
                }
            }

            return list;
        }

        internal static List<T> BaseModelListFromJsoString<T>(string jsonString) where T : BaseModel
        {

            if (string.IsNullOrEmpty(jsonString))
            {
                JSONNode jn = JSON.Parse(jsonString);
                return BaseModelListFromJsonArray<T>(jn);
            }

            return null;
        }
    }


    internal static class Dictionary
    {
        // 因为JSONNode key 只支持string，所以此处的key必须为string类型。
        // T: BaseModel子类
        internal static Dictionary<string, T> BaseModelDictionaryFromJsonObject<T>(JSONNode jo) where T : BaseModel
        {
            if (!jo.IsObject) return null;

            Dictionary<string, T> ret = new Dictionary<string, T>();

            foreach (string s in jo.Keys)
            {
                ret.Add(s, ModelHelper.CreateWithJsonObject<T>(jo[s].AsObject));
            }

            return ret;
        }

        internal static Dictionary<string, string> StringDictionaryFromJsonObject(JSONNode jo)
        {

            if (jo == null) return null;

            Dictionary<string, string> ret = new Dictionary<string, string>();

            foreach (string s in jo.Keys)
            {
                ret.Add(s, jo[s]);
            }

            return ret;
        }
    }

    internal static class JsonObject
    {

        internal static JSONNode JsonArrayFromStringList(List<string> list)
        {
            JSONArray ja = new JSONArray();
            if (list != null)
            {
                foreach (string str in list)
                {
                    ja.Add(str);
                }
            }

            return ja;
        }

        internal static JSONNode JsonArrayFromList<T>(List<T> list) where T : BaseModel
        {
            if (list == null) return null;

            JSONArray jsonArray = new JSONArray();

            foreach (BaseModel bm in list)
            {
                jsonArray.Add(bm.ToJsonObject());
            }

            return jsonArray;
        }

        internal static string JsonObjectFromDictionary(Dictionary<string, string> dictionary)
        {
            JSONObject jo = new JSONObject();

            if (null != dictionary && dictionary.Count > 0)
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

            return jo;
        }

        internal static JSONObject JsonObjectFromAttributes(Dictionary<string, AttributeValue> attributes = null)
        {
            if (null == attributes || 0 == attributes.Count) return null;

            JSONObject jo = new JSONObject();
            foreach (var item in attributes)
            {
                jo[item.Key] = item.Value.ToJsonObject();
            }
            return jo;
        }
    }


    internal static class MessageDirectionHelper 
    {
        public static int ToInt(this MessageDirection direction) 
        {
            switch (direction)
            {
                case MessageDirection.SEND: return 0;
                case MessageDirection.RECEIVE: return 1;
                default:
                    return 0;

            }
        }

        public static MessageDirection ToMesssageDirection(this int direction)
        {
            switch (direction) 
            {
                case 0: return MessageDirection.SEND;
                case 1: return MessageDirection.RECEIVE;
                default: return MessageDirection.SEND;
            }
        }
    }

    internal static class ConversationTypeHelper
    {
        public static int ToInt(this ConversationType type)
        {
            switch (type)
            {
                case ConversationType.Chat: return 0;
                case ConversationType.Group: return 1;
                case ConversationType.Room: return 2;
                default: return 0;
            }
        }

        public static ConversationType ToConversationType(this int i)
        {
            switch (i)
            {
                case 0: return ConversationType.Chat;
                case 1: return ConversationType.Group;
                case 2: return ConversationType.Room;
                default:
                    return ConversationType.Chat;
            }
        }
    }

    internal static class BodyTypeHelper
    {
        public static int ToInt(this MessageBodyType type)
        {
            switch (type)
            {
                case MessageBodyType.TXT: return 0;
                case MessageBodyType.IMAGE: return 1;
                case MessageBodyType.VIDEO: return 2;
                case MessageBodyType.LOCATION: return 3;
                case MessageBodyType.VOICE: return 4;
                case MessageBodyType.FILE: return 5;
                case MessageBodyType.CMD: return 6;
                case MessageBodyType.CUSTOM: return 7;
                default: return 0;
            }
        }

        public static MessageBodyType ToMessageBodyType(this int i)
        {
            switch (i)
            {
                case 0: return MessageBodyType.TXT;
                case 1: return MessageBodyType.IMAGE;
                case 2: return MessageBodyType.VIDEO;
                case 3: return MessageBodyType.LOCATION;
                case 4: return MessageBodyType.VOICE;
                case 5: return MessageBodyType.FILE;
                case 6: return MessageBodyType.CMD;
                case 7: return MessageBodyType.CUSTOM;
                default:
                    return MessageBodyType.TXT;
            }
        }
    }

    internal static class MessageSearchDirectionHelper
    {
        public static int ToInt(this MessageSearchDirection type)
        {
            switch (type)
            {
                case MessageSearchDirection.UP: return 0;
                case MessageSearchDirection.DOWN: return 1;
                default: return 0;
            }
        }

        public static MessageSearchDirection ToMessageSearchDirection(this int i)
        {
            switch (i)
            {
                case 0: return MessageSearchDirection.UP;
                case 1: return MessageSearchDirection.DOWN;
                default:
                    return MessageSearchDirection.UP;
            }
        }
    }

    internal static class ChatThreadOperationHelper
    {
        public static int ToInt(this ChatThreadOperation type)
        {
            switch (type)
            {
                case ChatThreadOperation.UnKnown: return 0;
                case ChatThreadOperation.Create: return 1;
                case ChatThreadOperation.Update: return 2;
                case ChatThreadOperation.Delete: return 3;
                case ChatThreadOperation.Update_Msg: return 4;
                default: return 0;
            }
        }

        public static ChatThreadOperation ToChatThreadOperation(this int i)
        {
            switch (i)
            {
                case 0: return ChatThreadOperation.UnKnown;
                case 1: return ChatThreadOperation.Create;
                case 2: return ChatThreadOperation.Update;
                case 3: return ChatThreadOperation.Delete;
                case 4: return ChatThreadOperation.Update_Msg;
                default:
                    return ChatThreadOperation.UnKnown;
            }
        }
    }

    internal static class GroupPermissionTypeHelper
    {
        public static int ToInt(this GroupPermissionType type)
        {
            switch (type)
            {
                case GroupPermissionType.Member: return 0;
                case GroupPermissionType.Admin: return 1;
                case GroupPermissionType.Owner: return 2;
                default: return -1;
            }
        }

        public static GroupPermissionType ToGroupPermissionType(this int i)
        {
            switch (i)
            {
                case 0: return GroupPermissionType.Member;
                case 1: return GroupPermissionType.Admin;
                case 2: return GroupPermissionType.Owner;
                default:
                    return GroupPermissionType.Unknown;
            }
        }
    }

    internal static class RoomPermissionTypeHelper
    {
        public static int ToInt(this RoomPermissionType type)
        {
            switch (type)
            {
                case RoomPermissionType.Member: return 0;
                case RoomPermissionType.Admin: return 1;
                case RoomPermissionType.Owner: return 2;
                default: return -1;
            }
        }

        public static RoomPermissionType ToRoomPermissionType(this int i)
        {
            switch (i)
            {
                case 0: return RoomPermissionType.Member;
                case 1: return RoomPermissionType.Admin;
                case 2: return RoomPermissionType.Owner;
                default:
                    return RoomPermissionType.Unknown;
            }
        }
    }



    internal static class MessageStatusHelper
    {
        public static int ToInt(this MessageStatus type)
        {
            switch (type)
            {
                case MessageStatus.CREATE: return 0;
                case MessageStatus.PROGRESS: return 1;
                case MessageStatus.SUCCESS: return 2;
                case MessageStatus.FAIL: return 3;
                default: return 0;
            }
        }

        public static MessageStatus ToMessageStatus(this int i)
        {
            switch (i)
            {
                case 0: return MessageStatus.CREATE;
                case 1: return MessageStatus.PROGRESS;
                case 2: return MessageStatus.SUCCESS;
                case 3: return MessageStatus.FAIL;
                default:
                    return MessageStatus.CREATE;
            }
        }
    }

    internal static class MessageTypeHelper
    {
        public static MessageType ToMessageType(this int intType)
        {
            switch (intType)
            {
                case 0: return MessageType.Chat;
                case 1: return MessageType.Group;
                case 2: return MessageType.Room;
                default:
                    return MessageType.Chat;

            }
        }

        public static int ToInt(this MessageType type)
        {
            switch (type)
            {
                case MessageType.Chat: return 0;
                case MessageType.Group: return 1;
                case MessageType.Room: return 2;
                default:
                    return 0;
            }
        }
    }

    internal static class DownLoadStatusHelper {
        public static DownLoadStatus ToDownLoadStatus(this int iType) 
        {
            switch (iType) {
                case 0: return DownLoadStatus.DOWNLOADING;
                case 1: return DownLoadStatus.SUCCESS;
                case 2: return DownLoadStatus.FAILED;
                case 3: return DownLoadStatus.PENDING;
                default:
                    return DownLoadStatus.FAILED;
            }
        }

        public static int ToInt(this DownLoadStatus type)
        {
            switch (type) { 
                case DownLoadStatus.DOWNLOADING: return 0;
                case DownLoadStatus.SUCCESS: return 1;
                 case DownLoadStatus.FAILED: return 2;
                case DownLoadStatus.PENDING: return 3;
                default:
                    return 2;
            }
        }
    }



    namespace InternalSpace
    {
        public static class MyJson
        {
            public static string ToJson(this BaseModel bs)
            {
                return bs.ToJsonObject().ToString();
            }

            public static T FromJson<T>(string json) where T : BaseModel
            {
                if (json.Length <= 0) return default(T);

                JSONNode jn = JSON.Parse(json);
                if (null == jn || !jn.IsObject) return default(T);

                JSONObject jo = jn.AsObject;

                BaseModel bs = (T)Activator.CreateInstance(typeof(T), true);
                bs.FromJsonObject(jo);
                return (T)bs;
            }

            // this function need "public" for constructor
            // but we set "internal" for constructor
            public static T FromJsonNeedPublic<T>(string json)
            {
                if (json.Length <= 0) return default(T);

                JSONNode jn = JSON.Parse(json);
                if (null == jn || !jn.IsObject) return default(T);

                JSONObject jo = jn.AsObject;
                return (T)Activator.CreateInstance(typeof(T), new object[] { jo }); // work
                //return (T)Activator.CreateInstance(typeof(T), json); // work
                //return (T)Activator.CreateInstance(typeof(T), new object[] { true, json}); // work
                //return (T)Activator.CreateInstance(typeof(T), jo); // NOT work
            }
        }
    }
}
