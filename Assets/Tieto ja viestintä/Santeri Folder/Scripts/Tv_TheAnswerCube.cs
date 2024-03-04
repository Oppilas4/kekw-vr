using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tv_TheAnswerCube : MonoBehaviour
{
    public int id;
    public bool selected;
    Vector3 resetTransform;

    public void Start()
    {
       resetTransform = gameObject.transform.position;
    }


    public void ResetAnwsers()
    {
        selected = false;
        gameObject.transform.position = resetTransform;
    }
}
