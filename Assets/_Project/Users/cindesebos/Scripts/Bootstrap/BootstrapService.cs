using Scripts.Services.Loader;
using Scripts.Settings;
using UnityEngine;
using Zenject;

namespace Scripts.Bootstrap
{
    public class BootstrapService : IInitializable
    {
        private const Scene SceneToLoad = Scene.Gameplay;

        private readonly ISettingsProvider _settingsProvider;
        private readonly ISceneLoader _sceneLoader;

        public BootstrapService(ISettingsProvider settingsProvider, ISceneLoader sceneLoadere)
        {
            _settingsProvider = settingsProvider;
            _sceneLoader = sceneLoadere;
        }

        public async void Initialize()
        {
            await _settingsProvider.LoadAllSettingsAsync();

            PrintAllItems();

            await _sceneLoader.LoadSceneAsync(SceneToLoad);
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

                var message = $"typeId is {typeId} - titleLid is {titleLid}\n";

                Debug.Log(message);
            }
        }
    }
}
