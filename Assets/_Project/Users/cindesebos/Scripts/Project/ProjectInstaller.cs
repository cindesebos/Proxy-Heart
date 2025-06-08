using Zenject;
using Scripts.Settings;

namespace Scripts.Project
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSettingsProvider();
        }

        private void BindSettingsProvider()
        {
            Container.Bind<ISettingsProvider>()
                .To<SettingsProvider>()
                .AsSingle();
        }
    }
}