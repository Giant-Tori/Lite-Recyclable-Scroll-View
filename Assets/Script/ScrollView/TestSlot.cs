using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Tori.UI.R_ScrollView
{
    public class TestSlot : MonoBehaviour, ISlot
    {
        [SerializeField] private TMP_Text _text;

        public void SetText(string text)
        {
            _text.text = text;
        }

        public void MakeSlot(SlotInfo slotInfo)
        {
            var TestSlotInfo = (TestSlotInfo)slotInfo;
            SetText(TestSlotInfo.text);
        }
    }
}

