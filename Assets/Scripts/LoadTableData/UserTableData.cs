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

    // Задание параметров подключения к базе данных MySQL
    private string connectionString = "Server=127.0.0.1;Database=testbaseunity;Uid=root;Pwd=root;";

    void Start()
    {
        // Создание объекта для подключения к базе данных MySQL
        MySqlConnection connection = new MySqlConnection(connectionString);

        // Открытие соединения с базой данных MySQL
        connection.Open();

        // Запрос SQL для извлечения данных пользователей
        string query = "SELECT * FROM users;";

        // Создание объекта для выполнения запроса SQL
        MySqlCommand command = new MySqlCommand(query, connection);

        // Выполнение запроса SQL и получение данных из базы данных MySQL
        MySqlDataReader dataReader = command.ExecuteReader();

        // Обработка данных, полученных из базы данных MySQL
        while (dataReader.Read())
        {
            string username = dataReader.IsDBNull(dataReader.GetOrdinal("Name")) ? "Неизвестный" : dataReader.GetString("Name");
            string role = dataReader.IsDBNull(dataReader.GetOrdinal("Role")) ? "Неизвестный" : dataReader.GetString("Role");

            userName.text += username + "\n\n";
            userRole.text += role + "\n\n";

            Debug.Log("Name: " + username + ", Role: " + role);
        }

        // Закройте соединение с базой данных MySQL
        connection.Close();
    }
}