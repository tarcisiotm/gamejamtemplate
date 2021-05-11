using UnityEngine;

/// <summary>
/// Disables this GO on the selected platforms
/// </summary>
public class PlatformDependentGO : MonoBehaviour
{
    [System.Flags]
    enum Platform {
        None,
        WebGL,
        MacOS,
        Windows,
        Linux,
        Ios,
        Android
    }

    [SerializeField] Platform platformsToDisable = Platform.None;

    private void Awake() {
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

    void DisableGO(Platform platform) {
        if (platformsToDisable.HasFlag(platform)) {
            gameObject.SetActive(false);
        }
    }
}