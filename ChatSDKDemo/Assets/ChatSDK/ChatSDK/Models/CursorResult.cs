using System.Collections.Generic;
using System.Runtime.InteropServices;
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

    [StructLayout(LayoutKind.Sequential)]
    public struct CursorResultTO
    {
        public string NextPageCursor;
        public DataType Type;
        public int Size;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
        public MessageBodyType[] SubTypes; //sub types if any
        [MarshalAs(UnmanagedType.ByValArray,SizeConst = 200)]
        public IntPtr[] Data; //list of data
    }
}