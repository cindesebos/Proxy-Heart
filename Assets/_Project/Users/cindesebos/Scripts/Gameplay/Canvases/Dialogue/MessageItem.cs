using TMPro;
using UnityEngine;

namespace Scripts.Gameplay.Canvases.Dialogue
{
    public class MessageItem : MonoBehaviour
    {
        [SerializeField] private RectTransform _background;

        [SerializeField] private TextMeshProUGUI _text;

        [SerializeField] private Vector2 _padding = new Vector2(20, 0);

        private RectTransform _rectTransform;

        private void Awake() => _rectTransform = GetComponent<RectTransform>();

        public void SetValue(string message)
        {
            _text.text = message;

            Canvas.ForceUpdateCanvases();

            float maxWidth = _text.rectTransform.rect.width;
            Vector2 preferredSize = _text.GetPreferredValues(message, maxWidth, 0f);

            float textWidth = Mathf.Min(preferredSize.x, maxWidth);
            float textHeight = preferredSize.y;

            _text.rectTransform.sizeDelta = new Vector2(textWidth, textHeight);

            Vector2 bgSize = new Vector2(
                textWidth + _padding.x,
                textHeight + _padding.y
            );

            _background.sizeDelta = bgSize;
            _rectTransform.sizeDelta = bgSize;
        }
    }
}
