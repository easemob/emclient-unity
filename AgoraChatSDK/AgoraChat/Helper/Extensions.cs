using System;
using System.Collections.Generic;
using AgoraChat.SimpleJSON;
using AgoraChat.InternalSpace;
using AgoraChat.MessageBody;
using System;

namespace AgoraChat
{
    internal static class List
    {
        internal static List<string> StringListFromJsonObject(JSONNode jsonNode)
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

        internal static List<string> StringListFromJson(string json)
        {
            if (json == null) return new List<string>();
            JSONNode jn = JSON.Parse(json);
            return StringListFromJsonObject(jn);
        }

        internal static List<MessageReaction> ReactionListFromJsonObject(JSONNode jsonNode)
        {
            List<MessageReaction> list = new List<MessageReaction>();
            if (jsonNode != null && jsonNode.IsArray)
            {
                foreach (JSONNode item in jsonNode.AsArray)
                {
                    if (item.AsObject)
                    {
                        list.Add(new MessageReaction(item.AsObject));
                    }
                }
            }
            return list;
        }
    }

    internal static class Dictionary
    {
        internal static Dictionary<string, string> DictionaryFromJson(string jsonString)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            if (jsonString == null || jsonString.Length == 0) return ret;
            
            JSONNode jn = JSON.Parse(jsonString);
            if (null == jn || jn.IsNull || !jn.IsObject) return ret;

            JSONObject jo = jn.AsObject;
            foreach (string s in jo.Keys)
            {
                ret.Add(s, jo[s]);
            }

            return ret;
        }

        internal static Dictionary<string, List<MessageReaction>> ReactionMapFromJsonObject(JSONNode jsonNode)
        {
            Dictionary<string, List<MessageReaction>> dict = new Dictionary<string, List<MessageReaction>>();
            if (jsonNode != null && jsonNode.IsObject)
            {
                JSONObject jo = jsonNode.AsObject;
                foreach (string s in jo.Keys)
                {
                    dict.Add(s, List.ReactionListFromJsonObject(jo[s]));
                }
            }
            return dict;
        }

        internal static Dictionary<string, AttributeValue> AttributesFromJson(string jsonString)
        {
            Dictionary<string, AttributeValue> ret = new Dictionary<string, AttributeValue>();

            if (null == jsonString || jsonString.Length <= 2) return ret;

            JSONNode jn = JSON.Parse(jsonString);
            if (null == jn || !jn.IsObject) return ret;

            JSONObject jo = jn.AsObject;
            foreach (string k in jo.Keys)
            {
                ret.Add(k, new AttributeValue(jo[k].AsObject));
            }
            return ret;
        }
    }

    internal static class JsonString
    {

        internal static string JsonStringFromStringList(List<string> list)
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

        internal static string JsonStringFromDictionary(Dictionary<string, string> dictionary)
        {
            JSONObject jo = new JSONObject();

            if (null !=  dictionary && dictionary.Count > 0)
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

        internal static string JsonStringFromAttributes(Dictionary<string, AttributeValue> attributes = null)
        {
            if (null == attributes || 0 == attributes.Count) return null;

            JSONObject jo = new JSONObject();
            foreach (var item in attributes)
            {
                jo[item.Key] = item.Value.ToJsonObject();
            }
            return jo.ToString();
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

            public static T FromJson<T>(string json)where T:BaseModel
            {
                if (json.Length <= 0) return default(T);

                JSONNode jn = JSON.Parse(json);
                if (null == jn || !jn.IsObject) return default(T);

                JSONObject jo = jn.AsObject;

                //set nonPublic with true, to tell CreateInstance use non-public constructor
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

    public static class BaseModel
    {
        public static T FromJson<T>(JSONObject jsonObject) {
            return (T)Activator.CreateInstance(typeof(T), jsonObject);
        }
    }
}