using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_BLCylinder : MonoBehaviour
{
    public List<Elec_BlSingleCylinder> Cylinders = new List<Elec_BlSingleCylinder>();
    public int CurrenCylinderId = 0;
    [ContextMenu("Fortnite balls")]
    public void RotateToNExtRound()
    {
        if (CurrenCylinderId == Cylinders.Count - 1 || CurrenCylinderId == Cylinders.Count)
        {
            CurrenCylinderId = 0;
        }
        else
        {
            transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 60.0f);
            CurrenCylinderId++;
        }  
    }
}
