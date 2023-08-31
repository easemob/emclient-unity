using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AgoraChat
{
    internal class Tools
    {
        private static int MIN_RANDOM_SEED = 1;
        private static int MAX_RANDOM_SEED = 10000;
        private static int random_seed = MIN_RANDOM_SEED;
        private static bool debugMode = false;

        internal static int GetRandom()
        {
            Random random = new Random(random_seed);

            random_seed++;
            if (random_seed > MAX_RANDOM_SEED) random_seed = MIN_RANDOM_SEED;

            return random.Next();
        }

        internal static string GetUnicodeStringFromUTF8(string utf8Str)
        {
#if _WIN32
            if (utf8Str.Length == 0) return utf8Str;

            string ret = Encoding.UTF8.GetString(Encoding.Unicode.GetBytes(utf8Str));
            int index = ret.IndexOf('\0');
            if (index > 0)
                ret = ret.Substring(0, index);
#else
            string ret = utf8Str;
#endif
            return ret;
        }

        internal static string PtrToString(IntPtr ptr)
        {
            string ret = "";

            if(null != ptr && IntPtr.Zero != ptr)
            {
#if _WIN32
                ret = Marshal.PtrToStringUni(ptr);
#else
                ret = Marshal.PtrToStringAnsi(ptr);
#endif
            }

            return ret;
        }

        internal static void SetDebugMode(bool mode)
        {
            debugMode = mode;
        }

        internal static void LogDebug(string info)
        {
            if (debugMode) CWrapperNative.LogNativeCall(SDKMethod.client, SDKMethod.logDebug, info);
        }

        internal static void LogWarn(string info)
        {
            if (debugMode) CWrapperNative.LogNativeCall(SDKMethod.client, SDKMethod.logWarn, info);
        }

        internal static void LogError(string info)
        {
            CWrapperNative.LogNativeCall(SDKMethod.client, SDKMethod.logError, info);
        }
    }
}
