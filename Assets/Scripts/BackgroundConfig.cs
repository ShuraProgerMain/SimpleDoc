using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Scripts
{
    [CreateAssetMenu(menuName = "SimpleDoc / Background Config")]
    public class BackgroundConfig : ScriptableObject
    {
        [field: SerializeField] public Sprite Background { get; private set; }
    }
}
