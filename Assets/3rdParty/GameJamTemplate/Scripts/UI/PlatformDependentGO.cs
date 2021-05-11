using UnityEngine;

/// <summary>
/// Disables this GO on the selected platforms
/// </summary>
public class PlatformDependentGO : MonoBehaviour
{
    [System.Flags]
    private enum Platform
    {
        None,
        WebGL,
        MacOS,
        Windows,
        Linux,
        Ios,
        Android
    }

    [SerializeField] private Platform _platformsToDisable = Platform.None;

    private void Awake()
    {
#if UNITY_WEBGL
        DisableGO(Platform.WebGL);
#elif UNITY_STANDALONE_OSX
        DisableGO(Platform.MacOS);
#elif UNITY_STANDALONE_WIN
        DisableGO(Platform.Windows);
#elif UNITY_STANDALONE_LINUX
        DisableGO(Platform.Linux);
#elif UNITY_IOS
        DisableGO(Platform.Ios);
#elif UNITY_ANDROID
        DisableGO(Platform.Android);
#endif
    }

    private void DisableGO(Platform platform)
    {
        if (_platformsToDisable.HasFlag(platform))
        {
            gameObject.SetActive(false);
        }
    }
}