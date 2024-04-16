using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BooleanEvent : UnityEvent<bool> { }
public class MC_FaucetController : MonoBehaviour, IDial
{
    public bool isWaterOn = false;

    [SerializeField] ParticleSystem waterParticles;
    private float minFlowRate = 0f;
    private float maxFlowRate = 40f;
    public AudioSource _audioSource;
    public BooleanEvent OnWaterStatusChanged;

    public void DialChanged(float dialValue)
    {
        // Assuming that turning the dial clockwise increases the water flow
        var flowRate = Mathf.Clamp(dialValue / 90f, minFlowRate, maxFlowRate);

        // Adjust water flow based on dial rotation
        var emission = waterParticles.emission;
        emission.rateOverTime = Mathf.Lerp(0, maxFlowRate, flowRate);

        bool wasWaterOn = isWaterOn;
        // Enable or disable the water particles based on the flow rate
        isWaterOn = flowRate > 0.02f;
        if (wasWaterOn != isWaterOn)
        {
            OnWaterStatusChanged?.Invoke(isWaterOn); // Invoke the event with the new status

            if (isWaterOn)
            {
                _audioSource.Play();
            }
            else
            {
                _audioSource.Stop();
            }
        }
        emission.enabled = isWaterOn;
    }
    public bool GetIsWaterOn()
    {
        return isWaterOn;
    }
}