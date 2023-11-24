using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

namespace Scripts.UI
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private UIDocument resultPanel;
        [SerializeField] private AssetReferenceT<BackgroundConfig> backgroundConfig;

        private VisualElement _background;
        private Label _label;
        
        private void OnEnable()
        {
            _background = resultPanel.rootVisualElement.Q<VisualElement>("Background");
            _label = resultPanel.rootVisualElement.Q<Label>("Label");
        }

        private async void Start()
        {
            var bgConfig = await backgroundConfig.LoadAssetAsync<BackgroundConfig>().Task;
            _background.style.backgroundImage = new StyleBackground(bgConfig.Background);
        }
    }
}