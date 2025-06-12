using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.Gameplay.Canvases.Buttons
{
    public abstract class BaseCanvasButton : MonoBehaviour
    {
        [field: SerializeField] protected CanvasType TargetType;

        [SerializeField] private Button _button;

        protected CanvasesHandler Handler;

        private void OnValidate() => _button ??= GetComponent<Button>();

        [Inject]
        private void Construct(CanvasesHandler handler)
        {
            Handler = handler;

            _button.onClick.AddListener(delegate
            {
                OnClick();
            });
        }

        public virtual void OnClick() {}
    }
}
