using TG.Core;
using UnityEngine;

namespace TG.GameJamTemplate
{
    public class SplashScreenController : MonoBehaviour
    {
        [SerializeField] private int mainMenuSceneBuildIndex = 2;

        void OnEnable()
        {
            if (GameManagerBase.I != null && GameManager.I.Initialized) 
            {
                OnGameManagerInitialized();
                return;
            }

            GameManagerBase.OnInitialized += OnGameManagerInitialized;
        }

        private void OnDisable()
        {
            GameManagerBase.OnInitialized -= OnGameManagerInitialized;
        }

        private void OnGameManagerInitialized()
        {
            var scenesManager = GameManager.I.GetModule<ScenesManager>();
            scenesManager.LoadScene(mainMenuSceneBuildIndex, false);
        }
    }
}