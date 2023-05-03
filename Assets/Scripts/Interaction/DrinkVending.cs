using System.Collections;
using UnityEngine;
using Kekw.Pool;
using Kekw.Pool.Drink;

namespace Kekw.Interaction
{
    /// <summary>
    /// Drink vending machine.<br></br><br></br>
    /// Machine uses drink pool that must be in scene in advance. Can only spawn one drink at the time.
    /// </summary>
    class DrinkVending: MonoBehaviour
    {
        /// <summary>
        /// Component that spawns drinks.
        /// </summary>
        [SerializeField]
        [Tooltip("Drink spawner")]
        DrinkSpawner _drinkSpawner;

        /// <summary>
        /// Button can be interacted material
        /// </summary>
        [SerializeField]
        [Tooltip("Button available material")]
        Material _available;

        /// <summary>
        /// Button cannot be interacted material
        /// </summary>
        [SerializeField]
        [Tooltip("Button not available")]
        Material _disabled;

        /// <summary>
        /// Audio when button is pressed
        /// </summary>
        [SerializeField]
        [Tooltip("Button press audio")]
        AudioSource _buttonPressAudio;

        /// <summary>
        /// Drink sliding inside machine audio.
        /// </summary>
        [SerializeField]
        [Tooltip("Drink coming audio")]
        AudioSource _drinkComingAudio;

        APoolMember _trackedDrink;
        MeshRenderer _meshRenderer;
        Coroutine _coroutine;

        private void Awake()
        {
            // Subscribe to event when drink is returned to pool
            DrinkSpawnManager.OnDrinkReset += DrinkSpawnManager_OnDrinkReset;
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void DrinkSpawnManager_OnDrinkReset()
        {
            _meshRenderer.material = _available;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("LeftHand") || collision.gameObject.CompareTag("RightHand"))
            {
                if ((_trackedDrink == null || !_trackedDrink.gameObject.activeSelf) && _coroutine == null)
                {
                    _coroutine = StartCoroutine(WaitAudio(_drinkComingAudio.clip.length));
                    _buttonPressAudio.PlayOneShot(_buttonPressAudio.clip);
                    _drinkComingAudio.PlayOneShot(_drinkComingAudio.clip);
                }
            }
        }

        /// <summary>
        /// Spawn drink after work audio <seealso cref="_drinkComingAudio"/> is played.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        IEnumerator WaitAudio(float time)
        {
            yield return new WaitForSeconds(time);
            _trackedDrink = _drinkSpawner.SpawnDrink();
            if (_trackedDrink != null)
            {
                _meshRenderer.material = _disabled;
            }
            _coroutine = null;
        }
        
    }
}
