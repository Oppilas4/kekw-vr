using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecGridTes : MonoBehaviour
{
    public GameObject gridComponent;
    public int gridHEight, gridWidght;
    public float gridSpacing;
    public Vector3 gridOrigin;
    public Quaternion originRot;
    void Start()
    {
        gridOrigin = transform.position;
        originRot = transform.rotation;
        SpawnGrid();
    }
    public void SpawnGrid()
    {
        for( int i = 0; i < gridHEight; i++ ) 
        {
            for( int j = 0; j < gridWidght; j++ ) 
            {
             Vector3 spawnPoint = new Vector3( i * gridSpacing , j * gridSpacing, 0  )+ gridOrigin;
                Instantiate(gridComponent,spawnPoint,originRot,gameObject.transform);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
