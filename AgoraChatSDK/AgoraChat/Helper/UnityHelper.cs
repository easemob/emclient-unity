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
    {
        private static UnityHelper instance;
        internal static UnityHelper Instance()
        {
            if (null == instance)
            {
                instance = new UnityHelper();
            }
            return instance;
        }
    }
#else
    internal class UnityHelper : MonoBehaviour
    {
        private static UnityHelper instance;
        private static string game_name = "UnityHelper";

        internal static UnityHelper Instance()
        {
            if (null == instance)
            {
                GameObject callback_gameobj = new GameObject(game_name);
                DontDestroyOnLoad(callback_gameobj);
                instance = callback_gameobj.AddComponent<UnityHelper>();
            }
            return instance;
        }

        void Update()
        {
            CallbackQueue_UnityMode.Instance().Process();
        }

        private void OnApplicationQuit()
        {
            if (IClient.IsInit)
            {
                if (SDKClient.Instance.IsLoggedIn)
                {
                    SDKClient.Instance.Logout(false);
                }
                SDKClient.Instance.ClearResource();
            }
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
            if (IClient.IsInit)
            {
                if (SDKClient.Instance.IsLoggedIn)
                {
                    SDKClient.Instance.Logout(false);
                }
                SDKClient.Instance.ClearResource();
            }
            Debug.Log("Quit...");
            return true;
        }

        static void OnPlayModeStateChanged(PlayModeStateChange stateChange)
        {
            switch (stateChange)
            {
                case (PlayModeStateChange.EnteredPlayMode):
                    {
                        Instance();
                        EditorApplication.LockReloadAssemblies();
                        Debug.Log("Assembly Reload locked as entering play mode");
                        break;
                    }
                case (PlayModeStateChange.ExitingPlayMode):
                    {
                        instance = null;
                        Debug.Log("Assembly Reload unlocked as exiting play mode");
                        EditorApplication.UnlockReloadAssemblies();
                        break;
                    }
            }
        }
#endif
    }
#endif

#if _WIN32
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class PreserveAttribute : Attribute
    {
        // Empty attribute
    }
#endif
}
