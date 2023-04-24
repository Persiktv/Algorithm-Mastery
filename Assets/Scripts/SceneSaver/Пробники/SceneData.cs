using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneData
{
    public string sceneName;
    public GameObject[] rootObjects;

    public SceneData(string sceneName, GameObject[] rootObjects)
    {
        this.sceneName = sceneName;
        this.rootObjects = rootObjects;
    }
}
