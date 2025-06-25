using Cysharp.Threading.Tasks;
using FlatBuffersSetup.Gameplay;
using Scripts.Settings;
using UnityEngine;

namespace Scripts.Gameplay.Clues.Initializer
{
    public class ClueInitializer : IClueInitializer
    {
        private ISettingsProvider _settingsProvider;

        public ClueInitializer(ISettingsProvider settingsProvider) =>
            _settingsProvider = settingsProvider;

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

        public UniTask<ClueGroupSettingsT> InitializeGroupVariantById(string typeId)
        {
            var gameSettings = _settingsProvider.GameSettings;

            for (int i = 0; i < gameSettings.ClueGroupsLength; i++)
            {
                var clue = gameSettings.ClueGroups(i);

                if (clue.HasValue && clue.Value.TypeId == typeId)
                {
                    var unpacked = clue.Value.UnPack();

                    return UniTask.FromResult(unpacked);
                }
            }

            return UniTask.FromResult<ClueGroupSettingsT>(null);
        }

        public UniTask<string> InitializeClueById(string typeId)
        {
            var gameSettings = _settingsProvider.GameSettings;

            for (int i = 0; i < gameSettings.ClueObjectsLength; i++)
            {
                var clue = gameSettings.ClueObjects(i);

                if (clue.HasValue && clue.Value.TypeId == typeId)
                {
                    var unpacked = clue.Value.UnPack();

                    return UniTask.FromResult(unpacked.TitleLid);
                }
            }

            return UniTask.FromResult<string>(null);
        }

        public UniTask<GoogleSettingsT> InitializeGoogleById(string variantId)
        {
            Debug.Log("Try initialize google by " + variantId);

            var gameSettings = _settingsProvider.GameSettings;

            for (int i = 0; i < gameSettings.GooglesLength; i++)
            {
                var google = gameSettings.Googles(i);

                var googleVariantId = google.Value.VariantId;

                if (google.HasValue && (googleVariantId == variantId || googleVariantId == ReverseText(variantId)))
                {
                    var unpacked = google.Value.UnPack();

                    return UniTask.FromResult(unpacked);
                }
            }

            return UniTask.FromResult<GoogleSettingsT>(null);
        }

        private string ReverseText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            char[] array = text.ToCharArray();

            System.Array.Reverse(array);
            
            return new string(array);
        }
    }
}
