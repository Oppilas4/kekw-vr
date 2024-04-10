using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Jami_VastausKohta : MonoBehaviour
{
    public Tv_Yhdistelm‰Tarkistus yhdistelm‰t;
    TV_AnswerCube nykyinenKuutio;
    XRSocketInteractor mySocket;

    private void Start()
    {
        mySocket = GetComponent<XRSocketInteractor>();
    }

    public void KunLaitettuTarkista()
    {
        var selectedGameObject = mySocket.GetOldestInteractableSelected();
        nykyinenKuutio = selectedGameObject.transform.gameObject.GetComponent<TV_AnswerCube>();
        nykyinenKuutio.particleRefSystem.SetActive(true);
        yhdistelm‰t.TarkistaOnkoYhdistelm‰‰();
    }

    public void KunPoistetaanTarkista()
    {
        var selectedGameObject = mySocket.GetOldestInteractableSelected();
        nykyinenKuutio = selectedGameObject.transform.gameObject.GetComponent<TV_AnswerCube>();
        nykyinenKuutio.particleRefSystem.SetActive(false);
        yhdistelm‰t.TarkistaOnkoYhdistelm‰‰();
    }
}
