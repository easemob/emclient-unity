using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    static object locker = new object();
    static List<Action> aList = new List<Action>();
    static int count = 0;

    public static int MainThreadID { get; private set; }

    private void Awake()
    {
        MainThreadID = Thread.CurrentThread.ManagedThreadId;
    }

    private void Update()
    {
        if (count != 0)
        {
            lock (locker)
            {
                foreach (var a in aList)
                {
                    if (a != null) a();
                }
                aList.Clear();
                count = 0;
            }
        }
    }

    static public AlertView TitleAlert(Transform transform, string title, string info, Action confirm, Action cancel = null, string confirmText = "确定", string cancelText = "取消")
    {
        return new AlertView(new AlertInfo(title, info, confirm, cancel, confirmText, cancelText), transform);
    }

    static public AlertView DefaultAlert(Transform transform, string info, Action confirm = null, Action cancel = null)
    {
        return TitleAlert(transform, "提示", info, confirm, cancel);
    }

    static public AlertView AskAlert(Transform transform, string info,Action confirm = null, Action cancel = null) {
        return TitleAlert(transform, "提示", info, confirm, cancel, "同意", "拒绝");
    }

    static public AlertView SuccessAlert(Transform transform) {
        return TitleAlert(transform, "提示", "成功", null, null);
    }

    static public AlertView ErrorAlert(Transform transform, int code, string desc)
    {
        return TitleAlert(transform, "失败", $"{code}: {desc}", null, null);
    }

    static public AlertView UnfinishedAlert(Transform transform) {
        return TitleAlert(transform, "提示", "未实现", null, null);
    }

    static public InputAlert DefaultInputAlert(Transform transform, InputAlertConfig config) {
        InputAlert alert = new InputAlert(config, transform);
        return alert;
    }

}
