using UnityEditor;

/// <summary>
/// Android SDK Preference
/// </summary>
public static class AndroidSdkPref
{
    /// <summary>
    /// 環境変数のキー
    /// </summary>
    private const string SDK_KEY = "ANDROID_SDK_ROOT";
    private const string JDK_KEY = "JDK_PATH";
    private const string NDK_KEY = "ANDROID_NDK_ROOT";

    /// <summary>
    /// Android SDKのルートパスを設定・取得します。
    /// </summary>
    public static string AndroidSdkRoot
    {
        get { return EditorPrefs.GetString(SDK_KEY); }
        set { EditorPrefs.SetString(SDK_KEY, value); }
    }

    /// <summary>
    /// JDKのルートパスを設定・取得します。
    /// </summary>
    public static string JdkRoot
    {
        get { return EditorPrefs.GetString(JDK_KEY); }
        set { EditorPrefs.SetString(JDK_KEY, value); }
    }

    /// <summary>
    /// Android NDKのルートパスを設定・取得します。
    /// Unity 5.3以上が必要
    /// </summary>
    public static string AndroidNdkRoot
    {
        get { return EditorPrefs.GetString(NDK_KEY); }
        set { EditorPrefs.SetString(NDK_KEY, value); }
    }



}
