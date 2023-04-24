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

        // SQL-������ �� ��������� ���� ����� �� �������
        string query = "SELECT `Tusk` FROM tusks ORDER BY ID DESC LIMIT 1";

        // �������� ������� ����������� � ���� ������
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            // �������� �����������
            connection.Open();

            // �������� ������� ������� � SQL-�������� � ������������
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                // ���������� ������� � ������ ����������
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // ��������� ���� ����� �� ���������� �������
                        string filePath = reader.GetString("Tusk");

                        // �������� ����� �� ����� .json
                        string sceneJson = File.ReadAllText(filePath);
                        //UnityEngine.SceneManagement.SceneManager.LoadSceneFromJson(sceneJson);
                    }
                }
            }
        }
    }
}