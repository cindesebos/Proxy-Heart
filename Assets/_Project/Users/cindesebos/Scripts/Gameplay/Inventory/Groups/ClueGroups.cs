using System.Collections.Generic;
using Scripts.Inventory;
using UnityEngine;

namespace Scripts.Inventory.Groups
{
    public class ClueGroups : MonoBehaviour
    {
        [SerializeField] private List<ClueDisplayer> _displayers;
        [SerializeField] private List<SelectVariantButton> _selectVariantButtons;
        [SerializeField] private GameObject _marker;

        private int _currentDispalyersCount;

        private void Start()
        {
            _marker.SetActive(false);

            foreach (var displayer in _displayers)
            {
                displayer.OnClueDisplayed += OnClueDisplayed;
            }
        }

        private void OnClueDisplayed(ClueDisplayer displayer)
        {
            _currentDispalyersCount++;

            if (_displayers.Count > _currentDispalyersCount)
            {
                ShowVariants();

                _marker.SetActive(true);
            }
        }

        private void ShowVariants()
        {
            foreach (var selectVariantButton in _selectVariantButtons) selectVariantButton.Show();
        }
    }
}
