using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class ModulesForAnswers
{
    public Tv_TheAnswerCube ifAnswer;
    public Tv_TheAnswerCube thenAnswer;
    public string debugLogText;
}

public class Tv_QuestionLogic : MonoBehaviour
{
    public ModulesForAnswers[] answerModules;

    [SerializeField] TMP_Text nameForQuestionText;

    public void NewQuestion()
    {
        nameForQuestionText.text = answerModules[1].debugLogText;
    }
}
