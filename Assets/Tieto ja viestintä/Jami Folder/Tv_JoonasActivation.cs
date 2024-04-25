using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tv_JoonasActivation : MonoBehaviour
{
    public TV_JoonasBehaviour joonas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftHand") || other.CompareTag("RightHand"))
        {
            joonas.StartToMove(joonas.whereToGoStand);
            this.gameObject.SetActive(false);
        }
    }
}
