﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSDK
{
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE
#else
    class Debug
    {
        internal static void Log(string str)
        {

        }

        internal static void LogError(string str)
        {

        }

        internal static void LogWarning(string str)
        {

        }
    }
#endif
}
