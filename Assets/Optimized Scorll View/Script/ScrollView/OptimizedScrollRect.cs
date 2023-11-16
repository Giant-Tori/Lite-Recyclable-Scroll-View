using UnityEngine.UI;
using UnityEngine;

namespace Tori.UI
{
    public class OptimizedScrollRect : ScrollRect
    {
        [SerializeField] private GameObject _slotPrefab;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;
        [SerializeField] private float _verticalPadding;
        [SerializeField] private float _horizontalPadding;

        private Rect _viewportRect;
        private Vector2 _prePosition;

        private int _slotCount;
        private float _slotHeight;
        private float _slotWidth;

        private int _currentStartIndex = 0;
        private float _epsilon = 0.01f; // for float comparison
        private int _buffer = 2; // buffer for slot count

        private int _verticalSlotCount => Mathf.CeilToInt(_viewportRect.height / _slotHeight) + _buffer;
        private int _horizontalSlotCount => Mathf.CeilToInt(_viewportRect.width / _slotWidth) + _buffer;

        protected override void OnEnable()
        {
            if (Application.isPlaying == false)
            {
                return;
            }

            base.OnEnable();
            _viewportRect = viewport.rect;

            if (horizontal)
            {
                onValueChanged.AddListener(OnValueChangedHorizontal);
            }
            else
            {

                onValueChanged.AddListener(OnValueChangedVertical);
            }

            Refresh();
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            onValueChanged.RemoveAllListeners();
        }
        public void Refresh()
        {
            if (!TryInit())
            {
                return;
            }

            SetContentSIze();

            // Reset Data
            _currentStartIndex = 0;
            _prePosition = new Vector2(0f, 0f);

            // Set Slot Active
            var childCount = content.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                if (vertical && i < _verticalSlotCount)
                {
                    content.GetChild(i).gameObject.SetActive(true);

                }
                else if (horizontal && i < _horizontalSlotCount)
                {
                    content.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    content.GetChild(i).gameObject.SetActive(false);
                }
            }

            SetSlotPosition(_currentStartIndex);


        }

        private void OnValueChangedVertical(Vector2 normalizedPosition)
        {

            var current = content.anchoredPosition;
            var diff = current.y - _prePosition.y;
            var isDown = diff >= _slotHeight - _epsilon;
            var isUp = -diff >= _slotHeight - _epsilon;

            if (isDown)
            {

                var isLast = _currentStartIndex + _verticalSlotCount >= _slotCount;
                if (isLast)
                {
                    return;
                }

                // Move content
                content.anchoredPosition -= new Vector2(0, _slotHeight);
                _prePosition = content.anchoredPosition;

                // Set slot active
                SetSlotActive(_currentStartIndex, false);
                SetSlotActive(_currentStartIndex + _verticalSlotCount, true);
                _currentStartIndex++;

                // Set slot position
                SetSlotPosition(_currentStartIndex);


            }
            else if (isUp)
            {
                if (_currentStartIndex <= 0)
                {
                    return;
                }

                // Move content
                content.anchoredPosition += new Vector2(0, _slotHeight);
                _prePosition = content.anchoredPosition;

                // Set slot active
                SetSlotActive(_currentStartIndex + _verticalSlotCount - 1, false);
                SetSlotActive(_currentStartIndex - 1, true);
                _currentStartIndex--;

                // Set slot position
                SetSlotPosition(_currentStartIndex);
            }
        }

        private void OnValueChangedHorizontal(Vector2 normalizedPosition)
        {
            var current = content.anchoredPosition;
            var diff = current.x - _prePosition.x;
            var isRight = -diff >= _slotWidth - _epsilon;
            var isLeft = diff >= _slotWidth - _epsilon;

            if (isRight)
            {
                var isLast = _currentStartIndex + _horizontalSlotCount >= _slotCount;
                if (isLast)
                {
                    return;
                }
                // Move content
                content.anchoredPosition += new Vector2(_slotWidth, 0);
                _prePosition = content.anchoredPosition;

                // Set slot active
                SetSlotActive(_currentStartIndex, false);
                SetSlotActive(_currentStartIndex + _horizontalSlotCount, true);
                _currentStartIndex++;

                // Set slot position
                SetSlotPosition(_currentStartIndex);

            }
            else if (isLeft)
            {
                if (_currentStartIndex <= 0)
                {
                    return;
                }
                // Move content
                content.anchoredPosition -= new Vector2(_slotWidth, 0);
                _prePosition = content.anchoredPosition;

                // Set slot active
                SetSlotActive(_currentStartIndex + _horizontalSlotCount - 1, false);
                SetSlotActive(_currentStartIndex - 1, true);
                _currentStartIndex--;

                // Set slot position
                SetSlotPosition(_currentStartIndex);
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
            _slotWidth = slotRect.rect.width;

            return true;
        }

        private void SetSlotActive(int index, bool isActive)
        {
            content.GetChild(index).gameObject.SetActive(isActive);
        }

        private void SetContentSIze()
        {
            var size = content.sizeDelta;
            if (vertical)
            {
                size.x = _slotWidth;
                size.y = _slotHeight * _verticalSlotCount;
                size.y += _verticalPadding * (_verticalSlotCount - 1);
            }
            else
            {
                size.x = _slotWidth * _horizontalSlotCount;
                size.y = _slotHeight;
                size.x += _horizontalPadding * (_horizontalSlotCount - 1);
            }
            content.sizeDelta = size;
        }

        private void SetSlotPosition(int start)
        {
            if (vertical)
            {
                SetVerticalSlotPosition(start);

            }
            else
            {
                SetHorizontalSlotPosition(start);
            }

        }


        private void SetVerticalSlotPosition(int start)
        {
            for (int i = start; i < start + _verticalSlotCount; i++)
            {
                var rect = content.GetChild(i).GetComponent<RectTransform>();
                var posX = _slotWidth / 2;
                var posY = -_slotHeight / 2 - _slotHeight * (i - start);
                posY -= _verticalPadding * (i - start);

                rect.anchoredPosition = new Vector2(posX, posY);
                rect.sizeDelta = new Vector2(_slotWidth, _slotHeight);
            }
        }

        private void SetHorizontalSlotPosition(int start)
        {
            for (int i = start; i < start + _horizontalSlotCount; i++)
            {
                var rect = content.GetChild(i).GetComponent<RectTransform>();
                var posX = _slotWidth / 2 + _slotWidth * (i - start);
                var posY = -_slotHeight / 2;
                posX += _horizontalPadding * (i - start);

                rect.anchoredPosition = new Vector2(posX, posY);
                rect.sizeDelta = new Vector2(_slotWidth, _slotHeight);
            }
        }

    }
}
