using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_OttoHead : MonoBehaviour
{
    public float rotationSpeed = 5f;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
        }
    }
    void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion finalRotation = Quaternion.Euler(33.81f, targetRotation.eulerAngles.y, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, finalRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
