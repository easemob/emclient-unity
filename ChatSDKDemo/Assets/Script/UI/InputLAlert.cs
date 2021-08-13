using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputAlertConfig
{
    
    public InputAlertConfig(string title, Action<Dictionary<string, string>> confirmAction, Action cancelAction = null)
    {
        this.title = title;
        this.onConfirm = confirmAction;
        this.onCancel = cancelAction;
    }

    public List<string> list = new List<string>();
    public string title;
    public string info;
    public string confirmBtnInfo = "确定";
    public string cancelBtnInfo = "取消";

    public Action<Dictionary<string, string>> onConfirm;
    public Action onCancel;

    public InputAlertConfig AddField(string field) {
        list.Add(field);
        return this;
    }
}

public class InputAlert
{

    private GameObject alertView = null;
    Transform contentTransform;
    private InputAlertConfig alertConfig;

    private Text m_TitleText;
    private Button m_ConfirmBtn;
    private Button m_CancelBtn;

    private List<GameObject> childs = new List<GameObject>();


    public InputAlert(InputAlertConfig config, Transform transform)
    {
        alertConfig = config;
        
        alertView = GameObject.Instantiate(Resources.Load("UI/AlertLabelView")) as GameObject;
        alertView.transform.SetParent(transform);
        alertView.transform.localPosition = Vector3.zero;
        alertView.transform.localScale = Vector3.one;

        contentTransform = alertView.transform.Find("Scroll View/Viewport/Content").GetComponent<Transform>();
        AddFieldToContent(contentTransform);


        m_TitleText = alertView.transform.Find("Title").GetComponent<Text>();
        m_TitleText.text = alertConfig.title;


        m_ConfirmBtn = alertView.transform.Find("Panel/ConfirmBtn").GetComponent<Button>();
        m_CancelBtn = alertView.transform.Find("Panel/CancelBtn").GetComponent<Button>();
        m_ConfirmBtn.onClick.AddListener(OnConfirmClicked);
        m_CancelBtn.onClick.AddListener(OnCancelClicked);
    }

    private void AddFieldToContent(Transform contentTransform) {
        foreach (string info in alertConfig.list) {
            GameObject textField = GameObject.Instantiate(Resources.Load("Prefabs/TextField")) as GameObject;
            textField.transform.SetParent(contentTransform);
            textField.transform.localPosition = Vector3.zero;
            textField.transform.localScale = Vector3.one;
            childs.Add(textField);
            Text text = textField.transform.Find("Placeholder").GetComponent<Text>() as Text;
            text.text = info;
        }
    }

    private Dictionary<string, string> getAllInputValues() {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        foreach (var gameObject in childs) {
            Text placeholder = gameObject.transform.Find("Placeholder").GetComponent<Text>() as Text;
            Text text = gameObject.transform.Find("Text").GetComponent<Text>() as Text;
            dic.Add(placeholder.text, text.text ?? "");
        }
        return dic;
    }

    private void OnConfirmClicked()
    {
        if (alertConfig.onConfirm != null)
        {
            alertConfig.onConfirm.Invoke(getAllInputValues());
        }
        ClosePanel();
    }

    private void OnCancelClicked()
    {
        if (alertConfig.onCancel != null)
        {
            alertConfig.onCancel.Invoke();
        }
        ClosePanel();
    }

    private void ClosePanel()
    {
        GameObject.Destroy(alertView);
    }

}
