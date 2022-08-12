﻿using System.Runtime.InteropServices;
using SimpleJSON;

namespace ChatSDK
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
    [StructLayout(LayoutKind.Sequential)]
    public class GroupSharedFile
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

        internal GroupSharedFile(string jsonString)
        {
            if (jsonString != null) {
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull && jn.IsObject)
                {
                    JSONObject jo = jn.AsObject;
                    FileName = jo["name"].Value;
                    FileId = jo["fileId"].Value;
                    FileOwner = jo["owner"].Value;
                    CreateTime = jo["createTime"].AsInt;
                    FileSize = jo["fileSize"].AsInt;
                }
            }            
        }

        internal GroupSharedFile()
        {

        }
    }

}