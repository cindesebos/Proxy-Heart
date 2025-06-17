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

        public UniTask<ClueTextSettingsT> InitializeClueTextById(string typeId)
        {
            var gameSettings = _settingsProvider.GameSettings;

            for (int i = 0; i < gameSettings.ClueTextsLength; i++)
            {
                var clue = gameSettings.ClueTexts(i);

                if (clue.HasValue && clue.Value.TypeId == typeId)
                {
                    var unpacked = clue.Value.UnPack();

                    return UniTask.FromResult(unpacked);
                }
            }

            return UniTask.FromResult<ClueTextSettingsT>(null);
        }

        public UniTask<ClueObjectSettingsT> InitializeClueObjectById(string typeId)
        {
            var gameSettings = _settingsProvider.GameSettings;

            for (int i = 0; i < gameSettings.ClueObjectsLength; i++)
            {
                var clue = gameSettings.ClueObjects(i);

                if (clue.HasValue && clue.Value.TypeId == typeId)
                {
                    var unpacked = clue.Value.UnPack();

                    return UniTask.FromResult(unpacked);
                }
            }

            return UniTask.FromResult<ClueObjectSettingsT>(null);
        }
    }
}
