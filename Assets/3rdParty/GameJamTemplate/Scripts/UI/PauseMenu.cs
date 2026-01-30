using System;
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

        private GameStateManagerBase gameStateManager;

        private void OnEnable()
        {
            GameStateManagerBase.OnGameStateChanged += OnGameStateChanged;

            if (GameManager.I != null && GameManager.I.Initialized) OnGameManagerInitialized();
            else GameManager.OnInitialized += OnGameManagerInitialized;
        }

        private void OnGameManagerInitialized()
        {
            gameStateManager = GameManager.I.GetModule<GameStateManagerBase>();
        }

        private void OnDisable()
        {
            GameStateManagerBase.OnGameStateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState previousGameState, GameState newGameState)
        {
            if (newGameState == GameState.Paused) { ShowPauseMenu(); }
            else if (previousGameState == GameState.Paused) { HidePauseMenu(); }
        }

        private void Update()
        {
            if (!_canUpdate) { return; }

#if INPUT_LEGACY
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameStateManager.Pause(gameStateManager.CurrentState != GameState.Paused);
            }
#endif
        }

        private void ShowPauseMenu()
        {
            _isPaused = true;
            _pausePanel.SetActive(true);
        }

        public void UnpauseUICallback() 
        {
            gameStateManager.Pause(false);
        }

        private void HidePauseMenu()
        {
            _isPaused = false;
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