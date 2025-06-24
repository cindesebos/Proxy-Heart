using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderDragHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public MusicPlayer musicPlayer;

    private Slider slider;
    private RectTransform rectTransform;

    private bool isDragging = false;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        rectTransform = GetComponent<RectTransform>();

        slider.interactable = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        musicPlayer.OnSliderDragStart();
        isDragging = true;
        UpdateSliderFromPointer(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            UpdateSliderFromPointer(eventData);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isDragging)
        {
            UpdateSliderFromPointer(eventData);
            isDragging = false;
            musicPlayer.OnSliderDragEnd();
        }
    }

    private void UpdateSliderFromPointer(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            float pct = Mathf.InverseLerp(rectTransform.rect.xMin, rectTransform.rect.xMax, localPoint.x);
            slider.SetValueWithoutNotify(Mathf.Clamp01(pct));
        }
    }
}
