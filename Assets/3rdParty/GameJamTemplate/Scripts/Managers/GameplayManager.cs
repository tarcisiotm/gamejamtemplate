using System.Collections;
using TG.Core;
using UnityEngine;

namespace TG.GameJamTemplate
{
    /// <summary>
    /// Handles gameplay functionality
    /// </summary>
    public class GameplayManager : Singleton<GameplayManager>
    {
        [Header("References")]
        [SerializeField] HUDManager _hudManager = default;

        [SerializeField] Camera _mainCamera = default;

        PoolingController _poolingController = default;
        GameObject _auxGO = default;

        private void Start()
        {
        }

        public void CreateObjectFromPool(GameObject prefab, Vector3 pos)
        {
            _auxGO = _poolingController.GetPooledObject(prefab);
            _auxGO.transform.position = pos;
            _auxGO.SetActive(true);
        }

        public void GameOver()
        {
            _hudManager.Save();
            StartCoroutine(DelayGameOver());
        }

        private IEnumerator DelayGameOver()
        {
            yield return new WaitForSeconds(1);
            _hudManager.ShowGameOver();
        }
    }
}