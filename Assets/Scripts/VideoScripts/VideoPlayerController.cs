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
        // ”становка первого видео
        videoPlayer.clip = videoClips[currentClipIndex];
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += (source) =>
        {
            rawImage.texture = videoPlayer.texture;
            videoPlayer.Play();
        };

        // ”становка временной шкалы
        timeSlider.minValue = 0;
        timeSlider.maxValue = (float)videoPlayer.clip.length;

        // ѕрив€зка свойства value слайдера к свойству time видеоплеера
        timeSlider.onValueChanged.AddListener(delegate { videoPlayer.time = timeSlider.value; });

        // «аполнение списка скоростей воспроизведени€
        if (playbackSpeedDropdown != null)
        {
            foreach (float speed in playbackSpeeds)
            {
                playbackSpeedDropdown.options.Add(new TMP_Dropdown.OptionData(speed.ToString()));
            }

            // ”становка стандартного значени€ списка
            playbackSpeedDropdown.value = playbackSpeeds.IndexOf(1.0f);
        }

        // –егистраци€ метода SetPlaybackSpeed как обработчика событи€ выбора значени€ в списке
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

        // ”становка значени€ слайдера времени воспроизведени€
        timeSlider.value = (float)videoPlayer.time;

        // ѕроверка окончани€ видео
        if (videoPlayer.time >= videoPlayer.clip.length)
        {
            timeSlider.value = timeSlider.maxValue;
        }

    }

    public void PlayNextVideo()
    {
        // ѕроверка просмотра текущего видео на 90%
        float progress = (float)videoPlayer.frame / (float)videoPlayer.frameCount;
        if (progress < 0.9f)
        {
            // ¬ывод сообщени€ о том, что видео не просмотрено на 90%
            messageText.text = "¬ы не просмотрели это видео на 90%.";
            return;
        }

        // ѕроигрывание следующего видео
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
            messageText.text = ""; // ќчистка сообщени€
        };
    }

    public void PlayVideo(int clipIndex)
    {
        playIcon.SetActive(false);
        // ѕроверка наличи€ видео по указанному индексу
        if (clipIndex < 0 || clipIndex >= videoClips.Length)
        {
            Debug.LogError("Video clip with index " + clipIndex + " not found.");
            return;
        }

        // ќстановка текущего видео и воспроизведение нового
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
        // ѕолучение скорости воспроизведени€ из списка
        float playbackSpeed = playbackSpeeds[speedIndex];

        // ”становка скорости воспроизведени€
        videoPlayer.playbackSpeed = playbackSpeed;
    }
}