using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Tv_IdCheck : MonoBehaviour
{
    public int requiredID;
    public bool hasCorrect;
    public bool selected;
    Tv_TheAnswerCube anwserCube;
    XRSocketInteractor mySocket;
    GameObject myGameObject;
    [System.Obsolete]

    private void Start()
    {
        mySocket = GetComponent<XRSocketInteractor>();
    }
    private void Update()
    {
        
       

    }
    public void Check(int number)
    {
        if(!hasCorrect)
        {
            if (number == requiredID)
            {
                hasCorrect = true;
                Debug.Log("OIKEIN");
            }
        }

        else if (hasCorrect)
        {
            if (number != requiredID)
            {
                hasCorrect = false;
                Debug.Log("VÄÄRIN");
                
            }
        }

    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (!mySocket)
        {
            if (other.CompareTag("Tv_vastausCube"))
            {
                anwserCube = other.GetComponent<Tv_TheAnswerCube>();
                Check(anwserCube.id);
                //selected = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (mySocket)
        {
            if (other.CompareTag("Tv_vastausCube"))
            {
                //Check(anwserCube.id);
                //selected = false;
            }
        }
    }

    */
    public void JokuNimi()
    {
        var selectedGameObject = mySocket.GetOldestInteractableSelected();
        anwserCube = selectedGameObject.transform.gameObject.GetComponent<Tv_TheAnswerCube>();
        Debug.Log(anwserCube.id);
        Check(anwserCube.id);
    }

}
