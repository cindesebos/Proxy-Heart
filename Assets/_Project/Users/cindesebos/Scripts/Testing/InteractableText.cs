using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

namespace Scripts.Testing
{
    public class InteractableText : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI _text;

        private void Start()
        {
            _text.text = "Нажми <link=\"event1\"><color=yellow><u>здесь</u></color></link> для запуска события.";
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("working");

            int linkIndex = TMP_TextUtilities.FindIntersectingLink(_text, eventData.position, eventData.pressEventCamera);

            if (linkIndex != -1)
            {
                TMP_LinkInfo linkInfo = _text.textInfo.linkInfo[linkIndex];
                string linkId = linkInfo.GetLinkID();
                HandleLink(linkId);
            }
        }

        private void HandleLink(string id)
        {
            switch (id)
            {
                case "event1":
                    Debug.Log("Событие 1 запущено!");
                    break;
                default:
                    Debug.Log("Неизвестная ссылка: " + id);
                    break;
            }
        }
    }
}
