    using System;
    using Cysharp.Threading.Tasks;
    using FlatBuffersSetup.Gameplay;
    using Scripts.Settings;
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;

    namespace Scripts.Gameplay.Canvases
    {
        public class DialogueView : MonoBehaviour
        {
            [SerializeField] private DialogueItem _playerItem, _girlItem;
            [SerializeField] private Transform _itemsParent;
            [SerializeField] private RectTransform _contentTransform;

            private DialogueHandler _handler;

            [Inject]
            private void Construct(DialogueHandler handler)
            {
                _handler = handler;

                _handler.OnMessageSend += OnMessageSend;
            }

            private void OnMessageSend(AuthorType authorType, string messageLid)
            {
                Debug.Log("OnMessageSend");

                LayoutRebuilder.ForceRebuildLayoutImmediate(_contentTransform);

                switch (authorType)
                {
                    case AuthorType.Player:
                        DialogueItem playerInstance = Instantiate(_playerItem, _itemsParent);
                        playerInstance.SetTextValue(messageLid);
                        break;
                    case AuthorType.Girl:
                        DialogueItem girlInstance = Instantiate(_girlItem, _itemsParent);
                        girlInstance.SetTextValue(messageLid);
                        break;
                }
            }

            private void OnDestroy()
            {
                _handler.OnMessageSend -= OnMessageSend;
            }
        }
    }
