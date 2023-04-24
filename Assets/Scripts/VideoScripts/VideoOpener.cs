using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoOpener : MonoBehaviour
{

    public GameObject video;

    public void OpenPanel()
    {
        if(video != null)
        {
            video.SetActive(true);
        }
    }

}
