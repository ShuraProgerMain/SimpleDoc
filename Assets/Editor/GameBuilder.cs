using UnityEditor;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Editor
{
    public class GameBuilder
    {
        [MenuItem("CI / Windows Build")]
        public static void GitWindowsBuild()
        {
            BuildAddressableContent();
            var buildPlayerOptions = new BuildPlayerOptions()
            {
                scenes = new []{ "Assets/Scenes/SampleScene.unity" },
                locationPathName = "build/Windows.exe",
                target = BuildTarget.StandaloneWindows64,
                options = BuildOptions.None
                
            };

            var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            var summary = report.summary;

            if (summary.result == BuildResult.Succeeded)
            {
                Debug.Log("Build succeeded " + summary.totalSize + " bytes");
            }

            if (summary.result == BuildResult.Failed)
            {
                Debug.Log("Build failed");
            }
        }
        
        static bool BuildAddressableContent() {
            AddressableAssetSettings
                .BuildPlayerContent(out var result);
            var success = string.IsNullOrEmpty(result.Error);

            if (!success) {
                Debug.LogError("Addressables build error encountered: " + result.Error);
            }
            return success;
        }
    }
}