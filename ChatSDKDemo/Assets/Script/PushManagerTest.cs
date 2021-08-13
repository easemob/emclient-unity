using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PushManagerTest : MonoBehaviour
{

    private Button backButton;

    private void Awake()
    {
        Debug.Log("push manager test script has load");

        backButton = transform.Find("BackBtn").GetComponent<Button>();

        backButton.onClick.AddListener(backButtonAction);
    }


    void backButtonAction()
    {
        SceneManager.LoadSceneAsync("Main");
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
