using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_KahviFlakesParticles : MonoBehaviour
{
    ParticleSystem CoffeeSystem;
    void Start()
    {
        CoffeeSystem = GetComponent<ParticleSystem>();
    }
    public void PartIt()
    {
        CoffeeSystem.Play();
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.GetComponent<Elec_Tero_AI>() != null) Elec_Tero_AI.Instance.Say(Elec_Tero_AI.dialoguetype.COFFEE);
    }
}
