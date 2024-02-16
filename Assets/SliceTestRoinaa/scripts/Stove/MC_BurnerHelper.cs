using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_BurnerHelper : MonoBehaviour, IHotObject
{
    public FlameController flameController;

    private void OnEnable()
    {
        HotObjectManager.RegisterHotObject(this);
    }

    private void OnDisable()
    {
        HotObjectManager.UnregisterHotObject(this);
    }

    public void SetHot(bool isHot)
    {
        // Implement logic to set the hot state of the pot
    }

    public bool IsHot()
    {
        return isBurnerOn();
    }

    public bool isBurnerOn()
    {
        bool isOn = flameController.GetIsOn();
        return isOn;
    }
}
