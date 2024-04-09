using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_HIhnaCOntr : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public List<GameObject> onBelt;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < onBelt.Count; i++)
        {
            if (onBelt[i] != null)
            {
                Rigidbody rigidbody = onBelt[i].GetComponent<Rigidbody>();
                if (rigidbody != null)
                {
                    rigidbody.velocity = speed * direction;
                    rigidbody.useGravity = false;
                    rigidbody.freezeRotation = true;
                }
            }
            else
            {
                // Remove null objects from the list
                onBelt.RemoveAt(i);
                i--; // Decrease i to properly check the next item in the list
            }
        }
    }

    // When something collides with the belt
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != null)
        {
            onBelt.Add(collision.gameObject);
        }
    }

    // When something leaves the belt
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject != null)
        {
            onBelt.Remove(collision.gameObject);
        }
    }
}
