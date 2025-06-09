using Cysharp.Threading.Tasks;
using FlatBuffersSetup.Gameplay;
using Scripts.Gameplay.Clues.Initializer;
using TMPro;
using UnityEngine;
using Zenject;

namespace Scripts.Gameplay.Clues
{
    public class ClueClickable : MonoBehaviour, IClue
    {
        [field: SerializeField] public string TypeId { get; private set; }

        public string TitleLid { get; private set; }

        [SerializeField] private TextMeshProUGUI _text;

        private IClueInitializer _clueInitializer;

        private void OnValidate() => _text ??= GetComponent<TextMeshProUGUI>();

        [Inject]
        private void Construct(IClueInitializer clueInitializer)
        {
            _clueInitializer = clueInitializer;
        }

        private async UniTask Start()
        {
            Debug.Log("Started");

            var settings = await _clueInitializer.InitializeClueById(TypeId);

            await Initialize(settings);
        }

        public async UniTask Initialize(ItemSettingsT settings)
        {
            Debug.Log("Initialized");

            TypeId = settings.TypeId;
            TitleLid = settings.TitleLid;

            _text.text = TitleLid;
        }
    }
}
