using Cysharp.Threading.Tasks;
using FlatBuffersSetup;
using Google.FlatBuffers;
using Scripts.Settings.Global;
using UnityEngine;

namespace Scripts.Settings
{
    public class SettingsProvider : ISettingsProvider
    {
        public GameSettings GameSettings { get; private set; }
        public ProjectSettings ProjectSettings { get; private set; }

        public async UniTask LoadAllSettingsAsync()
        {
            LoadProjectSettingsAsync("GlobalProjectSettings");

            await LoadGameSettingsAsync("settings");
        }

        private UniTask LoadGameSettingsAsync(string settingsFilePath)
        {
            var gameSettingsAsset = Resources.Load<TextAsset>(settingsFilePath);

            if (gameSettingsAsset == null)
            {
                Debug.LogError($"[SettingsImporter] Failed to load GameSettings at path: {settingsFilePath}");

                return UniTask.CompletedTask;
            }

            var byteBuffer = new ByteBuffer(gameSettingsAsset.bytes);

            GameSettings = GameSettings.GetRootAsGameSettings(byteBuffer);

            Debug.Log("[SettingsImporter] GameSettings loaded successfully.");

            return UniTask.CompletedTask;
        }

        private void LoadProjectSettingsAsync(string assetPath)
        {
            ProjectSettings = Resources.Load<ProjectSettings>(assetPath);

            Debug.Log("ProjectSettings is " + ProjectSettings);
        }
    }
}