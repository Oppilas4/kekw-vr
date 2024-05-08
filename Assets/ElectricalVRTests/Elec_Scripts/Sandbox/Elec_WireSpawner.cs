using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_WireSpawner : MonoBehaviour
{
    public GameObject WirePrefab;
    public Transform WirePos;
    void Start()
    {
        
    }
    [ContextMenu("SpawnWire")]
    public void SpawnWire()
    {
        Instantiate(WirePrefab,WirePos.position,WirePos.rotation);
    }
}
