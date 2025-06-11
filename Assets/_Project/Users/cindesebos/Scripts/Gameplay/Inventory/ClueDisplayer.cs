using System;
using Scripts.Gameplay.Clues;
using TMPro;
using UnityEngine;
using Zenject;

namespace Scripts.Inventory
{
    public class ClueDisplayer : MonoBehaviour
    {
        [field: SerializeField] public string TitleLid { get; private set; }

        [SerializeField] private TextMeshProUGUI _text;

        private void Start() => _text.enabled = false;

        public void Display() => _text.enabled = true;
    }
}