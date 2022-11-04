using System;
using System.Collections.Generic;
using AgoraChat.SimpleJSON;

namespace AgoraChat
{

    internal static class ModelHelper
    {
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
