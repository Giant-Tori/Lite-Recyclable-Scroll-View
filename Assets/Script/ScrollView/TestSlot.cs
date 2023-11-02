using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Tori.UI.R_ScrollView
{
    public class TestSlot : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public void SetText(string text)
        {
            _text.text = text;
        }
    }
}

