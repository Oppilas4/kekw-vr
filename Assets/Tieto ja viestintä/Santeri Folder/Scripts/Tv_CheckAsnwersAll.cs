using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tv_CheckAsnwersAll : MonoBehaviour
{
    public Tv_IdCheck[] checkes;

    public int howMuchIsNeeded;
    int howManyNow = 0;

    private void OnTriggerEnter(Collider other)
    {
        AnswerTime();
    }

    public void AnswerTime()
    {
        foreach (var idCheck in checkes)
        {
            if (idCheck != null)
            {
                if (idCheck.hasCorrect)
                {
                    howManyNow++;
                    WhatHappensWhenAll();
                }
            }
        }
    }

    public void WhatHappensWhenAll()
    {
        if(howMuchIsNeeded == howManyNow)
        {
            Debug.Log("Kaikki oli oikein");
        }
    }
}
