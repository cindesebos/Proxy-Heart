using System;
using System.Collections.Generic;
using Scripts.Gameplay.Clues;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Scripts.Inventory
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private List<ClueDisplayer> _clueDisplayers;
        [SerializeField] private TextMeshProUGUI _clueVfxPrefab;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private Transform _canvasTransform;

        private IInventory _inventory;
        private ClueDisplayVFX _vfx;

        [Inject]
        private void Construct(IInventory inventory)
        {
            _inventory = inventory;

            _vfx = new ClueDisplayVFX(_clueVfxPrefab, _endPoint, _canvasTransform);
            
            _inventory.OnClueAdded += OnClueAdded;
        }

        private void OnClueAdded(IClue clue)
        {
            foreach (ClueDisplayer clueDisplayer in _clueDisplayers)
            {
                if (clueDisplayer.TypeId == clue.TypeId)
                {
                    Vector2 clickPosition = Mouse.current.position.ReadValue();

                    _vfx.MoveTo(clue.TitleLid, clickPosition);

                    clueDisplayer.Display();

                    return;
                }
            }
        }

        private void OnDestroy()
        {
            _inventory.OnClueAdded -= OnClueAdded;
        }
    }
}
