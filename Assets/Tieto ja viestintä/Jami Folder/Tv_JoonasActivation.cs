using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tv_JoonasActivation : MonoBehaviour
{
    public TV_JoonasBehaviour joonas;
    public bool hasReachedDesk = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftHand") || other.CompareTag("RightHand"))
        {
            if(hasReachedDesk)
            {
                joonas.StartToMove(joonas.whereToGoStand);
            }
        }
    }
}
