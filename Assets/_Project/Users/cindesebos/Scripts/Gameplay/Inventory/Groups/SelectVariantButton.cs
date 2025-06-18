using Cysharp.Threading.Tasks;
using FlatBuffersSetup.Gameplay;
using Scripts.Gameplay.Clues.Initializer;
using TMPro;
using UnityEngine;
using Zenject;

namespace Scripts
{
    public class SelectVariantButton : MonoBehaviour
    {
        [field: SerializeField] public string TypeId { get; private set; }

        public string TitleLid { get; private set; }

        [SerializeField] private TextMeshProUGUI _text;

        private IClueInitializer _clueInitializer;
        private ClueGroupSettingsT _settings;

        private void OnValidate() => _text ??= GetComponent<TextMeshProUGUI>();

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
    }
}
