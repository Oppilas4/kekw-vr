using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Elec_EmergencyStopButton : MonoBehaviour
{
    public float threshold = 0.002f;
    public Transform target;
    public UnityEvent Reached;
    private bool wasReached = false;
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < threshold && !wasReached)
        {
            Reached.Invoke();
            wasReached = true;
        }
        else if (distance >= threshold)
        {
            wasReached = false;
        }
    }
}
