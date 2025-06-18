using System;
using Cysharp.Threading.Tasks;
using FlatBuffersSetup.Gameplay;
using Scripts.Settings;
using UnityEngine;

namespace Scripts.Gameplay.Canvases
{
    public class DialogueHandler
    {
        public event Action<AuthorType, string> OnMessageSend;

        private readonly ISettingsProvider _settingsProvider;

        public DialogueHandler(ISettingsProvider settingsProvider) => _settingsProvider = settingsProvider;

        public void SendInitialegMesssages()
        {
            var gameSettings = _settingsProvider.GameSettings;

            Debug.Log("SendInitialegMesssages");

            foreach (string id in _settingsProvider.ProjectSettings.DialoguesSettings.InitialMessagesTypeIds)
            {
                Debug.Log("Step 1: id: " + id);

                for (int i = 0; i < gameSettings.DialoguesLength; i++)
                {
                    var dialogue = gameSettings.Dialogues(i);

                    var settings = dialogue.Value;

                    if (dialogue.HasValue && settings.TypeId == id)
                    {
                        Debug.Log($"Initiale Message with id: {i} AuthorType: {settings.AuthorType}, MessageLid: {settings.MessageLid}");

                        OnMessageSend?.Invoke(settings.AuthorType, settings.MessageLid);
                    }
                }
            }
        }
    }
}
