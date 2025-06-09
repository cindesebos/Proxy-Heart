using Zenject;
using Scripts.Settings;
using Scripts.Gameplay.Clues.Initializer;

namespace Scripts.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindClueInitializer();
        }

        private void BindClueInitializer()
        {
            Container.Bind<IClueInitializer>()
                .To<ClueInitializer>()
                .AsSingle();
        }
    }
}