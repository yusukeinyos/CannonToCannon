using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class BuildBatch
{
    // Android ビルド出力ファイル
    private static readonly string APK_FILE_NAME = $"{Application.productName}.apk";

    // iOS　ビルド出力ファイル
    private static readonly string XCODE_PROJECT_DIRECTORY = $"build";

    // Androidビルドを実行します。
    [MenuItem("Build/Android")]
    public static void BuildAndroid()
    {
        ExecuteBuildProcess(APK_FILE_NAME, BuildTarget.Android);
    }

    // iOSビルドを実行します。
    [MenuItem("Build/iOS")]
    public static void BuildiOS()
    {
        ExecuteBuildProcess(XCODE_PROJECT_DIRECTORY, BuildTarget.iOS);
    }

    // バッチモードビルドを実行します。
    private static void ExecuteBuildProcess(string locationPathName, BuildTarget target)
    {
        var sceneList = GetBuildSceneList();
        foreach (var scene in sceneList)
        {
            Console.WriteLine($"Build scene: {scene}");
        }

        var buildOptions = BuildOptions.StrictMode;
        var buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = sceneList,
            locationPathName = locationPathName,
            target = target,
            options = buildOptions,
        };
        var buildReport = BuildPipeline.BuildPlayer(buildPlayerOptions);


        Console.WriteLine($"Build failed. ErrorMessage: {buildReport?.summary}");

        Console.WriteLine("Build succeeded.");

    }

    // UnityEditorのBuildSettingで設定されているビルド対象のシーンを取得します。
    private static string[] GetBuildSceneList()
    {
        return Array.ConvertAll(EditorBuildSettings.scenes, scene => scene.path);
    }
}

