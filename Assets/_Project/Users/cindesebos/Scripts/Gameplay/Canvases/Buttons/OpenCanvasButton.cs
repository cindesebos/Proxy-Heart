using FMODUnity;
using UnityEngine;

namespace Scripts.Gameplay.Canvases.Buttons
{
    public class OpenCanvasButton : BaseCanvasButton
    {
        public override void OnClick()
        {
            RuntimeManager.PlayOneShot("event:/SFX/MouseClick");
            Handler.GetCanvasByType(TargetType).Open();
        }
    }
}
