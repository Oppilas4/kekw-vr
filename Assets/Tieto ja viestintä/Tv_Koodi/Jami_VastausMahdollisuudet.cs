using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class Yhdistelm‰t
{
    public Juho_VastausKuutio firstRowAnswer;
    public Juho_VastausKuutio secondRowAnswer;
    public Juho_VastausKuutio thirdRowAnswer;
    public Juho_VastausKuutio fourthRowAnswer;
    public GameObject finishedParticle;
}
public class Jami_VastausMahdollisuudet : MonoBehaviour
{
    public Yhdistelm‰t[] vaikeaKysymykset;
    [SerializeField] Juho_VastausKuutio[] vastausKuutioReferencet;
    public XRSocketInteractor vaikeaRiviYksiKuutioSlot, vaikeaRiviKaksiKuutioSlot, vaikeaRiviKolmeKuutioSlot, vaikeaRiviNelj‰KuutioSlot;
}
