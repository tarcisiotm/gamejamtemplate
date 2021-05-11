using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TG.GameJamTemplate
{
    /// <summary>
    /// Component used to select a GameObject for highlighting UI
    /// </summary>
    public class SelectUIObject : MonoBehaviour
    {
        [SerializeField] private bool _onStart = true;
        [SerializeField] private GameObject _gameObjectToSelect = default;

        private IEnumerator Start()
        {
            if (!_onStart) { yield break; }

            yield return null;
            SelectGameObject();
        }

        public void SelectGameObject()
        {
            if (_gameObjectToSelect == null)
            {
                Debug.LogWarning("Null GameObject Selected");
                return;
            }

            EventSystem.current.SetSelectedGameObject(_gameObjectToSelect);
        }
    }
}