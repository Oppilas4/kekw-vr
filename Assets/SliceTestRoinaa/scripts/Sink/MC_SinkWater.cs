using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_SinkWater : MonoBehaviour
{
    private Material waterMat;
    public ParticleSystem _bubbles;
    public ParticleSystem _foam;
    private void Start()
    {
        waterMat = GetComponent<Renderer>().material;
    }
    public bool hasWater()
    {
        if (waterMat != null)
        {
            float fillValue = waterMat.GetFloat("_Fill");
            return fillValue > 0.8f;
        }
        else
        {
            Debug.LogWarning("Water material not assigned.");
            return false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plate") && _bubbles.isPlaying)
        {
            PlateController plateController = other.GetComponent<PlateController>();
            if (plateController != null && plateController.DecalProjector != null)
            {
                plateController.DecalProjector.SetActive(false);
            }
            
        }
    }

    public void EnableSoapEffect()
    {
        _bubbles.Play();
        _foam.Play();
    }

    public void DisableSoapEffect()
    {
        _bubbles.Stop();
        _foam.Stop();
    }
}
