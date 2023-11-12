using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Utilities.Helpers
{
    public static class GraduateHelper
    {
        public static async void Graduate(Action<float> action, float duration, bool reverse = false)
        {
            try
            {
                await GraduateAsync(action, duration, reverse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public static async Task GraduateAsync(Action<float> action, float duration, bool reverse = false)
        {
            for (var time = 0f; time < duration; time += Time.deltaTime)
            {
                var ratio = time / duration;
                ratio = reverse ? 1f - ratio : ratio;

                var progress = ratio;

                action.Invoke(progress);

                await Task.Yield();
            }

            action.Invoke(reverse ? 0f : 1f);
        }
    }
}