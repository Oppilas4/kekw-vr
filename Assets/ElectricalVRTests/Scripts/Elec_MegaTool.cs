using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_MegaTool : MonoBehaviour
{
    public GameObject EndPrefab;
    public GameObject SpawnPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MakeWireEnd()
    {
        Instantiate(EndPrefab, SpawnPos.transform.position, SpawnPos.transform.rotation,SpawnPos.transform);
    }
}
