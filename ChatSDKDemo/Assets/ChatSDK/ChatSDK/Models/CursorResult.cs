using System.Collections.Generic;
using System.Runtime.Serialization;
using System;

namespace ChatSDK
{
    public class CursorResult<T>
    {

        static internal CursorResult<T> FromJsonString(string jsonString)
        {
            CursorResult<T> cr = new CursorResult<T>();

            return cr;
        }

        public string Cursor { get; private set; }
        public List<T> Data { get; private set; }

        internal CursorResult()
        {

        }
    }
}