using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AddressableExtensions;
using Scripts.StealMiniGame.Configs;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Random = UnityEngine.Random;

namespace Scripts.StealMiniGame
{
    public class StealMiniGameController
    {
        private readonly Material _resizableObject;
        private readonly Material _staticObject;
        private readonly StealMiniGameConfig _stealMiniGameConfig;
        private static readonly int InternalLineSize = Shader.PropertyToID("_InternalLineSize");
        private static readonly int ExternalLineSize = Shader.PropertyToID("_ExternalLineSize");
        private static readonly int RhombusColor = Shader.PropertyToID("_Color");
        private static readonly int ThicknessColor = Shader.PropertyToID("_ColorThickness");
        

        public StealMiniGameController(Material resizableObject, Material staticObject, StealMiniGameConfig stealMiniGameConfig)
        {
            _resizableObject = resizableObject;
            _stealMiniGameConfig = stealMiniGameConfig;
            _staticObject = staticObject;
        }

        private float _iteration;
        private Dictionary<string, RhombusStyleConfig> _rhombusStyleConfigs;

        public async Task InitMiniGame()
        {
            _rhombusStyleConfigs = (await Addressables.LoadAssetsAsync<RhombusStyleConfig>("RhombusStyles", o =>
                                      {
                                          Debug.Log(o.name);
                                      }).Task).ToDictionary(x => x.name);
                
            SetViewState(_rhombusStyleConfigs[DefaultLocalGroup.Defaultstyle]);
            InitZoneRhombus();
            await InitZoomRhombus();
        }

        private void InitZoneRhombus()
        {
            var someValue = Random.Range(_iteration, _iteration + 1f);
            var internalSize = Random.Range(.5f, _stealMiniGameConfig.MaxExternalRhombusSize
                                                 - _stealMiniGameConfig.RhombusWidthProgress.Evaluate(someValue)) + .2f;

            var externalSize = internalSize + _stealMiniGameConfig.RhombusWidthProgress.Evaluate(someValue);

            _staticObject.SetFloat(InternalLineSize, internalSize);
            _staticObject.SetFloat(ExternalLineSize, externalSize);

            _iteration += 0.1f;
        }

        private CancellationTokenSource _cancellationToken;
        private async Task InitZoomRhombus()
        {
            _cancellationToken = new CancellationTokenSource();
            await AdjustSize(_stealMiniGameConfig.MaxZoomRhombusSize, .1f, 
                    target: this, target => target.SomeD(), _cancellationToken.Token);
        }

        public void FixedRhombusPosition()
        {
            SetViewState(_rhombusStyleConfigs[DefaultLocalGroup.Losestyle]);
            _cancellationToken.Cancel();
        }

        private void SomeD()
        {
            Debug.Log("Some info");
        }
        
        public async Task AdjustSize<T>(float startSize, float finalSize, T target, Action<T> onComplete, CancellationToken token)
        {
            await GraduateHelper.GraduateAsync(Progress, 10f, token: token);
            
            return;

            void Progress(float progress)
            {
                var cSize = Mathf.Lerp(startSize, finalSize, progress);
                
                _resizableObject.SetFloat(InternalLineSize, cSize - _stealMiniGameConfig.MovableLineWidth);
                _resizableObject.SetFloat(ExternalLineSize, cSize);

                if (progress >= 1f)
                {
                    onComplete?.Invoke(target);
                }
            }
        }

        private void SetViewState(RhombusStyleConfig styleConfig)
        {
            _staticObject.SetColor(RhombusColor, styleConfig.StaticRhombusBackgroundColor);
            _staticObject.SetColor(ThicknessColor, styleConfig.StaticRhombusOutlineColor);
            _resizableObject.SetColor(RhombusColor, styleConfig.MovableRhombusColor);
        }
    }
}