using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_ScreenImage : MonoBehaviour
{
    [SerializeField] MeshRenderer mRenderer;
    [SerializeField] Texture[] theArtworks;

    [SerializeField] bool usesRandom = true;
    public int whatIsSelected = 0;


    private void Start()
    {
        if(mRenderer == null)
        {
            mRenderer = GetComponent<MeshRenderer>();
        }

        Material newMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        if(usesRandom )
        {
            int randomIndex = Random.Range(0, theArtworks.Length);
            newMaterial.mainTexture = theArtworks[randomIndex];
        }
        else
        {
            newMaterial.mainTexture = theArtworks[whatIsSelected];
        }
        newMaterial.SetFloat("_Smoothness", 0f);
        mRenderer.material = newMaterial;
    }
}
