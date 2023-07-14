using System;
using System.Collections.Generic;
using AgoraChat.MessageBody;
using AgoraChat.SimpleJSON;

namespace AgoraChat
{


    internal static class ModelHelper
    {

        internal static void AddWithoutNull(this JSONObject jo, string key, object value)
        {
            if (value == null)
            {
                return;
            }

            if (value.GetType() == typeof(long))
            {
                double d = Convert.ToDouble(value);
                jo.Add(key, d);
            }

            else if (value.GetType() == typeof(double))
            {
                jo.Add(key, (double)value);
            }

            else if (value.GetType() == typeof(float))
            {
                jo.Add(key, (float)value);
            }

            else if (value.GetType() == typeof(string))
            {
                jo.Add(key, (string)value);
            }

            else if (value.GetType() == typeof(bool))
            {
                jo.Add(key, (bool)value);
            }

            else if (value.GetType() == typeof(int))
            {
                jo.Add(key, (int)value);
            }

            else if (value.GetType() == typeof(JSONObject))
            {
                jo.Add(key, (JSONObject)value);
            }

            else
            {
                jo.Add(key, (JSONNode)value);
            }
        }

        internal static IMessageBody CreateBodyWithJsonObject(JSONNode jsonNode)
        {
            if (jsonNode.IsNull || !jsonNode.IsObject) return null;

            IMessageBody body = null;
            MessageBodyType type = jsonNode["type"].AsInt.ToMessageBodyType();

            switch (type)
            {
                case MessageBodyType.TXT:
                    body = CreateWithJsonObject<TextBody>(jsonNode["body"]);
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
                case MessageBodyType.COMBINE:
                    body = CreateWithJsonObject<CombineBody>(jsonNode["body"]);
                    break;
            }

            body.FromJsonObjectToIMessageBody(jsonNode.AsObject);

            return body;
        }

