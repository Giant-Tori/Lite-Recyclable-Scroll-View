using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tori.UI.R_ScrollView
{
    public class RecyclableScrollRect : ScrollRect
    {
        private List<Action> _makeSlotActions = new List<Action>();

        protected override void Awake()
        {
            base.Awake();
        }

        public void Refresh()
        {


            
        }
    }

}