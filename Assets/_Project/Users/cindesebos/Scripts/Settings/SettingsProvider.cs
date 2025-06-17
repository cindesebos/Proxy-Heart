using Cysharp.Threading.Tasks;
using FlatBuffersSetup;
using Google.FlatBuffers;
using UnityEngine;

namespace Scripts.Settings
{
    public class SettingsProvider : ISettingsProvider
    {
        public GameSettings GameSettings { get; private set; }

        public async UniTask LoadAllSettingsAsync()
        {
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
    }
}