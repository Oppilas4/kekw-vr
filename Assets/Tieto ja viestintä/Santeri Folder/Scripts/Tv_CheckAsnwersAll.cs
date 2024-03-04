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
    public GameObject socketOBJ1, socketOBJ2;
    Vector3 socket1, socket2;


    public int howMuchIsNeeded;
    int howManyNow = 0;

    bool buttonWorks = true;

    private void Start()
    {
        questionText.text = "Using UnityEnine;\r\n\r\npublic class Luo Esine : MonoBehavioura\r\n{\r\nEsine (           ) \r\n\r\nSijainti (          )\r\n \r\n}";

        socket1 = new Vector3(-0.6569991f, 0.041f, 0.277f);
        socket2 = new Vector3(-0.6569991f, -0.058f, 0.269f);

        socketOBJ1.transform.localPosition = socket1;
        socketOBJ2.transform.localPosition = socket2;
    }



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
                    questionText.text = "Using UnityEnine;\r\n\r\npublic class Luo Esine : MonoBehavioura\r\n{\r\nEsine (           ) \r\n\r\nSijainti (          )\r\n \r\n}";

                    socket1 = new Vector3(-0.6569991f, 0.041f, 0.277f);
                    socket2 = new Vector3(-0.6569991f, -0.062f, 0.272f);

                    socketOBJ1.transform.localPosition = socket1;
                    socketOBJ2.transform.localPosition = socket2;

                    checkes[0].requiredID = 1;
                    checkes[1].requiredID = 2;

                    questionID++;
                    howManyNow = 0;
                    Debug.Log("Eka");
                    break;

                case 1:

                    questionText.text = "Using UnityEnine;\r\n\r\nclass bla bla bla\r\n{\r\n  if(              )\r\n {\r\n\r\n\r\n}\r\n\r\n}";

                    socket1 = new Vector3(-0.6569991f, 0.018f, 0.2860771f);
                    socket2 = new Vector3(-0.6569991f, -0.123f, 0.3460771f);

                    socketOBJ1.transform.localPosition = socket1;
                    socketOBJ2.transform.localPosition = socket2;

                    checkes[0].requiredID = 3;
                    checkes[1].requiredID = 4;

                   
                   howManyNow = 0;
                    questionID++;

                    break;

                case 2:

                    questionText.text = "Using UnityEnine;\r\n\r\npublic class VesiPyssy : MonoBehavioura\r\n{\r\n   if(                  )\r\n\r\n    {\r\n\r\n\r\n    }\r\n \r\n}";

                    socket1 = new Vector3(-0.6569991f, 0.041f, 0.3f);
                    socket2 = new Vector3(-0.6569991f, -0.134f, 0.332f);

                    socketOBJ1.transform.localPosition = socket1;
                    socketOBJ2.transform.localPosition = socket2;

                    checkes[0].requiredID = 5;
                    checkes[1].requiredID = 6;

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

}
