using UnityEngine;

/// <summary>
/// Proxy for the Panels Handler
/// </summary>
public class PanelsHandlerProxy : MonoBehaviour
{
    void Start(){ }

    public void ActivateMainMenu()
    {
        FindObjectOfType<PanelsHandler>().ActivateMainMenu();
    }
}
