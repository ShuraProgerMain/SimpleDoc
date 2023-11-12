using Scripts.StealMiniGame;
using Scripts.StealMiniGame.Configs;
using UnityEngine;

namespace Scripts.TweenTest
{
    public class SomeTweenShit : MonoBehaviour
    {
        [SerializeField] private Material stealMaterial;
        [SerializeField] private Material staticMaterial;
        [SerializeField] private StealMiniGameConfig stealMiniGameConfig;
        private StealMiniGameController _stealMiniGameController;

        private void Awake()
        {
            _stealMiniGameController = new StealMiniGameController(stealMaterial,
                staticMaterial, stealMiniGameConfig);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _stealMiniGameController.InitMiniGame();
                _stealMiniGameController.AdjustSize(1f, .3f, 
                    target: this, target => target.OnComplete());
            }
        }

        private void OnComplete()
        {
            _stealMiniGameController.AdjustSize(.3f, 1f, this, default);
        }
    }
}