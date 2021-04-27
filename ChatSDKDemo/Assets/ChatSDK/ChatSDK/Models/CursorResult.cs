using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ChatSDK
{
    [DataContract]
    public class CursorResult<T>
    {

        internal List<T> _Data;
        internal string _Cursor;


        internal string ToJson() { return ""; }

        public string Cursor { get { return _Cursor; } }
        public List<T> Data { get { return _Data; } }

        public CursorResult()
        {
            
        }
    }
}