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
    bool hasStartedToShoot = false;
    bool canHit = true;


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
        transform.LookAt(lookAt);

        if (Vector3.Distance(transform.position, player.transform.position) > range)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        if (Vector3.Distance(transform.position, player.transform.position) <= range)
        {
            if (isRanged && !hasStartedToShoot)
            {
                StartCoroutine(RangedAttackDelay());
                hasStartedToShoot = true;
            }
            else if (canHit)
            {
                StartCoroutine(MeleeAttackDelay());
                canHit = false;
            }
        }
    }

    IEnumerator RangedAttackDelay()
    {
        GameObject projectileGO = Instantiate(enemyBulletPrefab, enemyShootTransform.transform.position, transform.rotation);
        Rigidbody projectileRb = projectileGO.GetComponent<Rigidbody>();
        projectileRb.AddForce(enemyShootTransform.transform.forward * vitunforce, ForceMode.Impulse);
        yield return new WaitForSeconds(2);
        hasStartedToShoot = false;
    }

    IEnumerator MeleeAttackDelay()
    {
        health.TakeDamage();
        yield return new WaitForSeconds(2);
        canHit = true;
    }
}



