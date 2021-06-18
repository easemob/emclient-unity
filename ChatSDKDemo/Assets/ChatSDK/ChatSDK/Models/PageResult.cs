using System.Collections.Generic;

namespace ChatSDK
{
    public class PageResult<T>
    {
        public int PageCount { get; internal set; }
        public List<T> Data { get; internal set; }
    }
}