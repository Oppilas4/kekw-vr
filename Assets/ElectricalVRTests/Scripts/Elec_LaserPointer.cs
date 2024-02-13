using Autodesk.Fbx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_LaserPointer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject Point;
    public bool HandHeld = false;
     void Start()
    {
        Point.GetComponent<MeshRenderer>().materials[0].color = Color.red;
        lineRenderer = GetComponent<LineRenderer>();
        if (HandHeld)
        {
            Point.SetActive(false);
            lineRenderer.enabled = false;
        }  
    }
    public void TurnOnOffLaser()
    {
        if(lineRenderer.enabled) 
        {
            lineRenderer.enabled = false;
            Point.SetActive(false);
        }
        else if(!lineRenderer.enabled) 
        {
            lineRenderer.enabled = true;
            Point.SetActive(true);
        }
    }
    void Update()
    {
        if(lineRenderer.enabled) 
        {
            lineRenderer.SetPosition(0,transform.position);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit, 10000))
            {
                lineRenderer.SetPosition(1, hit.point);
                Point.transform.position = hit.point;
            }       
        }
    }
}
