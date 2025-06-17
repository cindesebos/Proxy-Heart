using UnityEngine;

namespace Scripts.Gameplay.Canvases.Buttons
{
    public class ToggleCanvasButton : BaseCanvasButton
    {
        public override void OnClick()
        {
            Handler.GetCanvasByType(TargetType).Toggle();

            Handler.SetFrontLayer(TargetType);
        }
    }
}
