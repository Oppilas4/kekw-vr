using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_DestroyAfterDelay : MonoBehaviour
{
    public int Delay;

    private void Start()
    {
        Destroy(gameObject, Delay);
    }
}
