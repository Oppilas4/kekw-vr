using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Juho_VaikeusAsetus : MonoBehaviour
{
    public int mik‰VaikeusNumero;
    public Juho_KysymysLuoja kysymysLuoja;
    public TMP_Text mik‰VaikeusTeksti;
    public Juho_VastausKuutio[] kaikkiVastausKuutiot;

    bool voikoNappiaPainaa = true;

    private void Start()
    {
        StartCoroutine(NapinReset());
    }

    public void OnTriggerEnter(Collider other)
    {
        if (voikoNappiaPainaa)
        {
            VaihdaVaikeus();
        }
    }

    public void VaihdaVaikeus()
    {
        StartCoroutine(kysymysLuoja.PalautaKuutiot());

        foreach (var kuutio in kaikkiVastausKuutiot)
        {
            kuutio.PalautaAloitusAsemaan();
        }

        kysymysLuoja.vaikeusID = mik‰VaikeusNumero;

        if(mik‰VaikeusNumero == 0)
        {
            mik‰VaikeusTeksti.text = "Easy";
        }
        if(mik‰VaikeusNumero == 1)
        {
            mik‰VaikeusTeksti.text = "Medium";
        }
        if(mik‰VaikeusNumero == 2)
        {
            mik‰VaikeusTeksti.text = "Hard";
        }

        kysymysLuoja.AloitaKoodiPeli();
        StartCoroutine(NapinReset());
    }

    IEnumerator NapinReset()
    {
        voikoNappiaPainaa = false;
        yield return new WaitForSeconds(2.0f);
        voikoNappiaPainaa = true;
    }
}
