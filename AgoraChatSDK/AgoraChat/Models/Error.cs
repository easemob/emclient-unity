#if !_WIN32
using UnityEngine.Scripting;
#endif

namespace AgoraChat
{
    /**
    * \~chinese
    * 错误信息类。
    *
    * \~english
    * The error information class.
    */
    [Preserve]
    public class Error
    {
        /**
         * \~chinese
         * 错误码。
         *
         * \~english
         * The error code.
         */
        public int Code { get; internal set; }

        /**
         * \~chinese
         * 错误描述。
         *
         * \~english
         * The error description.
         */
        public string Desc { get; internal set; }

        [Preserve]
        internal Error(int code, string desc)
        {
            Code = code;
            Desc = desc;
        }
    }
}