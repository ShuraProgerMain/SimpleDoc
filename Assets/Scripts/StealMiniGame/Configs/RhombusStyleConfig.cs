using UnityEngine;

namespace Scripts.StealMiniGame.Configs
{
    [CreateAssetMenu(menuName = "Mini Games / Steal / Style")]
    public class RhombusStyleConfig : ScriptableObject
    {
        [field: SerializeField]
        [field: ColorUsage(true, true)]
        public Color StaticRhombusBackgroundColor { get; private set; }
        
        [field: SerializeField]
        [field: ColorUsage(true, true)]
        public Color StaticRhombusOutlineColor { get; private set; }
        
        [field: SerializeField]
        [field: ColorUsage(true, true)]
        public Color MovableRhombusColor { get; private set; }
    }
}