        internal static T CreateWithJsonObject<T>(JSONNode jsonNode) where T : BaseModel
        {
            if (null == jsonNode || !jsonNode.IsObject) return null;
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

                if (null == jn || jn.IsNull || !jn.IsObject) break;

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
            List<T> list = new List<T>();

            if (null == jn || !jn.IsArray) return list;

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
        // Because JSONNode key only support string type，so key must be string type。
        // T is BaseModel child class
        internal static Dictionary<string, T> BaseModelDictionaryFromJsonObject<T>(JSONNode jo) where T : BaseModel
        {
            Dictionary<string, T> ret = new Dictionary<string, T>();

            if (!jo.IsObject) return ret;

            foreach (string s in jo.Keys)
            {
                if (jo[s].IsObject)
                {
                    ret.Add(s, ModelHelper.CreateWithJsonObject<T>(jo[s].AsObject));
                }
                else if (jo[s].AsArray)
                {
                    foreach (var it in jo[s].AsArray)
                    {

                    }
                }
            }

            return ret;
        }

        internal static Dictionary<string, List<T>> ListBaseModelDictionaryFromJsonObject<T>(JSONNode jo) where T : BaseModel
        {
            Dictionary<string, List<T>> ret = new Dictionary<string, List<T>>();

            if (!jo.IsObject) return ret;

            foreach (string s in jo.Keys)
            {
                if (jo[s].IsArray)
                {
                    List<T> list = new List<T>();
                    foreach (var it in jo[s].AsArray)
                    {
                        list.Add(ModelHelper.CreateWithJsonObject<T>(it));
                    }
                    ret.Add(s, list);
                }
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

        internal static Dictionary<string, Dictionary<string, string>> NestedStringDictionaryFromJsonObject(JSONNode jo)
        {
            Dictionary<string, Dictionary<string, string>> ret = new Dictionary<string, Dictionary<string, string>>();

            if (!jo.IsObject) return ret;

            foreach (string s in jo.Keys)
            {
                if (jo[s].IsObject)
                {
                    Dictionary<string, string> dict = StringDictionaryFromJsonObject(jo[s]);
                    if (null != dict) ret.Add(s, dict);
                }
            }
            return ret;
        }

        internal static Dictionary<string, T> SimpleTypeDictionaryFromJsonObject<T>(JSONNode jo) where T : IConvertible
        {
            if (jo == null) return null;

            Dictionary<string, T> ret = new Dictionary<string, T>();

            foreach (string s in jo.Keys)
            {
                if (jo[s].IsNumber)
                {
                    if (typeof(T) == typeof(long))
                    {
                        ret.Add(s, (T)Convert.ChangeType(jo[s].AsDouble, typeof(T)));
                    }
                    else if (typeof(T) == typeof(int))
                    {
                        ret.Add(s, (T)Convert.ChangeType(jo[s].AsInt, typeof(T)));
                    }
                    else if (typeof(T) == typeof(double))
                    {
                        ret.Add(s, (T)Convert.ChangeType(jo[s].AsDouble, typeof(T)));
                    }
                    else if (typeof(T) == typeof(float))
                    {
                        ret.Add(s, (T)Convert.ChangeType(jo[s].AsDouble, typeof(T)));
                    }
                }
                else if (jo[s].IsString)
                {
                    ret.Add(s, (T)Convert.ChangeType(jo[s].Value, typeof(T)));
                }
                else if (jo[s].IsBoolean)
                {
                    ret.Add(s, (T)Convert.ChangeType(jo[s].AsBool, typeof(T)));
                }
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

        internal static JSONNode JsonArrayFromIntList(List<int> list)
        {
            JSONArray ja = new JSONArray();
            if (list != null)
            {
                foreach (int i in list)
                {
                    ja.Add(i);
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

        internal static JSONObject JsonObjectFromDictionary(Dictionary<string, string> dictionary)
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

    internal static class MultiDevicesOperationHelper
    {
        public static int ToInt(this MultiDevicesOperation operation)
        {
            switch (operation)
            {
                case MultiDevicesOperation.UNKNOWN: return -1;
                case MultiDevicesOperation.CONTACT_REMOVE: return 2;
                case MultiDevicesOperation.CONTACT_ACCEPT: return 3;
                case MultiDevicesOperation.CONTACT_DECLINE: return 4;
                case MultiDevicesOperation.CONTACT_BAN: return 5;
                case MultiDevicesOperation.CONTACT_ALLOW: return 6;
                case MultiDevicesOperation.GROUP_CREATE: return 10;
                case MultiDevicesOperation.GROUP_DESTROY: return 11;
                case MultiDevicesOperation.GROUP_JOIN: return 12;
                case MultiDevicesOperation.GROUP_LEAVE: return 13;
                case MultiDevicesOperation.GROUP_APPLY: return 14;
                case MultiDevicesOperation.GROUP_APPLY_ACCEPT: return 15;
                case MultiDevicesOperation.GROUP_APPLY_DECLINE: return 16;
                case MultiDevicesOperation.GROUP_INVITE: return 17;
                case MultiDevicesOperation.GROUP_INVITE_ACCEPT: return 18;
                case MultiDevicesOperation.GROUP_INVITE_DECLINE: return 19;
                case MultiDevicesOperation.GROUP_KICK: return 20;
                case MultiDevicesOperation.GROUP_BAN: return 21;
                case MultiDevicesOperation.GROUP_ALLOW: return 22;
                case MultiDevicesOperation.GROUP_BLOCK: return 23;
                case MultiDevicesOperation.GROUP_UNBLOCK: return 24;
                case MultiDevicesOperation.GROUP_ASSIGN_OWNER: return 25;
                case MultiDevicesOperation.GROUP_ADD_ADMIN: return 26;
                case MultiDevicesOperation.GROUP_REMOVE_ADMIN: return 27;
                case MultiDevicesOperation.GROUP_ADD_MUTE: return 28;
                case MultiDevicesOperation.GROUP_REMOVE_MUTE: return 29;
                case MultiDevicesOperation.GROUP_ADD_USER_WHITE_LIST: return 30;
                case MultiDevicesOperation.GROUP_REMOVE_USER_WHITE_LIST: return 31;
                case MultiDevicesOperation.GROUP_ALL_BAN: return 32;
                case MultiDevicesOperation.GROUP_REMOVE_ALL_BAN: return 33;
                case MultiDevicesOperation.THREAD_CREATE: return 40;
                case MultiDevicesOperation.THREAD_DESTROY: return 41;
                case MultiDevicesOperation.THREAD_JOIN: return 42;
                case MultiDevicesOperation.THREAD_LEAVE: return 43;
                case MultiDevicesOperation.THREAD_UPDATE: return 44;
                case MultiDevicesOperation.THREAD_KICK: return 45;
                case MultiDevicesOperation.SET_METADATA: return 50;
                case MultiDevicesOperation.DELETE_METADATA: return 51;
                case MultiDevicesOperation.GROUP_MEMBER_METADATA_CHANGED: return 52;
                case MultiDevicesOperation.CONVERSATION_PINNED: return 60;
                case MultiDevicesOperation.CONVERSATION_UNPINNED: return 61;
                case MultiDevicesOperation.CONVERSATION_DELETED: return 62;
                default:
                    return -1;
            }
        }

        public static MultiDevicesOperation ToMultiDevicesOperation(this int operation)
        {
            switch (operation)
            {
                case -1: return MultiDevicesOperation.UNKNOWN;
                case 2: return MultiDevicesOperation.CONTACT_REMOVE;
                case 3: return MultiDevicesOperation.CONTACT_ACCEPT;
                case 4: return MultiDevicesOperation.CONTACT_DECLINE;
                case 5: return MultiDevicesOperation.CONTACT_BAN;
                case 6: return MultiDevicesOperation.CONTACT_ALLOW;
                case 10: return MultiDevicesOperation.GROUP_CREATE;
                case 11: return MultiDevicesOperation.GROUP_DESTROY;
                case 12: return MultiDevicesOperation.GROUP_JOIN;
                case 13: return MultiDevicesOperation.GROUP_LEAVE;
                case 14: return MultiDevicesOperation.GROUP_APPLY;
                case 15: return MultiDevicesOperation.GROUP_APPLY_ACCEPT;
                case 16: return MultiDevicesOperation.GROUP_APPLY_DECLINE;
                case 17: return MultiDevicesOperation.GROUP_INVITE;
                case 18: return MultiDevicesOperation.GROUP_INVITE_ACCEPT;
                case 19: return MultiDevicesOperation.GROUP_INVITE_DECLINE;
                case 20: return MultiDevicesOperation.GROUP_KICK;
                case 21: return MultiDevicesOperation.GROUP_BAN;
                case 22: return MultiDevicesOperation.GROUP_ALLOW;
                case 23: return MultiDevicesOperation.GROUP_BLOCK;
                case 24: return MultiDevicesOperation.GROUP_UNBLOCK;
                case 25: return MultiDevicesOperation.GROUP_ASSIGN_OWNER;
                case 26: return MultiDevicesOperation.GROUP_ADD_ADMIN;
                case 27: return MultiDevicesOperation.GROUP_REMOVE_ADMIN;
                case 28: return MultiDevicesOperation.GROUP_ADD_MUTE;
                case 29: return MultiDevicesOperation.GROUP_REMOVE_MUTE;
                case 30: return MultiDevicesOperation.GROUP_ADD_USER_WHITE_LIST;
                case 31: return MultiDevicesOperation.GROUP_REMOVE_USER_WHITE_LIST;
                case 32: return MultiDevicesOperation.GROUP_ALL_BAN;
                case 33: return MultiDevicesOperation.GROUP_REMOVE_ALL_BAN;
                case 40: return MultiDevicesOperation.THREAD_CREATE;
                case 41: return MultiDevicesOperation.THREAD_DESTROY;
                case 42: return MultiDevicesOperation.THREAD_JOIN;
                case 43: return MultiDevicesOperation.THREAD_LEAVE;
                case 44: return MultiDevicesOperation.THREAD_UPDATE;
                case 45: return MultiDevicesOperation.THREAD_KICK;
                case 50: return MultiDevicesOperation.SET_METADATA;
                case 51: return MultiDevicesOperation.DELETE_METADATA;
                case 52: return MultiDevicesOperation.GROUP_MEMBER_METADATA_CHANGED;
                case 60: return MultiDevicesOperation.CONVERSATION_PINNED;
                case 61: return MultiDevicesOperation.CONVERSATION_UNPINNED;
                case 62: return MultiDevicesOperation.CONVERSATION_DELETED;
                default: return MultiDevicesOperation.UNKNOWN;
            }
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
                case MessageBodyType.COMBINE: return 8;
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
                case 8: return MessageBodyType.COMBINE;
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

    internal static class MessagePriorityHelper
    {
        public static int ToInt(this RoomMessagePriority priority)
        {
            switch (priority)
            {
                case RoomMessagePriority.High: return 0;
                case RoomMessagePriority.Normal: return 1;
                case RoomMessagePriority.Low: return 2;
                default: return 1;
            }
        }

        public static RoomMessagePriority ToMessagePriority(this int i)
        {
            switch (i)
            {
                case 0: return RoomMessagePriority.High;
                case 1: return RoomMessagePriority.Normal;
                case 2: return RoomMessagePriority.Low;
                default:
                    return RoomMessagePriority.Normal;
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

    internal static class DownLoadStatusHelper
    {
        public static DownLoadStatus ToDownLoadStatus(this int iType)
        {
            switch (iType)
            {
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
            switch (type)
            {
                case DownLoadStatus.DOWNLOADING: return 0;
                case DownLoadStatus.SUCCESS: return 1;
                case DownLoadStatus.FAILED: return 2;
                case DownLoadStatus.PENDING: return 3;
                default:
                    return 2;
            }
        }
    }

    internal static class DisconnectReasonHelper
    {
        public static DisconnectReason ToDisconnectReason(this int iType)
        {
            switch (iType)
            {
                case 202: return DisconnectReason.Reason_AuthenticationFailed;
                case 206: return DisconnectReason.Reason_LoginFromOtherDevice;
                case 207: return DisconnectReason.Reason_RemoveFromServer;
                case 214: return DisconnectReason.Reason_LoginTooManyDevice;
                case 216: return DisconnectReason.Reason_ChangePassword;
                case 217: return DisconnectReason.Reason_KickedByOtherDevice;
                case 305: return DisconnectReason.Reason_ForbidByServer;
                default:
                    return DisconnectReason.Reason_Disconnected;
            }
        }
    }

    internal static class MessageReactionOperateHelper
    {
        public static int ToInt(this MessageReactionOperate operate)
        {
            switch (operate)
            {
                case MessageReactionOperate.MessageReactionOperateRemove: return 0;
                case MessageReactionOperate.MessageReactionOperateAdd: return 1;
                default: return -1;
            }
        }

        public static MessageReactionOperate ToMessageReactionOperate(this int i)
        {
            switch (i)
            {
                case 0: return MessageReactionOperate.MessageReactionOperateRemove;
                case 1: return MessageReactionOperate.MessageReactionOperateAdd;
                default:
                    return MessageReactionOperate.MessageReactionOperateAdd;
            }
        }
    }

    namespace InternalSpace
    {
        public static class MyTest
        {
            public static void DelegateTester()
            {
                SDKClient.Instance.DelegateTester();
            }
        }

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
