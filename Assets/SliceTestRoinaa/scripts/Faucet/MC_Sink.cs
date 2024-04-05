using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[Obsolete]
public class MC_Sink : MonoBehaviour
{
    public GameObject waterObject; // Reference to the water object
    public Renderer waterRenderer; // Reference to the renderer of the water object
    public XRSocketInteractor socket; // Reference to the XR socket interactor
    public XRBaseInteractable plug; // Reference to the plug interactable
    private MC_FaucetController faucetController;
    public float maxWaterLevel = 1f; // Maximum water level
    public float waterFillSpeed = 0.1f; // Speed at which water fills the sink
    public float waterDrainSpeed = 0.1f; // Speed at which water drains from the sink

    private bool isFaucetOn = false; // State of the faucet
    private bool isPlugInserted = false; // State of the plug insertion
    private bool isPotOnWater = false; //state of the pot
    private float currentWaterLevel = 0f; // Current water level

    private bool needsShaderUpdate = false;

    private MC_SinkWater sinkWater;


    private void OnEnable()
    {
        // Subscribe to the events when the component is enabled
        socket.onSelectEntered.AddListener(OnPlugInserted);
        socket.onSelectExited.AddListener(OnPlugRemoved);
        // Subscribe to the OnWaterStatusChanged event
        faucetController = FindAnyObjectByType<MC_FaucetController>();
        faucetController.OnWaterStatusChanged.AddListener(HandleWaterStatusChange);

        MC_PotController.OnPotWaterStatusChanged += HandlePotWaterStatusChange;
    }

    private void OnDisable()
    {
        // Unsubscribe from the events when the component is disabled
        socket.onSelectEntered.RemoveListener(OnPlugInserted);
        socket.onSelectExited.RemoveListener(OnPlugRemoved);
        faucetController.OnWaterStatusChanged.RemoveListener(HandleWaterStatusChange);
        MC_PotController.OnPotWaterStatusChanged -= HandlePotWaterStatusChange;
    }

    private void Start()
    {
        // Assuming the waterObject has a Renderer component for its material
        waterRenderer = waterObject.GetComponent<Renderer>();
        sinkWater = FindAnyObjectByType<MC_SinkWater>();
    }

    private void Update()
    {
        // Check if the faucet is on and the plug is not inserted
        if (isFaucetOn && isPlugInserted && !isPotOnWater)
        {
            // Increase the water level
            currentWaterLevel = Mathf.Min(currentWaterLevel + Time.deltaTime * waterFillSpeed, maxWaterLevel);
            needsShaderUpdate = true; // Set the flag to true when the water level changes
        }
        else if (currentWaterLevel != 0 && !isPlugInserted)
        {
            // Decrease the water level
            currentWaterLevel = Mathf.Max(currentWaterLevel - Time.deltaTime * waterDrainSpeed, 0f);
            needsShaderUpdate = true; // Set the flag to true when the water level changes
        }

        // Only update the shader if the flag is set
        if (needsShaderUpdate)
        {
            waterRenderer.material.SetFloat("_Fill", currentWaterLevel);
            needsShaderUpdate = false; // Reset the flag after updating the shader
        }
    }

    // Call this method to toggle the faucet on or off
    public void ToggleFaucet(bool isOn)
    {
        isFaucetOn = isOn;
    }

    // Call this method when the plug is inserted or removed
    public void InsertPlug(bool isInserted)
    {
        isPlugInserted = isInserted;
    }

    public void potOnWater(bool isOnWater)
    {
        isPotOnWater = isOnWater;
    }

    // Call this method to check if the sink is full
    public bool IsSinkFull()
    {
        return currentWaterLevel >= maxWaterLevel;
    }

    // Call this method to reset the water level
    public void ResetWaterLevel()
    {
        currentWaterLevel = 0f;
        waterRenderer.material.SetFloat("_Fill", currentWaterLevel);
    }

    private void OnPlugInserted(XRBaseInteractable interactable)
    {
        if (interactable == plug)
        {
            InsertPlug(true);
        }
    }

    private void OnPlugRemoved(XRBaseInteractable interactable)
    {
        if (interactable == plug)
        {
            InsertPlug(false);
            sinkWater.DisableSoapEffect();
        }
    }

    private void HandleWaterStatusChange(bool isWaterOn)
    {
        ToggleFaucet(isWaterOn);
    }

    private void HandlePotWaterStatusChange(bool isPotOnWater)
    {
        potOnWater(isPotOnWater);
    }
}
