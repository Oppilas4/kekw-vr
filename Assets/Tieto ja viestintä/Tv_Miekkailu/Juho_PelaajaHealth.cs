using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Juho_PelaajaHealth : MonoBehaviour
{
    public int maxHealth;
    int currentHealth; 
    GameObject Player;
    public GameObject DeathPosition;
    private void Start()
    {
        Player = GameObject.Find("XrCharacterSetupWithHands(Clone)");
        currentHealth = maxHealth;
    }

    public void TakeDamage()
    {
        currentHealth--;
        Debug.Log("Otsis");

        if (currentHealth <= 0)
        {
            DieHEHEHAW();
        }
    }

    public void DieHEHEHAW()
    {
        Debug.Log("Ku9l");
        Player.transform.position = DeathPosition.transform.position;
    }
}
