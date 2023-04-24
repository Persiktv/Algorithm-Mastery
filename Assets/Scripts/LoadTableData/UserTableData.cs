using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using MySql.Data.MySqlClient;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UserTableData : MonoBehaviour
{

    public TMP_Text userName;
    public TMP_Text userRole;

    // ������� ���������� ����������� � ���� ������ MySQL
    private string connectionString = "Server=127.0.0.1;Database=testbaseunity;Uid=root;Pwd=root;";

    void Start()
    {
        // �������� ������� ��� ����������� � ���� ������ MySQL
        MySqlConnection connection = new MySqlConnection(connectionString);

        // �������� ���������� � ����� ������ MySQL
        connection.Open();

        // ������ SQL ��� ���������� ������ �������������
        string query = "SELECT * FROM users;";

        // �������� ������� ��� ���������� ������� SQL
        MySqlCommand command = new MySqlCommand(query, connection);

        // ���������� ������� SQL � ��������� ������ �� ���� ������ MySQL
        MySqlDataReader dataReader = command.ExecuteReader();

        // ��������� ������, ���������� �� ���� ������ MySQL
        while (dataReader.Read())
        {
            string username = dataReader.IsDBNull(dataReader.GetOrdinal("Name")) ? "�����������" : dataReader.GetString("Name");
            string role = dataReader.IsDBNull(dataReader.GetOrdinal("Role")) ? "�����������" : dataReader.GetString("Role");

            userName.text += username + "\n\n";
            userRole.text += role + "\n\n";

            Debug.Log("Name: " + username + ", Role: " + role);
        }

        // �������� ���������� � ����� ������ MySQL
        connection.Close();
    }
}