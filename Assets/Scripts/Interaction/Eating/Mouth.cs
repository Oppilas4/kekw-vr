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
            Debug.Log("asdasdasd");
            EdibleType edibleType = GetEdibleType(other);
            
            if(edibleType.ETYPE == EdibleTypes.EAT && _eatingDelay == null)
            {
                _eatingDelay = StartCoroutine(EatSingleChunkDelay(other));
                return;
            }

            if (edibleType.ETYPE == EdibleTypes.DRINK)
            {
                if (!_drinkingAudio.isPlaying)
                {
                    _drinkingAudio.Play();
                }
                return;
            }
        }

        private EdibleType GetEdibleType(Collider other)
        {
            EdibleType edibleType;

            other.TryGetComponent<EdibleType>(out edibleType);

            if (!edibleType)
            {
                edibleType = GetComponentInChildren<EdibleType>();
            }

            if (!edibleType)
            {
                throw new System.Exception("Edible type missing on edible object!");
            }
            return edibleType;
        }

        /// <summary>
        /// When edible exits
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit(Collider other)
        {
            EdibleType edibleType = GetEdibleType(other);
            if (edibleType.ETYPE == EdibleTypes.DRINK)
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
