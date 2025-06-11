using System;
using Scripts.Gameplay.Clues;

namespace Scripts.Inventory
{
    public class Inventory : IInventory
    {
        public event Action<IClue> OnClueAdded;

        public bool TryAddClue(IClue clue)
        {
            if (clue == null) return false;

            OnClueAdded(clue);

            return true;
        }
    }
}
