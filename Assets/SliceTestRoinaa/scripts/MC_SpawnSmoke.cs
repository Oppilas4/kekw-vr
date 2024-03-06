using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_SpawnSmoke : MonoBehaviour
{
    public GameObject Smoke;
    private void OnDestroy()
    {
        Debug.Log("tapahtuko");
        Instantiate(Smoke, transform.position, Quaternion.identity);
    }
}
