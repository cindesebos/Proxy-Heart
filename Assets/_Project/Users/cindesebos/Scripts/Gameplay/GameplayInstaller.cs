using Zenject;
using UnityEngine;
using Scripts.Settings;
using Scripts.Gameplay.Clues.Initializer;
using Scripts.Inventory;
using Scripts.Services.Loader.Assets;
using Scripts.Gameplay.Canvases;
using Scripts.Gameplay.Canvases.Dialogue;

namespace Scripts.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private CanvasesHandler _canvasesHandler;

        public override void InstallBindings()
        {
            BindLocalAssetLoader();
            BindClueInitializer();
            BindInventory();
            BindCanvasesHandler();
            BindDialueHandler();
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

        private void BindCanvasesHandler()
        {
            Container.Bind<CanvasesHandler>()
                .FromInstance(_canvasesHandler)
                .AsSingle();
        }

        private void BindDialueHandler()
        {
            Container.BindInterfacesAndSelfTo<DialogueHandler>()
                .AsSingle();
        }
    }
}