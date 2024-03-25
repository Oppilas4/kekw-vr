using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_FryableObjectHelper : MonoBehaviour
{
    public bool IsFrying { get; set; }

    public void StartFrying()
    {
        // Implement logic to start frying
        IsFrying = true;
    }

    public void StopFrying()
    {
        // Implement logic to stop frying
        IsFrying = false;
    }
}
