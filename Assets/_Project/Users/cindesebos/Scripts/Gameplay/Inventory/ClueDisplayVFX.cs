using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Scripts.Inventory
{
    public class ClueDisplayVFX
    {
        private const float MoveDuration = 0.65f;

        private readonly TextMeshProUGUI _clueVfxText;
        private readonly Vector2 _endPosition;

        private Tween _moveTween;

        public ClueDisplayVFX(TextMeshProUGUI clueVfxPrefab, Transform endPoint, Transform canvasTransform)
        {
            _clueVfxText = GameObject.Instantiate(clueVfxPrefab, canvasTransform);

            _clueVfxText.gameObject.SetActive(false);

            _endPosition = endPoint.position;
        }

        public void MoveTo(string word, Vector2 startPosition)
        {
            _moveTween?.Kill();

            _clueVfxText.text = word;
            _clueVfxText.transform.position = startPosition;
            _clueVfxText.gameObject.SetActive(true);

            _moveTween = _clueVfxText.transform.DOMove(_endPosition, MoveDuration)
                .SetEase(Ease.OutCubic)
                .OnComplete(() =>
                {
                    _clueVfxText.gameObject.SetActive(false);
                    _moveTween = null;
                });
        }
    }
}