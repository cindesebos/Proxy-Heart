using System;
using Zenject;

namespace Scripts.Bootstrap
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindBootstrapService();
        }

        private void BindBootstrapService()
        {
            Container.BindInterfacesAndSelfTo<BootstrapService>()
                .AsSingle();
        }
    }
}
