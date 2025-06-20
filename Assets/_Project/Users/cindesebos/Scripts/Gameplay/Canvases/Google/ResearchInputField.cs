using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Scripts.Gameplay.Canvases.Google
{
    public class ResearchInputField : MonoBehaviour
    {
        [SerializeField] private ClueItemsDropdown[] _clueItemsDropdowns;
        [SerializeField] private TextMeshProUGUI _searchQueryText;

        private Dictionary<int, string> _selectedTitlesByDropdown;

        private void OnValidate()
        {
            if (_clueItemsDropdowns == null || _clueItemsDropdowns.Length == 0)
                _clueItemsDropdowns = GetComponentsInChildren<ClueItemsDropdown>();
        }

        private void Start()
        {
            _selectedTitlesByDropdown = new Dictionary<int, string>();

            foreach (var dropdown in _clueItemsDropdowns)
            {
                dropdown.OnClueSelected += AddTitleToList;
                dropdown.OnClueDeselected += RemoveTitleFromList;
            }
        }

        private void AddTitleToList(string title, int dropdownId)
        {
            foreach (var kvp in _selectedTitlesByDropdown)
            {
                if (kvp.Key != dropdownId && kvp.Value == title) return;
            }

            if (_selectedTitlesByDropdown.ContainsKey(dropdownId))
            {
                RemoveTitleFromList(title, dropdownId);

                return;
            }

            _selectedTitlesByDropdown[dropdownId] = title;

            UpdateSearchText();
        }

        private void RemoveTitleFromList(string title, int dropdownId)
        {
            if (_selectedTitlesByDropdown.TryGetValue(dropdownId, out var storedTitle) && storedTitle == title)
            {
                _selectedTitlesByDropdown.Remove(dropdownId);

                UpdateSearchText();
            }
        }

        private void UpdateSearchText()
        {
            _searchQueryText.text = string.Join(" + ", _selectedTitlesByDropdown.Values);
        }

        private void OnDestroy()
        {
            foreach (var dropdown in _clueItemsDropdowns)
            {
                dropdown.OnClueSelected -= AddTitleToList;
                dropdown.OnClueDeselected -= RemoveTitleFromList;
            }
        }
    }
}