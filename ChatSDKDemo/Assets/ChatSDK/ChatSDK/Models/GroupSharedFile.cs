using System;
using SimpleJSON;

namespace ChatSDK
{
    /// <summary>
    /// 群共享文件
    /// </summary>
    public class GroupSharedFile
    {

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; internal set; }

        /// <summary>
        /// 文件id
        /// </summary>
        public string FileId { get; internal set; }

        /// <summary>
        /// 上传用户
        /// </summary>
        public string FileOwner { get; internal set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public long CreateTime { get; internal set; }

        /// <summary>
        /// 文件大小
        /// </summary>
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
    }

}