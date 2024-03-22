using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Juho_VastausTarkistus : MonoBehaviour
{
    public Juho_KysymysLuoja kysymysLuoja;

    public Juho_VastausKohta easySlot1, easySlot2;
    public Juho_VastausKohta mediumSlot1, mediumSlot2, mediumSlot3;
    public Juho_VastausKohta hardSlot1, hardSlot2, hardSlot3, hardSlot4;
    public Juho_VastausKuutio[] kaikkiVastausKuutiot;

    public TMP_Text kuinkaMeniTeksti;

    bool voikoNappiaPainaa = true;

    private void Start()
    {
        StartCoroutine(NapinReset());
        kuinkaMeniTeksti.text = "";
    }

    public void OnTriggerEnter(Collider other)
    {
        if(voikoNappiaPainaa)
        {
            TarkistaOnkoVastausOikein(difficulty);
        }
    }

    public void TarkistaOnkoVastausOikein(int difficultyID)
    {
        switch (difficultyID)
        {
            case 0:

                if (easySlot1.oikeaNappiOn && easySlot2.oikeaNappiOn)
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
                    easySlot1.oikeaNappiOn = false;
                    easySlot2.oikeaNappiOn = false;
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
                break;

            case 1:
                if (mediumSlot1.oikeaNappiOn && mediumSlot2.oikeaNappiOn && mediumSlot3.oikeaNappiOn)
                {
                    Debug.Log("Helppo Vastaus Meni Oikein");

                    kuinkaMeniTeksti.text = "Correct";
                    StartCoroutine(NapinReset());

                    StartCoroutine(kysymysLuoja.PalautaKuutiot());

                    foreach (var kuutio in kaikkiVastausKuutiot)
                    {
                        kuutio.PalautaAloitusAsemaan();
                    }

                    kysymysLuoja.KysymysLuonti();
                    mediumSlot1.oikeaNappiOn = false;
                    mediumSlot2.oikeaNappiOn = false;
                    mediumSlot3.oikeaNappiOn = false;
                }
                else
                {
                    Debug.Log("Helppo Vastaus Meni V‰‰rin");

                    kuinkaMeniTeksti.text = "Wrong";
                    StartCoroutine(NapinReset());

                    StartCoroutine(kysymysLuoja.PalautaKuutiot());

                    foreach (var kuutio in kaikkiVastausKuutiot)
                    {
                        kuutio.PalautaAloitusAsemaan();
                    }
                }
                break;

            case 2:
                if (hardSlot1.oikeaNappiOn && hardSlot2.oikeaNappiOn && hardSlot3.oikeaNappiOn && hardSlot4.oikeaNappiOn)
                {
                    Debug.Log("Medium Vastaus Meni Oikein");

                    kuinkaMeniTeksti.text = "Correct";
                    StartCoroutine(NapinReset());

                    StartCoroutine(kysymysLuoja.PalautaKuutiot());

                    foreach (var kuutio in kaikkiVastausKuutiot)
                    {
                        kuutio.PalautaAloitusAsemaan();
                    }

                    kysymysLuoja.KysymysLuonti();
                    hardSlot1.oikeaNappiOn = false;
                    hardSlot2.oikeaNappiOn = false;
                    hardSlot3.oikeaNappiOn = false;
                    hardSlot4.oikeaNappiOn = false;
                }
                else
                {
                    Debug.Log("Medium Vastaus Meni V‰‰rin");

                    kuinkaMeniTeksti.text = "Wrong";
                    StartCoroutine(NapinReset());

                    StartCoroutine(kysymysLuoja.PalautaKuutiot());

                    foreach (var kuutio in kaikkiVastausKuutiot)
                    {
                        kuutio.PalautaAloitusAsemaan();
                    }
                }
                break;

            default:
                Debug.LogError("Invalid difficultyId");
                break;
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
