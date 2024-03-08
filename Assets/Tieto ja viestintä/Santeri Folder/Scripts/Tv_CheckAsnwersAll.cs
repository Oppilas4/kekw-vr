using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

[System.Serializable]
public class QuestionInfo
{
    [TextArea(3, 10)]
    public string theQuestion;

    public Vector3 socket1Position;
    public Vector3 Socket2Position;

    public int requiredId;
    public int requiredId2;
}

public class Tv_CheckAsnwersAll : MonoBehaviour
{
    public QuestionInfo[] questionInfos;

    public Tv_IdCheck[] checkes;
    public int questionID;
    [SerializeField] TMP_Text questionText;
    [SerializeField] Tv_TheAnswerCube[] AnswerOBJ;
    public XRSocketInteractor[] XRSocketInteractor;

    public int howMuchIsNeeded;
    int howManyNow = 0;

    bool buttonWorks = true;


    private void OnTriggerEnter(Collider other)
    {
        if(buttonWorks)
        {
            AnswerTime();
            StartCoroutine(SocketReseter());
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
            switch (questionID)
          {
            
                case 0:

                    QuestionInfo questionInfo0 = questionInfos[questionID];
                    questionText.text = questionInfo0.theQuestion;

                    AnswerOBJ[0].transform.localPosition = questionInfo0.socket1Position;
                    AnswerOBJ[1].transform.localPosition = questionInfo0.Socket2Position;

                    checkes[0].requiredID = questionInfo0.requiredId;
                    checkes[1].requiredID = questionInfo0.requiredId2;

                    questionID++;
                    howManyNow = 0;
                    break;

                case 1:

                    QuestionInfo questionInfo1 = questionInfos[questionID];
                    questionText.text = questionInfo1.theQuestion;

                    AnswerOBJ[0].transform.localPosition = questionInfo1.socket1Position;
                    AnswerOBJ[1].transform.localPosition = questionInfo1.Socket2Position;

                    checkes[0].requiredID = questionInfo1.requiredId;
                    checkes[1].requiredID = questionInfo1.requiredId2;


                    howManyNow = 0;
                    questionID = 0;

                    break;
            }
        }
    }

    IEnumerator SocketReseter()
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
