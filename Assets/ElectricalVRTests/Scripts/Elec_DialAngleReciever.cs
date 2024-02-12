using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Elec_DialAngleReciever : MonoBehaviour, IElecDial
{
    public UnityEvent OnReachedAngle;
    public UnityEvent OnZeroDegree;
    public void DialChanged(float dialvalue)
    {
        if (dialvalue == 90)
        {
            OnReachedAngle.Invoke();
        }
        else if (dialvalue == 0)
        {
            OnZeroDegree.Invoke();
        }
    }
}
