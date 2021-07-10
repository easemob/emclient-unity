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