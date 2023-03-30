using UnityEngine;
using System.Collections;
using UnityEngine.VFX;

namespace Kekw.Interaction
{
    /// <summary>
    /// Class provides mouth and eating functionality.
    /// </summary>
    class Mouth: MonoBehaviour
    {

        [SerializeField]
        [Tooltip("VFX to play when taking bite")]
        VisualEffect _visualEffect;

        [SerializeField]
        [Tooltip("Eating audio")]
        AudioSource _audioSource;

        Coroutine _eatingDelay;

        private void OnTriggerStay(Collider other)
        {
            if(_eatingDelay == null)
            {
                _eatingDelay = StartCoroutine(EatSingleChunkDelay(other));
            }
        }

        IEnumerator EatSingleChunkDelay(Collider other)
        {
            yield return new WaitForSeconds(1f);
            other.GetComponent<Edible>().EatChunk();
            _visualEffect.Play();
            _audioSource.PlayOneShot(_audioSource.clip);
            _eatingDelay = null;
        }
    }
}
