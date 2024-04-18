using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MC_EnableRayTrigger : MonoBehaviour
{
    private MC_DisableRayInteractor _rayScript;
    void Start()
    {
        _rayScript = FindAnyObjectByType<MC_DisableRayInteractor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _rayScript.EnableAllRayInteractors();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _rayScript.DisableAllRayInteractors();
        }
    }
}
