using System;
using Cysharp.Threading.Tasks;
using Scripts.Gameplay.Clues;
using Scripts.Gameplay.Clues.Initializer;
using TMPro;
using UnityEngine;
using Zenject;

namespace Scripts.Inventory
{
    public class ClueDisplayer : MonoBehaviour, IClue
    {
        public event Action<ClueDisplayer> OnClueDisplayed;

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
            _text.enabled = false;

            var clueObject = await _clueInitializer.InitializeClueObjectById(TypeId);
            
            TitleLid = clueObject.TitleLid;

            await Initialize();
        }

        public async UniTask Initialize()
        {
            _text.text = TitleLid;
        }

        public void Display()
        {
            _text.enabled = true;

            OnClueDisplayed?.Invoke(this);
        }
    }
}