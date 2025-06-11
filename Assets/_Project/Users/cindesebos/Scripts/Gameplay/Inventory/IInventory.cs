using System;
using Scripts.Gameplay.Clues;
using UnityEngine;

namespace Scripts.Inventory
{
    public interface IInventory
    {
        event Action<IClue> OnClueAdded;

        bool TryAddClue(IClue clue);
    }
}
