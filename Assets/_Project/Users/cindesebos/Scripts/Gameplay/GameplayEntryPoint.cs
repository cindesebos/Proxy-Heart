using Scripts.Settings;
using UnityEngine;
using Zenject;

namespace Scripts.Gameplay
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        private ISettingsProvider _settingsProvider;

        [Inject]
        private void Construct(ISettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
        }

        private async void Start()
        {
            await _settingsProvider.LoadAllSettingsAsync();

            PrintAllItems();
        }

        private void PrintAllItems()
        {
            var gameSettings = _settingsProvider.GameSettings;

            var itemsAmount = gameSettings.ItemsLength;

            for (var i = 0; i < itemsAmount; i++)
            {
                var itemSettingsEntry = gameSettings.Items(i);

                if (!itemSettingsEntry.HasValue) continue;

                var itemSettings = itemSettingsEntry.Value;

                var typeId = itemSettings.TypeId;
                var titleLid = itemSettings.TitleLid;

                var message = $"Item:\ntypeId is {typeId}\ntitleLid is {titleLid}\n";

                Debug.Log(message);
            }
        }
    }
}
