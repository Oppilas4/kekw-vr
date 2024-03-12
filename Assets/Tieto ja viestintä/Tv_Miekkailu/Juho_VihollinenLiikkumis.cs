using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Juho_VihollinenLiikkumis : MonoBehaviour
{
    public float speed;
    public Transform player;
    Juho_PelaajaHealth health;

    private void Start()
    {
        player = GameObject.Find("XR Origin").GetComponent<Transform>();
        health = GameObject.Find("PlayerHealth").GetComponent<Juho_PelaajaHealth>();
    }

    void Update()
    {
        Vector3 lookAt = player.position;
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            health.TakeDamage();
            Juho_VihollinenHeath healt = GetComponent<Juho_VihollinenHeath>();
            healt.TakeDamage(9999999);
        }
    }
}
