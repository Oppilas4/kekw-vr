using UnityEngine;

public class Jami_HihnaV2 : MonoBehaviour
{
    public float speed = 2f;
    public Vector3 direction = Vector3.forward;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Jami_Enemy"))
        {
            Debug.Log("Colliding");
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                float moveDistance = speed * Time.deltaTime;
                Vector3 moveDirection = direction.normalized;

                rb.MovePosition(rb.position + moveDirection * moveDistance);
            }
        }
    }
}