using FlatBuffersSetup.Gameplay;
using Scripts.Gameplay.Clues.Initializer;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.Gameplay.Canvases.Google
{
    public class SearchButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private ResearchInputField _researchInputField;
        [SerializeField] private GoogleCanvas _googleCanvas;

        private IClueInitializer _clueInitializer;

        private void OnValidate()
        {
            _button ??= GetComponent<Button>();
            _researchInputField ??= GetComponentInParent<ResearchInputField>();
            _googleCanvas ??= GetComponentInParent<GoogleCanvas>();
        }

        [Inject]
        private void Construct(IClueInitializer clueInitializer)
        {
            _clueInitializer = clueInitializer;

            _button.onClick.AddListener( delegate {
                OnButtonClicked();
            });
        }

        private async void OnButtonClicked()
        {
            string inputField = _researchInputField.GetInputField();

            GoogleSettingsT settings = await _clueInitializer.InitializeGoogleById(inputField);

            if (settings == null) return;

            _googleCanvas.ChangeWebsitePanelActivity(true);
            _googleCanvas.ChangeMainPanelActivity(false);

            Debug.Log("Settings is " + settings.MessageLid);
        }
    }
}
