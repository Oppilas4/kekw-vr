using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Juho_VastausKohta : MonoBehaviour
{
    public TV_AnswerCube vaadittuKuutio;
    TV_AnswerCube nykyinenKuutio;
    XRSocketInteractor mySocket;

    public bool oikeaNappiOn = false;

    private void Start()
    {
        mySocket = GetComponent<XRSocketInteractor>();
    }

    public void AsetaKuutio(TV_AnswerCube asetettavaKuutio)
    {
        vaadittuKuutio = asetettavaKuutio;
    }

    public void Tarkistus(TV_AnswerCube laitettuKuutio)
    {
        if(laitettuKuutio == vaadittuKuutio)
        {
            Debug.Log("Laitoit Oikean Napin " + laitettuKuutio);
            oikeaNappiOn = true;
        }
        if (laitettuKuutio != vaadittuKuutio)
        {
            Debug.Log("Laitoit V‰‰r‰n Napin " + laitettuKuutio);
            oikeaNappiOn = false;
        }
    }

    public void KunLaitettuTarkista()
    {
        var selectedGameObject = mySocket.GetOldestInteractableSelected();
        nykyinenKuutio = selectedGameObject.transform.gameObject.GetComponent<TV_AnswerCube>();
        Tarkistus(nykyinenKuutio);
    }

}
