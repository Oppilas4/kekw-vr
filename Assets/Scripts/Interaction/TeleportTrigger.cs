using UnityEngine;
using System.Collections;

namespace Kekw.Interaction
{
    /// <summary>
    /// Detecs player entering to teleport area.
    /// </summary>
    class TeleportTrigger: MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Gameobject to activate when player enters the trigger.")]
        GameObject _target;

        [SerializeField]
        [Tooltip("Audio to play when shown")]
        AudioSource _audioShow;

        [SerializeField]
        [Tooltip("Audio to play when hiding")]
        AudioSource _audioHide;

        Coroutine _runningCoroutine;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                _target.SetActive(true);
                if(_runningCoroutine != null)
                {
                    StopCoroutine(_runningCoroutine);
                }
                _audioShow.PlayOneShot(_audioShow.clip);
                _runningCoroutine = StartCoroutine(AnimateShow(1f, .1f));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _audioHide.PlayOneShot(_audioHide.clip);
                if (_runningCoroutine != null)
                {
                    StopCoroutine(_runningCoroutine);
                }
                _runningCoroutine = StartCoroutine(AnimateHide(0f, -.1f));
            }
        }

        IEnumerator AnimateShow(float scaleTarget, float step)
        {
            while (_target.transform.localScale.y < scaleTarget)
            {
                yield return new WaitForSeconds(.1f);
                _target.transform.localScale = new Vector3(
                    _target.transform.localScale.x,
                    _target.transform.localScale.y + step, _target.transform.localScale.z);
            }
        }

        IEnumerator AnimateHide(float scaleTarget, float step)
        {
            while ( _target.transform.localScale.y > scaleTarget)
            {
                yield return new WaitForSeconds(.1f);
                _target.transform.localScale = new Vector3(
                    _target.transform.localScale.x,
                    _target.transform.localScale.y + step, _target.transform.localScale.z);
            }
            _target.SetActive(false);
        }
    }
}
