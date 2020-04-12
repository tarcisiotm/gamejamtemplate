using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Component used to select a GameObject for highlighting UI
/// </summary>
public class SelectUIObject : MonoBehaviour
{
    [SerializeField] bool onEnable = true;
    [SerializeField] GameObject gameObjectToSelect;

    private void OnEnable() {
        if (!onEnable) { return; }
        SelectGameObject();
    }

    public void SelectGameObject() {
        if(gameObjectToSelect == null) {
            Debug.LogWarning("Null GameObject Selected");
            return;
        }

        EventSystem.current.SetSelectedGameObject(gameObjectToSelect);
    }
}