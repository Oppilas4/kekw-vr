using UnityEngine;

public class TV_AnswerCube : MonoBehaviour
{
    public string koodiNimiKuutiolle;
    public GameObject particleRefSystem;
    Vector3 resetTransform;

    public void Start()
    {
        resetTransform = gameObject.transform.position;
    }

    public void PalautaAloitusAsemaan()
    {
        gameObject.transform.position = resetTransform;
    }
}
