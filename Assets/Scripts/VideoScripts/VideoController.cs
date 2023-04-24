using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage rawImage;

    public VideoClip[] videoClips;
    private int currentVideoIndex = 0;

    private void Start()
    {
        PlayNextVideo();
    }

    public void PlayNextVideo()
    {
        // ѕровер€ем, что индекс не выходит за границы массива
        if (currentVideoIndex >= videoClips.Length)
        {
            currentVideoIndex = 0;
        }

        videoPlayer.clip = videoClips[currentVideoIndex];
        videoPlayer.Play();
        rawImage.texture = videoPlayer.texture;
        currentVideoIndex++;
    }
}