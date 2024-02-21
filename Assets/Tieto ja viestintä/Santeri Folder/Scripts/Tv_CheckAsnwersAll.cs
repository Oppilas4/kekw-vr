using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class Tv_CheckAsnwersAll : MonoBehaviour
{
    public Tv_IdCheck[] checkes;
    public int questionID;
    [SerializeField] TMP_Text questionText;
    [SerializeField] Tv_TheAnswerCube[] AnswerOBJ;
    public XRSocketInteractor[] XRSocketInteractor;
    Vector3 socket1, socket2;


    public int howMuchIsNeeded;
    int howManyNow = 0;

    bool buttonWorks = true;

    


    private void OnTriggerEnter(Collider other)
    {
        if(buttonWorks)
        {
            AnswerTime();
            StartCoroutine(MyCoroutine());
        }
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
                    WhatHappensWhenAllCorrect();
                }
            }
        }
    }


    public void WhatHappensWhenAllCorrect()
    {
        if (howMuchIsNeeded == howManyNow)
        {
            Debug.Log("Before switch");

            switch (questionID)
          {
            
                case 0:
                    questionText.text = "Using UnityEnine;\r\n\r\nclass bla bla bla\r\n{\r\n  if(              )\r\n {\r\n\r\n\r\n}\r\n\r\n}";

                    //socket1 = new Vector3(0, 0, 0);
                    //socket2 = new Vector3(0, 0, 0);
                    checkes[0].requiredID = 1;
                    checkes[1].requiredID = 2;
                    //checkes[0].transform.position = socket1;
                    //checkes[1].transform.position = socket2;
                    questionID++;
                    howManyNow = 0;

                    break;

                case 1:

                    questionText.text = "Using UnityEnine;\r\n\r\nclass bla bla bla\r\n{\r\n  if(              )\r\n {\r\n\r\n\r\n}\r\n\r\n}";

                    socket1 = new Vector3(-0.6569991f, 0.018f, 0.2860771f);
                    socket2 = new Vector3(-0.6569991f, -0.123f, 0.3460771f);
                    checkes[0].requiredID = 3;
                    checkes[1].requiredID = 4;
                    checkes[0].transform.position = socket1;
                    checkes[1].transform.position = socket2;


                    howManyNow = 0;
                    questionID++;

                    break;

                case 2:

                    questionText.text = "Using UnityEnine;\r\n\r\nclass bla bla bla\r\n{\r\n  if(              )\r\n {\r\n\r\n\r\n}\r\n\r\n}";

                    //socket1 = new Vector3(0, 0, 0);
                    //socket2 = new Vector3(0, 0, 0);
                    checkes[0].requiredID = 3;
                    checkes[1].requiredID = 4;
                    //checkes[0].transform.position = socket1;
                    //checkes[1].transform.position = socket2;


                    howManyNow = 0;
                    questionID = 0;

                    break;


            }
        }
    }

    IEnumerator MyCoroutine()
    {   
        buttonWorks = false;

        foreach (var answerObj in AnswerOBJ)
        {
            if (answerObj != null)
            {
                answerObj.ResetAnwsers();
            }
        }

        foreach (var questionSocket in XRSocketInteractor)
        {
            if (questionSocket != null)
            {
                questionSocket.socketActive = false;
                
            }
        }

        yield return new WaitForSeconds(1.0f);
        buttonWorks = true;

        foreach (var questionSocket in XRSocketInteractor)
        {
            if (questionSocket != null)
            {
                questionSocket.socketActive = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

}
