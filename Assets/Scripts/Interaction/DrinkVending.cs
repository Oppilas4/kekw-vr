using System;
using System.Collections;
using UnityEngine;
using Kekw.Pool;

namespace Kekw.Interaction
{
    /// <summary>
    /// Drink vending machine
    /// </summary>
    class DrinkVending: MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Drink spawner")]
        DrinkSpawner _drinkSpawner;

        [SerializeField]
        [Tooltip("Button available material")]
        Material _available;

        [SerializeField]
        [Tooltip("Button not available")]
        Material _disabled;

        [SerializeField]
        [Tooltip("Button press audio")]
        AudioSource _buttonPressAudio;

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
