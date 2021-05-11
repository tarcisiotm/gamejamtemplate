using UnityEngine;
using TMPro;

/// <summary>
/// Handles a HUD
/// </summary>
public class HUDManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreTXT = default;
    [SerializeField] TextMeshProUGUI highScoreTXT = default;

    [SerializeField] GameObject extraLife = default;
    [SerializeField] GameObject gameOverPanel = default;

    const string PLAYER_PREFS_SCORE_KEY = "HighScoreKey";
    int highestScore = 0;
    bool dirty = false;

    void Start()
    {
        highestScore = PlayerPrefs.GetInt(PLAYER_PREFS_SCORE_KEY, 0);
        UpdateScore(0);
        UpdateHighScore(highestScore);
        //load highest score, if any    
    }

    public void LoseLife() {
        extraLife.SetActive(false);
    }

    public void UpdateScore(int newScore) {
        scoreTXT.text = newScore + "";
        if(newScore > highestScore) {
            UpdateHighScore(newScore);
        }
    }

    void UpdateHighScore(int newHighScore) {
        highestScore = newHighScore;
        highScoreTXT.text = highestScore + "";
        dirty = true;
    }

    public void Save() {
        if (!dirty) { return; }
        PlayerPrefs.SetInt(PLAYER_PREFS_SCORE_KEY, highestScore);
        PlayerPrefs.Save();
    }

    public void ShowGameOver() {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }
}
