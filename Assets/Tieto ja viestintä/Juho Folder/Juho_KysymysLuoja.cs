using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class HelpotKysymykset
{
    public string questionName;
    public Juho_VastausKuutio firstRowAnswer;
    public Juho_VastausKuutio secondRowAnswer;
}
[System.Serializable]
public class KeskiVertoKysymykset
{
    public string questionName;
    public Juho_VastausKuutio firstRowAnswer;
    public Juho_VastausKuutio secondRowAnswer;
    public Juho_VastausKuutio thirdRowAnswer;
}
[System.Serializable]
public class VaikeatKysymykset
{
    public string questionName;
    public Juho_VastausKuutio firstRowAnswer;
    public Juho_VastausKuutio secondRowAnswer;
    public Juho_VastausKuutio thirdRowAnswer;
    public Juho_VastausKuutio fourthRowAnswer;
}

public class Juho_KysymysLuoja : MonoBehaviour
{
    public TMP_Text kysymysTeksti;
    [SerializeField] Juho_VastausKuutio[] vastausKuutioReferencet;

    [Space(20)]
    [Header("Helpot Kysymykset")]
    [Space(10)]
    public HelpotKysymykset[] HelpotKysymykset;
    public List<HelpotKysymykset> j‰ljell‰OlevatHelpotKysymykset;
    public HelpotKysymykset nykyinenHelppoKysymys;
    Juho_VastausKohta helppoVastausKohtaYksi, helppoVastausKohtaKaksi;
    public XRSocketInteractor helppoRiviYksiKuutioSlot, helppoRiviKaksiKuutioSlot;

    [Space(20)]
    [Header("Keski Verto Kysymykset")]
    [Space(10)]
    public KeskiVertoKysymykset[] keskiKysymykset;
    public List<KeskiVertoKysymykset> j‰ljell‰OlevatKeskiKysymykset;
    public KeskiVertoKysymykset nykyinenKeskiKysymys;
    Juho_VastausKohta keskiVastausKohtaYksi, keskiVastausKohtaKaksi, keskiVastausKohtaKolme;
    public XRSocketInteractor keskiRiviYksiKuutioSlot, keskiRiviKaksiKuutioSlot, keskiRiviKolmeKuutioSlot;


    private void Start()
    {
        j‰ljell‰OlevatHelpotKysymykset = new List<HelpotKysymykset>(HelpotKysymykset);
        KysymysLuonti();
    }

    public void KysymysLuonti()
    {
        if (j‰ljell‰OlevatHelpotKysymykset.Count == 0)
        {
            Debug.LogWarning("Kaikki L‰pi");
            return;
        }

        int randomIndex = Random.Range(0, j‰ljell‰OlevatHelpotKysymykset.Count);
        nykyinenHelppoKysymys = j‰ljell‰OlevatHelpotKysymykset[randomIndex];
        j‰ljell‰OlevatHelpotKysymykset.RemoveAt(randomIndex);
        kysymysTeksti.text = nykyinenHelppoKysymys.questionName;
        helppoVastausKohtaYksi = helppoRiviYksiKuutioSlot.GetComponent<Juho_VastausKohta>();
        helppoVastausKohtaYksi.AsetaKuutio(nykyinenHelppoKysymys.firstRowAnswer);
        helppoVastausKohtaKaksi = helppoRiviKaksiKuutioSlot.GetComponent<Juho_VastausKohta>();
        helppoVastausKohtaKaksi.AsetaKuutio(nykyinenHelppoKysymys.secondRowAnswer);

    }

    public IEnumerator PalautaKuutiot()
    {
        foreach (var vastausKuutiot in vastausKuutioReferencet)
        {
            vastausKuutiot.PalautaAloitusAsemaan();
        }

        helppoRiviYksiKuutioSlot.socketActive = false;
        helppoRiviKaksiKuutioSlot.socketActive = false;

        yield return new WaitForSeconds(1.0f);

        helppoRiviYksiKuutioSlot.socketActive = true;
        helppoRiviKaksiKuutioSlot.socketActive = true;
    }
}
