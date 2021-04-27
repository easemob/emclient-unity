using System;

namespace ChatSDK
{
    public class GroupSharedFile
    {

        internal string _FileName, _FileId, _FileOwner;
        internal long _CreateTime, _FileSize;

        public string FileName { get { return _FileName; } }
        public string FileId { get { return _FileId; } }
        public string FileOwner { get { return _FileOwner; } }
        public long CreateTime { get { return _CreateTime; } }
        public long FileSize { get { return _FileSize; } }

        public GroupSharedFile()
        {

        }
    }

}