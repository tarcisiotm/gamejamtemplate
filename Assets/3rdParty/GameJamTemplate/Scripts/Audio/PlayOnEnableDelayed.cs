using System.Collections;
using UnityEngine;

namespace TG.GameJamTemplate
{
    /// <summary>
    /// A simmple component to delay playing an audio on enable
    /// </summary>
    public class PlayOnEnableDelayed : MonoBehaviour
    {
        [SerializeField] float _delay = 0;

        private Coroutine _waitRoutine;

        private void OnEnable()
        {
            _waitRoutine = StartCoroutine(DelayToPlay());
        }

        private IEnumerator DelayToPlay()
        {
            yield return new WaitForSeconds(_delay);
            GetComponent<AudioSource>().Play();
            _waitRoutine = null;
        }

        private void OnDisable()
        {
            if (_waitRoutine != null)
            {
                StopCoroutine(_waitRoutine);
            }
        }
    }
}