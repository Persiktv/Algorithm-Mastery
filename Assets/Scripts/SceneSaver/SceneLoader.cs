using UnityEngine;
using UnityEditor;
using System.IO;
using System.Data;
using MySql.Data.MySqlClient;

public class SceneLoader : MonoBehaviour
{
    private string server;
    private string database;
    private string uid;
    private string password;

    public void LoadScene()
    {
        server = "127.0.0.1";
        database = "testbaseunity";
        uid = "root";
        password = "root";
        string connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

        // SQL-запрос на получение пути файла из таблицы
        string query = "SELECT `Tusk` FROM tusks ORDER BY ID DESC LIMIT 1";

        // Создание объекта подключения к базе данных
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            // Открытие подключения
            connection.Open();

            // Создание объекта команды с SQL-запросом и подключением
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                // Выполнение команды и чтение результата
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Получение пути файла из результата запроса
                        string filePath = reader.GetString("Tusk");

                        // Загрузка сцены из файла .json
                        string sceneJson = File.ReadAllText(filePath);
                        //UnityEngine.SceneManagement.SceneManager.LoadSceneFromJson(sceneJson);
                    }
                }
            }
        }
    }
}