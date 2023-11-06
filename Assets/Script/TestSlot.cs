using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Tori.UI
{
    public class TestSlot : MonoBehaviour
    {
        [SerializeField] TMP_Text _text;

        public void SetText(string name)
        {
            _text.text = name;
        }
    }
}
