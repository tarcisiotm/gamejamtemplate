using System.Collections;
using System.Collections.Generic;
using TG.Core;
using UnityEngine;

/// <summary>
/// Handles gameplay functionality
/// </summary>
public class GameplayManager : Singleton<GameplayManager>
{
    [Header("References")]
    [SerializeField] HUDManager hudManager = default;

    [SerializeField] Camera mainCamera = default;

    PoolingController poolingController = default;
    GameObject auxGO = default;

    void Start()
    {
    }

    public void CreateObjectFromPool(GameObject prefab, Vector3 pos) {
        auxGO = poolingController.GetPooledObject(prefab);
        auxGO.transform.position = pos;
        auxGO.SetActive(true);
    }

    public void ParentToCamera(Transform newChild) {
        newChild.SetParent(mainCamera.transform, true);
    }

    #region Player
    //public Vector3 GetPlayerPos() {
        //return Player.transform.position;
    //}

    IEnumerator DoRespawn() {
        yield return new WaitForSeconds(3);
        //Player.gameObject.SetActive(true);
        //Player.Respawn(pilot.transform);
    }

    #endregion Player

    #region HUD
    public void IncrementScore(int points) {
        //Score += points;
        //hudManager.UpdateScore(Score);
    }

    void LoseLife() {
        hudManager.LoseLife();
    }
    #endregion HUD

    public void GameOver() {
        hudManager.Save();
        StartCoroutine(DelayGameOver());
    }

    IEnumerator DelayGameOver() {
        yield return new WaitForSeconds(1);
        hudManager.ShowGameOver();
    }

}