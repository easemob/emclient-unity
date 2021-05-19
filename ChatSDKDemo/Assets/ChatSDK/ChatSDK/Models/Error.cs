using System;

namespace ChatSDK {

    public class Error
    {

        public int Code { get; internal set; }
        public string Desc { get; internal set; }

        internal Error(int code, string desc)
        {
            Code = code;
            Desc = desc;
        }
    }

}
