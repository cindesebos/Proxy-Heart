using Cysharp.Threading.Tasks;
using FlatBuffersSetup.Gameplay;
using Scripts.Gameplay.Clues.Initializer;
using TMPro;
using UnityEngine.EventSystems;
using Zenject;
using UnityEngine;
using Scripts.Inventory;

namespace Scripts.Gameplay.Clues
{
    public class ClueObject : MonoBehaviour, IClue, IPointerDownHandler
    {
        [field: SerializeField] public string TypeId { get; private set; }

        public string TitleLid { get; private set; }

        private IClueInitializer _clueInitializer;
        private IInventory _inventory;
        private ClueObjectSettingsT _settings;

        private bool _isUsed;

        [Inject]
        private void Construct(IClueInitializer clueInitializer, IInventory inventory)
        {
            _clueInitializer = clueInitializer;
            _inventory = inventory;
        }

        private async UniTask Start()
        {
            Debug.Log("Started");

            _settings = await _clueInitializer.InitializeClueObjectById(TypeId);

            await Initialize();
        }

        public async UniTask Initialize()
        {
            Debug.Log("Initialized");

            TypeId = _settings.TypeId;
            TitleLid = _settings.TitleLid;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_isUsed && _inventory.TryAddClue(this))
            {
                _isUsed = true;

                Debug.Log($"You clicked on {TitleLid}");
            }

        }
    }
}
