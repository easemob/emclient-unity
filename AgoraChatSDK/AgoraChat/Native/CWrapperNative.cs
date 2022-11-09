using System;
using System.Runtime.InteropServices;
using System.Text;
//using UnityEngine;

namespace AgoraChat
{

    public sealed class CWrapperNative
    {
        internal static void NativeCall(string manager, string method, SimpleJSON.JSONNode json, string callbackId = null)
        {
            //Debug.Log($"NativeCall: {manager}, {method}, {json}, {callbackId}");
            _NativeCall(manager, method, json?.ToString(), callbackId ?? "");
        }


        internal static string NativeGet(string manager, string method, SimpleJSON.JSONNode json, string callbackId = null)
        {
            StringBuilder sbuilder = new StringBuilder(512);
            _NativeGet(manager, method, json?.ToString(), sbuilder, callbackId ?? "");
            string ret = Tools.GetUnicodeStringFromUTF8(sbuilder.ToString());
            return ret;
        }


        [DllImport("ChatCWrapper")]
        internal static extern void Init(int type, NativeListenerEvent listener);


        [DllImport("ChatCWrapper")]
        internal static extern void UnInit();

        [DllImport("ChatCWrapper")]
        private extern static void _NativeCall(string manager, string method, [In, MarshalAs(UnmanagedType.LPTStr)] string jsonString = null, string callbackId = null);


        [DllImport("ChatCWrapper")]
        private extern static int _NativeGet(string manager, string method, [In, MarshalAs(UnmanagedType.LPTStr)] string jsonString = null, [Out, MarshalAs(UnmanagedType.LPTStr)] StringBuilder buf = null, string callbackId = null);
    }
}