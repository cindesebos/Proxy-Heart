using System;
using Scripts.Services.Loader.Scenes;
using UnityEngine;
using Zenject;

namespace Scripts.Bootstrap
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private Scene _sceneToLoad;

        public override void InstallBindings()
        {
            BindBootstrapService();
        }

        private void BindBootstrapService()
        {
            Container.Bind<Scene>()
                .FromInstance(_sceneToLoad)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<BootstrapService>()
                .AsSingle();
        }
    }
}
