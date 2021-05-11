using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TG.Core;

namespace TG.GameJamTemplate
{
    /// <summary>
    /// Provides visual feedback for loading screen progress with filling an image and/or updating a text label.
    /// </summary>
    public class UIPercentageController : MonoBehaviour
    {
        [SerializeField] protected Image _loadingImage;
        [SerializeField] protected TextMeshProUGUI _loadingText;

        protected void OnEnable()
        {
            ScenesManager.OnSceneProgressUpdated += OnSceneProgressUpdated;
            SetImage(0);
            SetText(0);
        }

        private void OnSceneProgressUpdated(float loadingProgress)
        {
            SetImage(loadingProgress);
            SetText(loadingProgress);
        }

        protected void OnDisable()
        {
            ScenesManager.OnSceneProgressUpdated -= OnSceneProgressUpdated;
        }

        private void SetText(float perc)
        {
            if (_loadingText == null) { return; }
            _loadingText.text = Mathf.FloorToInt(perc * 100f) + "%";
        }

        private void SetImage(float perc)
        {
            if (_loadingImage == null || _loadingImage.type != Image.Type.Filled) { return; }
            _loadingImage.fillAmount = Mathf.Clamp01(perc);
        }
    }
}