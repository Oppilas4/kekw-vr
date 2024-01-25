using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_BurnerHelper : MonoBehaviour
{
    public FlameController flameController;

    public bool isBurnerOn()
    {
        bool isOn = flameController.GetIsOn();
        return isOn;
    }
}
