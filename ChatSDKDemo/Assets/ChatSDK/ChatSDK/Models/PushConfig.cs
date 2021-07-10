using System;
using SimpleJSON;

namespace ChatSDK {
    public class PushConfig
    {

        public bool NoDisturb { get; internal set; }
        public int NoDisturbStartHour { get; internal set; }
        public int NoDisturbEndHour { get; internal set; }
        public PushStyle Style { get; internal set; }

        internal PushConfig(string jsonString)
        {
            if (jsonString != null)
            {
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull && jn.IsObject)
                {
                    JSONObject jo = jn.AsObject;
                    NoDisturb = jo["noDisturb"].AsBool;
                    NoDisturbStartHour = jo["noDisturbStartHour"].AsInt;
                    NoDisturbEndHour = jo["noDisturbEndHour"].AsInt;
                    NoDisturb = jo["noDisturb"].AsBool;
                    if (jo["pushStyle"].AsInt == 0)
                    {
                        Style = PushStyle.Simple;
                    }
                    else
                    {
                        Style = PushStyle.Summary;
                    }
                }
            }
        }
    }

}

