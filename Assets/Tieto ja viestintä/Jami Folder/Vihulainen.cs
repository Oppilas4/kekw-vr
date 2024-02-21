using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Vihulainen : MonoBehaviour
{

    Transform enemy;
    public float speed;
    public Transform player;




    void Update()
    {
        transform.LookAt(player);
        transform.position += transform.forward * speed * Time.deltaTime;
    }   
}
