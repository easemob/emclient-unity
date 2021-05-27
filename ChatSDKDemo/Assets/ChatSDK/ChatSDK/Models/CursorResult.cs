using System.Collections.Generic;
using System.Runtime.Serialization;
using System;

namespace ChatSDK
{

    public class CursorResult<T>
    {
        public string Cursor { get; internal set; }
        public List<T> Data { get; internal set; }
    }
}