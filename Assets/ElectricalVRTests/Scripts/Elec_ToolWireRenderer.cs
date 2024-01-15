using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_ToolWireRenderer : MonoBehaviour
{
    public List<GameObject> WireComponents = new List<GameObject>();
    public LineRenderer WireRenderer;
    void Start()
    {       
       
    }

    void Update()
    {
        WireRenderer.positionCount = WireComponents.Count;
        for (int i = 0; i < WireComponents.Count; i++)
        { 
            WireRenderer.SetPosition(i, WireComponents[i].transform.position);
        }
    }
}
