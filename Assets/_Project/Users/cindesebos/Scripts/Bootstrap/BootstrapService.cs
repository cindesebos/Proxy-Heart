using FlatBuffersSetup;
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

            PrintAllTextsSettings();

            await _sceneLoader.LoadSceneAsync(SceneToLoad);
        }

        private void PrintAllTextsSettings()
        {
            var gameSettings = _settingsProvider.GameSettings;

            var message = $"{GetClueTextsSettings(gameSettings)} \n {GetClueObjectsSettings(gameSettings)} \n";

            Debug.Log(message);
        }

        private string GetClueTextsSettings(GameSettings gameSettings)
        {
            var count = gameSettings.ClueTextsLength;

            string message = string.Empty;

            for (int i = 0; i < count; i++)
            {
                var textSetting = gameSettings.ClueTexts(i);

                if (!textSetting.HasValue) continue;

                var settings = textSetting.Value;

                var typeId = settings.TypeId;
                var messageLid = settings.MessageLid;

                message += $"\nTypeId: {typeId} - MessageLid: {messageLid}";
            }

            return message;
        }

        private string GetClueObjectsSettings(GameSettings gameSettings)
        {
            var count = gameSettings.ClueObjectsLength;

            string message = string.Empty;

            for (int i = 0; i < count; i++)
            {
                var textSetting = gameSettings.ClueObjects(i);

                if (!textSetting.HasValue) continue;

                var settings = textSetting.Value;

                var typeId = settings.TypeId;
                var titleLid = settings.TitleLid;

                message += $"\nTypeId: {typeId} - TitleLid: {titleLid}";
            }

            return message;
        }
    }
}
