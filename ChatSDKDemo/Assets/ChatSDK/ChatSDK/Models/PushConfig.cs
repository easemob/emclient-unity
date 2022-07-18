using System;
using System.Collections.Generic;
using SimpleJSON;
using System.Runtime.InteropServices;
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace ChatSDK {
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class PushConfig
    {
        /// <summary>
        /// 免打扰
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool NoDisturb;
        //public bool NoDisturb { get; internal set; }

        /// <summary>
        /// 免打扰开始时间
        /// </summary>
        public int NoDisturbStartHour { get; internal set; }

        /// <summary>
        /// 免打扰结束时间
        /// </summary>
        public int NoDisturbEndHour { get; internal set; }

        /// <summary>
        /// 推送显示类型
        /// </summary>
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

        public override string ToString()
        {
            string style = Style == PushStyle.Simple ? "Simple" : "Summary";
            return $"NoDisturb: {NoDisturb}," +
                $"NoDisturbStartHour: {NoDisturbStartHour}, " +
                $"NoDisturbEndHour: {NoDisturbEndHour}, Style: " + style;
                ;  
        }

        internal PushConfig()
        {

        }

        internal PushConfig(PushConfig pg)
        {
            NoDisturb = pg.NoDisturb;
            NoDisturbStartHour = pg.NoDisturbStartHour;
            NoDisturbEndHour = pg.NoDisturbEndHour;
            Style = pg.Style;
        }
    }


    public class SilentModeTime
    {
        public int hours;
        public int minutes;
    }

    public class SilentModeParam
    {
        public SilentModeParamType ParamType;
        public int SilentModeDuration;
        public PushRemindType RemindType;
        public SilentModeTime SilentModeStartTime;
        public SilentModeTime SilentModeEndTime;

        internal string ToJson()
        {
            JSONObject jo = new JSONObject();
            jo.Add("paramType", (int)ParamType);
            jo.Add("duration",  SilentModeDuration);
            jo.Add("type", (int)RemindType);
            jo.Add("startHour", SilentModeStartTime.hours);
            jo.Add("startMin", SilentModeStartTime.minutes);
            jo.Add("endHour", SilentModeEndTime.hours);
            jo.Add("endMin", SilentModeEndTime.minutes);
            return jo.ToString();
        }
    }

    public class SilentModeItem
    {
        public long ExpireTimestamp;
        public PushRemindType RemindType;
        public SilentModeTime SilentModeStartTime;
        public SilentModeTime SilentModeEndTime;
        public string ConvId;
        public ConversationType ConvType;

        static internal SilentModeItem FromJsonObject(JSONNode jn)
        {
            SilentModeItem ret = null;
            if (!jn.IsNull && jn.IsObject)
            {
                ret = new SilentModeItem();
                ret.SilentModeStartTime = new SilentModeTime();
                ret.SilentModeEndTime = new SilentModeTime();
                JSONObject jo = jn.AsObject;
                if (!jo["expireTS"].IsNull)         ret.ExpireTimestamp = long.Parse(jo["expireTS"].Value);
                if (!jo["type"].IsNull)             ret.RemindType = (PushRemindType)jo["type"].AsInt;
                if (!jo["startHour"].IsNull)        ret.SilentModeStartTime.hours = jo["startHour"].AsInt;
                if (!jo["startMin"].IsNull)         ret.SilentModeStartTime.minutes = jo["startMin"].AsInt;
                if (!jo["endHour"].IsNull)          ret.SilentModeEndTime.hours = jo["endHour"].AsInt;
                if (!jo["endMin"].IsNull)           ret.SilentModeEndTime.minutes = jo["endMin"].AsInt;
                if (!jo["conversationId"].IsNull)   ret.ConvId = jo["conversationId"].Value;
                if (!jo["conversationType"].IsNull) ret.ConvType = (ConversationType)jo["conversationType"].AsInt;
            }
            return ret;
        }

        static internal SilentModeItem FromJson(string json)
        {
            if (null == json || json.Length == 0) return null;

            Debug.Log($"FromJson json : {json}");
            SilentModeItem ret = null;
            if (null != json && json.Length > 0)
            {
                JSONNode jn = JSON.Parse(json);
                ret = FromJsonObject(jn);
            }
            return ret;
        }

        static internal Dictionary<string, SilentModeItem> MapFromJson(string json)
        {
            Dictionary<string, SilentModeItem> dict = new Dictionary<string, SilentModeItem>();
            if (null == json || json.Length == 0) return dict;
            Debug.Log($"FromJson json : {json}");

            if (null != json && json.Length > 0)
            {
                JSONNode jn = JSON.Parse(json);
                if (null == jn) return dict;

                JSONObject jo = jn.AsObject;

                foreach (string s in jo.Keys)
                {
                    SilentModeItem item = FromJsonObject(jo[s]);
                    if (null != item)
                        dict.Add(s, item);
                }
            }
            return dict;
        }
    }
}

