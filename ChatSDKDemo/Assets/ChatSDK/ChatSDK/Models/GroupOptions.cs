﻿using System.Runtime.InteropServices;
using SimpleJSON;

namespace ChatSDK
{
    /// <summary>
    /// 群配置信息
    /// </summary>

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class GroupOptions
    {
        /// <summary>
        /// 群类型
        /// </summary>
        public GroupStyle Style;

        /// <summary>
        /// 群人数上限
        /// </summary>
        public int MaxCount;

        /// <summary>
        /// 加群是否需要验证
        /// </summary>
        public bool InviteNeedConfirm;

        /// <summary>
        /// 群扩展
        /// </summary>
        public string Ext;

        //public GroupOptions(string jsonString)
        internal GroupOptions(string jsonString)
        {
            if (jsonString != null)
            {
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull && jn.IsObject)
                {
                    JSONObject jo = jn.AsObject;
                    int style = jo["style"].AsInt;
                    if (style == 0)
                    {
                        Style = GroupStyle.PrivateOnlyOwnerInvite;
                    }
                    else if (style == 1)
                    {
                        Style = GroupStyle.PrivateMemberCanInvite;
                    }
                    else if (style == 2)
                    {
                        Style = GroupStyle.PublicJoinNeedApproval;
                    }
                    else if (style == 3)
                    {
                        Style = GroupStyle.PublicOpenJoin;
                    }
                    MaxCount = jo["maxCount"].AsInt;
                    InviteNeedConfirm = jo["inviteNeedConfirm"].AsBool;
                    Ext = jo["ext"].Value;
                }
            }
        }


        internal string ToJsonString() {
            JSONObject jsonObject = new JSONObject();
            jsonObject.Add("style", StyleToInt(Style));
            jsonObject.Add("maxCount", MaxCount);
            jsonObject.Add("inviteNeedConfirm", InviteNeedConfirm);
            jsonObject.Add("ext", Ext);
            return jsonObject.ToString();
        }

        public GroupOptions(GroupStyle style, int count = 200, bool inviteNeedConfirm = false, string ext = "") 
        {
            Style = style;
            MaxCount = count;
            InviteNeedConfirm = inviteNeedConfirm;
            Ext = ext;
        }

        private int StyleToInt(GroupStyle style) {
            int ret = 0;
            switch (style) {
                case GroupStyle.PrivateOnlyOwnerInvite:
                    ret = 0;
                    break;
                case GroupStyle.PrivateMemberCanInvite:
                    ret = 1;
                    break;
                case GroupStyle.PublicJoinNeedApproval:
                    ret = 2;
                    break;
                case GroupStyle.PublicOpenJoin:
                    ret = 3;
                    break;
            }
            return ret;
        }
    }
}