using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager 
{
    public static Button Button {
        get => GameObject.Instantiate(Resources.Load("Prefabs/Button")) as Button;
    }
    
}
