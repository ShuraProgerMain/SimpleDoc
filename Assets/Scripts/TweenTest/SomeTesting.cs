using System.Collections.Generic;
using System.Linq;
using AddressableExtensions;
using Scripts.StealMiniGame;
using Scripts.StealMiniGame.Configs;
using UnityEngine;
using UnityEngine.AddressableAssets;
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
        private const string ShowEffectClass = "showEffect";
        
        private VisualElement _endGamePanel;
        private Label _endGameText;
        private Dictionary<string, ColorConfig> _colorConfigs;

        private async void OnEnable()
        {
            _endGameText = uiDocument.rootVisualElement.Q<Label>("Label");
            _endGamePanel = uiDocument.rootVisualElement.Q<VisualElement>("EndGamePanel");
            
            _colorConfigs = (await Addressables.LoadAssetsAsync<ColorConfig>("Colors", o =>
                                      {
                                          Debug.Log(o.name);
                                      }).Task).ToDictionary(x => x.name);
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
                ResetUI();
                await _stealMiniGameController.InitMiniGame(); 
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _stealMiniGameController.FixedRhombusPosition();
            }
        }


        private void OnWinUI()
        {
            var textColors = _colorConfigs[Colors.Wincolors];
            
            UpdateLabelState(_endGameText, textColors, "GREAT");
            
            _endGamePanel.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        }
        private void ResetUI()
        {
            _endGamePanel.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            _endGameText.RemoveFromClassList(ShowEffectClass);
        }
        
        private void OnLoseUI()
        {
            var textColors = _colorConfigs[Colors.Losecolors];
            
            UpdateLabelState(_endGameText, textColors, "LOSE");
            
            _endGamePanel.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        }

        private void UpdateLabelState(Label label, ColorConfig textColors, string labelText)
        {
            label.text = labelText;
            label.style.color = new StyleColor(textColors.Color);
            label.style.textShadow = new StyleTextShadow(new TextShadow
            {
                blurRadius = 10,
                color = textColors.AlternativeColor
            });
            
            label.AddToClassList(ShowEffectClass);
        }
    }
}