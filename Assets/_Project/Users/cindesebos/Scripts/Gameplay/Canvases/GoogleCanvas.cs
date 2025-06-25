using UnityEngine;

namespace Scripts.Gameplay.Canvases
{
    public class GoogleCanvas : BaseCanvas
    {
        [SerializeField] private GameObject _mainPanel, _websitePanel;

        private void Start()
        {
            ChangeWebsitePanelActivity(false);
        }

        public override void Open()
        {
            base.Open();

            ChangeWebsitePanelActivity(false);
            ChangeMainPanelActivity(true);
        }

        public void ChangeMainPanelActivity(bool activity) => _mainPanel.SetActive(activity);

        public void ChangeWebsitePanelActivity(bool activity) => _websitePanel.SetActive(activity);

        public void TogglePanels()
        {
            ChangeMainPanelActivity(!_mainPanel.activeSelf);
            ChangeWebsitePanelActivity(!_websitePanel.activeSelf);
        }
    }
}
