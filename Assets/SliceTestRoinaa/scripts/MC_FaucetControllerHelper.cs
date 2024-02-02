using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_FaucetControllerHelper : MonoBehaviour
{
    public MC_FaucetController faucetController;
    
    public bool isWaterOn()
    {
        bool isOn = faucetController.GetIsWaterOn();
        return isOn;
    }
}
