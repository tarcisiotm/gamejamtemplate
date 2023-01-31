using TG.Core;
using UnityEngine;

namespace TG.GameJamTemplate
{
    /// <summary>
    /// A Pause menu handler
    /// </summary>
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] GameObject _pausePanel = default;

        private bool _isPaused = false;
        private bool _canUpdate = true;
        private bool _isLocked = false;

        public delegate void PauseEvent(bool pauseStatus);
        public static event PauseEvent OnPauseEvent;

        private void Start() { }

        private void Update()
        {
            if (!_canUpdate) { return; }

#if INPUT_LEGACY
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_isPaused) { UnpauseGame(); } else { PauseGame(); }
            }
#endif
        }

        public void PauseGame()
        {
            _isPaused = true;
            OnPauseEvent?.Invoke(_isPaused);
            Time.timeScale = 0;
            _pausePanel.SetActive(true);
        }

        public void UnpauseGame()
        {
            _isPaused = false;
            OnPauseEvent?.Invoke(_isPaused);
            Time.timeScale = 1;
            _pausePanel.SetActive(false);
        }

        public void DisablePause()
        {
            _canUpdate = false;
        }

        public void LoadMainMenu()
        {
            _canUpdate = false;
            Time.timeScale = 1;
            GameManager.I.GetModule<ScenesManager>().LoadMainMenu();
        }

        public void ReloadScene()
        {
            if (_isLocked) { return; }
            GameManager.I.GetModule<ScenesManager>().ReloadScene();
            _isLocked = true;
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}