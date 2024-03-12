using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Juho_VastausKohta : MonoBehaviour
{
    public Juho_VastausKuutio vaadittuKuutio;
    Juho_VastausKuutio nykyinenKuutio;
    XRSocketInteractor mySocket;

    public bool oikeaNappiOn = false;

    private void Start()
    {
        mySocket = GetComponent<XRSocketInteractor>();
    }

    public void AsetaKuutio(Juho_VastausKuutio asetettavaKuutio)
    {
        vaadittuKuutio = asetettavaKuutio;
    }

    public void Tarkistus(Juho_VastausKuutio laitettuKuutio)
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
        nykyinenKuutio = selectedGameObject.transform.gameObject.GetComponent<Juho_VastausKuutio>();
        Tarkistus(nykyinenKuutio);
    }

}
