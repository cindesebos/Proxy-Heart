using System.Collections.Generic;
using Scripts.Gameplay.Canvases.Dialogue;
using Scripts.Inventory;
using UnityEngine;
using Zenject;

namespace Scripts.Inventory.Groups
{
    public class ClueGroup : MonoBehaviour
    {
        [field: SerializeField] public int Id { get; private set; }

        [SerializeField] private List<ClueDisplayer> _displayers;
        [SerializeField] private List<SelectVariantButton> _selectVariantButtons;
        [SerializeField] private GameObject _showVariantsButton;

        private int _currentDispalyersCount;
        private DialogueHandler _dialogueHandler;
        private SelectVariantButton _selectedVariantButton;

        [Inject]
        private void Construct(DialogueHandler dialogueHandler)
        {
            _dialogueHandler = dialogueHandler;
        }

        private void Start()
        {
            foreach (var displayer in _displayers)
            {
                displayer.OnClueDisplayed += OnClueDisplayed;
            }

            foreach (var selectVariantButton in _selectVariantButtons) selectVariantButton.Hide();

            _showVariantsButton.SetActive(false);
        }

        private void OnClueDisplayed(ClueDisplayer displayer)
        {
            _currentDispalyersCount++;

            if (_displayers.Count > _currentDispalyersCount)
            {
                ShowVariants();
                _showVariantsButton.SetActive(true);
            }
        }

        public void ShowVariants()
        {
            foreach (var selectVariantButton in _selectVariantButtons) selectVariantButton.Show();
        }

        public void SelectVariant(SelectVariantButton selectedVariantButton)
        {
            _selectedVariantButton = selectedVariantButton;

            foreach (var selectVariantButton in _selectVariantButtons) selectVariantButton.Hide();

            _selectedVariantButton.Show();

            _dialogueHandler.SelectTopicVarian(this, _selectedVariantButton.TypeId);
        }

        public void OnVariantWasUsed()
        {
            foreach (var selectVariantButton in _selectVariantButtons) selectVariantButton.Hide();

            _showVariantsButton.SetActive(false);

            _selectedVariantButton.ShowAndLock();

        }
    }
}
