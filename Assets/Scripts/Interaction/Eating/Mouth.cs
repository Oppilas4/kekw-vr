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
        AudioSource _eatingAudio;

        [SerializeField]
        [Tooltip("Drinking audio")]
        AudioSource _drinkingAudio;

        Coroutine _eatingDelay;

        private void OnTriggerStay(Collider other)
        {
            if(other.GetComponentInChildren<EdibleType>().ETYPE == EdibleTypes.EAT && _eatingDelay == null)
            {
                _eatingDelay = StartCoroutine(EatSingleChunkDelay(other));
                return;
            }

            if (other.GetComponentInChildren<EdibleType>().ETYPE == EdibleTypes.DRINK)
            {
                _drinkingAudio.Play();
                return;
            }
        }

        /// <summary>
        /// When edible exits
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponentInChildren<EdibleType>().ETYPE == EdibleTypes.DRINK)
            {
                _drinkingAudio.Stop();
                return;
            }
        }

        IEnumerator EatSingleChunkDelay(Collider other)
        {
            yield return new WaitForSeconds(1f);
            other.GetComponent<Edible>().EatChunk();
            _visualEffect.Play();
            _eatingAudio.PlayOneShot(_eatingAudio.clip);
            _eatingDelay = null;
        }
    }
}
