using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tori.UI.R_ScrollView
{
    public abstract class Slot : MonoBehaviour
    {
        private List<Action> _makeSlotActions;
        public void MakeSlot()
        {
            if(_makeSlotActions is null)
            {
                Debug.LogError("MakeSlotActions is null");
                return;
            }

            foreach(var action in _makeSlotActions)
            {
                action?.Invoke();
            }
        }

        public void SetMakeSlotActions(List<Action> actions)
        {
            _makeSlotActions = actions;
        }
       
    }

}
