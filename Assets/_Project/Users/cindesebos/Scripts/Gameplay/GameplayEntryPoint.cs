using Scripts.Gameplay.Canvases.Dialogue;
using Scripts.Settings;
using UnityEngine;
using Zenject;

namespace Scripts.Gameplay
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private string[] _instanceDialogueMessageTypeIds;

        private DialogueHandler _dialogueHandler;

        [Inject]
        private void Construct(DialogueHandler dialogueHandler)
        {
            _dialogueHandler = dialogueHandler;
        }

        private void Start()
        {
            SentInstanceMessages();
        }

        private void SentInstanceMessages()
        {
            for (int i = 0; i < _instanceDialogueMessageTypeIds.Length; i++)
            {
                _dialogueHandler.SendMessage(_instanceDialogueMessageTypeIds[i]);
            }
        }
    }
}
