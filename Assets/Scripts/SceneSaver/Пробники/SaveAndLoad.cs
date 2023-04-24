using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveAndLoad : MonoBehaviour
{
    public UIManager uiManager;

    public void SaveScene2()
    {
        string savePath = Application.dataPath + "/SavedScenes";
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }
        string sceneName = SceneManager.GetActiveScene().name;
        string filePath = savePath + "/" + sceneName + ".json";

        // Get all root objects in the scene
        List<GameObject> rootObjects = new List<GameObject>(SceneManager.GetActiveScene().GetRootGameObjects());

        // Add UI elements from UIManager to rootObjects
        foreach (GameObject uiElement in uiManager.uiElements)
        {
            rootObjects.Add(uiElement);
        }

        SceneData sceneData = new SceneData(sceneName, rootObjects.ToArray());
        string sceneJson = JsonUtility.ToJson(sceneData);
        File.WriteAllText(filePath, sceneJson);
    }

    public void LoadScene2()
    {
        string sceneName = "Add Tusk Scene";
        string filePath = Application.dataPath + "/../Assets/Scenes/SavedScenes/" + sceneName + ".json";
        if (File.Exists(filePath))
        {
            string sceneJson = File.ReadAllText(filePath);
            SceneData sceneData = JsonUtility.FromJson<SceneData>(sceneJson);
            SceneManager.LoadScene(sceneData.sceneName);
            foreach (GameObject rootObject in sceneData.rootObjects)
            {
                Instantiate(rootObject);
            }
        }
    }
}

