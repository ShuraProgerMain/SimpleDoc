using System;
using AddressableExtensions;
using Scripts.StealMiniGame;
using Scripts.StealMiniGame.Configs;
using UnityEngine;
using UnityEngine.UIElements;

namespace Scripts.TweenTest
{
    public class SomeTweenShit : MonoBehaviour
    {
        [SerializeField] private Material stealMaterial;
        [SerializeField] private Material staticMaterial;
        [SerializeField] private StealMiniGameConfig stealMiniGameConfig;
        [SerializeField] private UIDocument uiDocument;
        
        private StealMiniGameController _stealMiniGameController;
        
        //UI
        private VisualElement _endGamePanel;
        private Label _endGameText;

        private void OnEnable()
        {
            _endGameText = uiDocument.rootVisualElement.Q<Label>("Label");
            _endGamePanel = uiDocument.rootVisualElement.Q<VisualElement>("EndGamePanel");
        }

        private void Awake()
        {
            _stealMiniGameController = new StealMiniGameController(stealMaterial,
                staticMaterial, stealMiniGameConfig);
            _stealMiniGameController.OnWin += OnWinUI;
            _stealMiniGameController.OnLose += OnLoseUI;
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

        private void OnWinUI()
        {
            _endGameText.text = GetGradientString(Gradients.Wingradient, "GREAT");
            _endGamePanel.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        }

        private void OnLoseUI()
        {
            _endGameText.text = GetGradientString(Gradients.Losegradient, "LOSE");
            _endGamePanel.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        }

        private string GetGradientString(string gradientName, string inGradientText)
        {
            // <gradient="grdnt">GREAT</gradient>
            return $"<gradient=\"{gradientName}\">{inGradientText}</gradient>";
        }
    }
}