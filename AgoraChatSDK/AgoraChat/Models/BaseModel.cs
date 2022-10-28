using AgoraChat.SimpleJSON;

namespace AgoraChat
{
    public abstract class BaseModel
    {
        internal BaseModel() { }

        internal BaseModel(string json)
        {
            if (json.Length > 0)
            {
                JSONNode jn = JSON.Parse(json);
                if (null != jn && jn.IsObject)
                {
                    FromJsonObject(jn.AsObject);
                }
            }
        }

        internal BaseModel(JSONObject jo)
        {
            FromJsonObject(jo);
        }

        internal abstract void FromJsonObject(JSONObject jo);
        internal abstract JSONObject ToJsonObject();
    }
}