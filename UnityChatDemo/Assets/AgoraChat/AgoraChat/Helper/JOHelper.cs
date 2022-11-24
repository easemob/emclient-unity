using System;

namespace AgoraChat
{

    internal class JOHelper
    {

        SimpleJSON.JSONObject jo;

        internal JOHelper()
        {
            jo = new SimpleJSON.JSONObject();
        }

        internal void Add(string key, SimpleJSON.JSONNode jn)
        {
            jo.Add(key, jn);
        }

        internal SimpleJSON.JSONObject JsonObject
        {
            get
            {
                return jo;
            }
        }
    }
}