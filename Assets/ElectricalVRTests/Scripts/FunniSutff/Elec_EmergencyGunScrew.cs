using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_EmergencyGunScrew : MonoBehaviour
{
    Elec_EmergencyBox MamaBox;
    bool unscrewed = false;
    private void Start()
    {
        MamaBox = GetComponentInParent<Elec_EmergencyBox>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ScrewDriver" && !unscrewed)
        {
            unscrewed=true;
            GetComponent<Rigidbody>().isKinematic = false;
            MamaBox.Screws--;
        }
    }
}
