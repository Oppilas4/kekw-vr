using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Juho_VastausKuutio : MonoBehaviour
{
    public string koodiNimiKuutiolle;
    public TMP_Text kuutionTeksti;
    public GameObject particleRefSystem;
    Vector3 resetTransform;

    public void Start()
    {
        kuutionTeksti.text = koodiNimiKuutiolle;
        resetTransform = gameObject.transform.position;
    }

    public void PalautaAloitusAsemaan()
    {
        gameObject.transform.position = resetTransform;
    }
}
