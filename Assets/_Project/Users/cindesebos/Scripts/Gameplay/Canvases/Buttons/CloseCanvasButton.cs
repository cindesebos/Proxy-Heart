using UnityEngine;

namespace Scripts.Gameplay.Canvases.Buttons
{
    public class CloseCanvasButton : BaseCanvasButton
    {
        public override void OnClick()
        {
            Handler.GetCanvasByType(TargetType).Close();
        }
    }
}
