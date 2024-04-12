using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Elec_CatWakeyAssistant : MonoBehaviour
{
    public Transform GoTo;
    public void OnAnimation()
    {
        GetComponentInParent<Elec_CatAI>().Slepy = false;
        GetComponentInParent<NavMeshAgent>().enabled = true;
    }
    public void GoAway()
    {
        GetComponentInParent<NavMeshAgent>().enabled = true;
        GetComponentInParent<NavMeshAgent>().SetDestination(GoTo.position);
    }
}
