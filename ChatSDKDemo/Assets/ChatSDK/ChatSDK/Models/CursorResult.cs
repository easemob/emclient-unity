using System.Collections.Generic;
using System.Runtime.Serialization;
using System;

namespace ChatSDK
{
    /// <summary>
    /// SDK游标
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CursorResult<T>
    {
        /// <summary>
        /// 游标
        /// </summary>
        public string Cursor { get; internal set; }

        /// <summary>
        /// 列表
        /// </summary>
        public List<T> Data { get; internal set; }
    }
}