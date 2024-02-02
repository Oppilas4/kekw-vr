using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

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
    private MC_BurnerHelper currentBurnerController;

    private float pourThreshold = 30f; // Adjust this angle based on your preference
    private bool isPouring = false;
    [SerializeField] private VisualEffect pourEffect;

    public List<Transform> pourEffectPositions; // List of points around the rim for pouring effect
    private Vector3 lowestPosition;

    private void Start()
    {
        // Get the renderer component from the water object
        waterRenderer = waterObject.GetComponent<Renderer>();
        pourEffect.SendEvent("Stop");
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

        // Check if the pot is tilted forward and play the pour effect
        if (fillLevel != 0 && IsPotTilted())
        {
            PourWater();
        }
        else
        {
            pourEffect.SendEvent("Stop");
            isPouring = false;
        }
    }

    private bool IsPotTilted()
    {
        // Check if the pot is tilted forward or to the side based on the x and z-axis rotations
        float xRotation = transform.parent.rotation.eulerAngles.x;
        float zRotation = transform.parent.rotation.eulerAngles.z;

        // Normalize the rotation angles to be between -180 and 180 degrees
        xRotation = (xRotation > 180f) ? xRotation - 360f : xRotation;
        zRotation = (zRotation > 180f) ? zRotation - 360f : zRotation;

        // Check if the pot is tilted beyond the pour threshold in either x or z direction
        return (Mathf.Abs(xRotation) > pourThreshold || Mathf.Abs(zRotation) > pourThreshold);
    }

    private void PourWater()
    {
        if (!isPouring)
        {
            // Choose the lowest point among the pourEffectPositions
            Vector3 pourPosition = GetLowestPourPosition();

            // Set the pour effect position
            pourEffect.transform.position = pourPosition;

            pourEffect.SendEvent("Pour");
            isPouring = true;
        }

        // Add your logic here for pouring water out of the pot
        // You may want to decrease the fill level or trigger other effects

        // Example: Decrease the fill level gradually
        fillLevel = Mathf.MoveTowards(fillLevel, 0f, Time.deltaTime * 0.2f);
        waterRenderer.material.SetFloat("_Fill", fillLevel);

        // Check if the pot is empty
        if (fillLevel == 0f)
        {
            isPouring = false;
            isPotFilled = false;

            pourEffect.SendEvent("Stop");
        }
    }

    private Vector3 GetLowestPourPosition()
    {
        lowestPosition = pourEffectPositions[0].position; // Initialize with the position of the first pour effect position

        for (int i = 1; i < pourEffectPositions.Count; i++)
        {
            Transform pourTransform = pourEffectPositions[i];

            // Compare pourTransform.position.y with the y-coordinate of lowestPosition
            if (pourTransform.position.y < lowestPosition.y)
            {
                lowestPosition = pourTransform.position;
            }
        }

        return lowestPosition;
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
            // Check if the burner is on using MC_BurnerHelper
            currentBurnerController = other.GetComponent<MC_BurnerHelper>();
            if (currentBurnerController != null && currentBurnerController.isBurnerOn())
            {
                isPotOnBurner = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (currentFaucetController != null && currentFaucetController.isWaterOn())
        {
            isPotOnWater = true;
        }

        if (currentBurnerController != null && currentBurnerController.isBurnerOn())
        {
            isPotOnBurner = true;
        }
        else if (currentBurnerController != null && !currentBurnerController.isBurnerOn() )
        {
            isPotOnBurner = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the pot is leaving the burner
        if (other.CompareTag("Burner"))
        {
            isPotOnBurner = false;

            if (currentBurnerController != null)
            {
                currentBurnerController = null;
            }
        }
        if (other.CompareTag(waterSourceTag))
        {
            isPotOnWater = false;
            currentFaucetController = null;
        }
    }

    private void FillPotWithWater()
    {
        // Gradually increase the fill level in the shader
        fillLevel = Mathf.MoveTowards(fillLevel, 1f, Time.deltaTime * 0.2f);
        waterRenderer.material.SetFloat("_Fill", fillLevel); // Set the shader property

        // Check if the pot is fully filled
        if (fillLevel >= 0.8f)
        {
            isPotFilled = true;
        }
    }

    private void BoilWater()
    {
        if (!boilingParticles.isPlaying)
        {
            // Start the boiling particle effect
            boilingParticles.Play();
        }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Draw gizmos for all pourEffectPositions
        foreach (Transform pourTransform in pourEffectPositions)
        {
            Gizmos.DrawSphere(pourTransform.position, 0.01f);
        }

        // Highlight the lowest pour position in green
        Gizmos.color = Color.green;
        Vector3 lowestPourPosition = GetLowestPourPosition();
        Gizmos.DrawSphere(lowestPourPosition, 0.01f);
    }
}
