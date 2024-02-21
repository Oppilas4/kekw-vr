using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Vihulainen : MonoBehaviour
{


    public float speed;
    public Transform player;

    private void Start()
    {
        player = GameObject.Find("XR Origin").GetComponent<Transform>();
    }


    void Update()
    {
        Vector3 lookAt = player.position;
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Jami_Enemy"))

        {

        }
    }
}
