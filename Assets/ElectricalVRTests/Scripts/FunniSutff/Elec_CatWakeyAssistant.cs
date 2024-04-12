using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Elec_CatWakeyAssistant : MonoBehaviour
{
    public void OnAnimation()
    {
        GetComponentInParent<NavMeshAgent>().enabled = true;
    }
}
