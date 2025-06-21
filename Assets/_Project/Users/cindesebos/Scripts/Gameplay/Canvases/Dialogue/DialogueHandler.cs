using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using FlatBuffersSetup.Gameplay;
using Scripts.Gameplay.Clues.Initializer;
using Scripts.Inventory.Groups;
using Scripts.Settings;
using UnityEngine;
using Zenject;

namespace Scripts.Gameplay.Canvases.Dialogue
{
    public class DialogueHandler
    {
        public event Action<AuthorType, string> OnMessageSent;
        public event Action<ClueGroup, string> OnTopicVarianSelected;

        private ISettingsProvider _settingsProvider;
        private IClueInitializer _clueInitializer;
        private List<string> _correctIds;

        public DialogueHandler(ISettingsProvider settingsProvider, IClueInitializer clueInitializer)
        {
            _settingsProvider = settingsProvider;
            _clueInitializer = clueInitializer;

            InitializeCorrectIds().Forget();
        }

        private async UniTaskVoid InitializeCorrectIds()
        {
            var groupSettings = await _clueInitializer.InitializeGroupVariantById("0");

            _correctIds = groupSettings.CorrectIds;
        }

        public async void SendMessage(string typeId)
        {
            DialogueSettingsT settings = await InitializeClueTextById(typeId);

            OnMessageSent?.Invoke(settings.AuthorType, settings.MessageLid);
        }

        public async void SelectTopicVarian(ClueGroup clueGroup, string variantId)
        {
            ClueGroupSettingsT settings = await _clueInitializer.InitializeGroupVariantById(variantId);

            bool isCorrect = settings.CorrectIds != null && _correctIds.Contains(variantId);

            Debug.Log($"Выбранный ID: {variantId}, Правильные ответы: {string.Join(", ", _correctIds ?? new())}, isCorrect: {isCorrect}");

            OnTopicVarianSelected?.Invoke(clueGroup, settings.TitleLid);
        }

        private UniTask<DialogueSettingsT> InitializeClueTextById(string typeId)
        {
            var gameSettings = _settingsProvider.GameSettings;

            for (int i = 0; i < gameSettings.DialoguesLength; i++)
            {
                var clue = gameSettings.Dialogues(i);

                if (clue.HasValue && clue.Value.TypeId == typeId)
                {
                    var unpacked = clue.Value.UnPack();

                    return UniTask.FromResult(unpacked);
                }
            }

            return UniTask.FromResult<DialogueSettingsT>(null);
        }

        internal void SelectTopicVarian(int id, string typeId)
        {
            throw new NotImplementedException();
        }
    }
}
