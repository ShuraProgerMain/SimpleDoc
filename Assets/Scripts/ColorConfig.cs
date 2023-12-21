using UnityEngine;

namespace Scripts
{
    [CreateAssetMenu(menuName = "Configs / Color config")]
    public class ColorConfig : ScriptableObject
    {
        [field: SerializeField] public Color Color { get; private set; }
        [field: SerializeField] public Color AlternativeColor { get; private set; }
    }
}