using UnityEngine;

public class AC_HoseCollisionGuard : MonoBehaviour
{
    public Rigidbody headRb;

    void Start()
    {
        // Ignore collisions between this trigger and all hose segments
        foreach (var segment in GameObject.FindGameObjectsWithTag("HoseSegment"))
        {
            Collider segCol = segment.GetComponent<Collider>();
            Collider bufferCol = GetComponent<Collider>();

            if (segCol && bufferCol)
                Physics.IgnoreCollision(bufferCol, segCol);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Optional: add a layer/tag filter
        if (other.gameObject.CompareTag("Obstacle")) // Tag the table or wall "Obstacle"
        {
            if (headRb != null)
            {
                // Stop movement
                headRb.velocity = Vector3.zero;
                headRb.angularVelocity = Vector3.zero;

                // Optional: slightly push it back
                Vector3 pushDir = (headRb.position - other.ClosestPoint(headRb.position)).normalized;
                headRb.MovePosition(headRb.position + pushDir * 0.05f);
            }
        }
    }
}
