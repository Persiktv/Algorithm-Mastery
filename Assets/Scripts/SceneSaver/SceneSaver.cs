using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Data;
using MySql.Data.MySqlClient;

public class SceneSaver : MonoBehaviour
{
    [SerializeField] private string defaultSavePath = "Assets/Scenes/SavedScenes/scene.json"; 

    private string server;
    private string database;
    private string uid;
    private string password;

    public void SaveScene()
    {
        // Show save file dialog
        string savePath = EditorUtility.SaveFilePanel(
            "Save Scene",
            "Assets/Scenes",
            "",
            "json"
        );

        if (string.IsNullOrEmpty(savePath))
        {
            return;
        }

        // Get all GameObjects in the scene
        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();

        // Serialize the position, rotation, and scale of each GameObject
        List<GameObjectData> objectDataList = new List<GameObjectData>();
        foreach (GameObject obj in objects)
        {
            Transform transform = obj.transform;
            GameObjectData objectData = new GameObjectData
            {
                name = obj.name,
                position = transform.position,
                rotation = transform.rotation,
                scale = transform.localScale
            };
            objectDataList.Add(objectData);
        }

        // Serialize the object data to JSON
        string sceneJson = JsonUtility.ToJson(new SceneData { objects = objectDataList }, true);

        // Save the JSON to the specified file path
        File.WriteAllText(savePath, sceneJson);

        // Refresh the Asset Database
        AssetDatabase.Refresh();

        server = "127.0.0.1";
        database = "testbaseunity";
        uid = "root";
        password = "root";
        string connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

        // SQL-запрос на добавление пути файла в таблицу
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(savePath);
        string query = $"INSERT INTO tusks (Tusk, Name) VALUES ('{savePath}', '{fileNameWithoutExtension}')";

        // Создание объекта подключения к базе данных

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            // Открытие подключения
            connection.Open();

            // Создание объекта команды с SQL-запросом и подключением
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                // Выполнение команды
                command.ExecuteNonQuery();
            }
        }
    }

    // Helper class to store information about a GameObject
    [System.Serializable]
    private class GameObjectData
    {
        public string name;
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }

    // Helper class to store information about a scene
    [System.Serializable]
    private class SceneData
    {
        public List<GameObjectData> objects;
    }
}