using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[Obsolete]
public class MC_OrderTicketMovement : MonoBehaviour
{
    private Transform targetPosition; // The target position to move towards
    public float speed = 1f; // The speed at which the order ticket moves
    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;
    private MC_OrderTicketManager orderTicketManager;
    public Collider col;
    

    private void OnEnable()
    {
        orderTicketManager = FindAnyObjectByType<MC_OrderTicketManager>();
        GameObject targetObject = GameObject.Find("TicketMoveLoc");
        if (targetObject != null)
        {
            targetPosition = targetObject.transform;
        }
        else
        {
            Debug.LogError("Object 'TicketMoveLoc' not found.");
        }

        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.onSelectEntered.AddListener(HandleGrab);
    }


    private void OnDisable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.onSelectEntered.RemoveListener(HandleGrab);
        }
    }

    
    private void HandleGrab(XRBaseInteractor interactor)
    {
        //rb.isKinematic = false;
        orderTicketManager.removeTicketFromList(gameObject);
        rb.constraints = RigidbodyConstraints.None;
        // Remove the listener to ensure this method is only called once
        grabInteractable.onSelectEntered.RemoveListener(HandleGrab);
        grabInteractable = null;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (!orderTicketManager.isOccupied(gameObject))
        {
            StartCoroutine(MoveTicket());
        }
    }

    public void Move()
    {
        StartCoroutine(MoveTicket());
    }
    private IEnumerator MoveTicket()
    {
        orderTicketManager.PlayPrintSound();
        while (Vector3.Distance(transform.position, targetPosition.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);
            yield return null;
        }
        col.enabled = true;
    }

    private void OnDestroy()
    {
        orderTicketManager.removeTicketFromList(gameObject);
    }
}
