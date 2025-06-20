using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FpsManager : MonoBehaviour
{
    public static FpsManager Instance;

    [Header("UI References")]
    public Slider fpsSlider;
    public TextMeshProUGUI fpsLimitText;

    public Slider fpsDisplayToggleSlider;
    public TextMeshProUGUI fpsDisplayStatusText;
    public GameObject fpsDisplayObject;

    public Slider vsyncSlider;
    public TextMeshProUGUI vsyncStatusText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadSettings();

        fpsSlider.onValueChanged.AddListener(OnFpsSliderChanged);
        fpsDisplayToggleSlider.onValueChanged.AddListener(OnDisplayToggleChanged);
        vsyncSlider.onValueChanged.AddListener(OnVsyncSliderChanged);

        // ��������� ��������� ��������
        ApplyVSync();         // �����: ������� VSync
        ApplyTargetFps();     // ����� FPS
        ApplyFpsDisplay();
    }

    private void OnFpsSliderChanged(float value)
    {
        PlayerPrefs.SetInt("fpsValue", Mathf.RoundToInt(value));
        ApplyTargetFps();
    }

    private void OnDisplayToggleChanged(float value)
    {
        PlayerPrefs.SetInt("fpsDisplay", value > 0.5f ? 1 : 0);
        ApplyFpsDisplay();
    }

    private void OnVsyncSliderChanged(float value)
    {
        PlayerPrefs.SetInt("vsync", value > 0.5f ? 1 : 0);
        ApplyVSync();
    }

    private void ApplyTargetFps()
    {
        int intValue = PlayerPrefs.GetInt("fpsValue", 2);
        int targetFPS;
        string label;

        switch (intValue)
        {
            case 0: targetFPS = 15; label = "15 FPS"; break;
            case 1: targetFPS = 30; label = "30 FPS"; break;
            case 2: targetFPS = 60; label = "60 FPS"; break;
            case 3: targetFPS = 144; label = "144 FPS"; break;
            case 4: targetFPS = -1; label = "none"; break;
            default: targetFPS = 60; label = "60 FPS"; break;
        }

        Application.targetFrameRate = targetFPS;
        fpsLimitText.text = label;
    }

    private void ApplyVSync()
    {
        int vsync = PlayerPrefs.GetInt("vsync", 1);
        QualitySettings.vSyncCount = vsync;
        vsyncStatusText.text = vsync == 1 ? "ВКЛ" : "ВЫКЛ";
    }

    private void ApplyFpsDisplay()
    {
        bool show = PlayerPrefs.GetInt("fpsDisplay", 0) == 1;

        if (fpsDisplayObject != null)
            fpsDisplayObject.SetActive(show);

        fpsDisplayStatusText.text = show ? "ВКЛ" : "ВЫКЛ";
    }

    private void LoadSettings()
    {
        fpsSlider.value = PlayerPrefs.GetInt("fpsValue", 2);
        fpsDisplayToggleSlider.value = PlayerPrefs.GetInt("fpsDisplay", 0);
        vsyncSlider.value = PlayerPrefs.GetInt("vsync", 1);
    }
}
