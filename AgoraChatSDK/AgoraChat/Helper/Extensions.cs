using System;
using System.Collections.Generic;
using AgoraChat.SimpleJSON;
namespace AgoraChat
{
    public static class List
    {

        public static List<string> StringListFromJsonObject(JSONNode jsonNode)
        {
            List<string> list = new List<string>();
            if (jsonNode != null && jsonNode.IsArray) {
                foreach (JSONNode item in jsonNode.AsArray)
                {
                    if(item.IsString)
                    {
                        list.Add(item.Value);
                    }
                }
            }
            return list;
        }

        public static List<MessageReaction> ReactionListFromJsonObject(JSONNode jsonNode) {
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

    public static class Dictionary
    {
        public static Dictionary<string, List<MessageReaction>> ReactionMapFromJsonObject(JSONNode jsonNode)
        {
            Dictionary<string, List<MessageReaction>> dict = new Dictionary<string, List<MessageReaction>>();
            if (jsonNode != null && jsonNode.IsObject) {
                JSONObject jo = jsonNode.AsObject;
                foreach (string s in jo.Keys)
                {
                    dict.Add(s, List.ReactionListFromJsonObject(jo[s]));
                }
            }
            return dict;
        }
    }

    public static class BaseModel
    {
        public static T FromJson<T>(JSONObject jsonObject) {
            return (T)Activator.CreateInstance(typeof(T), jsonObject);
        }
    }
}