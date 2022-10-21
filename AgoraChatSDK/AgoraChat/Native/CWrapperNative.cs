using System.Runtime.InteropServices;
using System.Text;
namespace AgoraChat
{

    public sealed class CWrapperNative
    {
        internal static void NativeCall(string manager, string method, SimpleJSON.JSONNode json, string callbackId = null) {
            _NativeCall(manager, method, json.ToString(), callbackId);
        }


        internal static string NativeGet(string manager, string method, SimpleJSON.JSONNode json, string callbackId = null)
        {
            StringBuilder sbuilder = new StringBuilder(512);
            _NativeGet(manager, method, json.ToString(), sbuilder, callbackId);
            return sbuilder.ToString();
        }


        [DllImport("ChatCWrapper")]
        internal static extern void AddListener(NativeListenerEvent listener);


        [DllImport("ChatCWrapper")]
        internal static extern void CleanListener();

        [DllImport("ChatCWrapper")]
        private extern static void _NativeCall(string manager, string method, string jsonString = null, string callbackId = null);


        [DllImport("ChatCWrapper")]
        private extern static int _NativeGet(string manager, string method, string jsonString = null, [Out, MarshalAs(UnmanagedType.LPTStr)] StringBuilder buf = null, string callbackId = null);
    }
}