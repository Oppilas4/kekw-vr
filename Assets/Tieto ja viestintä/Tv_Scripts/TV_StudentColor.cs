using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_StudentColor : MonoBehaviour
{
    public Color color;
    public Material originalMat;
    public float intensity = 11f;
    Material newlyCreatedMat;
    public MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        newlyCreatedMat = new Material(originalMat);
        Color hdrColor = color * intensity; // Adjusting color intensity for HDR
        newlyCreatedMat.SetColor("_MainColor", hdrColor);
        meshRenderer.material = newlyCreatedMat;
    }
}
