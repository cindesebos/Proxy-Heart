using FMODUnity;
using UnityEngine;

namespace Scripts.Gameplay.Canvases.Buttons
{
    public class ToggleCanvasButton : BaseCanvasButton
    {
        public override void OnClick()
        {
            RuntimeManager.PlayOneShot("event:/SFX/MouseClick");

            Handler.GetCanvasByType(TargetType).Toggle();

            Handler.SetFrontLayer(TargetType);
        }
    }
}
