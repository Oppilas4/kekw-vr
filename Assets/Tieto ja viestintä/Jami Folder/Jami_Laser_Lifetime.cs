using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jami_Laser_Lifetime : MonoBehaviour
{
    public float lifetime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Jami_Enemy"))
           
        {
            Destroy(collision.gameObject);
        }
    }
}
