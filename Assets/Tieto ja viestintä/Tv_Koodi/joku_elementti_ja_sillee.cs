using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class joku_elementti_ja_sillee : MonoBehaviour
{
    Jami_ElementtiTarkistus tarkistus;
    XRSocketInteractor mySocket;
    Juho_VastausKuutio nykyinenKuutio;

    public int kuutioNumero = 1;




    private void Start()
    {
        mySocket = GetComponent<XRSocketInteractor>();
        tarkistus = GameObject.Find("Elementcheck").GetComponent<Jami_ElementtiTarkistus>();
    }

    public void KunLaitettuTarkista()
    {
        var selectedGameObject = mySocket.GetOldestInteractableSelected();
        nykyinenKuutio = selectedGameObject.transform.gameObject.GetComponent<Juho_VastausKuutio>();
        if (kuutioNumero == 1)
        {
            tarkistus.elementti1 = nykyinenKuutio.koodiNimiKuutiolle;
            tarkistus.TestCombination(tarkistus.elementti1, tarkistus.elementti2);
            Debug.Log(nykyinenKuutio.koodiNimiKuutiolle);
        }
        if (kuutioNumero == 2)
        {
            tarkistus.elementti2 = nykyinenKuutio.koodiNimiKuutiolle;
            tarkistus.TestCombination(tarkistus.elementti1, tarkistus.elementti2);
            Debug.Log(nykyinenKuutio.koodiNimiKuutiolle);
        }
        //yhdistelm‰t.TarkistaOnkoYhdistelm‰‰();
    }

    public void KunPoistetaanTarkista()
    {
        if (kuutioNumero == 1)
        {
            tarkistus.elementti1 = "";
            tarkistus.TestCombination(tarkistus.elementti1, tarkistus.elementti2);
            Debug.Log(nykyinenKuutio.koodiNimiKuutiolle);
        }
        if (kuutioNumero == 2)
        {
            tarkistus.elementti2 = "";
            tarkistus.TestCombination(tarkistus.elementti1, tarkistus.elementti2);
            Debug.Log(nykyinenKuutio.koodiNimiKuutiolle);
        }
    }
}