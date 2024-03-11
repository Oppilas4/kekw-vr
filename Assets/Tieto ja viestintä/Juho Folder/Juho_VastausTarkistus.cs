using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Juho_VastausTarkistus : MonoBehaviour
{
    public Juho_KysymysLuoja kysymysLuoja;
    public Juho_VastausKohta riviYksiKohta, riviKaksiKohta;
    public Juho_VastausKuutio[] kaikkiVastausKuutiot;

    public TMP_Text kuinkaMeniTeksti;

    bool voikoNappiaPainaa = true;

    private void Start()
    {
        kuinkaMeniTeksti.text = "";
    }
    public void OnTriggerEnter(Collider other)
    {
        if(voikoNappiaPainaa)
        {
            TarkistaOnkoVastausOikein();
        }
    }

    public void TarkistaOnkoVastausOikein()
    {
        if(riviYksiKohta.oikeaNappiOn && riviKaksiKohta.oikeaNappiOn)
        {
            Debug.Log("Vastaus Meni Oikein");

            kuinkaMeniTeksti.text = "Correct";
            StartCoroutine(NapinReset());

            StartCoroutine(kysymysLuoja.PalautaKuutiot());

            foreach (var kuutio in kaikkiVastausKuutiot)
            {
                kuutio.PalautaAloitusAsemaan();
            }

            kysymysLuoja.KysymysLuonti();
            riviYksiKohta.oikeaNappiOn = false;
            riviKaksiKohta.oikeaNappiOn = false;
        }
        else
        {
            Debug.Log("Vastaus Meni V‰‰rin");

            kuinkaMeniTeksti.text = "Wrong";
            StartCoroutine(NapinReset());

            StartCoroutine(kysymysLuoja.PalautaKuutiot());

            foreach (var kuutio in kaikkiVastausKuutiot)
            {
                kuutio.PalautaAloitusAsemaan();
            }
        }
    }

    IEnumerator NapinReset()
    {
        voikoNappiaPainaa = false;
        yield return new WaitForSeconds(2.0f);
        kuinkaMeniTeksti.text = "";
        voikoNappiaPainaa = true;
    }
}
