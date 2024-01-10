using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_WireEnds : MonoBehaviour
{
    public int WireEndVolt;
    public Wire MainestWire;
    // Start is called before the first frame update
    void Start()
    {
        MainestWire = gameObject.GetComponentInParent<Wire>();
    }

    // Update is called once per frame
    void Update()
    {    
        WireEndVolt = MainestWire.WireVoltage;
    }
}
