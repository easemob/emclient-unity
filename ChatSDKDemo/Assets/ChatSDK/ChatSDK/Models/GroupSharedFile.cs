using System;

namespace ChatSDK
{
    public class GroupSharedFile
    {

        public string FileName { get; internal set; }
        public string FileId { get; internal set; }
        public string FileOwner { get; internal set; }
        public long CreateTime { get; internal set; }
        public long FileSize { get; internal set; }

        public GroupSharedFile()
        {

        }
    }

}