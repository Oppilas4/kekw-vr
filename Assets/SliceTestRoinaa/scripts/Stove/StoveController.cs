using UnityEngine;
using System.Collections;

public class StoveController : MonoBehaviour
{
    public string steakTag = "Steak";
    private SteakController currentSteak;
    public bool isPanHot = false;
    private float panHeat = 5f;
    private bool isCoroutineRunning = false; // Flag to check if the coroutine is running

    IEnumerator panHeatCoroutine()
    {
        isCoroutineRunning = true;

        float startTime = Time.time;

        while (panHeat > 0)
        {
            float elapsedTime = Time.time - startTime;
            panHeat = Mathf.Clamp(5f - elapsedTime, 0f, 5f);

            yield return null;
        }

        isPanHot = false;

        isCoroutineRunning = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(steakTag))
        {
            currentSteak = collision.gameObject.GetComponent<SteakController>();
            if (currentSteak != null && isPanHot)
            {
                currentSteak.StartCooking();
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
                currentSteak = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Burner"))
        {
            // Check the parent for FlameController
            FlameController flameController = other.transform.parent.GetComponent<FlameController>();
            if (flameController != null && flameController.isOn)
            {
                isPanHot = true;

                if (currentSteak != null)
                {
                    currentSteak.StartCooking();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Burner"))
        {
            // Check the parent for FlameController
            FlameController flameController = other.transform.parent.GetComponent<FlameController>();
            if (flameController != null && !isCoroutineRunning)
            {
                panHeat = 5f;
                StartCoroutine(panHeatCoroutine());
            }
        }
    }
}
