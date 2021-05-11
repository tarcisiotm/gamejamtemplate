using UnityEngine;

namespace TG.GameJamTemplate
{
    /// <summary>
    /// Handles the activation of panels in a UI
    /// </summary>
    public class PanelsHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _mainMenuPanel = default;
        [SerializeField] private GameObject[] _panels = default;

        private void Start() { }

        public void Activate(GameObject panel)
        {
            SetPanel(panel);
        }

        public void ActivateMainMenu()
        {
            Activate(_mainMenuPanel);
        }

        private void SetPanel(GameObject panel)
        {
            foreach (var go in _panels)
            {
                go.SetActive(go == panel);
            }
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}