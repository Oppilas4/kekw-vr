using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    LineRenderer wire;
    public GameObject start, end;
    public int lenght = 0;

    void Start()
    {
        wire = GetComponent<LineRenderer>();
        wire.positionCount = lenght;
    }

    // Update is called once per frame
    void Update()
    {
       wire.SetPosition(0, start.transform.position);
        wire.SetPosition(1, end.transform.position);
    }
}
