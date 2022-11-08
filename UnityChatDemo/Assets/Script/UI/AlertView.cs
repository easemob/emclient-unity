using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AlertInfo {

    public AlertInfo(string title, string info, Action confirmAction, Action cancelAction, string confirmText = "确定", string cancelText = "取消") {

        Debug.Log($"AlertInfo : {title}, {info}, {confirmAction}, {cancelAction}, {confirmText}, {cancelText}");

        this.title = title;
        this.info = info;
        this.onConfirm = confirmAction;
        this.onCancel = cancelAction;
        this.confirmBtnInfo = confirmText;
        this.cancelBtnInfo = cancelText;
    }

    public string title;
    public string info;
    public string confirmBtnInfo;
    public string cancelBtnInfo;

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


        Debug.Log($"AlertView : ${transform}");

        alertView = GameObject.Instantiate(Resources.Load("UI/AlertView")) as GameObject;
        alertView.transform.SetParent(transform);
        alertView.transform.localPosition = Vector3.zero;
        alertView.transform.localScale = Vector3.one;

        m_TitleText = alertView.transform.Find("Title").GetComponent<Text>();
        m_InfoText = alertView.transform.Find("Info").GetComponent<Text>();
        m_ConfirmBtn = alertView.transform.Find("Panel/ConfirmBtn").GetComponent<Button>();
        m_CancelBtn = alertView.transform.Find("Panel/CancelBtn").GetComponent<Button>();

        m_TitleText.text = info.title;
        m_InfoText.text = info.info;

        Text confirmText = m_ConfirmBtn.transform.Find("Text").GetComponent<Text>();
        Text cancelText = m_CancelBtn.transform.Find("Text").GetComponent<Text>();

        confirmText.text = info.confirmBtnInfo;
        cancelText.text = info.cancelBtnInfo;

        m_ConfirmBtn.onClick.AddListener(OnConfirmClicked);
        m_CancelBtn.onClick.AddListener(OnCancelClicked);
    }

    private void OnConfirmClicked() {
        alertInfo.onConfirm?.Invoke();
        ClosePanel();
    }

    private void OnCancelClicked() {
        alertInfo.onCancel?.Invoke();
        ClosePanel();
    }


    private void ClosePanel()
    {
        GameObject.Destroy(alertView);
    }

}
