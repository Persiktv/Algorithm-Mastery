using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using MySql.Data.MySqlClient;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class MySQLConnection : MonoBehaviour
{
    public float delay = 1f; // �������� � ��������
    public TMP_InputField loginInput;
    public TMP_InputField passwordInput;
    public TMP_Dropdown dropdown;
    public Text messageText;
    private string userRole;

    private MySqlConnection connection;
    private string server;
    private string database;
    private string uid;
    private string password;


    // ������������� ���������� � ����� ������
    void Start()
    {

        server = "127.0.0.1";
        database = "testbaseunity";
        uid = "root";
        password = "root";
        string connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

        connection = new MySqlConnection(connectionString);

        try
        {
            connection.Open();
            Debug.Log("����������� � ���� ������ MySQL");
        }
        catch (MySqlException ex)
        {
            Debug.LogError("������ ����������� � ���� ������ MySQL: " + ex.ToString());
        }
    }

    // ���������� ������� � ���� ������
    public void ExecuteQuery(string query)
    {
        if (connection.State == ConnectionState.Open)
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
        }
        else
        {
            Debug.LogError("MySQL ���������� �������!");
        }
    }

    public void Login()
    {
        string login = loginInput.text;
        string password = passwordInput.text;

        // ������ � ���� ������ �� ����� ������������
        string query = "SELECT * FROM users WHERE Login='" + login + "' AND Password='" + password + "'";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        MySqlDataReader dataReader = cmd.ExecuteReader();

        // ���� ������������ ������, �������� ��� ����
        if (dataReader.HasRows)
        {
            messageText.color = Color.green;
            messageText.text = "����� ���������� " + login + "!";

            while (dataReader.Read())
            {
                login = dataReader.GetString("Login");
                password = dataReader.GetString("Password");
                userRole = dataReader.GetString("Role");
            }
            dataReader.Close();

            IEnumerator LoadNextScene(string sceneName, float delay)
            {
                yield return new WaitForSeconds(delay);
                SceneManager.LoadScene(sceneName);
            }

            // ���������� ����� ����� ������� � ����������� �� ���� ������������
            if (userRole == "�������")
            {
                StartCoroutine(LoadNextScene("Student Scene", 1f)); // �������� � 1 �������
            }
            else if (userRole == "�������������")
            {
                StartCoroutine(LoadNextScene("Teacher Scene", 1f)); // �������� � 1 �������
            }
            else if (userRole == "�����")
            {
                StartCoroutine(LoadNextScene("Admin Scene", 1f)); // �������� � 1 �������
            }
        }
        else
        {
            dataReader.Close();
            messageText.text = "������������ ����� ��� ������!";
        }
    }

    public void GetSelectedOption()
    {
        // ���� ������� ����� ������ ���� TMP_Dropdown.OptionDataList
        TMP_Dropdown.OptionDataList options = new TMP_Dropdown.OptionDataList();

        // �������� ������ �� ������ dropdown.options � ����� ������ options
        foreach (TMP_Dropdown.OptionData option in dropdown.options)
        {
            options.options.Add(new TMP_Dropdown.OptionData(option.text));
        }

        // �������� ������ ���������� ��������
        int selectedOptionIndex = dropdown.value;

        // �������� ����� ���������� ��������
        string selectedOptionText = options.options[selectedOptionIndex].text;

        // ������� ����� ���������� �������� � �������
        Debug.Log("������ �������: " + selectedOptionText);
        AddUser(selectedOptionText);

    }

    public void AddUser(string selectedOptionText)
    {
        string login = loginInput.text;
        string password = passwordInput.text;
        string role = selectedOptionText;

        string query = "INSERT INTO users (Login, Password, Role) VALUES('" + login + "', '" + password + "', '" + role + "')";

        MySqlCommand cmd = new MySqlCommand(query, connection);

        try
        {
            cmd.ExecuteNonQuery();
            Debug.Log("����� ������������ ��������.");
        }
        catch (Exception ex)
        {
            Debug.Log("������ ��� ���������� ������������: " + ex.Message);
        }

        loginInput.text = "";
        passwordInput.text = "";
    }
}
