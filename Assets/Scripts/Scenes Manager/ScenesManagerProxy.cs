using TG.GameJamTemplate;
using UnityEngine;

namespace TG.Core {
    /// <summary>
    /// Scenes Manager Proxy for operations that require a reference
    /// </summary>
    public class ScenesManagerProxy : MonoBehaviour {

        public virtual void LoadMainMenu() {
            GameManager.I.GetModule<AudioManager>().FadeOutBGM();
            GameManager.I.GetModule<ScenesManager>().LoadMainMenu();
        }

        public virtual void LoadNextLevel() {
            //GameManager.I.GetModule<AudioManager>().FadeOutBGM();
            GameManager.I.GetModule<ScenesManager>().LoadNextSceneWithFade();
        }

        public virtual void ReloadScene() {
            GameManager.I.GetModule<AudioManager>().FadeOutBGM();
            GameManager.I.GetModule<ScenesManager>().ReloadScene();
        }
    }
}