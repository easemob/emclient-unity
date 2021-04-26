using System;

namespace ChatSDK
{
    public class GroupOptions
    {
        public GroupStyle Style;
        public int MaxCount;
        public bool InviteNeedConfirm;
        public string Ext;

        public GroupOptions(GroupStyle style, int count = 200, bool inviteNeedConfirm = false, string ext = "") 
        {
            Style = style;
            MaxCount = count;
            InviteNeedConfirm = inviteNeedConfirm;
            Ext = ext;
        }
    }
}