#if UNITY_EDITOR
#if UNITY_IPHONE 

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;
using UnityEngine;

namespace AgoraChat {
    

    
    public class BL_BuildPostProcess
    {
        const string defaultLocationInProj = "AgoraChat/Plugins/iOS";

        [PostProcessBuildAttribute(99)]
        public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
        {
            if (buildTarget == BuildTarget.iOS)
            {
                LinkLibraries(path);
            }
        }

        static string GetTargetGuid(PBXProject proj)
        {
#if UNITY_2019_3_OR_NEWER
            return proj.GetUnityFrameworkTargetGuid();
#else
            return proj.TargetGuidByName("Unity-iPhone");
#endif
        }

        // The followings are the addtional frameworks to add to the project
        static string[] ProjectFrameworks = new string[] {
        };

        static string[] EmbeddedFrameworks = new string[] {
	        "wrapper.framework",
            "ChatCWrapper.framework",
            "HyphenateChat.framework"
	    };

        static string GetChatFrameworkGuid(PBXProject proj, string framework)
        {

            string guid = "";

            // Get PBXProject type information
            Type pbxProjectType = typeof(PBXProject);

            // Use reflect to find the method
            MethodInfo methodInfo = pbxProjectType.GetMethod("GetRealPathsOfAllFiles",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

            // Check method exist or not
            if (methodInfo != null)
            {

                object[] parameters = new object[] { PBXSourceTree.Source };

                // Method exist, then call it
                List<string> list = (List<string>)methodInfo.Invoke(proj, parameters);

                foreach (var item in list)
                {
                    if (item.Contains(framework))
                    {
                        guid = proj.FindFileGuidByRealPath(item);
                    }
                }

                Debug.Log($"Automatically get the guid of {framework}");
            }
            else
            {
                // Method NOT exist, then just print log
                Debug.Log($"Cannot automatically get the guid of {framework}, return empty");
            }

            return guid;
        }

        static void EmbedFramework(PBXProject proj, string target, string frameworkPath, string customPath)
        {

            string guid;
            guid = GetChatFrameworkGuid(proj, frameworkPath);

            if (string.IsNullOrEmpty(guid))
            {
                Debug.Log($"Construct project path manually for {frameworkPath} to get guid");
                string ChatFrameWorkPath = Path.Combine(defaultLocationInProj, frameworkPath);
                string projectPath = customPath ?? "";
                guid = proj.AddFile(ChatFrameWorkPath, "Frameworks/" + projectPath + ChatFrameWorkPath, PBXSourceTree.Sdk);
            }

            Debug.Log($"Get guid for {frameworkPath}: {guid}");

            PBXProjectExtensions.AddFileToEmbedFrameworks(proj, target, guid);

            //string ChatFrameWorkPath = Path.Combine(defaultLocationInProj, frameworkPath);
            //string projectPath = customPath ?? "";
            //string fileGuid = proj.AddFile(ChatFrameWorkPath, "Frameworks/" + projectPath + ChatFrameWorkPath, PBXSourceTree.Sdk);
            //PBXProjectExtensions.AddFileToEmbedFrameworks(proj, target, frameworkGuild);
        }

        static void LinkLibraries(string path)
        {
            // linked library
            string projPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";
            PBXProject proj = new PBXProject();
            proj.ReadFromFile(projPath);
            string target = GetTargetGuid(proj);

            // disable bit-code
            proj.SetBuildProperty(target, "ENABLE_BITCODE", "false");

            // Frameworks
            foreach (string framework in ProjectFrameworks)
            {
                proj.AddFrameworkToProject(target, framework, true);
            }

            // embedded frameworks
#if UNITY_2019_1_OR_NEWER
	    target = proj.GetUnityMainTargetGuid();
#endif
	    foreach (string framework in EmbeddedFrameworks) 
	    {
		    EmbedFramework(proj, target, framework, AgoraChat.IOSBuildSetting.CustomPackagePath);
	    }

            proj.SetBuildProperty(target, "LD_RUNPATH_SEARCH_PATHS", "$(inherited) @executable_path/Frameworks");

            // done, write to the project file
            File.WriteAllText(projPath, proj.WriteToString());
        }

    }

}
#endif
#endif

namespace AgoraChat
{
    
    /// <summary>
    /// This class is used to set the path of the AgoraChat SDK.
    /// Sample code:
    ///
    /// #if UNITY_EDITOR
    /// using UnityEditor.Callbacks;
    /// using UnityEditor;
    /// 
    /// public class BuildAgoraChat
    /// {
    ///     [PostProcessBuildAttribute(0)]
    ///     public static void SetAgoraChatPath(BuildTarget buildTarget, string path)
    ///     {
    ///        // SDK Path: Assets/ThirdParties/AgoraChat/Plugins/iOS
    ///        AgoraChat.IOSBuildSetting.CustomPackagePath = "ThirdParties/";
    ///     }
    /// }
    /// #endif
    /// 
    /// </summary>
    public class IOSBuildSetting
    {
        public static string CustomPackagePath = null;
    }
}
