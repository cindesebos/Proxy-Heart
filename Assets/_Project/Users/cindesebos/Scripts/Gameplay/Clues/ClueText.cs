using Cysharp.Threading.Tasks;
using FlatBuffersSetup.Gameplay;
using Scripts.Gameplay.Clues.Initializer;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using System.Text.RegularExpressions;
using Scripts.Inventory;

namespace Scripts.Gameplay.Clues
{
    public class ClueText : MonoBehaviour, IClue, IPointerClickHandler
    {
        [field: SerializeField] public string TypeId { get; private set; }

        public string TitleLid { get; private set; }
        public string MessaegLid { get; private set; }

        [SerializeField] private TextMeshProUGUI _text;

        private IClueInitializer _clueInitializer;
        private IInventory _inventory;
        private ClueTextSettingsT _settings;

        private void OnValidate() => _text ??= GetComponent<TextMeshProUGUI>();

        [Inject]
        private void Construct(IClueInitializer clueInitializer, IInventory inventory)
        {
            _clueInitializer = clueInitializer;
            _inventory = inventory;
        }

        private async UniTask Start()
        {
            _settings = await _clueInitializer.InitializeClueTextById(TypeId);

            await Initialize();
        }

        public async UniTask Initialize()
        {
            MessaegLid = Compile(_settings.MessageLid);

            _text.text = MessaegLid;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(_text, eventData.position, eventData.pressEventCamera);

            if (linkIndex != -1)
            {
                TMP_LinkInfo linkInfo = _text.textInfo.linkInfo[linkIndex];

                string linkId = linkInfo.GetLinkID();

                OnTextClicked(linkId);
            }
        }

        private void OnTextClicked(string word)
        {
            TitleLid = word;

            if (_inventory.TryAddClue(this))
            {
                Debug.Log($"You clicked on {word}");

                RemoveLinkFromText(word);
            }
        }

        private string Compile(string rawText)
        {
            if (string.IsNullOrEmpty(rawText)) return string.Empty;

            string compiled = rawText;

            compiled = Regex.Replace(compiled, @"<name=""(.*?)""><""red"">(\S+)(.*?)", match =>
            {
                string linkId = match.Groups[1].Value;
                string firstWord = match.Groups[2].Value;
                string restText = match.Groups[3].Value;

                return $"<link=\"{linkId}\"><color=red>{firstWord}</color></link>{restText}";
            });

            compiled = compiled.Replace("</name>", "").Replace("</\"red\">", "");

            return compiled;
        }

        private void RemoveLinkFromText(string linkId)
        {
            string pattern = $@"<link=""{Regex.Escape(linkId)}""><color=red>(.*?)</color></link>";

            string replacement = "<color=white>$1</color>";

            MessaegLid = Regex.Replace(MessaegLid, pattern, replacement);

            _text.text = MessaegLid;

            _text.ForceMeshUpdate();
        }

    }
}