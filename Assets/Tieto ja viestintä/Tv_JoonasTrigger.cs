using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tv_JoonasTrigger : MonoBehaviour
{
    public Tv_JoonasBehaviour joonasB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // If the player enters the trigger area, make the NPC stand up from the chair
            if (joonasB.isSeated)
            {
                print("gäy");
                joonasB.StandUp();
            }
        }
    }
}
