using System;
using Scripts.StealMiniGame.Configs;
using UnityEngine;
using Utilities.Helpers;
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

        public StealMiniGameController(Material resizableObject, Material staticObject, StealMiniGameConfig stealMiniGameConfig)
        {
            _resizableObject = resizableObject;
            _stealMiniGameConfig = stealMiniGameConfig;
            _staticObject = staticObject;
        }

        private float _iteration;

        public void InitMiniGame()
        {
            var someValue = Random.Range(_iteration, _iteration + 1f);
            var internalSize = Random.Range(.5f, _stealMiniGameConfig.MaxExternalRhombusSize
                                                 - _stealMiniGameConfig.RhombusWidthProgress.Evaluate(someValue)) + .2f;

            var externalSize = internalSize + _stealMiniGameConfig.RhombusWidthProgress.Evaluate(someValue);

            Debug.Log($"This is ExternalSize: {externalSize} | and | InternalSize {internalSize} with iteration => {someValue}");
            _staticObject.SetFloat(InternalLineSize, internalSize);
            _staticObject.SetFloat(ExternalLineSize, externalSize);

            _iteration += 0.1f;
        }
        
        public void AdjustSize<T>(float startSize, float finalSize, T target, Action<T> onComplete)
        {
            GraduateHelper.Graduate(Progress, 1f);
            
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
    }
}