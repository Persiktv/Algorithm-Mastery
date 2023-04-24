using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using MySql.Data.MySqlClient;

public class UnlockObject : MonoBehaviour
{
    public GameObject objectToUnlock;
    public string columnName;
    public string columnValue;

    private void Start()
    {
        // ���������� ���������� � ����� ������ MySQL
        string connectionString = "Server=127.0.0.1;Database=testbaseunity;Uid=root;Pwd=root;";
        MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        // ��������� ������ � ���� ������
        string query = "SELECT * FROM tusks WHERE " + columnName + " = '" + columnValue + "'";
        MySqlCommand command = new MySqlCommand(query, connection);
        MySqlDataReader reader = command.ExecuteReader();

        // ��������� ������� ������� � ���� ������
        if (reader.HasRows)
        {
            // �������������� ������ � Unity
            objectToUnlock.SetActive(true);
        }

        // ������� ���������� � ���������� �������
        reader.Close();
        connection.Close();
    }
}