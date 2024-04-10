using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_ArtWorkScript : MonoBehaviour
{
    [SerializeField] MeshRenderer mRenderer;
    [SerializeField] Texture theArtwork;

    private void Start()
    {
        if(mRenderer == null)
        {
            mRenderer = GetComponent<MeshRenderer>();
        }

        Material newMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        newMaterial.mainTexture = theArtwork;
        mRenderer.material = newMaterial;
    }
}
