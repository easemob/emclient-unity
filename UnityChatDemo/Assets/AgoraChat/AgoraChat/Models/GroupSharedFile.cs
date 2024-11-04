using AgoraChat.SimpleJSON;
#if !_WIN32
using UnityEngine.Scripting;
#endif

namespace AgoraChat
{
    /**
    * \~chinese
    * 共享文件信息类。
    * 
    * 例如：通过 {@link IGroupManager#GetGroupFileListFromServer(String, int, int, ValueCallBack)} 接口获取群共享文件的相关信息。
    *
    * \~english
    * The shared file information class, which defines how to manage shared files.
    * 
    * For example, you can get information about a group shared file by using {@link IGroupManager#GetGroupFileListFromServer(String, int, int, ValueCallBack)}.
    * 
    */
    [Preserve]
    public class GroupSharedFile : BaseModel
    {
        /**
         * \~chinese
         * 共享文件的名称。
         *
         * \~english
         * The name of the shared file.
         */
        public string FileName { get; internal set; }

        /**
         * \~chinese
         * 共享文件的 ID。
         *
         * \~english
         * The ID of the shared file.
         */
        public string FileId { get; internal set; }

        /**
         * \~chinese
         * 上传共享文件的成员的用户 ID。
         *
         * \~english
         * The user ID of the member who uploads the shared file.
         */
        public string FileOwner { get; internal set; }

        /**
         * \~chinese
         * 共享文件更新的 Unix 时间戳，单位为毫秒。
         *
         * \~english
         * The Unix timestamp for updating the shared file. The unit is millisecond.
         */
        public long CreateTime { get; internal set; }

        /**
         * \~chinese
         * 共享文件的大小，单位为字节。
         *
         * \~english
         * The size of the shared file, in bytes.
         */
        public long FileSize { get; internal set; }

        [Preserve]
        internal GroupSharedFile() { }

        [Preserve]
        internal GroupSharedFile(string jsonString) : base(jsonString) { }

        [Preserve]
        internal GroupSharedFile(JSONObject jsonObject) : base(jsonObject) { }

        internal override void FromJsonObject(JSONObject jsonObject)
        {
            FileName = jsonObject["name"];
            FileId = jsonObject["fileId"];
            FileOwner = jsonObject["owner"];
            CreateTime = (long)jsonObject["createTime"].AsDouble;
            FileSize = (long)jsonObject["fileSize"].AsDouble;
        }

        internal override JSONObject ToJsonObject()
        {
            JSONObject jo = new JSONObject();
            jo.AddWithoutNull("name", FileName);
            jo.AddWithoutNull("fileId", FileId);
            jo.AddWithoutNull("owner", FileOwner);
            jo.AddWithoutNull("createTime", CreateTime);
            jo.AddWithoutNull("fileSize", FileSize);
            return jo;
        }
    }
}