using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tv_SakariTrigger : MonoBehaviour
{
    public Tv_Sakari sakariBehaviour;
    bool testipaska = false;
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
        if (other.gameObject.CompareTag("tv_pullo") && !testipaska)
        {
            sakariBehaviour.bottle = other.gameObject;
            sakariBehaviour.isBottle = true;
            Debug.Log("Pullo");
            testipaska=true;
        }
    }
}
