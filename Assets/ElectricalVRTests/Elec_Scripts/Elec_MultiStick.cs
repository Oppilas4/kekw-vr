using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_MultiStick : MonoBehaviour
{
    public Elec_Multimeter MamaMultimeter;
    void Start()
    {
        MamaMultimeter = transform.parent.GetComponentInChildren<Elec_Multimeter>();
    }
}
