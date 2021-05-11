using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Component used to select a GameObject for highlighting UI
/// </summary>
public class SelectUIObject : MonoBehaviour {
    [SerializeField] bool onStart = true;
    [SerializeField] GameObject gameObjectToSelect = default;

    IEnumerator Start() {
        if (!onStart) { yield break; }

        yield return null;
        SelectGameObject();
    }

    public void SelectGameObject() {
        if (gameObjectToSelect == null) {
            Debug.LogWarning("Null GameObject Selected");
            return;
        }

        EventSystem.current.SetSelectedGameObject(gameObjectToSelect);
    }
}