using System.Collections.Generic;
using UnityEngine.UI;

namespace Tori.UI.R_ScrollView
{
    public class RecyclableScrollRect : ScrollRect
    {
        private List<Slot> _slots = new List<Slot>(); 

        protected override void Awake()
        {
            base.Awake();
        }

        public void Refresh(List<Slot> slots)
        {
            _slots = slots;

            // For Test
            for (int i = 0; i < _slots.Count; i++)
            {
                _slots[i].MakeSlot();
            }
        }
    }

}