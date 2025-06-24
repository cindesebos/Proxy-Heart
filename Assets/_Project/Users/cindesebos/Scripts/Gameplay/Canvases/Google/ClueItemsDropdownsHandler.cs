using System;
using System.Collections.Generic;
using Scripts.Gameplay.Clues;
using Scripts.Inventory;
using TMPro;
using UnityEngine;
using Zenject;

namespace Scripts.Gameplay.Canvases.Google
{
    public class ClueItemsDropdownsHandler : MonoBehaviour
    {
        public event Action<IClue, int> OnClueSelected;
        public event Action<IClue, int> OnClueDeselected;

        [SerializeField]
        private TMP_Dropdown _mainDropdown;

        [SerializeField]
        private TMP_Dropdown _secondDropdown;

        private IInventory _inventory;
        private List<IClue> _clues = new();
        private Dictionary<int, int> _selectedIndices = new();
        private List<IClue> _filteredClues = new();
        private List<TMP_Dropdown.OptionData> _tempOptions = new();

        private bool _isSecondDropdownActivated;

        [Inject]
        private void Construct(IInventory inventory)
        {
            _inventory = inventory;
            _inventory.OnClueAdded += AddClue;
        }

        private void Awake()
        {
            _mainDropdown.gameObject.SetActive(false);
            _secondDropdown.gameObject.SetActive(false);

            _selectedIndices[0] = -1;
            _selectedIndices[1] = -1;

            _mainDropdown.onValueChanged.AddListener(index => OnDropdownChanged(index, 0));
            _secondDropdown.onValueChanged.AddListener(index => OnDropdownChanged(index, 1));

            RefreshDropdowns();
        }

        public void ToggleDropdowns()
        {
            _mainDropdown.gameObject.SetActive(!_mainDropdown.gameObject.activeSelf);

            if (_mainDropdown.gameObject.activeSelf == false)
            {
                _secondDropdown.gameObject.SetActive(false);
            }
            else if (_isSecondDropdownActivated)
            {
                _secondDropdown.gameObject.SetActive(true);
            }
        }

        public void TryShowSecondDropdown()
        {
            if (_mainDropdown.gameObject.activeSelf == true)
                _secondDropdown.gameObject.SetActive(true);
        }

        private void AddClue(IClue clue)
        {
            _clues.Add(clue);
            RefreshDropdowns();
        }

        private void RefreshDropdowns()
        {
            UpdateDropdownOptions(_mainDropdown, 0);
            UpdateDropdownOptions(_secondDropdown, 1);
        }

        private void UpdateDropdownOptions(TMP_Dropdown dropdown, int dropdownId)
        {
            TryShowSecondDropdown();

            dropdown.ClearOptions();
            _tempOptions.Clear();
            _filteredClues.Clear();

            if (dropdownId == 1)
            {
                _tempOptions.Add(new TMP_Dropdown.OptionData("—"));
            }

            foreach (var clue in _clues)
            {
                bool isSelectedInOther = false;
                foreach (var kvp in _selectedIndices)
                {
                    if (
                        kvp.Key != dropdownId
                        && kvp.Value >= 0
                        && _clues[kvp.Value].TitleLid == clue.TitleLid
                    )
                    {
                        isSelectedInOther = true;
                        break;
                    }
                }

                if (!isSelectedInOther)
                {
                    _filteredClues.Add(clue);
                    _tempOptions.Add(new TMP_Dropdown.OptionData(clue.TitleLid));
                }
            }

            dropdown.AddOptions(_tempOptions);

            int currentIndex = _selectedIndices[dropdownId];
            if (currentIndex >= 0 && currentIndex < _clues.Count)
            {
                string selectedTitle = _clues[currentIndex].TitleLid;
                int optionIndex = _tempOptions.FindIndex(o => o.text == selectedTitle);

                if (optionIndex != -1)
                {
                    dropdown.SetValueWithoutNotify(optionIndex);
                    return;
                }
            }

            dropdown.SetValueWithoutNotify(0);
        }

        private void OnDropdownChanged(int dropdownIndex, int dropdownId)
        {
            var dropdown = dropdownId == 0 ? _mainDropdown : _secondDropdown;

            if (dropdownId == 1 && dropdownIndex == 0)
            {
                DeselectClue(dropdownId);
                return;
            }

            if (dropdownId == 1 && !_isSecondDropdownActivated)
            {
                _isSecondDropdownActivated = true;
            }

            int actualIndex = dropdownId == 1 ? dropdownIndex - 1 : dropdownIndex;

            _filteredClues.Clear();
            foreach (var clue in _clues)
            {
                bool isSelectedInOther = false;
                foreach (var kvp in _selectedIndices)
                {
                    if (
                        kvp.Key != dropdownId
                        && kvp.Value >= 0
                        && _clues[kvp.Value].TitleLid == clue.TitleLid
                    )
                    {
                        isSelectedInOther = true;
                        break;
                    }
                }

                if (!isSelectedInOther)
                    _filteredClues.Add(clue);
            }

            if (actualIndex < 0 || actualIndex >= _filteredClues.Count)
                return;

            var selectedClue = _filteredClues[actualIndex];

            if (_selectedIndices[dropdownId] != -1 && _selectedIndices[dropdownId] < _clues.Count)
                OnClueDeselected?.Invoke(_clues[_selectedIndices[dropdownId]], dropdownId);

            _selectedIndices[dropdownId] = _clues.IndexOf(selectedClue);
            OnClueSelected?.Invoke(selectedClue, dropdownId);

            RefreshDropdowns();
        }

        private void DeselectClue(int dropdownId)
        {
            if (_selectedIndices[dropdownId] != -1 && _selectedIndices[dropdownId] < _clues.Count)
            {
                OnClueDeselected?.Invoke(_clues[_selectedIndices[dropdownId]], dropdownId);

                _selectedIndices[dropdownId] = -1;

                RefreshDropdowns();
            }
        }

        private void OnDestroy()
        {
            if (_inventory != null)
                _inventory.OnClueAdded -= AddClue;

            _mainDropdown.onValueChanged.RemoveAllListeners();
            _secondDropdown.onValueChanged.RemoveAllListeners();
        }
    }
}
