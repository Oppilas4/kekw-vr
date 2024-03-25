using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juho_Dissolve : MonoBehaviour
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
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = newlyCreatedDissolveMat;
        }
        meshRenderer.materials = materials;
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
