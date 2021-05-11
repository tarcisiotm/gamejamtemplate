using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simmple component to delay playing an audio on enable
/// </summary>
public class PlayOnEnableDelayed : MonoBehaviour
{
    [SerializeField] float delay = 0;

    Coroutine WaitRoutine;
    private void OnEnable() {
        WaitRoutine = StartCoroutine(DelayToPlay());
    }

    IEnumerator DelayToPlay() {
        yield return new WaitForSeconds(delay);
        GetComponent<AudioSource>().Play();
        WaitRoutine = null;
    }

    private void OnDisable() {
        if(WaitRoutine != null) {
            StopCoroutine(WaitRoutine);
        }
    }
}
