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
    [Range(0, 2)]
    public int vaikeusID;
    public TMP_Text kysymysTeksti;
    [SerializeField] Juho_VastausKuutio[] vastausKuutioReferencet;

    [Space(15)]
    [Header("Helpot Kysymykset")]
    [Space(10)]
    public GameObject helppoTaulutParent;
    public HelpotKysymykset[] HelpotKysymykset;
    [HideInInspector] public List<HelpotKysymykset> j‰ljell‰OlevatHelpotKysymykset;
    [HideInInspector] public HelpotKysymykset nykyinenHelppoKysymys;
    Juho_VastausKohta helppoVastausKohtaYksi, helppoVastausKohtaKaksi;
    public XRSocketInteractor helppoRiviYksiKuutioSlot, helppoRiviKaksiKuutioSlot;

    [Space(15)]
    [Header("Keski Verto Kysymykset")]
    [Space(10)]
    public GameObject keskiTaulutParent;
    public KeskiVertoKysymykset[] keskiKysymykset;
    [HideInInspector] public List<KeskiVertoKysymykset> j‰ljell‰OlevatKeskiKysymykset;
    [HideInInspector] public KeskiVertoKysymykset nykyinenKeskiKysymys;
    Juho_VastausKohta keskiVastausKohtaYksi, keskiVastausKohtaKaksi, keskiVastausKohtaKolme;
    public XRSocketInteractor keskiRiviYksiKuutioSlot, keskiRiviKaksiKuutioSlot, keskiRiviKolmeKuutioSlot;

    [Space(15)]
    [Header("Vaikeat Verto Kysymykset")]
    [Space(10)]
    public GameObject vaikeaTaulutParent;
    public VaikeatKysymykset[] vaikeaKysymykset;
    [HideInInspector] public List<VaikeatKysymykset> j‰ljell‰OlevatVaikeatKysymykset;
    [HideInInspector] public VaikeatKysymykset nykyinenVaikeaKysymys;
    Juho_VastausKohta vaikeaVastausKohtaYksi, vaikeaVastausKohtaKaksi, vaikeaVastausKohtaKolme, vaikeaVastausKohtaNelj‰;
    public XRSocketInteractor vaikeaRiviYksiKuutioSlot, vaikeaRiviKaksiKuutioSlot, vaikeaRiviKolmeKuutioSlot, vaikeaRiviNelj‰KuutioSlot;

    public void AloitaKoodiPeli()
    {
        j‰ljell‰OlevatHelpotKysymykset = new List<HelpotKysymykset>(HelpotKysymykset);
        j‰ljell‰OlevatKeskiKysymykset = new List<KeskiVertoKysymykset>(keskiKysymykset);
        j‰ljell‰OlevatVaikeatKysymykset = new List<VaikeatKysymykset>(vaikeaKysymykset);
        KysymysLuonti();
    }

    public void KysymysLuonti()
    {
        switch (vaikeusID)
        {
            case 0:
                if (j‰ljell‰OlevatHelpotKysymykset.Count == 0)
                {
                    Debug.LogWarning("Kaikki helpot L‰pi");
                    vaikeusID++;
                    return;
                }

                helppoTaulutParent.SetActive(true);
                keskiTaulutParent.SetActive(false);
                vaikeaTaulutParent.SetActive(false);

                int randomIndexEasy = Random.Range(0, j‰ljell‰OlevatHelpotKysymykset.Count);
                nykyinenHelppoKysymys = j‰ljell‰OlevatHelpotKysymykset[randomIndexEasy];
                j‰ljell‰OlevatHelpotKysymykset.RemoveAt(randomIndexEasy);
                kysymysTeksti.text = nykyinenHelppoKysymys.questionName;
                helppoVastausKohtaYksi = helppoRiviYksiKuutioSlot.GetComponent<Juho_VastausKohta>();
                helppoVastausKohtaYksi.AsetaKuutio(nykyinenHelppoKysymys.firstRowAnswer);
                helppoVastausKohtaKaksi = helppoRiviKaksiKuutioSlot.GetComponent<Juho_VastausKohta>();
                helppoVastausKohtaKaksi.AsetaKuutio(nykyinenHelppoKysymys.secondRowAnswer);
                break;
            case 1:
                if (j‰ljell‰OlevatKeskiKysymykset.Count == 0)
                {
                    Debug.LogWarning("Kaikki keskiverto L‰pi");
                    vaikeusID++;
                    return;
                }

                helppoTaulutParent.SetActive(false);
                keskiTaulutParent.SetActive(true);
                vaikeaTaulutParent.SetActive(false);

                int randomIndexMedium = Random.Range(0, j‰ljell‰OlevatKeskiKysymykset.Count);
                nykyinenKeskiKysymys = j‰ljell‰OlevatKeskiKysymykset[randomIndexMedium];
                j‰ljell‰OlevatKeskiKysymykset.RemoveAt(randomIndexMedium);
                kysymysTeksti.text = nykyinenKeskiKysymys.questionName;
                keskiVastausKohtaYksi = keskiRiviYksiKuutioSlot.GetComponent<Juho_VastausKohta>();
                keskiVastausKohtaYksi.AsetaKuutio(nykyinenKeskiKysymys.firstRowAnswer);
                keskiVastausKohtaKaksi = keskiRiviKaksiKuutioSlot.GetComponent<Juho_VastausKohta>();
                keskiVastausKohtaKaksi.AsetaKuutio(nykyinenKeskiKysymys.secondRowAnswer);
                keskiVastausKohtaKolme = keskiRiviKolmeKuutioSlot.GetComponent<Juho_VastausKohta>();
                keskiVastausKohtaKolme.AsetaKuutio(nykyinenKeskiKysymys.thirdRowAnswer);
                break;
            case 2:
                if (j‰ljell‰OlevatVaikeatKysymykset.Count == 0)
                {
                    Debug.LogWarning("Kaikki vaikeat L‰pi");
                    return;
                }

                helppoTaulutParent.SetActive(false);
                keskiTaulutParent.SetActive(false);
                vaikeaTaulutParent.SetActive(true);

                int randomIndexHard = Random.Range(0, j‰ljell‰OlevatVaikeatKysymykset.Count);
                nykyinenVaikeaKysymys = j‰ljell‰OlevatVaikeatKysymykset[randomIndexHard];
                j‰ljell‰OlevatVaikeatKysymykset.RemoveAt(randomIndexHard);
                kysymysTeksti.text = nykyinenVaikeaKysymys.questionName;
                vaikeaVastausKohtaYksi = vaikeaRiviYksiKuutioSlot.GetComponent<Juho_VastausKohta>();
                vaikeaVastausKohtaYksi.AsetaKuutio(nykyinenVaikeaKysymys.firstRowAnswer);
                vaikeaVastausKohtaKaksi = vaikeaRiviKaksiKuutioSlot.GetComponent<Juho_VastausKohta>();
                vaikeaVastausKohtaKaksi.AsetaKuutio(nykyinenVaikeaKysymys.secondRowAnswer);
                vaikeaVastausKohtaKolme = vaikeaRiviKolmeKuutioSlot.GetComponent<Juho_VastausKohta>();
                vaikeaVastausKohtaKolme.AsetaKuutio(nykyinenVaikeaKysymys.thirdRowAnswer);
                vaikeaVastausKohtaNelj‰ = vaikeaRiviNelj‰KuutioSlot.GetComponent<Juho_VastausKohta>();
                vaikeaVastausKohtaNelj‰.AsetaKuutio(nykyinenVaikeaKysymys.fourthRowAnswer);
                break;
            default:
                Debug.LogError("Invalid difficultyId");
                break;
        }
    }

    public IEnumerator PalautaKuutiot()
    {
        foreach (var vastausKuutiot in vastausKuutioReferencet)
        {
            vastausKuutiot.PalautaAloitusAsemaan();
        }

        helppoRiviYksiKuutioSlot.socketActive = false;
        helppoRiviKaksiKuutioSlot.socketActive = false;
        keskiRiviYksiKuutioSlot.socketActive = false;
        keskiRiviKaksiKuutioSlot.socketActive = false;
        keskiRiviKolmeKuutioSlot.socketActive = false;
        vaikeaRiviYksiKuutioSlot.socketActive = false;
        vaikeaRiviKaksiKuutioSlot.socketActive = false;
        vaikeaRiviKolmeKuutioSlot.socketActive = false;
        vaikeaRiviNelj‰KuutioSlot.socketActive = false;

        yield return new WaitForSeconds(1.0f);

        helppoRiviYksiKuutioSlot.socketActive = true;
        helppoRiviKaksiKuutioSlot.socketActive = true;
        keskiRiviYksiKuutioSlot.socketActive = true;
        keskiRiviKaksiKuutioSlot.socketActive = true;
        keskiRiviKolmeKuutioSlot.socketActive = true;
        vaikeaRiviYksiKuutioSlot.socketActive = true;
        vaikeaRiviKaksiKuutioSlot.socketActive = true;
        vaikeaRiviKolmeKuutioSlot.socketActive = true;
        vaikeaRiviNelj‰KuutioSlot.socketActive = true;
    }
}
