using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juho_VihollinenHeath : MonoBehaviour
{
    public int maxHealth;
    int currentHealth;
    public Juho_VihollinenLiikkumis movement;
    Tv_EnemySpawner spawner;
    bool hasRemoved = false;
    public CapsuleCollider capCollider;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            if(!hasRemoved)
            {
                Debug.Log("Kuoli lolers");
                hasRemoved = true;
                spawner = FindObjectOfType<Tv_EnemySpawner>();
                spawner.KillEnemy();
                movement.enabled = false;
                capCollider.enabled = false;
            }
        }
    }
}
