using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections.Generic;
using TMPro;

public class VideoPlayerController : MonoBehaviour
{
    public TMP_Text messageText;
    public RawImage rawImage;
    public GameObject playIcon;
    public TMP_Dropdown playbackSpeedDropdown;
    public VideoPlayer videoPlayer;
    public Slider timeSlider;

    public VideoClip[] videoClips;

    private int currentClipIndex = 0;
    private List<float> playbackSpeeds = new List<float> { 0.5f, 1.0f, 1.5f, 2.0f };

    void Start()
    {
        // ��������� ������� �����
        videoPlayer.clip = videoClips[currentClipIndex];
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += (source) =>
        {
            rawImage.texture = videoPlayer.texture;
            videoPlayer.Play();
        };

        // ��������� ��������� �����
        timeSlider.minValue = 0;
        timeSlider.maxValue = (float)videoPlayer.clip.length;

        // �������� �������� value �������� � �������� time �����������
        timeSlider.onValueChanged.AddListener(delegate { videoPlayer.time = timeSlider.value; });

        // ���������� ������ ��������� ���������������
        if (playbackSpeedDropdown != null)
        {
            foreach (float speed in playbackSpeeds)
            {
                playbackSpeedDropdown.options.Add(new TMP_Dropdown.OptionData(speed.ToString()));
            }

            // ��������� ������������ �������� ������
            playbackSpeedDropdown.value = playbackSpeeds.IndexOf(1.0f);
        }

        // ����������� ������ SetPlaybackSpeed ��� ����������� ������� ������ �������� � ������
        if (playbackSpeedDropdown != null)
        {
            playbackSpeedDropdown.onValueChanged.AddListener(SetPlaybackSpeed);
        }
    }

    void Update()
    {
        if (!videoPlayer.isPlaying && videoPlayer.time >= videoPlayer.clip.length)
        {
            PlayNextVideo();
        }

        // ��������� �������� �������� ������� ���������������
        timeSlider.value = (float)videoPlayer.time;

        // �������� ��������� �����
        if (videoPlayer.time >= videoPlayer.clip.length)
        {
            timeSlider.value = timeSlider.maxValue;
        }

    }

    public void PlayNextVideo()
    {
        // �������� ��������� �������� ����� �� 90%
        float progress = (float)videoPlayer.frame / (float)videoPlayer.frameCount;
        if (progress < 0.9f)
        {
            // ����� ��������� � ���, ��� ����� �� ����������� �� 90%
            messageText.text = "�� �� ����������� ��� ����� �� 90%.";
            return;
        }

        // ������������ ���������� �����
        currentClipIndex++;
        if (currentClipIndex >= videoClips.Length)
        {
            currentClipIndex = 0;
        }
        videoPlayer.clip = videoClips[currentClipIndex];
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += (source) =>
        {
            rawImage.texture = videoPlayer.texture;
            videoPlayer.Play();
            messageText.text = ""; // ������� ���������
        };
    }

    public void PlayVideo(int clipIndex)
    {
        playIcon.SetActive(false);
        // �������� ������� ����� �� ���������� �������
        if (clipIndex < 0 || clipIndex >= videoClips.Length)
        {
            Debug.LogError("Video clip with index " + clipIndex + " not found.");
            return;
        }

        // ��������� �������� ����� � ��������������� ������
        videoPlayer.Stop();
        currentClipIndex = clipIndex;
        videoPlayer.clip = videoClips[currentClipIndex];
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += (source) =>
        {
            rawImage.texture = videoPlayer.texture;
            videoPlayer.Play();
        };
    }

    public void PauseVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            playIcon.SetActive(true);
        }
        else
        {
            videoPlayer.Play();
            playIcon.SetActive(false);
        }
    }

    public void SetPlaybackSpeed(int speedIndex)
    {
        // ��������� �������� ��������������� �� ������
        float playbackSpeed = playbackSpeeds[speedIndex];

        // ��������� �������� ���������������
        videoPlayer.playbackSpeed = playbackSpeed;
    }
}