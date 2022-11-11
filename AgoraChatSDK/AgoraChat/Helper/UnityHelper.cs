using System;

#if _WIN32
#else
using UnityEngine;
using UnityEditor;
#endif

namespace AgoraChat
{
#if _WIN32
    internal class UnityHelper
#else
    internal class UnityHelper : MonoBehaviour
#endif
    {
        public UnityHelper()
        {
        }

#if UNITY_EDITOR

        [RuntimeInitializeOnLoadMethod]
        static void InitializeOnLoadMethod()
        {
            EditorApplication.wantsToQuit -= Quit;
            EditorApplication.wantsToQuit += Quit;

            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        static bool Quit()
        {
            if (SDKClient.Instance.IsLoggedIn)
            {
                SDKClient.Instance.Logout(false);
            }
            SDKClient.Instance.ClearResource();
            Debug.Log("Quit...");
            return true;
        }

        static void OnPlayModeStateChanged(PlayModeStateChange stateChange)
        {
            switch (stateChange)
            {
                case (PlayModeStateChange.EnteredPlayMode):
                    {
                        EditorApplication.LockReloadAssemblies();
                        Debug.Log("Assembly Reload locked as entering play mode");
                        break;
                    }
                case (PlayModeStateChange.ExitingPlayMode):
                    {
                        Debug.Log("Assembly Reload unlocked as exiting play mode");
                        EditorApplication.UnlockReloadAssemblies();
                        break;
                    }
            }
        }

#endif
    }
}
