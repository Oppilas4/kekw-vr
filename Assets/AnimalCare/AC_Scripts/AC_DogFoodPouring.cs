using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class AC_DogFoodPouring : MonoBehaviour
{
    public GameObject cereal;
    public Transform spawnPoint;
    public float fireSpeed = 20;
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(PourFood);
    }
    public void PourFood(ActivateEventArgs arg)
    {
        GameObject spawnedFood = Instantiate(cereal);
        spawnedFood.transform.position = spawnPoint.position;
        spawnedFood.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
    }
}
