#if UNITY_IPHONE 

using System.IO;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;

namespace AgoraChat {
    
    
    public class BL_BuildPostProcess
    {
        // If you changed the default import path, add your new path here. If not set, it is "null".
        private static string CustomPackagePath = null; 
        
	    const string defaultLocationInProj = "AgoraChat/Plugins/iOS";

        [PostProcessBuild]
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
        

        static void EmbedFramework(PBXProject proj, string target, string frameworkPath, string customPath)
        {
            string ChatFrameWorkPath = Path.Combine(defaultLocationInProj, frameworkPath);
            string projectPath = customPath ?? "";
            string fileGuid = proj.AddFile(ChatFrameWorkPath, "Frameworks/" + projectPath + ChatFrameWorkPath, PBXSourceTree.Sdk);
            PBXProjectExtensions.AddFileToEmbedFrameworks(proj, target, fileGuid);
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
		    EmbedFramework(proj, target, framework, CustomPackagePath);
	    }

            proj.SetBuildProperty(target, "LD_RUNPATH_SEARCH_PATHS", "$(inherited) @executable_path/Frameworks");

            // done, write to the project file
            File.WriteAllText(projPath, proj.WriteToString());
        }

    }

}
#endif