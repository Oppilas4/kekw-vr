using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_DisablePlayerCollision : MonoBehaviour
{
    private Collider playerCollider;
    private List<Collider> drawerColliders;

    void Start()
    {
        // Assuming the player collider is attached to the same object as this script.
        playerCollider = FindAnyObjectByType<CharacterController>();

        // Get all colliders attached to the drawer.
        drawerColliders = new List<Collider>(GetComponents<Collider>());

        // Loop through each drawer collider and ignore collision with the player collider.
        foreach (var drawerCollider in drawerColliders)
        {
            Physics.IgnoreCollision(playerCollider, drawerCollider, true);
        }
    }
}
