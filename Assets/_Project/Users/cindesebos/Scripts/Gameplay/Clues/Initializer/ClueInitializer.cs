using Cysharp.Threading.Tasks;
using FlatBuffersSetup.Gameplay;
using Scripts.Settings;
using UnityEngine;

namespace Scripts.Gameplay.Clues.Initializer
{
    public class ClueInitializer : IClueInitializer
    {
        private ISettingsProvider _settingsProvider;

        public ClueInitializer(ISettingsProvider settingsProvider) => _settingsProvider = settingsProvider;

        public UniTask<ItemSettingsT> InitializeClueById(string typeId)
        {
            var gameSettings = _settingsProvider.GameSettings;

            for (int i = 0; i < gameSettings.ItemsLength; i++)
            {
                var item = gameSettings.Items(i);

                if (item.HasValue && item.Value.TypeId == typeId)
                {
                    var unpacked = item.Value.UnPack();

                    return UniTask.FromResult(unpacked);
                }
            }

            return UniTask.FromResult<ItemSettingsT>(null);
        }
    }
}
