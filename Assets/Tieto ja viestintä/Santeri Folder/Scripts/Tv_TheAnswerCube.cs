using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tv_TheAnswerCube : MonoBehaviour
{
    public int id;
    public Tv_IdCheck check;
    public bool selected;

    private void OnTriggerEnter(Collider other)
    {
        if(!selected)
        {
            if (other.CompareTag("Tv_vastaus"))
            {
                check = other.GetComponent<Tv_IdCheck>();
                check.Check(id);
                selected = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(selected)
        {
            if (other.CompareTag("Tv_vastaus"))
            {
                 check.Check(0);
                 selected = false;
            }
        }
    }
}
