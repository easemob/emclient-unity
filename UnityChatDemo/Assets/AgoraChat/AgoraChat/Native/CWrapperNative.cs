using System.Runtime.InteropServices;
using System.Text;
//using UnityEngine;

namespace AgoraChat
{


    public sealed class CWrapperNative
    {
        internal static void NativeCall(string manager, string method, SimpleJSON.JSONNode json, string callbackId = null)
        {
            _NativeCall(manager, method, json?.ToString(), callbackId ?? "");
        }


        internal static string NativeGet(string manager, string method, SimpleJSON.JSONNode json, string callbackId = null)
        {
            StringBuilder sbuilder = new StringBuilder(512);
            _NativeGet(manager, method, json?.ToString(), sbuilder, callbackId ?? "");
            return Tools.GetUnicodeStringFromUTF8(sbuilder.ToString());
        }

        #region DllImport
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        public const string MyLibName = "ChatCWrapper";
#else

#if UNITY_IPHONE
		public const string MyLibName = "__Internal";
#else
        public const string MyLibName = "ChatCWrapper";
#endif
#endif


        [DllImport(MyLibName)]
        internal static extern void Init(int type, NativeListenerEvent listener);

        [DllImport(MyLibName)]
        internal static extern void UnInit();

        [DllImport(MyLibName)]
        private extern static void _NativeCall(string manager, string method, [In, MarshalAs(UnmanagedType.LPTStr)] string jsonString = null, string callbackId = null);


        [DllImport(MyLibName)]
#if _WIN32
        private extern static int _NativeGet(string manager, string method, [In, MarshalAs(UnmanagedType.LPTStr)] string jsonString = null, [Out, MarshalAs(UnmanagedType.LPTStr)] StringBuilder buf = null, string callbackId = null);
#else
        private extern static int _NativeGet(string manager, string method, [In, MarshalAs(UnmanagedType.LPTStr)] string jsonString = null, StringBuilder buf = null, string callbackId = null);
#endif
        #endregion engine callbacks
    }
}