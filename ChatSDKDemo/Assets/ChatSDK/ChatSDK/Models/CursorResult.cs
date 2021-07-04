using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;

namespace ChatSDK
{

    public class CursorResult<T>
    {
        public string Cursor { get; internal set; }
        public List<T> Data { get; internal set; }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CursorResultTO
    {
        public string NextPageCursor;
        public DataType Type;
        public int Size;
        public IntPtr SubTypes; //sub types if any
        public IntPtr Data; //list of data
    }
}