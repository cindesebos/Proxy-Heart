using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Gameplay.Canvases
{
    public class CanvasesHandler : MonoBehaviour
    {
        [SerializeField] private List<BaseCanvas> _baseCanvases;
        [SerializeField] private Transform _iconsBarParent;

        private Dictionary<CanvasType, BaseCanvas> _canvasMap;

        private void Awake()
        {
            _canvasMap = new Dictionary<CanvasType, BaseCanvas>();

            foreach (var canvas in _baseCanvases)
            {
                _canvasMap[canvas.CurrentType] = canvas;

                canvas.Canvas.enabled = false;
            }
        }

        public void SetFrontLayer(CanvasType type)
        {
            foreach (var canvas in _baseCanvases)
            {
                canvas.SetCanvasOrder(1);
            }

            _canvasMap.TryGetValue(type, out var selectedCanvas);

            selectedCanvas.SetCanvasOrder(2);
        }

        public BaseCanvas GetCanvasByType(CanvasType type)
        {
            _canvasMap.TryGetValue(type, out var canvas);

            return canvas;
        }
    }
}
