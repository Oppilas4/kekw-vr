using UnityEngine;
using System.Collections;

namespace Kekw.Interaction
{
    /// <summary>
    /// Detecs player entering to teleport area.
    /// </summary>
    class TeleportTrigger: MonoBehaviour
    {
        /// <summary>
        /// What activates when player enters trigger.
        /// </summary>
        [SerializeField]
        [Tooltip("Gameobject to activate when player enters the trigger.")]
        GameObject _target;

        /// <summary>
        /// Audio to play when ui shows.
        /// </summary>
        [SerializeField]
        [Tooltip("Audio to play when shown")]
        AudioSource _audioShow;

        /// <summary>
        /// Audio to play when ui hides.
        /// </summary>
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

        /// <summary>
        /// Animates showing <seealso cref="_target"/>
        /// </summary>
        /// <param name="scaleTarget">Target scale of the object</param>
        /// <param name="step">Amount to change scale on step</param>
        /// <returns></returns>
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

        /// <summary>
        /// Animates hiding <seealso cref="_target"/><br><br></br></br>
        /// Set <see cref="_target"/> to non active after finished.
        /// </summary>
        /// <param name="scaleTarget">Target scale of the object</param>
        /// <param name="step">Amount to change scale on step</param>
        /// <returns></returns>
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
