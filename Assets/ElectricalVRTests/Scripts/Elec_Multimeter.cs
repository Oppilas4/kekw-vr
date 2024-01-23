using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Elec_Multimeter : MonoBehaviour
{
    LineRenderer MultiWire;
    public GameObject start, end;
    public int VoltageMusltimeter,StickyVoltage;
    public TextMeshPro VoltageText;
    // Start is called before the first frame update
    void Start()
    {

        MultiWire = GetComponent<LineRenderer>();   
        MultiWire.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        VoltageText.text = VoltageMusltimeter + "/" + StickyVoltage;
        MultiWire.SetPosition(0, start.transform.position);
        MultiWire.SetPosition(1, end.transform.position);
        if (VoltageMusltimeter > 0 && StickyVoltage > 0)
        {
            if (StickyVoltage == VoltageMusltimeter)
            {
                VoltageText.color = Color.green;
            }
            if (StickyVoltage != VoltageMusltimeter)
            {
                VoltageText.color = Color.red;
            }
        }
        else
        {
            VoltageText.color= Color.white;
        }
        
    }
}
