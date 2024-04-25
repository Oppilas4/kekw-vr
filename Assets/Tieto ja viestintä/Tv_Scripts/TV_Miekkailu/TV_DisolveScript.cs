using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_DisolveScript : MonoBehaviour
{
    public Color color;
    public Material oringinalDisolveMat;
    public Material newlyCreatedDissolveMat;
    public MeshRenderer meshRenderer;


    public void StartDissolve()
    {
        StartCoroutine(DissolveEnemy());
    }

    public void RenewTheInfo()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        newlyCreatedDissolveMat = new Material(oringinalDisolveMat);
        newlyCreatedDissolveMat.color = color;

        Material[] materials = meshRenderer.materials;
        List<Material> newMaterialsList = new List<Material>();

        for (int i = 0; i < materials.Length; i++)
        {
            if (i == 0)
            {
                newMaterialsList.Add(newlyCreatedDissolveMat);
            }
            else
            {
                Destroy(materials[i]); // Remove the material
            }
        }

        meshRenderer.material = newlyCreatedDissolveMat; // Assign the new material to the meshRenderer

        meshRenderer.materials = newMaterialsList.ToArray(); // Assign the new materials array to the meshRenderer
    }

    IEnumerator DissolveEnemy()
    {
        newlyCreatedDissolveMat.SetFloat("_Dissolve", 0);
        float dissolveAmount = 0f;

        while (dissolveAmount < 1f)
        {
            dissolveAmount += .5f * Time.deltaTime;

            newlyCreatedDissolveMat.SetFloat("_Dissolve", dissolveAmount);

            yield return null;
        }

        Destroy(gameObject);
    }
}
