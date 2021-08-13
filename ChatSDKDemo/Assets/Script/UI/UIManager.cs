using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager 
{
    public static Button Button {
        get => GameObject.Instantiate(Resources.Load("Prefabs/Button")) as Button;
    }

    static public AlertView TitleAlert(Transform transform, string title, string info, Action confirm, Action cancel = null)
    {
        return new AlertView(new AlertInfo(title, info, confirm, cancel), transform);
    }

    static public AlertView DefaultAlert(Transform transform, string info, Action confirm = null, Action cancel = null)
    {
        return TitleAlert(transform, "提示", info, confirm, cancel);
    }

    static public InputAlert DefaultInputAlert(Transform transform, InputAlertConfig config) {
        InputAlert alert = new InputAlert(config, transform);
        return alert;
    }

}
