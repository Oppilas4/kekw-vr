using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class Yhdistelm‰t
{
    public TV_AnswerCube firstRowAnswer;
    public TV_AnswerCube secondRowAnswer;
    public TV_AnswerCube thirdRowAnswer;
    public TV_AnswerCube fourthRowAnswer;
    public GameObject finishedParticle;
}
public class Jami_VastausMahdollisuudet : MonoBehaviour
{
    public Yhdistelm‰t[] vaikeaKysymykset;
    [SerializeField] TV_AnswerCube[] vastausKuutioReferencet;
    public XRSocketInteractor vaikeaRiviYksiKuutioSlot, vaikeaRiviKaksiKuutioSlot, vaikeaRiviKolmeKuutioSlot, vaikeaRiviNelj‰KuutioSlot;
}
