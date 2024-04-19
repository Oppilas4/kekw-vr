using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MC_DefaultSocket : MonoBehaviour
{
    public GameObject _socketInteractor;
    public GameObject _objectToSocket;

    void Start()
    {
        // Assuming the knife holder has an XR Socket Interactor component attached
        XRSocketInteractor socketInteractor = _socketInteractor.GetComponent<XRSocketInteractor>();
        if (socketInteractor != null)
        {
            // Set the knife as the starting selected interactable
            socketInteractor.startingSelectedInteractable = _objectToSocket.GetComponent<XRGrabInteractable>();
        }
    }
}
