using System.Collections;
using UnityEngine;


public class MC_HandsEffectManager : MonoBehaviour
{
    public float changeDuration = 2f; // Duration of the change in seconds
    public float changeAmount = 0.1f; // Amount to increase or decrease

    private Renderer rightHandRend;
    private Renderer leftHandRend;

    void Start()
    {
        // Find objects with the specified names
        GameObject object1 = GameObject.Find("asdMesh.001");
        GameObject object2 = GameObject.Find("asdMesh.002");

        // Check if objects are found
        if (object1 != null && object2 != null)
        {
            // Get the materials from the objects
            rightHandRend = object1.GetComponent<Renderer>();
            leftHandRend = object2.GetComponent<Renderer>();

            // Set initial _StepScale value to 0 for both objects
            SetStepScale(rightHandRend, 0);
            SetStepScale(leftHandRend, 0);
        }
        else
        {
            Debug.LogError("One or both objects not found!");
        }
    }

    void SetStepScale(Renderer renderer, float value)
    {
        // Set the _StepScale variable in the shader for the specified object
        renderer.material.SetFloat("_StepScale", value);
    }

    public void ChangeStepScale(Renderer renderer, float amount, float duration)
    {
        // Start the coroutine to change _StepScale over time for the specified object
        StartCoroutine(ChangeStepScaleOverTime(renderer, amount, duration));
    }

    IEnumerator ChangeStepScaleOverTime(Renderer renderer, float amount, float duration)
    {
        float elapsedTime = 0f;

        // Calculate the initial _StepScale value
        float startValue = renderer.material.GetFloat("_StepScale");

        // Calculate the target _StepScale value
        float targetValue = amount;

        // Interpolate _StepScale value over time for the specified object
        while (elapsedTime < duration)
        {
            float newValue = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
            SetStepScale(renderer, newValue);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the final value is set accurately for the specified object
        SetStepScale(renderer, targetValue);
    }
}
