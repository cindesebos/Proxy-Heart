using System;
using System.Collections.Generic;
using Scripts.Gameplay.Clues;
using Scripts.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.Gameplay.Canvases.Google
{
    public class ClueItemsDropdown : MonoBehaviour
    {
        public event Action<string, int> OnClueSelected;
        public event Action<string, int> OnClueDeselected;

        [SerializeField] private TMP_Dropdown _dropdown;
        [SerializeField] private int _id;

        private IInventory _inventory;
        private List<IClue> _clues = new();
        private int _lastSelectedIndex = -1;

        private void OnValidate() => _dropdown ??= GetComponentInChildren<TMP_Dropdown>();

        [Inject]
        private void Construct(IInventory inventory)
        {
            _inventory = inventory;
            _inventory.OnClueAdded += AddItem;
            _dropdown.onValueChanged.AddListener(OnDropdownChanged);
        }

        private void AddItem(IClue clue)
        {
            _clues.Add(clue);
            _dropdown.options.Add(new TMP_Dropdown.OptionData(clue.TitleLid));
            _dropdown.RefreshShownValue();
        }

        private void OnDropdownChanged(int index)
        {
            if (_lastSelectedIndex != -1 && _lastSelectedIndex < _clues.Count)
            {
                var deselectedClue = _clues[_lastSelectedIndex];

                OnClueDeselected?.Invoke(deselectedClue.TitleLid, _id);
            }

            if (index >= 0 && index < _clues.Count)
            {
                var selectedClue = _clues[index];

                OnClueSelected?.Invoke(selectedClue.TitleLid, _id);

                _lastSelectedIndex = index;
            }
        }

        public void Toggle()
        {
            if (_dropdown.gameObject.activeSelf) Close();
            else Open();
        }

        private void Open()
        {
            _dropdown.gameObject.SetActive(true);
        }

        private void Close()
        {
            _dropdown.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            if (_inventory != null)
                _inventory.OnClueAdded -= AddItem;

            _dropdown.onValueChanged.RemoveListener(OnDropdownChanged);
        }
    }
}