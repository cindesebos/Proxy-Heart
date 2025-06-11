using Zenject;
using Scripts.Settings;
using Scripts.Services.Loader;

namespace Scripts.Project
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSettingsProvider();
            BindSceneLoader();
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