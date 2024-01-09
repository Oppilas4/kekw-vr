using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_WireEnds : MonoBehaviour
{
    public int WireEndVolt;
    public Wire WireObject;
    // Start is called before the first frame update
    void Start()
    {
        WireEndVolt = WireObject.WireVoltage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
