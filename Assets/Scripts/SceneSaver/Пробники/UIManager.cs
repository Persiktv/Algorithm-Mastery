using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


public class UIManager : MonoBehaviour
{
    public List<GameObject> uiElements = new List<GameObject>();

    public void AddUIElement(GameObject uiElement)
    {
        uiElements.Add(uiElement);
    }

    public void RemoveUIElement(GameObject uiElement)
    {
        uiElements.Remove(uiElement);
    }
}
