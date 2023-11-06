using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tori.UI
{
    public class TestSlotMaker : MonoBehaviour
    {
        [SerializeField] private OptimizedScrollRect _scrollRect;
        [SerializeField] private Button _button;
        [SerializeField] private TestSlot _slotPrefab;
        [SerializeField] private Transform _content;
        [SerializeField] private int _slotCount;

        private void Awake()
        {
            _button.onClick.AddListener(OnClick);
        }
        private void OnClick()
        {
            for (int i = 0; i < _slotCount; i++)
            {
                var slot = Instantiate(_slotPrefab, _content);
                var name = $"Slot {i}";
                slot.SetText(name);
            }
            _scrollRect.Refresh();
        }
    }
}
