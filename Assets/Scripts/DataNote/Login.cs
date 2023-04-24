using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Login : MonoBehaviour
{

    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Button loginButton;

    ArrayList credentials;

    void Start()
    {
        loginButton.onClick.AddListener(login);

        if (File.Exists(Application.dataPath + "/credentials.txt"))
        {
            credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));
        }
        else
        {
            Debug.Log("Файл учетных данных не существует");
        }

    }


    void login()
    {
        bool AdminIsExists = false;
        bool StudentIsExists = false;
        bool PrepodIsExists = false;

        credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));

        foreach (var i in credentials)
        {
            string line = i.ToString();
            //Debug.Log(line);
            //Debug.Log(line.Substring(11));
            //substring 0-indexof(:) - indexof(:)+1 - i.length-1
            if (i.ToString().Substring(0, i.ToString().IndexOf(":")).Equals(usernameInput.text) &&
                i.ToString().Substring(i.ToString().IndexOf(":") + 1).Equals(passwordInput.text+3))
            {
                AdminIsExists = true;
                break;
            }

            else if (i.ToString().Substring(0, i.ToString().IndexOf(":")).Equals(usernameInput.text) &&
                i.ToString().Substring(i.ToString().IndexOf(":") + 1).Equals(passwordInput.text + 0))
            {
                StudentIsExists = true;
                break;
            }

            else if (i.ToString().Substring(0, i.ToString().IndexOf(":")).Equals(usernameInput.text) &&
                i.ToString().Substring(i.ToString().IndexOf(":") + 1).Equals(passwordInput.text + 1))
            {
                PrepodIsExists = true;
                break;
            }
        }

        if (AdminIsExists)
        {
            Debug.Log($"Вы вошли как '{usernameInput.text}'");
            SceneManager.LoadScene("Admin Scene");
        }

        else if (StudentIsExists)
        {
            Debug.Log($"Вы вошли как '{usernameInput.text}'");
            SceneManager.LoadScene("Student Scene");
        }

        else if (PrepodIsExists)
        {
            Debug.Log($"Вы вошли как '{usernameInput.text}'");
            SceneManager.LoadScene("Teacher Scene");
        }

        else
        {
            Debug.Log("Пользователь не найден.");
        }
    }

}