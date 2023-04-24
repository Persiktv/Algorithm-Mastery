using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using MySql.Data.MySqlClient;

public class RegisterUser : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public InputField postInput;

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
        OpenConnection();
    }

    // �������� ���������� � ����� ������
    private void OpenConnection()
    {
        try
        {
            connection.Open();
            Debug.Log("���������� �����������.");
        }
        catch (Exception ex)
        {
            Debug.Log("������ ��� ������������ ����������: " + ex.Message);
        }
    }

    // �������� ���������� � ����� ������
    private void CloseConnection()
    {
        try
        {
            connection.Close();
            Debug.Log("���������� �������.");
        }
        catch (Exception ex)
        {
            Debug.Log("������ ��� �������� ����������: " + ex.Message);
        }
    }

    // ���������� ������ ������������ � ���� ������
    //public void AddUser()
    //{
    //    string username = usernameInput.text;
    //    string password = passwordInput.text;
    //    string email = emailInput.text;

    //    string query = "INSERT INTO users (username, password, email) VALUES('" + username + "', '" + password + "', '" + email + "')";

    //    MySqlCommand cmd = new MySqlCommand(query, connection);

    //    try
    //    {
    //        cmd.ExecuteNonQuery();
    //        Debug.Log("����� ������������ ��������.");
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.Log("������ ��� ���������� ������������: " + ex.Message);
    //    }
    //}
}