using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Scripts
{
    public class SomeParentRefs : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] texts;

        public void SetValues(string[] values)
        {
            var superGamesList = new List<string>() { "Lottery", "RedOrBlack", "WheelOfFortune" };
            for (var i = 0; i < values.Length; i++)
            {
                texts[i].text = values[i];
            }

            for (var i = 0; i < values.Length; i++)
            {
                if (i == values.Length - 1) continue;

                if (superGamesList.Contains(values[i]) && superGamesList.Contains(values[i + 1]))
                {
                    texts[i].color = Color.red;
                    texts[i + 1].color = Color.red;
                }
            }
        }
    }
}
