using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_PowerCord : MonoBehaviour
{
    // I've made this script just for power cords to connect simple stuff,like screen,to the outlets
    LineRenderer lineRenderer;
    public Transform plug;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0,gameObject.transform.position);
        lineRenderer.SetPosition(1, plug.position);
    }
}
