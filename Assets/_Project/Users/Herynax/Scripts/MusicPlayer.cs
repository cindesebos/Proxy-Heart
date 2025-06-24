using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;

[System.Serializable]
public class TrackData
{
    public EventReference fmodEvent;
    public string displayName;   // Например: "Автор - Название трека"
    public Sprite coverImage;    // Обложка трека
}

public class MusicPlayer : MonoBehaviour
{
    [Header("FMOD")]
    [SerializeField] private List<TrackData> tracksData;

    [Header("UI - Buttons")]
    [SerializeField] private Button playPauseButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;

    [Header("UI - Progress")]
    [SerializeField] private Slider trackProgressSlider;

    [Header("UI - Playlist Buttons")]
    [SerializeField] private List<Button> playlistButtons;

    [Header("UI - Now Playing Panel")]
    [SerializeField] private TextMeshProUGUI nowPlayingText; // замените на Text, если не используете TMP
    [SerializeField] private Image coverImage;

    private EventInstance currentTrackInstance;
    private int currentTrackIndex = -1;
    private bool isPlaying = false;
    private bool isDraggingSlider = false;
    private bool pendingSeek = false;
    private float trackLengthSeconds = 0f;

    private float lockUpdateUntilTime = 0f;
    private const float lockDuration = 0.3f; // Время блокировки обновления слайдера после перемотки

    private void Start()
    {
        nextButton.onClick.AddListener(NextTrack);
        previousButton.onClick.AddListener(PreviousTrack);
        playPauseButton.onClick.AddListener(TogglePause);
        trackProgressSlider.onValueChanged.AddListener(OnSliderValueChanged);

        for (int i = 0; i < playlistButtons.Count && i < tracksData.Count; i++)
        {
            int index = i;
            playlistButtons[i].onClick.AddListener(() => PlayTrack(index));
        }
    }

    private void Update()
    {
        if (pendingSeek)
        {
            pendingSeek = false;

            if (currentTrackInstance.isValid() && trackLengthSeconds > 0)
            {
                float finalValue = trackProgressSlider.value;
                int newPositionMs = Mathf.FloorToInt(finalValue * trackLengthSeconds * 1000f);
                currentTrackInstance.setTimelinePosition(newPositionMs);
                lockUpdateUntilTime = Time.time + lockDuration;
            }

            return;
        }

        if (Time.time < lockUpdateUntilTime)
            return;

        if (isPlaying && currentTrackInstance.isValid() && !isDraggingSlider)
        {
            if (currentTrackInstance.getTimelinePosition(out int positionMs) == FMOD.RESULT.OK && trackLengthSeconds > 0)
            {
                float progress = (positionMs / 1000f) / trackLengthSeconds;
                trackProgressSlider.SetValueWithoutNotify(progress);
            }
        }
    }

    public void PlayTrack(int index)
    {
        if (index < 0 || index >= tracksData.Count)
            return;

        StopCurrentTrack();

        currentTrackIndex = index;
        TrackData track = tracksData[currentTrackIndex];

        currentTrackInstance = RuntimeManager.CreateInstance(track.fmodEvent);
        currentTrackInstance.start();
        isPlaying = true;

        currentTrackInstance.getDescription(out EventDescription desc);
        desc.getLength(out int lengthMs);
        trackLengthSeconds = lengthMs / 1000f;

        UpdateNowPlayingUI(track);
        lockUpdateUntilTime = 0f; // сброс блокировки
    }

    private void UpdateNowPlayingUI(TrackData track)
    {
        if (nowPlayingText != null)
            nowPlayingText.text = track.displayName;

        if (coverImage != null && track.coverImage != null)
            coverImage.sprite = track.coverImage;
    }

    public void NextTrack()
    {
        if (tracksData.Count == 0) return;

        int nextIndex = (currentTrackIndex + 1) % tracksData.Count;
        PlayTrack(nextIndex);
    }

    public void PreviousTrack()
    {
        if (tracksData.Count == 0) return;

        int prevIndex = (currentTrackIndex - 1 + tracksData.Count) % tracksData.Count;
        PlayTrack(prevIndex);
    }

    public void TogglePause()
    {
        if (!currentTrackInstance.isValid()) return;

        if (isPlaying)
        {
            currentTrackInstance.setPaused(true);
            isPlaying = false;
        }
        else
        {
            currentTrackInstance.setPaused(false);
            isPlaying = true;
        }
    }

    public void OnSliderValueChanged(float value)
    {
    }

    public void OnSliderDragStart()
    {
        isDraggingSlider = true;
    }

    public void OnSliderDragEnd()
    {
        isDraggingSlider = false;
        pendingSeek = true;
    }

    private void StopCurrentTrack()
    {
        if (currentTrackInstance.isValid())
        {
            currentTrackInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            currentTrackInstance.release();
        }
    }

    private void OnDestroy()
    {
        StopCurrentTrack();
    }
}
