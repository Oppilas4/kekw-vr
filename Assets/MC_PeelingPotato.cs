using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_PeelingPotato : MonoBehaviour
{
    public GameObject decalPrefab; // Assign the decal prefab in Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Potato")) // Assuming the tag of the potato is "Potato"
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit))
            {
                Vector3 collisionPoint = hit.point;
                Vector3 collisionNormal = hit.normal;

                // Create the decal at the collision point
                GameObject decal = Instantiate(decalPrefab, collisionPoint, Quaternion.FromToRotation(Vector3.up, collisionNormal), other.transform);
                decal.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
        }
    }
}
