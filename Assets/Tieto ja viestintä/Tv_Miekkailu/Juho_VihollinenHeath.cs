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

    public Juho_MaterialTest material1, material2;
    public float dissolveSpeed = 0.5f;

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
                hasRemoved = true;
                spawner = FindObjectOfType<Tv_EnemySpawner>();
                spawner.KillEnemy();
                movement.enabled = false;
                material1.StartDissolve();
                material2.StartDissolve();
                capCollider.enabled = false;
            }
        }
    }
}
