using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class ArrayGeneratorView : MonoBehaviour
    {
        [SerializeField] private SomeParentRefs prefab;
        [SerializeField] private RectTransform parent;

        private SomeArrayGenerator _someArrayGenerator;
        private readonly List<GameObject> _someParenRefsArr = new();

        private void Awake()
        {
            _someArrayGenerator = new SomeArrayGenerator();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                MoreGenerate(10);
            }
        }

        private void MoreGenerate(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var values = _someArrayGenerator.GetShuffledRewards();
                var pref = Instantiate(prefab, parent);
                pref.SetValues(values);
            
                _someParenRefsArr.Add(pref.gameObject);
            }
        }
    }
}
