using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TV_EnemyMovement : MonoBehaviour
{
    public float speed;
    public Transform player;
    public float range;
    public float detectionRange;
    public bool isRanged;
    bool hasStartedToShoot = false;
    bool canHit = true;


    public GameObject enemyShootTransform;
    public GameObject enemyBulletPrefab;
    public float vitunforce;

    private void Start()
    {
        player = GameObject.Find("XR Origin").GetComponent<Transform>();
    }

    void Update()
    {

        if (Vector3.Distance(transform.position, player.transform.position) < detectionRange)
        {
            EnnemyController();
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
      
        yield return new WaitForSeconds(2);
        canHit = true;
    }
}



