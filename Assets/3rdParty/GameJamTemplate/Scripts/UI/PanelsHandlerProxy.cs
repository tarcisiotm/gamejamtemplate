using UnityEngine;

namespace TG.GameJamTemplate
{
    /// <summary>
    /// Proxy for the Panels Handler
    /// </summary>
    public class PanelsHandlerProxy : MonoBehaviour
    {
        private void Start() { }

        public void ActivateMainMenu()
        {
            FindObjectOfType<PanelsHandler>().ActivateMainMenu();
        }
    }
}