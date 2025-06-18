using FlatBuffersSetup;
using Scripts.Services.Loader.Assets;
using Scripts.Services.Loader.Scenes;
using System.Threading.Tasks;
using Scripts.Settings;
using UnityEngine;
using Zenject;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.Bootstrap
{
    public class BootstrapService : IInitializable
    {
        private readonly ISettingsProvider _settingsProvider;
        private readonly Scene SceneToLoad;
        private readonly ISceneLoader _sceneLoader;
        protected readonly ILocalAssetLoader _localAssetLoader;

        public BootstrapService(ISettingsProvider settingsProvider, Scene scene, ISceneLoader sceneLoadere, ILocalAssetLoader localAssetLoader)
        {
            _settingsProvider = settingsProvider;

            if (scene == Scene.Bootstrap) SceneToLoad = Scene.Gameplay;
            else SceneToLoad = scene;

            _sceneLoader = sceneLoadere;
            _localAssetLoader = localAssetLoader;
        }

        public async void Initialize()
        {
            using (var loadingCanvas = await _localAssetLoader.LoadDisposable<GameObject>(AssetsConstants.LoadingCanvas))
            {
                await _settingsProvider.LoadAllSettingsAsync();

                PrintAllTextsSettings();

                await _sceneLoader.LoadSceneAsync(SceneToLoad);
            }
        }

        private void PrintAllTextsSettings()
        {
            var gameSettings = _settingsProvider.GameSettings;

            var message =
            $"\n[Clue Texts]{GetClueTextsSettings(gameSettings)}\n\n" +
            $"[Clue Objects]{GetClueObjectsSettings(gameSettings)}\n\n" +
            $"[Clue Groups]{GetClueGroupsSettings(gameSettings)}\n\n" +
            $"[Dialogues]{GetDialoguesSettings(gameSettings)}";

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

        private string GetClueGroupsSettings(GameSettings gameSettings)
        {
            var count = gameSettings.ClueGroupsLength;
            string message = string.Empty;

            for (int i = 0; i < count; i++)
            {
                var groupSetting = gameSettings.ClueGroups(i);
                if (!groupSetting.HasValue) continue;

                var settings = groupSetting.Value;

                var typeId = settings.TypeId;
                var titleLid = settings.TitleLid;

                string correctIds = "None";

                if (settings.CorrectIdsLength > 0)
                {
                    correctIds = string.Join(", ", Enumerable.Range(0, settings.CorrectIdsLength).Select(j => settings.CorrectIds(j)));
                }

                message += $"\nTypeId: {typeId} - TitleLid: {titleLid} - CorrectIds: {correctIds}";
            }

            return message;
        }

        private string GetDialoguesSettings(GameSettings gameSettings)
        {
            var count = gameSettings.DialoguesLength;
            string message = string.Empty;

            for (int i = 0; i < count; i++)
            {
                var dialogueSettings = gameSettings.Dialogues(i);

                if (!dialogueSettings.HasValue) continue;

                var settings = dialogueSettings.Value;

                var typeId = settings.TypeId;
                var titleLid = settings.MessageLid;
                var authorType = settings.AuthorType;

                message += $"\nTypeId: {typeId} - AuthorType: {authorType} - TitleLid: {titleLid}";
            }

            return message;
        }
    }
}