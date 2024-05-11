using AgoraChat.SimpleJSON;
#if !_WIN32
using UnityEngine.Scripting;
#endif

namespace AgoraChat
{
    [Preserve]
    public abstract class BaseModel
    {
        [Preserve]
        internal BaseModel() { }

        [Preserve]
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

        [Preserve]
        internal BaseModel(JSONObject jo)
        {
            FromJsonObject(jo);
        }

        internal abstract void FromJsonObject(JSONObject jo);
        internal abstract JSONObject ToJsonObject();
    }
}