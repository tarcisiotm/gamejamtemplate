using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the activation of panels in a UI
/// </summary>
public class PanelsHandler : MonoBehaviour
{
    [SerializeField] GameObject mainMenuPanel = default;
    [SerializeField] GameObject[] panels = default;

    void Start() {}

    public void Activate(GameObject panel)
    {
        SetPanel(panel);
    }

    public void ActivateMainMenu()
    {
        Activate(mainMenuPanel);
    }

    void SetPanel(GameObject panel) {
        foreach(var go in panels)
        {
            go.SetActive(go == panel);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

}
