using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TV_AnswerCubeSocket : MonoBehaviour
{
    TV_ElementChecker tarkistus;
    XRSocketInteractor mySocket;
    TV_AnswerCube nykyinenKuutio;

    public int kuutioNumero = 1;

    private void Start()
    {
        mySocket = GetComponent<XRSocketInteractor>();
        tarkistus = GameObject.Find("Elementcheck").GetComponent<TV_ElementChecker>();
    }

    public void KunLaitettuTarkista()
    {
        var selectedGameObject = mySocket.GetOldestInteractableSelected();
        nykyinenKuutio = selectedGameObject.transform.gameObject.GetComponent<TV_AnswerCube>();
        if (kuutioNumero == 1)
        {
            tarkistus.elementti1 = nykyinenKuutio.koodiNimiKuutiolle;
            if (tarkistus.activeElement != null)
            {
                tarkistus.activeElement.SetActive(false);
            }
            tarkistus.TestCombination(tarkistus.elementti1, tarkistus.elementti2);
            Debug.Log(nykyinenKuutio.koodiNimiKuutiolle);
        }
        if (kuutioNumero == 2)
        {
            tarkistus.elementti2 = nykyinenKuutio.koodiNimiKuutiolle;
            if (tarkistus.activeElement != null)
            {
                tarkistus.activeElement.SetActive(false);
            }
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
            if (tarkistus.activeElement != null)
            {
                tarkistus.activeElement.SetActive(false);
            }
            tarkistus.TestCombination(tarkistus.elementti1, tarkistus.elementti2);
            Debug.Log(nykyinenKuutio.koodiNimiKuutiolle);
        }
        if (kuutioNumero == 2)
        {
            tarkistus.elementti2 = "";
            if (tarkistus.activeElement != null)
            {
                tarkistus.activeElement.SetActive(false);
            }

            tarkistus.TestCombination(tarkistus.elementti1, tarkistus.elementti2);
            Debug.Log(nykyinenKuutio.koodiNimiKuutiolle);
        }
    }
}