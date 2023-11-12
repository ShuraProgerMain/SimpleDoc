using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Editor
{
    public class GameBuilder
    {
        [MenuItem("CI / Windows Build")]
        public static void GitWindowsBuild()
        {
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
    }
}