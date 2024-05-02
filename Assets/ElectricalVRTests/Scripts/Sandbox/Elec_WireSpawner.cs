using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_WireSpawner : MonoBehaviour
{
    public GameObject WirePrefab;
    void Start()
    {
        
    }
    [ContextMenu("SpawnWire")]
    public void SpawnWire()
    {
        Instantiate(WirePrefab,transform.position,transform.rotation);
    }
}
