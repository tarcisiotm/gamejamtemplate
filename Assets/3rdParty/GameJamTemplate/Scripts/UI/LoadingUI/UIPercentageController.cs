using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TG.Core;

/// <summary>
/// Provides visual feedback for loading screen progress with filling an image and/or updating a text label.
/// </summary>
public class UIPercentageController : MonoBehaviour
{
    [SerializeField] protected Image loadingImage;
    [SerializeField] protected TextMeshProUGUI loadingText;

    protected void OnEnable() {
        ScenesManager.OnSceneProgressUpdated += OnSceneProgressUpdated;
        SetImage(0);
        SetText(0);
    }

    private void OnSceneProgressUpdated(float loadingProgress) {
        SetImage(loadingProgress);
        SetText(loadingProgress);
    }

    protected void OnDisable() {
        ScenesManager.OnSceneProgressUpdated -= OnSceneProgressUpdated;
    }

    void SetText(float perc) {
        if(loadingText == null) { return; }
        loadingText.text = Mathf.FloorToInt(perc * 100f) + "%";
    }

    void SetImage(float perc)
	{
        if(loadingImage == null || loadingImage.type != Image.Type.Filled) { return; }
        loadingImage.fillAmount = Mathf.Clamp01(perc);
	}
}
