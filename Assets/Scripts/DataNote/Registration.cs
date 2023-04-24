using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Registration : MonoBehaviour
{

    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_Dropdown resolutionDropdown;
    public Button registerButton;

    ArrayList credentials;

    // Start is called before the first frame update
    void Start()
    {
        registerButton.onClick.AddListener(writeStuffToFile);

        if (File.Exists(Application.dataPath + "/credentials.txt"))
        {
            credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));
        }
        else
        {
            File.WriteAllText(Application.dataPath + "/credentials.txt", "");
        }

    }


    void writeStuffToFile()
    {
        bool isExists = false;

        credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));
        foreach (var i in credentials)
        {
            if (i.ToString().Contains(usernameInput.text))
            {
                isExists = true;
                break;
            }
        }

        if (isExists)
        {
            Debug.Log($"Username '{usernameInput.text}' already exists");
        }
        else
        {
            credentials.Add(usernameInput.text + ":" + passwordInput.text + resolutionDropdown.value);
            File.WriteAllLines(Application.dataPath + "/credentials.txt", (String[])credentials.ToArray(typeof(string)));
            passwordInput.text = "";
            usernameInput.text = "";
            Debug.Log("Account Registered");
        }
    }


}