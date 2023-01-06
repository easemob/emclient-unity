using System;

#if !_WIN32
using UnityEngine;
#endif

namespace AgoraChat
{
    public class LogPrinter
    {
        public static void Log(object message)
        {
#if !_WIN32
            //Debug.Log("UNITYSDK: " + message);
#endif
        }
    }
}
