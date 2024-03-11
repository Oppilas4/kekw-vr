using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jami_Laser_Lifetime : MonoBehaviour
{
    public float lifetime = 3f;
    Tv_EnemySpawner spawner;
    bool somali;
    void Start()
    {
        somali = false;
        Destroy(this.gameObject, lifetime);
        spawner = GameObject.Find("EnemySpawner").GetComponent<Tv_EnemySpawner>();
    }

    private void OnCollisionEnter(Collision collision)
    {      
        {
            if (!somali)
            {
                if (collision.gameObject.CompareTag("Jami_Enemy"))

                {
                    spawner.KillEnemy();
                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                    somali = true;
                }
                else
                {
                    Destroy(gameObject);
                }
                   
            }

        }
    }
}
