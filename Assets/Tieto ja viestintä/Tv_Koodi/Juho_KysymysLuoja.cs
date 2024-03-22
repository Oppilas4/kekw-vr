using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


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

    public GameObject vaikeaTaulutParent;
    public VaikeatKysymykset[] vaikeaKysymykset;
    [HideInInspector] public List<VaikeatKysymykset> j‰ljell‰OlevatVaikeatKysymykset;
    [HideInInspector] public VaikeatKysymykset nykyinenVaikeaKysymys;
    Juho_VastausKohta vaikeaVastausKohtaYksi, vaikeaVastausKohtaKaksi, vaikeaVastausKohtaKolme, vaikeaVastausKohtaNelj‰;
    public XRSocketInteractor vaikeaRiviYksiKuutioSlot, vaikeaRiviKaksiKuutioSlot, vaikeaRiviKolmeKuutioSlot, vaikeaRiviNelj‰KuutioSlot;

    public void AloitaKoodiPeli()
    {
        j‰ljell‰OlevatVaikeatKysymykset = new List<VaikeatKysymykset>(vaikeaKysymykset);
        KysymysLuonti();
    }

    public void KysymysLuonti()
    {
        if (j‰ljell‰OlevatVaikeatKysymykset.Count == 0)
        {
            Debug.LogWarning("Kaikki vaikeat L‰pi");
            return;
        }
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
    }

    /*public IEnumerator PalautaKuutiot()
    {
        foreach (var vastausKuutiot in vastausKuutioReferencet)
        {
            vastausKuutiot.PalautaAloitusAsemaan();
        }
        vaikeaRiviYksiKuutioSlot.socketActive = false;
        vaikeaRiviKaksiKuutioSlot.socketActive = false;
        vaikeaRiviKolmeKuutioSlot.socketActive = false;
        vaikeaRiviNelj‰KuutioSlot.socketActive = false;

        yield return new WaitForSeconds(1.0f);
        vaikeaRiviYksiKuutioSlot.socketActive = true;
        vaikeaRiviKaksiKuutioSlot.socketActive = true;
        vaikeaRiviKolmeKuutioSlot.socketActive = true;
        vaikeaRiviNelj‰KuutioSlot.socketActive = true;
    }
    */
}
