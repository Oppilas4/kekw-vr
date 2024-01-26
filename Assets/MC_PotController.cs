using UnityEngine;

public class MC_PotController : MonoBehaviour
{
    public string waterSourceTag = "WaterSource";
    public GameObject waterObject; // Reference to the water object
    public ParticleSystem boilingParticles; // Reference to the boiling particle system
    private bool isPotFilled = false;
    private bool isPotOnWater = false;
    private bool isPotOnBurner = false;

    private Renderer waterRenderer;
    private float fillLevel = 0f;
    private float boilingParticlesEmissionRate = 0f;

    private MC_FaucetControllerHelper currentFaucetController;

    private void Start()
    {
        // Get the renderer component from the water object
        waterRenderer = waterObject.GetComponent<Renderer>();
    }

    private void Update()
    {
        // Check if the pot is on the burner and has water
        if (isPotOnBurner && isPotFilled)
        {
            BoilWater();
        }
        if (!isPotOnBurner && isPotFilled && boilingParticles.isPlaying)
        {
            EndBoiling();
        }

        // Check if the pot is in contact with a water source and the water is turned on
        if (isPotOnWater && currentFaucetController != null && currentFaucetController.isWaterOn())
        {
            FillPotWithWater();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(waterSourceTag))
        {
            currentFaucetController = other.GetComponent<MC_FaucetControllerHelper>();

            if (currentFaucetController != null && currentFaucetController.isWaterOn())
            {
                isPotOnWater = true;
            }
        }

        if (other.CompareTag("Burner"))
        {
            isPotOnBurner = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if the pot is in contact with a water source and the water is turned on
        if (other.CompareTag(waterSourceTag) && isPotOnWater && currentFaucetController != null && currentFaucetController.isWaterOn())
        {
            FillPotWithWater();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the pot is leaving the burner
        if (other.CompareTag("Burner"))
        {
            isPotOnBurner = false;
        }
        if (other.CompareTag(waterSourceTag))
        {
            isPotOnWater = false;
            currentFaucetController = null;
        }
        // Add additional conditions for other triggers if needed
    }

    private void FillPotWithWater()
    {
        // Gradually increase the fill level in the shader
        fillLevel = Mathf.MoveTowards(fillLevel, 1f, Time.deltaTime * 0.2f);
        waterRenderer.material.SetFloat("_Fill", fillLevel); // Set the shader property

        // Check if the pot is fully filled
        if (fillLevel >= 1f)
        {
            isPotFilled = true;
        }
    }

    private void BoilWater()
    {
        // Start the boiling particle effect
        boilingParticles.Play();

        // Increase the emission rate gradually
        boilingParticlesEmissionRate = Mathf.MoveTowards(boilingParticlesEmissionRate, 20f, Time.deltaTime * 5f);
        var emission = boilingParticles.emission;
        emission.rateOverTime = boilingParticlesEmissionRate;

        // Here is where you can start the cooking process of potatoes
        // Add your code to cook potatoes based on the boiling water
    }

    private void EndBoiling()
    {
        boilingParticlesEmissionRate = Mathf.MoveTowards(boilingParticlesEmissionRate, 0f, Time.deltaTime * 5f);
        var emission = boilingParticles.emission;
        emission.rateOverTime = boilingParticlesEmissionRate;

        if (boilingParticlesEmissionRate <= 0.2)
        {
            boilingParticles.Stop();
        }
    }
}
