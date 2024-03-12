using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Juho_VihollinenLiikkumis : MonoBehaviour
{
    public float speed;
    public Transform player;
    Juho_PelaajaHealth health;
    public float range;
    private Vector3 playerRange;

    private void Start()
    {
        player = GameObject.Find("XR Origin").GetComponent<Transform>();
        health = GameObject.Find("PlayerHealth").GetComponent<Juho_PelaajaHealth>();
        playerRange = player.transform.position;
    }

    void Update()
    {

        Vector3 lookAt = player.position;
        lookAt.y = transform.position.y;

        if (Vector3.Distance(transform.position, player.transform.position) <= range)
        {
            transform.LookAt(lookAt);
        }
       
        if (Vector3.Distance(transform.position, player.transform.position) > range)
        {
            transform.LookAt(lookAt);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
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
