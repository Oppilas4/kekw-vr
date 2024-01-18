using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Elec_TriggerTest : MonoBehaviour
{
    public TextMeshPro TextMeshPro;
    // Update is called once per frame
    void Update()
    {
        TextMeshPro.text = "" + Input.GetAxis("XRI_Left_Trigger");
    }
}
