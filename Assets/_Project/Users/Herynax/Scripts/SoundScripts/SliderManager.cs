using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using FMODUnity;
using TMPro;

public class SliderManager : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private TextMeshProUGUI _percantageTMPro;
    [SerializeField] private Slider _slider;
    [SerializeField] private EventReference _testSound;
    [SerializeField] private string _busPath;

    private FMOD.Studio.Bus _bus;




    private void Start()
    {

        if (!string.IsNullOrEmpty(_busPath))
        {
            _bus = RuntimeManager.GetBus(_busPath);
            _bus.getVolume(out float volume);
            _slider.value = volume;
            UpdateSliderOutput();
        }

    }

    public void UpdateSliderOutput()
    {
        if (_percantageTMPro != null && _slider != null)
        {
            float percentage = _slider.value * 100f;
            _percantageTMPro.text = $"{percentage:0}%";
            _bus.setVolume(_slider.value);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        RuntimeManager.PlayOneShot(_testSound);
    }
}
