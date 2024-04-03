using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_VuoksiBeatsDestroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("VuoksiBeats"))
        {
            Destroy(other.gameObject);
        }
    }
}
