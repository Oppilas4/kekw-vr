using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_SherEyeAnimAssist : MonoBehaviour
{
    Elec_RamiEye Eye;
    private void Start()
    {
        Eye = GetComponentInParent<Elec_RamiEye>();
    }
    public void OnAnimCallDeez()
    {
        Eye.GoodGuitar();
    }
}
