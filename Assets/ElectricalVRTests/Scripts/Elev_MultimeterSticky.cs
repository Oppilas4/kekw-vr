using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elev_MultimeterSticky : MonoBehaviour
{
    public Elec_Multimeter multimeterDad;
    public int SitckyVoltage = 0;
    public void EqualVoltages(int VoltageStick)
    {
        if (multimeterDad.VoltageMusltimeter == VoltageStick)
        {
            multimeterDad.VoltageText.color = Color.green;
        }
        else
        {
            multimeterDad.VoltageText.color = Color.red;
        }
    }
    public void Chill()
    {
        multimeterDad.VoltageText.color = Color.white;
    }
}
