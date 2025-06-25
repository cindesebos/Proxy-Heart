using FlatBuffersSetup.Gameplay;
using Scripts.Inventory.Groups;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.Gameplay.Canvases.Dialogue
{
    public class StartTopicButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _topicText;

        private bool _isInitialized = false;
        private ClueGroup _clueGroup;

        private void OnValidate()
        {
            _button ??= GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            _clueGroup.OnVariantWasUsed();
        }

        public void Initialize(string topic, ClueGroup clueGroup)
        {
            _isInitialized = true;
            _clueGroup = clueGroup;

            _topicText.text = topic;
        }
    }
}
