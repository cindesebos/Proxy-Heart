using Zenject;
using Scripts.Settings;
using Scripts.Gameplay.Clues.Initializer;
using Scripts.Inventory;
using Scripts.Services.Loader.Assets;

namespace Scripts.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindLocalAssetLoader();
            BindClueInitializer();
            BindInventory();
        }

        private void BindLocalAssetLoader()
        {
            Container.Bind<ILocalAssetLoader>()
                .To<LocalAssetLoader>()
                .AsSingle();
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