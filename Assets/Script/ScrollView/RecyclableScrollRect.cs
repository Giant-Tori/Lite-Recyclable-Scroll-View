using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tori.UI.R_ScrollView
{
    public class RecyclableScrollRect : ScrollRect
    {
        private List<SlotInfo> _slotInfos = new List<SlotInfo>();
        [SerializeField] private RectTransform _slot;

        protected override void Awake()
        {
            base.Awake();
        }

        public void Refresh(List<SlotInfo> slotInfo)
        {
            _slotInfos = slotInfo;

            //
            for(int i = 0; i < _slotInfos.Count; i++)
            {
                var slotobj = Instantiate(_slot.gameObject, content);

            }
        }
    }

}