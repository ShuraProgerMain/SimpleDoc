using UnityEngine; 

namespace Scripts.StealMiniGame.Configs
{
    [CreateAssetMenu(menuName = "Mini Games / StealConfig")]
    public class StealMiniGameConfig : ScriptableObject
    {
        [field: SerializeField]
        [field: Range(0f, 1f)]
        public float MovableLineWidth { get; private set; }
        
        [field: SerializeField]
        [field: Range(0f, 5f)]
        public float MaxExternalRhombusSize { get; private set; }
        
        [field: SerializeField]
        [field: Range(0f, 5f)]
        public float MaxZoomRhombusSize { get; private set; }
        
        [field: SerializeField]
        public AnimationCurve RhombusWidthProgress { get; private set; }
    }
}
