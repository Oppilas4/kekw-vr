using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_RamiCellPack : MonoBehaviour
{
    public GameObject Battery,SpawnPoint;
    public void SpawnBatter()
    {
        Instantiate(Battery,SpawnPoint.transform.position,SpawnPoint.transform.rotation);
    }
}
