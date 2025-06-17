using Zenject;
using Scripts.Settings;
using Scripts.Services.Loader.Scenes;
using Scripts.Services.Loader.Assets;

namespace Scripts.Project
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindLocalAssetLoader();
            BindSettingsProvider();
            BindSceneLoader();
        }

        private void BindLocalAssetLoader()
        {
            Container.Bind<ILocalAssetLoader>()
                .To<LocalAssetLoader>()
                .AsSingle();
        }

        private void BindSettingsProvider()
        {
            Container.Bind<ISettingsProvider>()
                .To<SettingsProvider>()
                .AsSingle();
        }

        private void BindSceneLoader()
        {
            Container.Bind<ISceneLoader>()
                .To<SceneLoader>()
                .AsSingle();
        }
    }
}