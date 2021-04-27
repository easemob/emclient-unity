using System.Collections.Generic;

namespace ChatSDK
{
    public class PageResult<T>
    {
        internal List<T> _Data;
        internal int _PageCount;

        public int PageCount { get { return _PageCount; } }
        public List<T> Data { get { return _Data; } }

        public PageResult()
        {

        }
    }
}