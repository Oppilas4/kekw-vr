using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_SithWire : MonoBehaviour
{
    ConfigurableJoint Spring;
    public GameObject Origin;
    private void Start()
    {
        Spring = Origin.GetComponent<ConfigurableJoint>();
    }
    private void Update()
    {
    }
}
    