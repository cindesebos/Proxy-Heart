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
        private readonly float _fixedZ;

        private Tween _moveTween;

        public ClueDisplayVFX(TextMeshProUGUI clueVfxPrefab, Transform endPoint, Transform canvasTransform)
        {
            _clueVfxText = GameObject.Instantiate(clueVfxPrefab, canvasTransform);

            _fixedZ = _clueVfxText.transform.position.z;

            _clueVfxText.gameObject.SetActive(false);

            _endPosition = endPoint.position;
        }

        public void MoveTo(string word, Vector2 screenPosition)
        {
            _moveTween?.Kill();

            _clueVfxText.text = word;

            Vector3 worldStartPosition = Camera.main.ScreenToWorldPoint(
                new Vector3(screenPosition.x, screenPosition.y, Camera.main.WorldToScreenPoint(new Vector3(0, 0, _fixedZ)).z)
            );

            _clueVfxText.transform.position = worldStartPosition;
            _clueVfxText.gameObject.SetActive(true);

            Vector3 targetPosition = _endPosition;
            targetPosition.z = _fixedZ;

            _moveTween = _clueVfxText.transform.DOMove(targetPosition, MoveDuration)
                .SetEase(Ease.OutCubic)
                .OnComplete(() =>
                {
                    _clueVfxText.gameObject.SetActive(false);
                    _moveTween = null;
                });
        }
    }
}