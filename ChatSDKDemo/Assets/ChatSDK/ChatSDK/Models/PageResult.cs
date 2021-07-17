using System.Collections.Generic;

namespace ChatSDK
{
    /// <summary>
    /// 分页类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageResult<T>
    {
        /// <summary>
        /// 页数
        /// </summary>
        public int PageCount { get; internal set; }

        /// <summary>
        /// 数据
        /// </summary>
        public List<T> Data { get; internal set; }
    }
}