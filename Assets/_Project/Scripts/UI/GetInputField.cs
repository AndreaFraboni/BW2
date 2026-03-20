using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class GetInputField : MonoBehaviour
{
    public TMP_InputField InputFieldObj;
    public string NewNamePlayer;

    public void Awake()
    {
        InputFieldObj.ActivateInputField();

        //PlayerNameText.text = "PLAYER NAME : " + mPlayerData.UserName;
    }

    // Start is called before the first frame update
    void Start()
    {
        InputFieldObj.ActivateInputField();
        InputFieldObj.text = "";
        InputFieldObj.caretBlinkRate = 1000;
    }

    public void ReadTextField(string s)
    {
        string input = s;
        Debug.Log(input);

        NewNamePlayer = input;

        InputFieldObj.ActivateInputField();
    }

   

}
