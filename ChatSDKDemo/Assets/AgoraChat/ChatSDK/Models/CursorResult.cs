using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;

namespace AgoraChat
{
    /**
     * \~chinese
     * 包含游标及分页结果的泛型类。
     * 
     * 做为分页获取数据的返回对象。
     * 
     * \~english
     * The generic class which contains the cursor and pagination result.
     * 
     * The class instance is returned when you make a paginated query.
     * 
     */
    public class CursorResult<T>
    {
        /**
	     * \~chinese
	     * 游标。
	     *
	     * \~english
	     * The cursor.
	     */
        public string Cursor { get; internal set; }

        /**
        * \~chinese
        * 数据列表。
        *
        * \~english
        * The data list.
        */
        public List<T> Data { get; internal set; }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CursorResultTO
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