using UnityEngine;
using System.Collections;
using UnityEngine.VFX;

namespace Kekw.Interaction
{
    /// <summary>
    /// Playable character mouth implementation.
    /// Plays correct sounds and vfx effect based on collider that hits mouth.
    /// Relies heavily to <seealso cref="EdibleType"/>.
    /// </summary>
    public class Mouth: MonoBehaviour
    {
        /// <summary>
        /// Biting vfx effect
        /// </summary>
        [SerializeField]
        [Tooltip("VFX to play when taking bite")]
        VisualEffect _visualEffect;

        /// <summary>
        /// Audio to play when eating solid stuff.
        /// </summary>
        [SerializeField]
        [Tooltip("Eating audio")]
        AudioSource _eatingAudio;

        /// <summary>
        /// Audio to play when drinking stuff.
        /// </summary>
        [SerializeField]
        [Tooltip("Drinking audio")]
        AudioSource _drinkingAudio;

        Coroutine _eatingDelay;

        private void OnTriggerStay(Collider other)
        {
            EdibleType edibleType = other.GetComponentInChildren<EdibleType>();
            
            if(edibleType.ETYPE == EdibleTypes.EAT && _eatingDelay == null)
            {
                _eatingDelay = StartCoroutine(EatSingleChunkDelay(other));
                return;
            }

            if (edibleType.ETYPE == EdibleTypes.DRINK)
            {
                if (!_drinkingAudio.isPlaying && other.transform.parent.GetComponentInChildren<Vatupassi>().IsPouring)
                {
                    _drinkingAudio.Play();
                }
                return;
            }
        }


        /// <summary>
        /// When edible exits
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit(Collider other)
        {
            EdibleType edibleType = other.GetComponentInChildren<EdibleType>();
            if (edibleType.ETYPE == EdibleTypes.DRINK)
            {
                _drinkingAudio.Stop();
                return;
            }
        }

        /// <summary>
        /// Delay between chunks while eating.
        /// </summary>
        /// <param name="other">Target we are eating.</param>
        /// <returns></returns>
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
