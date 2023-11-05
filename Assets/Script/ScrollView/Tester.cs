using System;
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

        private List<SlotInfo> _slotInfos = new List<SlotInfo>();
        private void Awake()
        {
            _button.onClick.AddListener(OnClick);
        }
        public void OnClick()
        {
            List<SlotInfo> slots = new List<SlotInfo>();
            for (int i = 0; i < 100; i++)
            {
                int index = i;
                var info = new TestSlotInfo()
                {
                    text = $"Slot {index}"
                };
                slots.Add(info);

            }
            _slotInfos = slots;

            _scrollRect.Refresh(slots);
        }
        public void MakeSlot(ISlot slot, int index)
        {
            var slotObj = slot as TestSlot;
            slotObj.MakeSlot(_slotInfos[index]);
        }
    }
}

