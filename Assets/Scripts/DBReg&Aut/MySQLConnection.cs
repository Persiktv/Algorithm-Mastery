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
    public float delay = 1f; // задержка в секундах
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


    // Инициализация соединения с базой данных
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
            Debug.Log("Подключение к базе данных MySQL");
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Ошибка подключения к базе данных MySQL: " + ex.ToString());
        }
    }

    // Выполнение запроса к базе данных
    public void ExecuteQuery(string query)
    {
        if (connection.State == ConnectionState.Open)
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
        }
        else
        {
            Debug.LogError("MySQL подключено успешно!");
        }
    }

    public void Login()
    {
        string login = loginInput.text;
        string password = passwordInput.text;

        // Запрос к базе данных на поиск пользователя
        string query = "SELECT * FROM users WHERE Login='" + login + "' AND Password='" + password + "'";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        MySqlDataReader dataReader = cmd.ExecuteReader();

        // Если пользователь найден, получаем его роль
        if (dataReader.HasRows)
        {
            messageText.color = Color.green;
            messageText.text = "Добро пожаловать " + login + "!";

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

            // Определяем какую форму открыть в зависимости от роли пользователя
            if (userRole == "Студент")
            {
                StartCoroutine(LoadNextScene("Student Scene", 1f)); // Задержка в 1 секунды
            }
            else if (userRole == "Преподаватель")
            {
                StartCoroutine(LoadNextScene("Teacher Scene", 1f)); // Задержка в 1 секунды
            }
            else if (userRole == "Админ")
            {
                StartCoroutine(LoadNextScene("Admin Scene", 1f)); // Задержка в 1 секунды
            }
        }
        else
        {
            dataReader.Close();
            messageText.text = "Неправильный Логин или Пароль!";
        }
    }

    public void GetSelectedOption()
    {
        // явно создаем новый объект типа TMP_Dropdown.OptionDataList
        TMP_Dropdown.OptionDataList options = new TMP_Dropdown.OptionDataList();

        // копируем данные из списка dropdown.options в новый объект options
        foreach (TMP_Dropdown.OptionData option in dropdown.options)
        {
            options.options.Add(new TMP_Dropdown.OptionData(option.text));
        }

        // Получаем индекс выбранного элемента
        int selectedOptionIndex = dropdown.value;

        // Получаем текст выбранного элемента
        string selectedOptionText = options.options[selectedOptionIndex].text;

        // Выводим текст выбранного элемента в консоль
        Debug.Log("Выбран элемент: " + selectedOptionText);
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
            Debug.Log("Новый пользователь добавлен.");
        }
        catch (Exception ex)
        {
            Debug.Log("Ошибка при добавлении пользователя: " + ex.Message);
        }

        loginInput.text = "";
        passwordInput.text = "";
    }
}
