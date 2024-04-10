using System.Collections.Generic;
using UnityEngine;

public class TV_EcoloopHihna : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public List<GameObject> onBelt;

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
                onBelt.RemoveAt(i);
                i--;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != null)
        {
            onBelt.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject != null)
        {
            onBelt.Remove(collision.gameObject);
        }
    }
}
