using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBox : MonoBehaviour
{
    public Flower flower;
    private int waterLayer;
    private void Start()
    {
        flower.SetToInitial();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Water")
            flower.GrowFlower();
    }
}
