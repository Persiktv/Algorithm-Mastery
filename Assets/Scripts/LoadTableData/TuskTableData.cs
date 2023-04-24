using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MySql.Data.MySqlClient;

public class TuskTableData : MonoBehaviour
{

    public TMP_Text databaseText;

    private MySqlConnection connection;
    private MySqlCommand command;
    private MySqlDataReader reader;
    private List<string[]> data = new List<string[]>();

    void Start()
    {
        string connectionString = "Server=127.0.0.1;Database=testbaseunity;Uid=root;Pwd=root;";
        connection = new MySqlConnection(connectionString);
        connection.Open();

        string query = "SELECT Name FROM tusks;";
        command = new MySqlCommand(query, connection);
        reader = command.ExecuteReader();

        while (reader.Read())
        {
            string Name = reader.GetString(0);
            data.Add(new string[] { Name });
        }
        reader.Close();
        connection.Close();

        foreach (string[] record in data)
        {
            string Name = record[0];
            databaseText.text += Name + "\n\n";

        }
    }
}