using UnityEngine;

public class StoveController : MonoBehaviour
{
    public string steakTag = "Steak";

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colliding");
        if (collision.gameObject.CompareTag(steakTag))
        {
            SteakController steakController = collision.gameObject.GetComponent<SteakController>();
            if (steakController != null)
            {
                steakController.StartCooking();
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(steakTag))
        {
            SteakController steakController = collision.gameObject.GetComponent<SteakController>();
            if (steakController != null)
            {
                steakController.StopCooking();
            }
        }
    }
}
