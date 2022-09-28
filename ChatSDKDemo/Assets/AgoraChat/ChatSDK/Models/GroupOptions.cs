using System.Runtime.InteropServices;
using SimpleJSON;

namespace AgoraChat
{
    /**
     * \~chinese
     * 群组属性类，这些属性在创建群组时配置。
     *
     * \~english
     * The class that contains options to be configured during group creation.
     */

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class GroupOptions
    {
        /**
         * \~chinese
         * 群组类型，详见 {@link GroupStyle}。
         *
         * \~english
         * The group style. See {@link GroupStyle}.
         */
        public GroupStyle Style;

        /**
         * \~chinese
         * 群组的最大成员数。
         *
         * \~english
         * The maximum number of members allowed in a group.
         */
        public int MaxCount;

        /**
         * \~chinese
         * 邀请用户进群是否需要对方同意。
         *
         * 收到邀请是否自动入群取决于两个设置：创建群组时设置 `inviteNeedConfirm` 以及通过 {@link Options#AutoAcceptGroupInvitation} 确定是否自动接受加群邀请。
         * 具体使用如下：
         * - 如果 `inviteNeedConfirm` 设置为 `false`，在服务端直接加受邀人进群，与受邀人对 {@link Options#AutoAcceptGroupInvitation} 的设置无关。
         * - 如果 `inviteNeedConfirm` 设置为 `true`，是否自动入群取决于被邀请人对 {@link Options#AutoAcceptGroupInvitation} 的设置。
         *
         * {@link Options#AutoAcceptGroupInvitation} 为 SDK 级别操作，设置为 `true` 时，受邀人收到入群邀请后，SDK 在内部调用同意入群的 API，自动接受邀请入群；
         * 若设置为 `false`，即非自动同意其邀请，用户可以选择接受或拒绝入群邀请。
         *
         * \~english
         * Whether to ask for consent when inviting a user to join a group.
         * 
         * Whether automatically accepting the invitation to join a group depends on two settings: 
         * - `inviteNeedConfirm`, an option for group creation.
         * - {@link Options#AutoAcceptGroupInvitation} which determines whether to automatically accept an invitation to join the group.
         * 
         * There are two cases:
         * - If `inviteNeedConfirm` is set to `false`, the SDK adds the invitee directly to the group on the server side, regardless of the setting of {@link Options#AutoAcceptGroupInvitation} on the invitee side.
         * - If `inviteNeedConfirm` is set to `true`, whether the invitee automatically joins the chat group or not depends on the settings of {@link Options#AutoAcceptGroupInvitation}.
         * 
         * {@link Options#AutoAcceptGroupInvitation} is an SDK-level operation. If it is set to `true`, the invitee automatically joins the chat group; if it is set to `false`, the invitee can manually accept or decline the group invitation instead of joining the group automatically.
         */
        public bool InviteNeedConfirm;

        /**
         * \~chinese
         * 群组详情扩展，可以采用 JSON 格式，以包含更多群信息。
         *
         * \~english
         * The group detail extensions which can be in the JSON format to contain more group information.
         */
        public string Ext;

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

         /**
         * \~chinese
         * 群组选项类的构造方法。
         *
         * \~english
         * The group option class constructor.
         */
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