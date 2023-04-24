using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class DownloadFile : MonoBehaviour
{
    private string _url = "https://getfile.dokpub.com/yandex/get/https://disk.yandex.ru/d/mAAkR4-WArsLEg";
    private string _filePath;
    
    private void Start()
    {
        _filePath = Path.Combine(Application.dataPath, "DownloadedFiles");

        if (!Directory.Exists(_filePath))
        {
            Directory.CreateDirectory(_filePath);
        }

        _filePath = Path.Combine(_filePath, "DownloadedFile.zip"); 
        StartCoroutine(DownloadFileCoroutine(_url, _filePath));
    }

    private IEnumerator DownloadFileCoroutine(string url, string savePath)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.disposeDownloadHandlerOnDispose = false;
            www.disposeUploadHandlerOnDispose = false;

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                File.WriteAllBytes(savePath, www.downloadHandler.data);
                Debug.Log("Файл успешно сохранен в: " + savePath);
            }
        }
    }
}