using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConversationManagerTest : MonoBehaviour
{

    private Button backButton;
    private Button LastMessageBtn;
    private Button LastReceiveMessageBtn;
    private Button GetExtBtn;
    private Button SetExtBtn;
    private Button UnReadCountBtn;
    private Button MarkMessageAsReadBtn;
    private Button MarkAllMessageAsReadBtn;
    private Button InsertMessageBtn;
    private Button AppendMessageBtn;
    private Button UpdateMessageBtn;
    private Button DeleteMessageBtn;
    private Button DeleteAllMessageBtn;
    private Button LoadMessageBtn;
    private Button LoadMessagesBtn;
    private Button LoadMessagesWithKeywordBtn;
    private Button LoadMessagesWithTimeBtn;
    private Button LoadMessagesWithMsgTypeBtn;

    private void Awake()
    {
        Debug.Log("conversation manager test script has load");

        backButton = transform.Find("BackBtn").GetComponent<Button>();

        backButton.onClick.AddListener(backButtonAction);

        LastMessageBtn = transform.Find("Scroll View/Viewport/Content/LastMessageBtn").GetComponent<Button>();
        LastReceiveMessageBtn = transform.Find("Scroll View/Viewport/Content/LastReceiveMessageBtn").GetComponent<Button>();
        GetExtBtn = transform.Find("Scroll View/Viewport/Content/GetExtBtn").GetComponent<Button>();
        SetExtBtn = transform.Find("Scroll View/Viewport/Content/SetExtBtn").GetComponent<Button>();
        UnReadCountBtn = transform.Find("Scroll View/Viewport/Content/UnReadCountBtn").GetComponent<Button>();
        MarkMessageAsReadBtn = transform.Find("Scroll View/Viewport/Content/MarkMessageAsReadBtn").GetComponent<Button>();
        MarkAllMessageAsReadBtn = transform.Find("Scroll View/Viewport/Content/MarkAllMessageAsReadBtn").GetComponent<Button>();
        InsertMessageBtn = transform.Find("Scroll View/Viewport/Content/InsertMessageBtn").GetComponent<Button>();
        AppendMessageBtn = transform.Find("Scroll View/Viewport/Content/AppendMessageBtn").GetComponent<Button>();
        UpdateMessageBtn = transform.Find("Scroll View/Viewport/Content/UpdateMessageBtn").GetComponent<Button>();
        DeleteMessageBtn = transform.Find("Scroll View/Viewport/Content/DeleteMessageBtn").GetComponent<Button>();
        DeleteAllMessageBtn = transform.Find("Scroll View/Viewport/Content/DeleteAllMessageBtn").GetComponent<Button>();
        LoadMessageBtn = transform.Find("Scroll View/Viewport/Content/LoadMessageBtn").GetComponent<Button>();
        LoadMessagesBtn = transform.Find("Scroll View/Viewport/Content/LoadMessagesBtn").GetComponent<Button>();
        LoadMessagesWithKeywordBtn = transform.Find("Scroll View/Viewport/Content/LoadMessagesWithKeywordBtn").GetComponent<Button>();
        LoadMessagesWithTimeBtn = transform.Find("Scroll View/Viewport/Content/LoadMessagesWithTimeBtn").GetComponent<Button>();
        LoadMessagesWithMsgTypeBtn = transform.Find("Scroll View/Viewport/Content/LoadMessagesWithMsgTypeBtn").GetComponent<Button>();


        LastMessageBtn.onClick.AddListener(LastMessageBtnAction);
        LastReceiveMessageBtn.onClick.AddListener(LastReceiveMessageBtnAction);
        GetExtBtn.onClick.AddListener(GetExtBtnAction);
        SetExtBtn.onClick.AddListener(SetExtBtnAction);
        UnReadCountBtn.onClick.AddListener(UnReadCountBtnAction);
        MarkMessageAsReadBtn.onClick.AddListener(MarkMessageAsReadBtnAction);
        MarkAllMessageAsReadBtn.onClick.AddListener(MarkAllMessageAsReadBtnAction);
        InsertMessageBtn.onClick.AddListener(InsertMessageBtnAction);
        AppendMessageBtn.onClick.AddListener(AppendMessageBtnAction);
        UpdateMessageBtn.onClick.AddListener(UpdateMessageBtnAction);
        DeleteMessageBtn.onClick.AddListener(DeleteMessageBtnAction);
        DeleteAllMessageBtn.onClick.AddListener(DeleteAllMessageBtnAction);
        LoadMessageBtn.onClick.AddListener(LoadMessageBtnAction);
        LoadMessagesBtn.onClick.AddListener(LoadMessagesBtnAction);
        LoadMessagesWithKeywordBtn.onClick.AddListener(LoadMessagesWithKeywordBtnAction);
        LoadMessagesWithTimeBtn.onClick.AddListener(LoadMessagesWithTimeBtnAction);
        LoadMessagesWithMsgTypeBtn.onClick.AddListener(LoadMessagesWithMsgTypeBtnAction);
    }


    void backButtonAction()
    {
        SceneManager.LoadSceneAsync("Main");
    }

    void LastMessageBtnAction()
    {
        Debug.Log("LastMessageBtnAction");
    }
    void LastReceiveMessageBtnAction()
    {
        Debug.Log("LastReceiveMessageBtnAction");
    }
    void GetExtBtnAction()
    {
        Debug.Log("GetExtBtnAction");
    }
    void SetExtBtnAction()
    {
        Debug.Log("SetExtBtnAction");
    }
    void UnReadCountBtnAction()
    {
        Debug.Log("UnReadCountBtnAction");
    }
    void MarkMessageAsReadBtnAction()
    {
        Debug.Log("MarkMessageAsReadBtnAction");
    }
    void MarkAllMessageAsReadBtnAction()
    {
        Debug.Log("MarkAllMessageAsReadBtnAction");
    }
    void InsertMessageBtnAction()
    {
        Debug.Log("InsertMessageBtnAction");
    }
    void AppendMessageBtnAction()
    {
        Debug.Log("AppendMessageBtnAction");
    }
    void UpdateMessageBtnAction()
    {
        Debug.Log("UpdateMessageBtnAction");
    }
    void DeleteMessageBtnAction()
    {
        Debug.Log("DeleteMessageBtnAction");
    }
    void DeleteAllMessageBtnAction()
    {
        Debug.Log("DeleteAllMessageBtnAction");
    }
    void LoadMessageBtnAction()
    {
        Debug.Log("LoadMessageBtnAction");
    }
    void LoadMessagesBtnAction()
    {
        Debug.Log("LoadMessagesBtnAction");
    }
    void LoadMessagesWithKeywordBtnAction()
    {
        Debug.Log("LoadMessagesWithKeywordBtnAction");
    }
    void LoadMessagesWithTimeBtnAction()
    {
        Debug.Log("LoadMessagesWithTimeBtnAction");
    }
    void LoadMessagesWithMsgTypeBtnAction()
    {
        Debug.Log("LoadMessagesWithMsgTypeBtnAction");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
