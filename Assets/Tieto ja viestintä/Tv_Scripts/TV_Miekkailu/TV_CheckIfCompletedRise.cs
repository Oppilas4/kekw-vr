using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_CheckIfCompletedRise : MonoBehaviour
{
    public int hasTheNumber = 0;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        hasTheNumber = PlayerPrefs.GetInt("TV_RiseOfTheDark", 0);

        if(hasTheNumber == 0)
        {
            gameObject.SetActive(false);
        }
        
    }

    public void Congratulations()
    {
        audioSource.Play();
    }
}
