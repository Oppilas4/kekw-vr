using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Elec_Multimeter : MonoBehaviour
{
    LineRenderer MultiWire;
    public GameObject start, end;
    // Start is called before the first frame update
    void Start()
    {
        MultiWire = GetComponent<LineRenderer>();   
        MultiWire.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        MultiWire.SetPosition(0, start.transform.position);
        MultiWire.SetPosition(1, end.transform.position);
    }
}
