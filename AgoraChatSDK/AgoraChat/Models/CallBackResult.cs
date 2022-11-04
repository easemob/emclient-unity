using System;
using AgoraChat.SimpleJSON;

namespace AgoraChat
{
    public class CallBackResult : BaseModel
    {
        public CallBackResult()
        {
        }

        internal override void FromJsonObject(JSONObject jo)
        {
            throw new NotImplementedException();
        }

        internal override JSONObject ToJsonObject()
        {
            throw new NotImplementedException();
        }
    }
}
