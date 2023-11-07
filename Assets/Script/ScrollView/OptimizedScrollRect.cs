using UnityEngine.UI;
using UnityEngine;

namespace Tori.UI
{
    public class OptimizedScrollRect : ScrollRect
    {
        [SerializeField] private GameObject _slotPrefab;

        private Rect _viewportRect;
        private Vector2 _prePosition;

        private int _slotCount;
        private float _slotHeight;

        private int _currentStartIndex = 0;
        private float _epsilon = 0.01f; // for float comparison

        private int _verticalSlotCount => Mathf.CeilToInt(_viewportRect.height / _slotHeight) + 2;
       
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
        public void Refresh()
        {
            if (!TryInit())
            {
                return;
            }

            _currentStartIndex = 0;
            _prePosition = new Vector2(0f, 0f);

            var childCount = content.transform.childCount;
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

        private void OnValueChanged(Vector2 normalizedPosition)
        {
            var current = content.anchoredPosition;
            var diff = current.y - _prePosition.y;

            if (diff >= _slotHeight - _epsilon)
            {
                if (_currentStartIndex + _verticalSlotCount >= _slotCount)
                {
                    return;
                }
                SetSlotActive(_currentStartIndex, false);
                SetSlotActive(_currentStartIndex + _verticalSlotCount, true);
                _currentStartIndex++;

                content.anchoredPosition -= new Vector2(0, _slotHeight);
                _prePosition = content.anchoredPosition;

            }
            else if (-diff >= _slotHeight - _epsilon)
            {
                if (_currentStartIndex <= 0)
                {
                    return;
                }
                SetSlotActive(_currentStartIndex + _verticalSlotCount - 1, false);
                SetSlotActive(_currentStartIndex - 1, true);
                _currentStartIndex--;

                content.anchoredPosition += new Vector2(0, _slotHeight);
                _prePosition = content.anchoredPosition;
            }
        }


        
        //Get slot prefab height and slot count
        private bool TryInit()
        {
            _slotCount = content.transform.childCount;

            if (!_slotPrefab.TryGetComponent<RectTransform>(out var slotRect))
            {
                Debug.LogError("Failed to get RectTransform from slotPrefab");
                return false;
            }

            _slotHeight = slotRect.rect.height;

            return true;
        }

        private void SetSlotActive(int index, bool isActive)
        {
            content.GetChild(index).gameObject.SetActive(isActive);
        }
    }
}
