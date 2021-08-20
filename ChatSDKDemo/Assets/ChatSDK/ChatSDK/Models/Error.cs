using System;

namespace ChatSDK {

    /// <summary>
    /// 错误信息
    /// </summary>
    public class Error
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int Code { get; internal set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        public string Desc { get; internal set; }

        internal Error(int code, string desc)
        {
            Code = code;
            Desc = desc;
        }
    }

}
