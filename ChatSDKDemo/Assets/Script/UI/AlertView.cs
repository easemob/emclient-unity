using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AlertInfo {

    public AlertInfo(string title, string info, Action confirmAction, Action cancelAction) {
        this.title = title;
        this.info = info;
        this.onConfirm = confirmAction;
        this.onCancel = cancelAction;
    }

    public string title;
    public string info;
    public string confirmBtnInfo = "确定";
    public string cancelBtnInfo = "取消";

    public Action onConfirm;
    public Action onCancel;
}

public class AlertView
{

    private GameObject alertView = null;
    private AlertInfo alertInfo;
   
    private Text m_TitleText;
    private Text m_InfoText;
    private Button m_ConfirmBtn;
    private Button m_CancelBtn;


    public AlertView(AlertInfo info, Transform transform) {
        alertInfo = info;


        alertView = GameObject.Instantiate(Resources.Load("UI/AlertView")) as GameObject;
        alertView.transform.SetParent(transform);
        alertView.transform.localPosition = Vector3.zero;
        alertView.transform.localScale = Vector3.one;

        m_TitleText = alertView.transform.Find("Title").GetComponent<Text>();
        m_InfoText = alertView.transform.Find("Info").GetComponent<Text>();
        m_ConfirmBtn = alertView.transform.Find("Panel/ConfirmBtn").GetComponent<Button>();
        m_CancelBtn = alertView.transform.Find("Panel/CancelBtn").GetComponent<Button>();


        Debug.Log(info.title);
        Debug.Log(info.info);

        m_TitleText.text = info.title;
        m_InfoText.text = info.info;
       
        m_ConfirmBtn.onClick.AddListener(OnConfirmClicked);
        m_CancelBtn.onClick.AddListener(OnCancelClicked);
    }

    private void OnConfirmClicked() {
        if (alertInfo.onConfirm != null)
        {
            alertInfo.onConfirm.Invoke();
        }
        ClosePanel();
    }

    private void OnCancelClicked() {
        if (alertInfo.onCancel != null) {
            alertInfo.onCancel.Invoke();
        }
        ClosePanel();
    }


    private void ClosePanel()
    {
        GameObject.Destroy(alertView);
    }

}
