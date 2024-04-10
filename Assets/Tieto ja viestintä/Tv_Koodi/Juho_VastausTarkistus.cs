using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Juho_VastausTarkistus : MonoBehaviour
{
    public Juho_KysymysLuoja kysymysLuoja;

    public Juho_VastausKohta hardSlot1, hardSlot2, hardSlot3, hardSlot4;
    public TV_AnswerCube[] kaikkiVastausKuutiot;

    public TMP_Text kuinkaMeniTeksti;

    //bool voikoNappiaPainaa = true;

    public void TarkistaOnkoVastausOikein()
    {
        if (hardSlot1.oikeaNappiOn && hardSlot2.oikeaNappiOn && hardSlot3.oikeaNappiOn && hardSlot4.oikeaNappiOn)
        {
           // StartCoroutine(kysymysLuoja.PalautaKuutiot());

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
            //StartCoroutine(kysymysLuoja.PalautaKuutiot());

            foreach (var kuutio in kaikkiVastausKuutiot)
            {
                kuutio.PalautaAloitusAsemaan();
            }
        }
    }

    /*public void OnTriggerEnter(Collider other)
{
    if (voikoNappiaPainaa)
    {
        TarkistaOnkoVastausOikein();
    }
}

IEnumerator NapinReset()
{
    voikoNappiaPainaa = false;
    yield return new WaitForSeconds(2.0f);
    kuinkaMeniTeksti.text = "";
    voikoNappiaPainaa = true;
}
*/
}
