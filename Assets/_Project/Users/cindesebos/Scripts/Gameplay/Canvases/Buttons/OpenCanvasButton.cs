using UnityEngine;

namespace Scripts.Gameplay.Canvases.Buttons
{
    public class OpenCanvasButton : BaseCanvasButton
    {
        public override void OnClick()
        {
            Handler.GetCanvasByType(TargetType).Open();
        }
    }
}
