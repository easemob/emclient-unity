using System;
using SimpleJSON;
using System.Runtime.InteropServices;

namespace ChatSDK {

    /**
     * \~chinese
     * 群组的基本信息, 用于定义公开群的信息。
     * 
     * 若要从服务器获取群组基本信息，可调用 {@link GetGroupSpecificationFromServer(string  ValueCallBack<Group>)} 。
     *
     * \~english
     * The class that defines basic information of public groups.
     * 
     * To get basic information of a group from the server, you can call {@link IGroupManager#FetchPublicGroupsFromServer(int, String, ValueCallBack)}.
     */
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class GroupInfo
    {
        /**
	     * \~chinese
	     * 群组 ID。
	     *
	     * \~english
	     * The group ID.
	     */
        public string GroupId { get; internal set; }

        /**
	     * \~chinese
	     * 群组名称。
	     *
	     * \~english
	     * The group name.
	     */
        public string GroupName { get; internal set; }

        internal GroupInfo(string jsonString) {
            if (jsonString != null) {
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull)
                {
                    GroupId = jn["groupId"].Value;
                    GroupName = jn["groupName"].Value;
                }
            }
        }

        internal GroupInfo()
        {

        }

    }

}

