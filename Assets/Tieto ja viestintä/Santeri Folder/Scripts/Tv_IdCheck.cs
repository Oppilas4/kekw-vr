using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tv_IdCheck : MonoBehaviour
{
    public int requiredID;
    public bool hasCorrect;

    public void Check(int number)
    {
        if(!hasCorrect)
        {
            if (number == requiredID)
            {
                hasCorrect = true;
            }
        }
    }
}
