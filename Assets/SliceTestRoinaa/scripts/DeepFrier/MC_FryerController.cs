using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_FryerController : MonoBehaviour, IDial
{
    private bool isOn = false;
    public MC_OilController oilController;

    public void DialChanged(float dialValue)
    {
        Debug.Log(dialValue);
        if (dialValue >= 25 )
        {
            isOn = true;
            oilController.SetHot(true);
        }
        else
        {
            isOn = false;
            oilController.SetHot(false);
        }
    }

    public bool GetIsOn()
    {
        return isOn;
    }
}
