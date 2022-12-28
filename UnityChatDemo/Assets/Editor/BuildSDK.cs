
using UnityEditor;
using UnityEngine;

public class BuildSDK
{
    [MenuItem("Easemob/BuildAndMove/iOS")]
    public static void RunIOSShell()
    {
        string shellPath = Application.dataPath + "/../../AgoraChatSDK/buildScript/build_ios.sh";
        System.Diagnostics.Process.Start("/bin/bash", shellPath);
        UnityEngine.Debug.Log("build finished");
    }

    [MenuItem("Easemob/BuildAndMove/Android")]
    public static void RunAndroidShell()
    {

    }

    [MenuItem("Easemob/BuildAndMove/Windows")]
    public static void RunWinShell()
    {

    }

    [MenuItem("Easemob/BuildAndMove/Mac")]
    public static void RunMacShell()
    {

    }

    [MenuItem("Easemob/ExportSDKPackage")]
    public static void ExportPackage()
    {
        string str = System.DateTime.Now.ToString("d");
        Debug.Log(str);
    }

}