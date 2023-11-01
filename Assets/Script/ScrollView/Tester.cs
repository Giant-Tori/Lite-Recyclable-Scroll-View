using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tori.UI.R_ScrollView
{
    public class Tester : MonoBehaviour
    {
        [SerializeField] private RecyclableScrollRect _scrollRect;
        [SerializeField] private TestSlot _slotPrefab;
        [SerializeField] private Button _button;
        [SerializeField] private Transform _slotParent;

        private void Awake()
        {
            _button.onClick.AddListener(OnClick);
        }
        public void OnClick()
        {
            var slots = new List<Slot>();
            for (int i = 0; i < 100; i++)
            {
                var slot = Instantiate(_slotPrefab, _slotParent);
                var index = i;
                slot.SetMakeSlotActions(new List<System.Action>
                {
                    () => slot.SetText(index.ToString())
                });
                slots.Add(slot);
            }
            _scrollRect.Refresh(slots);
        }
    }
}

