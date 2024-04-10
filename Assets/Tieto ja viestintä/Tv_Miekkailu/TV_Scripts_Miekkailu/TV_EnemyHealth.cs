using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_EnemyHealth : MonoBehaviour
{
    Tv_EnemySpawner spawner;
    bool hasRemoved = false;
    
    public void TakeDamage()
    {
            if(!hasRemoved)
            {
                Debug.Log("Kuoli lolers");
                hasRemoved = true;
                spawner = FindObjectOfType<Tv_EnemySpawner>();
                spawner.KillEnemy();
            }
    }
}
