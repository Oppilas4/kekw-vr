using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    Rigidbody rb;

    public GameObject magFull;
    public GameObject magOneBullet;

    public int numberOfBullets = 8;
    public float force = 25f;

    public bool magEmpty;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        magFull.SetActive(true);
        magOneBullet.SetActive(true);
    }
    public void DropMagazine()
    {
        rb.AddForce(transform.up * force);
    }
    public void MagShoot()
    {
        numberOfBullets--;
        if (numberOfBullets == 1)
        {
            magFull.SetActive(false);
        }
        if (numberOfBullets <= 0)
        {
            magOneBullet.SetActive(false);
            magEmpty = true;
        }

    }
}
