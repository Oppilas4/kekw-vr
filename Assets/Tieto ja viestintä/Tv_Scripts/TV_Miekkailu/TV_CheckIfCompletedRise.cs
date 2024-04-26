using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_CheckIfCompletedRise : MonoBehaviour
{
    public int hasTheNumber = 0;

    private void Start()
    {
        hasTheNumber = PlayerPrefs.GetInt("TV_RiseOfTheDark", 0);

        if(hasTheNumber == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
