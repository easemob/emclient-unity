using System;
using System.Collections.Generic;
using SimpleJSON;
using System.Runtime.InteropServices;
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace ChatSDK {
    /**
     * \~chinese
     * 推送配置类。
     * \~english
     * The push configuration class.
     */
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class PushConfig
    {
        /**
         * \~chinese
         * 是否开启离线推送的免打扰模式。
         * `true`: 开启；
         * `false`：关闭。
         * 
         * \~english
         * Whether to enable the do-not-disturb mode for the offline message push.
         * `true`: Yes.
         * `false`: No.
         */
        [MarshalAs(UnmanagedType.U1)]
        public bool NoDisturb;
        //public bool NoDisturb { get; internal set; }

        /**
         * \~chinese
         * 离线推送免打扰的开始时间。
         * 该时间为 24 小时制，取值范围为 [0,24]。
         *
         * \~english
         * The start hour of the do-not-disturb mode for the offline message push.
         * The time is based on a 24-hour clock. The value range is [0,24].
         */
        public int NoDisturbStartHour { get; internal set; }

        /**
         * \~chinese
         * 离线推送免打扰的结束时间。
         * 该时间为 24 小时制，取值范围为 [0,24]。
         *
         * \~english
         * The end hour of the do-not-disturb mode for the offline message push.
         * The time is based on a 24-hour clock. The value range is [0,24].
         * 
         */
        public int NoDisturbEndHour { get; internal set; }

        /**
         * \~chinese
         * 推送通知的展示方式。
         *
         * \~english
         * The display style of push notifications.
         * 
         */
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
        /**
         * \~chinese
         * 获取字符串形式的推送配置信息。
         *
         * \~english
         * Gets the push configurations in the string format.
         * 
         */
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


    /**
     * \~chinese
     * 设置离线推送免打扰时间段。
     *
     * \~english
     * Sets the time frame of the do-not-disturb mode for offline push notifications.
     *
     */
    public class SilentModeTime
    {
        /**
        * \~chinese
        * 设置免打扰时段的小时时间点。
        *
        * \~english
        * Sets the hour point in time frame of the do-not-disturb mode for offline push notifications.
        *
        */
        public int hours;

        /**
        * \~chinese
        * 设置免打扰时段的分钟时间点。
        *
        * \~english
        * Sets the minute point in time frame of the do-not-disturb mode for offline push notifications.
        *
        */
        public int minutes;
    }

    public class SilentModeParam
    {
        /**
        * \~chinese
        * 离线推送免打扰参数类型枚举类。
        *
        * \~english
        * The parameter types of the do-not-disturb mode for the offline message push.
        */
        public SilentModeParamType ParamType;

        /**
        * \~chinese
        * 设置离线推送免打扰时长。
        *
        * @param silentDuration 离线推送免打扰时长，单位为分钟。
        *
        * \~english
        * Sets the duration of the do-not-disturb mode for offline push notifications.
        *
        * @param silentDuration The duration of the do-not-disturb mode, in minutes.
        *
        */
        public int SilentModeDuration;

        /**
        * \~chinese
        * 离线消息推送方式。
        *
        * \~english
        * The push notification modes.
        */
        public PushRemindType RemindType;

        /**
        * \~chinese
        * 设置离线推送免打扰时段的开始时间。
        *
        * \~english
        * Sets the start point in the do-not-disturb time frame for the offline message push.
        */
        public SilentModeTime SilentModeStartTime;

        /**
        * \~chinese
        * 设置离线推送免打扰时段的结束时间。
        *
        * \~english
        * Sets the end point in the do-not-disturb time frame for the offline message push.
        *
        */
        public SilentModeTime SilentModeEndTime;

        internal string ToJson()
        {
            JSONObject jo = new JSONObject();
            jo.Add("paramType", (int)ParamType);
            if(SilentModeParamType.RemindType == ParamType)
            {
                jo.Add("type", (int)RemindType);
            } 
            else if(SilentModeParamType.Duration == ParamType)
            {
                jo.Add("duration", SilentModeDuration);
            } 
            else if (SilentModeParamType.Interval == ParamType)
            {
                if (null != SilentModeStartTime)
                {
                    jo.Add("startHour", SilentModeStartTime.hours);
                    jo.Add("startMin", SilentModeStartTime.minutes);
                } 
                else
                {
                    jo.Add("startHour", 0);
                    jo.Add("startMin", 0);
                }
                if (null != SilentModeEndTime)
                {
                    jo.Add("endHour", SilentModeEndTime.hours);
                    jo.Add("endMin", SilentModeEndTime.minutes);
                }
                else
                {
                    jo.Add("endHour", 0);
                    jo.Add("endMin", 0);
                }
            }
            return jo.ToString();
        }
    }

    public class SilentModeItem
    {

	    /**
	     * \~chinese
	     * 获取离线推送免打扰模式结束时间的 Unix 时间戳，单位为毫秒。
	     *
	     * \~english
	     * Gets the Unix timestamp when the do-not-disturb mode of the offline message push expires, in milliseconds.
	     *
	     */
        public long ExpireTimestamp;

		 /**
	     * \~chinese
	     * 离线消息推送方式。
	     *
	     * \~english
	     * The push notification modes.
	     */
        public PushRemindType RemindType;

	    /**
	     * \~chinese
	     * 获取离线推送免打扰时段的开始时间。
	     *
	     * \~english
	     * Gets the start point in the do-not-disturb time frame for the offline message push.
	     */
        public SilentModeTime SilentModeStartTime;
	    /**
	     * \~chinese
	     * 获取离线推送免打扰时段的结束时间。
	     *
	     * \~english
	     * Gets the end point in the do-not-disturb time frame for the offline message push.
	     *
	     */
        public SilentModeTime SilentModeEndTime;

	    /**
	     * \~chinese
	     * 获取会话 ID。
	     *
	     * \~english
	     * Gets the conversation ID.
	     */
        public string ConvId;

	    /**
	     * \~chinese
	     * 获取会话类型。
	     *
	     * \~english
	     * Gets the conversation type.
	     */
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

