using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_CubeMove : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector3(transform.position.x + Time.deltaTime * 2, transform.position.y, transform.position.z);
    }
}
