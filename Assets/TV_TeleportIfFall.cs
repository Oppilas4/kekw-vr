using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_TeleportIfFall : MonoBehaviour
{
    [SerializeField] Transform whereToTeleport;

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.transform.position = whereToTeleport.position;
    }
}
