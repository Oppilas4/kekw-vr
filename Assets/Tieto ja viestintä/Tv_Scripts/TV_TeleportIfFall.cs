using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_TeleportIfFall : MonoBehaviour
{
    [SerializeField] Transform whereToTeleport;
    [SerializeField] bool onlySword = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(!onlySword)
        {
            collision.gameObject.transform.position = whereToTeleport.position;
        }
        else
        {
            if(collision.gameObject.CompareTag("TV_Sword"))
            {
                collision.gameObject.transform.position = whereToTeleport.position;
            }         
        }
    }
}
