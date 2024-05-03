using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Tv_JoonasActivation : MonoBehaviour
{
    public TV_JoonasBehaviour joonas;
    public bool hasReachedDesk = true;
    [SerializeField] AudioSource source;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftHand") || other.CompareTag("RightHand"))
        {
            if(hasReachedDesk)
            {
                source.Play();
                joonas.StartToMove(joonas.whereToGoStand);
            }
        }
    }
}
