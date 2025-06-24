using System.Collections.Generic;
using System.Text;
using Scripts.Gameplay.Clues;
using TMPro;
using UnityEngine;

namespace Scripts.Gameplay.Canvases.Google
{
    public class ResearchInputField : MonoBehaviour
    {
        [SerializeField] private ClueItemsDropdownsHandler _dropdownsHandler;
        [SerializeField] private TextMeshProUGUI _searchQueryText;
        [SerializeField] private string _currentInput;

        private Dictionary<int, IClue> _selectedCluesByDropdown = new();
        private StringBuilder _inputBuilder = new();
        private StringBuilder _titleBuilder = new();


        private void OnValidate()
        {
            _dropdownsHandler ??= GetComponentInParent<ClueItemsDropdownsHandler>();
        }

        private void Start()
        {
            _dropdownsHandler.OnClueSelected += OnClueSelected;
            _dropdownsHandler.OnClueDeselected += OnClueDeselected;
        }

        private void OnClueSelected(IClue clue, int dropdownId)
        {
            foreach (var kvp in _selectedCluesByDropdown)
            {
                if (kvp.Key != dropdownId && kvp.Value.TitleLid == clue.TitleLid)
                {
                    OnClueDeselected(clue, dropdownId);

                    return;
                }
            }

            if (_selectedCluesByDropdown.ContainsKey(dropdownId))
                _selectedCluesByDropdown.Remove(dropdownId);

            _selectedCluesByDropdown[dropdownId] = clue;
            UpdateSearchText();
        }

        private void OnClueDeselected(IClue clue, int dropdownId)
        {
            if (_selectedCluesByDropdown.TryGetValue(dropdownId, out var currentClue) && currentClue.TitleLid == clue.TitleLid)
            {
                _selectedCluesByDropdown.Remove(dropdownId);
                UpdateSearchText();
            }
        }

        private void UpdateSearchText()
        {
            _inputBuilder.Clear();
            _titleBuilder.Clear();

            bool isFirst = true;

            foreach (var clue in _selectedCluesByDropdown.Values)
            {
                if (!isFirst)
                {
                    _inputBuilder.Append(" + ");
                    _titleBuilder.Append(" + ");
                }

                _inputBuilder.Append(clue.TypeId);
                _titleBuilder.Append(clue.TitleLid);

                isFirst = false;
            }

            _currentInput = _inputBuilder.ToString();
            _searchQueryText.text = _titleBuilder.ToString();
        }

        public string GetInputField() => _currentInput;

        private void OnDestroy()
        {
            if (_dropdownsHandler != null)
            {
                _dropdownsHandler.OnClueSelected -= OnClueSelected;
                _dropdownsHandler.OnClueDeselected -= OnClueDeselected;
            }

            _selectedCluesByDropdown.Clear();
        }
    }
}