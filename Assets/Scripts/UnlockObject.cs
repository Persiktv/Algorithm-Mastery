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
        // установить соединение с базой данных MySQL
        string connectionString = "Server=127.0.0.1;Database=testbaseunity;Uid=root;Pwd=root;";
        MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        // выполнить запрос к базе данных
        string query = "SELECT * FROM tusks WHERE " + columnName + " = '" + columnValue + "'";
        MySqlCommand command = new MySqlCommand(query, connection);
        MySqlDataReader reader = command.ExecuteReader();

        // проверить наличие записей в базе данных
        if (reader.HasRows)
        {
            // разблокировать объект в Unity
            objectToUnlock.SetActive(true);
        }

        // закрыть соединение и освободить ресурсы
        reader.Close();
        connection.Close();
    }
}