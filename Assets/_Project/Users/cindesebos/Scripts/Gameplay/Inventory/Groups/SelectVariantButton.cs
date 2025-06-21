using Cysharp.Threading.Tasks;
using FlatBuffersSetup.Gameplay;
using Google.Apis.Auth.OAuth2.Requests;
using Scripts.Gameplay.Clues.Initializer;
using Scripts.Inventory.Groups;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Scripts
{
    public class SelectVariantButton : MonoBehaviour, IPointerClickHandler
    {
        [field: SerializeField] public string TypeId { get; private set; }

        public string TitleLid { get; private set; }

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private ClueGroup _parentClueGroup;

        private IClueInitializer _clueInitializer;
        private ClueGroupSettingsT _settings;
        private bool _isLock = false;

        private void OnValidate()
        {
            _text ??= GetComponent<TextMeshProUGUI>();
            _parentClueGroup ??= GetComponentInParent<ClueGroup>();
        }

        [Inject]
        private void Construct(IClueInitializer clueInitializer)
        {
            _clueInitializer = clueInitializer;
        }

        private async UniTask Start()
        {
            _text.enabled = false;

            _settings = await _clueInitializer.InitializeGroupVariantById(TypeId);

            await Initialize();
        }

        public async UniTask Initialize()
        {
            TitleLid = _settings.TitleLid;

            _text.text = TitleLid;
        }

        public void Show() => _text.enabled = true;

        public void ShowAndLock()
        {
            _text.enabled = true;

            _isLock = true;
        }

        public void Hide() => _text.enabled = false;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isLock == true) return;

            _parentClueGroup.SelectVariant(this);
        }
    }
}
