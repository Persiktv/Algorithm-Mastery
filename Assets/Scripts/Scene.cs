using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MySql.Data.MySqlClient;

public class Scene : MonoBehaviour
{

    public float delay = 1f; // задержка в секундах

    public void LoadNextSceneWithDelay(int idScene)
    {
        StartCoroutine(LoadNextSceneCoroutine(idScene));
    }

    private IEnumerator LoadNextSceneCoroutine(int idScene)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(idScene);
    }

}
