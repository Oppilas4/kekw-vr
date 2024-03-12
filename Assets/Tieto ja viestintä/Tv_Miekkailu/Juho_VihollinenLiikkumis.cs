using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Juho_VihollinenLiikkumis : MonoBehaviour
{
    public float speed;
    public Transform player;
    Juho_PelaajaHealth health;
    public float range;
    public float detectionRange;
    private Vector3 playerRange;
    public bool isRanged;



    public GameObject enemyShootTransform;
    public GameObject enemyBulletPrefab;
    public float vitunforce;

    private void Start()
    {
        player = GameObject.Find("XR Origin").GetComponent<Transform>();
        health = GameObject.Find("PlayerHealth").GetComponent<Juho_PelaajaHealth>();
        playerRange = player.transform.position;
    }

    void Update()
    {

        if (Vector3.Distance(transform.position, player.transform.position) > detectionRange)
        {
            EnnemyController();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            health.TakeDamage();
            Juho_VihollinenHeath healt = GetComponent<Juho_VihollinenHeath>();
            healt.TakeDamage(9999999);
        }
    }


    void EnnemyController()
    {

        Vector3 lookAt = player.position;
        lookAt.y = transform.position.y;

        if (Vector3.Distance(transform.position, player.transform.position) > range)
        {
            transform.LookAt(lookAt);
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        if (Vector3.Distance(transform.position, player.transform.position) <= range)
        {
            if (isRanged)
            {
                StartCoroutine(attackDelay());
            }
            else
            {

            }
        }
    }

    IEnumerator attackDelay()
    {
        GameObject projectileGO = (GameObject)Instantiate(enemyBulletPrefab, enemyShootTransform.transform.position,
        enemyBulletPrefab.transform.rotation);
        Rigidbody projectileRb = projectileGO.GetComponent<Rigidbody>();
        projectileRb.AddForce(vitunforce * Vector3.forward, ForceMode.Impulse);
        yield return new WaitForSeconds(2);
    }
}



