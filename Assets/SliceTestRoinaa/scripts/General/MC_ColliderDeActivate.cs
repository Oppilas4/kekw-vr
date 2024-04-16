using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class MC_ColliderDeActivate : MonoBehaviour
{
    public Collider lidCollider;
    private XRSocketInteractor socketInteractor;
    public AudioSource _audioSource;

    void Awake()
    {
        // Find the XR Socket Interactor on the pot
        socketInteractor = GetComponent<XRSocketInteractor>();
        if (socketInteractor == null)
        {
            Debug.LogError("No XR Socket Interactor found in parent objects.");
        }
    }

    void OnEnable()
    {
        // Subscribe to the selectEntered and selectExited events
        socketInteractor.selectEntered.AddListener(OnLidAttached);
        socketInteractor.selectExited.AddListener(OnLidDetached);
    }

    void OnDisable()
    {
        // Unsubscribe from the selectEntered and selectExited events
        socketInteractor.selectEntered.RemoveListener(OnLidAttached);
        socketInteractor.selectExited.RemoveListener(OnLidDetached);
    }

    private void OnLidAttached(SelectEnterEventArgs args)
    {
        // When the lid is attached, disable its collider
        lidCollider.isTrigger = true;
        if (_audioSource != null)
        {
            _audioSource.Play();
        }
    }

    private void OnLidDetached(SelectExitEventArgs args)
    {
        // When the lid is detached, re-enable its collider
        lidCollider.isTrigger = false;
    }
}
