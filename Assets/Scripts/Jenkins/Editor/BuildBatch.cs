using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class BuildBatch
{
    // Android ビルド出力ファイル
    private static readonly string APK_FILE_NAME = $"{Application.productName}.apk";
    // iOSビルド出力ファイル
    private static readonly string XCODE_PROJECT_DIRECTORY = $"build";

    // 環境変数の値のキャッシュ
    private static string _lastGetEnvValue;

    /// <summary>
    /// Androidビルドを実行します。
    /// </summary>
    [MenuItem("Build/Android")]
    public static void BuildAndroid()
    {
        SetAndroidSdkPath();

        ExecuteBuildProcess(APK_FILE_NAME, BuildTarget.Android);
    }

    /// <summary>
    /// iOSビルドを実行します。
    /// </summary>
    [MenuItem("Build/iOS")]
    public static void BuildiOS()
    {
        ExecuteBuildProcess(XCODE_PROJECT_DIRECTORY, BuildTarget.iOS);
    }

    /// <summary>
    /// バッチモードビルドを実行します。
    /// </summary>
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

    /// <summary>
    /// Android SDKのパスを環境変数から取得してUnityに設定します。
    /// </summary>
    private static void SetAndroidSdkPath() 
    {
        // 環境変数からAndroidSDKのパスを取得してUnityEditor Preferenceに設定する。
        if(CheckEnvValueExist("ANDROID_SDK_ROOT"))
        {
            AndroidSdkPref.AndroidSdkRoot = _lastGetEnvValue;
        }

        if (CheckEnvValueExist("JDK_PATH"))
        {
            AndroidSdkPref.JdkRoot = _lastGetEnvValue;
        }

        if (CheckEnvValueExist("ANDROID_NDK_ROOT"))
        {
            AndroidSdkPref.AndroidNdkRoot = _lastGetEnvValue;
        }
    }

    /// <summary>
    /// ビルド対象のシーンをUnityEditor BuildSettingから取得します。
    /// </summary>
    private static string[] GetBuildSceneList()
    {
        return Array.ConvertAll(EditorBuildSettings.scenes, scene => scene.path);
    }

    /// <summary>
    /// 指定したキーの環境変数が設定されているかどうかを判定します。
    /// </summary>
    private static bool CheckEnvValueExist(string key)
    {
        _lastGetEnvValue = Environment.GetEnvironmentVariable(key);
        Console.WriteLine($"[Env: {key}] {_lastGetEnvValue ?? "not defined"}");

        return !string.IsNullOrEmpty(_lastGetEnvValue);
    }
}

