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

        private async void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                await _stealMiniGameController.InitMiniGame();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _stealMiniGameController.FixedRhombusPosition();
            }
        }
    }
}