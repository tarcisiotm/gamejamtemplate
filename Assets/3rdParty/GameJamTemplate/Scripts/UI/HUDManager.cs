using UnityEngine;
using TMPro;

namespace TG.GameJamTemplate
{
    /// <summary>
    /// Handles a HUD
    /// </summary>
    public class HUDManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _scoreTXT = default;
        [SerializeField] TextMeshProUGUI _highScoreTXT = default;

        [SerializeField] GameObject _extraLife = default;
        [SerializeField] GameObject _gameOverPanel = default;

        private const string PLAYER_PREFS_SCORE_KEY = "HighScoreKey";

        private int _highestScore = 0;
        private bool _dirty = false;

        private void Start()
        {
            _highestScore = PlayerPrefs.GetInt(PLAYER_PREFS_SCORE_KEY, 0);
            UpdateScore(0);
            UpdateHighScore(_highestScore);
            //load highest score, if any    
        }

        public void LoseLife()
        {
            _extraLife.SetActive(false);
        }

        public void UpdateScore(int newScore)
        {
            _scoreTXT.text = newScore + "";
            if (newScore > _highestScore)
            {
                UpdateHighScore(newScore);
            }
        }

        private void UpdateHighScore(int newHighScore)
        {
            _highestScore = newHighScore;
            _highScoreTXT.text = _highestScore + "";
            _dirty = true;
        }

        public void Save()
        {
            if (!_dirty) { return; }
            PlayerPrefs.SetInt(PLAYER_PREFS_SCORE_KEY, _highestScore);
            PlayerPrefs.Save();
        }

        public void ShowGameOver()
        {
            Time.timeScale = 0;
            _gameOverPanel.SetActive(true);
        }
    }
}