using UnityEngine;

namespace Scripts.Gameplay.Canvases
{
    public class BaseCanvas : MonoBehaviour
    {
        [field: SerializeField] public CanvasType CurrentType { get; private set; }
        [field: SerializeField] public Canvas Canvas { get; private set; }
        [SerializeField] private GameObject _icon;

        private void OnValidate() => Canvas ??= GetComponent<Canvas>();

        public void Open()
        {
            Canvas.enabled = true;
            _icon.SetActive(true);
        }

        public void Close()
        {
            Canvas.enabled = false;
            _icon.SetActive(false);
        }

        public void Toggle()
        {
            Canvas.enabled = !Canvas.enabled;
        }

        public void SetCanvasOrder(int order) => Canvas.sortingOrder = order;
    }
}
