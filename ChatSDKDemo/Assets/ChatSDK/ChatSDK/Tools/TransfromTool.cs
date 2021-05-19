using System;
using System.Collections.Generic;
using SimpleJSON;

namespace ChatSDK {
    public class TransfromTool
    {
        static internal List<string> JsonStringToListString(string jsonString) {
            List<string> ret = new List<string>();
            if (jsonString != null) {
                JSONNode jsonArray = JSON.Parse(jsonString);
                if (jsonArray.IsArray)
                {
                    foreach (JSONNode v in jsonArray.AsArray)
                    {
                        ret.Add(v.Value);
                    }
                }
            }
            return ret;
        }
    }

}