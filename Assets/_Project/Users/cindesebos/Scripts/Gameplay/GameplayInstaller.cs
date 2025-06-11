using Zenject;
using Scripts.Settings;
using Scripts.Gameplay.Clues.Initializer;
using Scripts.Inventory;

namespace Scripts.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindClueInitializer();
            BindInventory();
        }

        private void BindClueInitializer()
        {
            Container.Bind<IClueInitializer>()
                .To<ClueInitializer>()
                .AsSingle();
        }

        private void BindInventory()
        {
            Container.Bind<IInventory>()
                .To<Inventory.Inventory>()
                .AsSingle();
        }
    }
}