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

        private void Awake()
        {
            _button.onClick.AddListener(OnClick);
        }
        public void OnClick()
        {
            List<TestSlotInfo> slots = new List<TestSlotInfo>();
            for (int i = 0; i < 100; i++)
            {
                int index = i;
                var info = new TestSlotInfo()
                {
                    text = $"Slot {index}"
                };
                slots.Add(info);

            }
            foreach(var slot in slots)
            {
                var s = Instantiate(_slotPrefab, _slotParent);
                s.MakeSlot(slot);
            }
        }
    }
}

