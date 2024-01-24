using UnityEngine;
using System.Collections;
using Unity.Collections.LowLevel.Unsafe;

public class StoveController : MonoBehaviour
{
    public string steakTag = "Steak";
    private SteakController currentSteak;
    public bool isPanHot = false;
    private float panHeat = 5f;
    private bool isCoroutineRunning = false; // Flag to check if the coroutine is running
    private MC_BurnerHelper currentBurnerHelper;

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
            currentBurnerHelper = other.GetComponent<MC_BurnerHelper>();
            if (currentBurnerHelper != null && currentBurnerHelper.isBurnerOn())
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
            currentBurnerHelper = null;

            if (!isCoroutineRunning)
            {
                panHeat = 5f;
                StartCoroutine(panHeatCoroutine());
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Burner"))
        {
            // Check if the burner is on and the pan is on a cold burner
            if (currentBurnerHelper != null && currentBurnerHelper.isBurnerOn() && !isPanHot)
            {
                isPanHot = true;

                if (currentSteak != null)
                {
                    currentSteak.StartCooking();
                }
            }
            else if (currentBurnerHelper != null && !currentBurnerHelper.isBurnerOn() && isPanHot)
            {
                // Burner is off, cool down the pan
                if (!isCoroutineRunning)
                {
                    panHeat = 5f;
                    StartCoroutine(panHeatCoroutine());
                }
            }
        }
    }

}
