using FlatBuffersSetup.Gameplay;
using Scripts.Inventory.Groups;
using UnityEngine;
using Zenject;

namespace Scripts.Gameplay.Canvases.Dialogue
{
    public class DialogueView : MonoBehaviour
    {
        [SerializeField] private MessageItem _playerMessagePrefab, _girlMessagePrefab;
        [SerializeField] private Transform _content;
        [SerializeField] private StartTopicButton[] _startTopicButtons;

        private DialogueHandler _handler;

        [Inject]
        private void Construct(DialogueHandler handler)
        {
            _handler = handler;

            _handler.OnMessageSent += CreateMessage;
            _handler.OnTopicVarianSelected += ShowSelectedTopic;
        }

        private void CreateMessage(AuthorType authorType, string messageLid)
        {
            switch (authorType)
            {
                case AuthorType.Player:
                    MessageItem playerMessageItem = Instantiate(_playerMessagePrefab, _content);
                    playerMessageItem.SetValue(messageLid);
                    break;
                case AuthorType.Girl:
                    MessageItem girlMessageItem = Instantiate(_girlMessagePrefab, _content);
                    girlMessageItem.SetValue(messageLid);
                    break;
            }
        }

        private void ShowSelectedTopic(ClueGroup clueGroup, string topic)
        {
            _startTopicButtons[clueGroup.Id].Initialize(topic, clueGroup);
        }

        private void OnDestroy()
        {
            _handler.OnMessageSent -= CreateMessage;
            _handler.OnTopicVarianSelected -= ShowSelectedTopic;
        }
    }
}
