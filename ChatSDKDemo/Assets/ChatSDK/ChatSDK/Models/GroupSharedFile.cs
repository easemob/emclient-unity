using System;
using SimpleJSON;

namespace ChatSDK
{
    public class GroupSharedFile
    {

        public string FileName { get; internal set; }
        public string FileId { get; internal set; }
        public string FileOwner { get; internal set; }
        public long CreateTime { get; internal set; }
        public long FileSize { get; internal set; }

        internal GroupSharedFile(JSONNode jo)
        {
            this.FileName = jo["name"].Value;
            this.FileId = jo["fileId"].Value;
            this.FileOwner = jo["owner"].Value;
            this.CreateTime = jo["createTime"].AsInt;
            this.FileSize = jo["fileSize"].AsInt;
        }
    }

}