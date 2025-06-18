using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Gameplay.Canvases
{
    public class DialogueItem : MonoBehaviour
    {
        [SerializeField] private RectTransform _parentContainer;
        [SerializeField] private RectTransform _backgroundPanel;
        [SerializeField] private TextMeshProUGUI _text;
        [Space]

        [SerializeField] private Vector2 _padding = new Vector2(-5f, 5f);
        [SerializeField] private float _horizontalOffset = 10f;

        public void SetTextValue(string message)
        {
            _text.text = message;

            Resize();
        }

        public void Resize()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(_text.rectTransform);

            float currentWidth = _text.rectTransform.rect.width;
            float preferredHeight = _text.preferredHeight;

            Vector2 targetSize = new Vector2(currentWidth + _padding.x, preferredHeight + _padding.y);

            _backgroundPanel.sizeDelta = targetSize;
            _parentContainer.sizeDelta = targetSize;

            _backgroundPanel.localPosition = Vector3.zero;

            _backgroundPanel.localPosition += new Vector3(_horizontalOffset, 0f, 0f);
        }
    }
}
