using AgoraChat.SimpleJSON;

namespace AgoraChat
{
    public class Message : BaseModel
    {
        internal Message(string jsonString) : base(jsonString) { }

        internal Message(JSONObject jsonObject) : base(jsonObject) { }

        internal override void FromJsonObject(JSONObject jsonObject)
        {

        }
    }
}