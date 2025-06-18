using Scripts.Gameplay.Canvases;
using Scripts.Settings;
using UnityEngine;
using Zenject;

namespace Scripts.Gameplay
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        private DialogueHandler _dialogueHandler;

        [Inject]
        private void Construct(DialogueHandler dialogueHandler)
        {
            _dialogueHandler = dialogueHandler;
        }

        private void Start()
        {
            _dialogueHandler.SendInitialegMesssages();
        }
    }
}
