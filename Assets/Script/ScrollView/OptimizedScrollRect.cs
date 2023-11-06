using UnityEngine.UI;
using UnityEngine;

namespace Tori.UI
{
    public class OptimizedScrollRect : ScrollRect
    {
        [SerializeField] private GameObject _slotPrefab;

        private int _slotCount;
        private float _sloHeight;

        private Rect _viewportRect;
        private int _buffer = 4;
        private int _childIndex = 0;
        private bool _isChanging = false;

        private Vector2 _prePosition;

        private int _verticalSlotCount => Mathf.CeilToInt(_viewportRect.height / _sloHeight) + _buffer;
        private float _verticalSlotNormalizedHeight => 1f / (float)_verticalSlotCount;
        protected override void Awake()
        {
            onValueChanged.RemoveAllListeners();
            onValueChanged.AddListener(OnValueChanged);

            _viewportRect = viewport.rect;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Refresh();

        }

        public void OnValueChanged(Vector2 normalizedPosition)
        {
            if (_isChanging)
            {
                return;
            }
            _isChanging = true;
            m_ContentStartPosition += GetDirection(normalizedPosition);
            _prePosition = content.anchoredPosition;
            _isChanging = false;
        }
        private Vector2 GetDirection(Vector2 normalizedPosition)
        {
            Vector2 result = Vector2.zero;

            var direction = content.anchoredPosition.y - _prePosition.y;
            var preVelocity = velocity;
            if (normalizedPosition.y < _verticalSlotNormalizedHeight && direction > 0f)
            {

                var index = _childIndex + _verticalSlotCount + 1;
                if (index >= _slotCount)
                {
                    return result;
                }
                if (_childIndex > 0)
                {
                    content.transform.GetChild(_childIndex - 1).gameObject.SetActive(false);
                }
                _childIndex++;
                content.transform.GetChild(index).gameObject.SetActive(true);
                result = new Vector2(0f, _verticalSlotNormalizedHeight);
            }
            else if (normalizedPosition.y > 1f - _verticalSlotNormalizedHeight && direction < 0f)
            {
                var index = _childIndex - 1;
                if (index < _slotCount)
                {
                    return result;
                }
                if (_childIndex < _slotCount - 1)
                {
                    content.transform.GetChild(_childIndex + 1).gameObject.SetActive(false);
                }
                _childIndex--;

                content.transform.GetChild(index).gameObject.SetActive(true);
                result = new Vector2(0f, -_verticalSlotNormalizedHeight);
            }

            return result;
        }

        public void Refresh()
        {
            if (!TryInit())
            {
                return;
            }
            Debug.Log($"verticalSlotCount: {_verticalSlotCount}, {content.childCount}");

            _childIndex = 0;
            _prePosition = new Vector2(0f, 1f);

            var childCount = content.childCount;
            for (int i = 0; i < childCount; i++)
            {
                if (i < _verticalSlotCount)
                {
                    content.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    content.GetChild(i).gameObject.SetActive(false);
                }
            }

        }

        private bool TryInit()
        {
            _slotCount = content.transform.childCount;

            if (!_slotPrefab.TryGetComponent<RectTransform>(out var slotRect))
            {
                Debug.LogError("Failed to get RectTransform from slotPrefab");
                return false;
            }

            _sloHeight = slotRect.rect.height;

            return true;
        }
    }
}
