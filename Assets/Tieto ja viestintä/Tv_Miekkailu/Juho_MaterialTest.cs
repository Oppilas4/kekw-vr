using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juho_MaterialTest : MonoBehaviour
{
    public GameObject go;
    public Color color;
    public Material material;
    Material theDissolveMat;
    MeshRenderer meshRenderer;

    public float dissolveSpeed = 0.5f;

    public void StartDissolve()
    {
        go = this.gameObject;
        meshRenderer = go.GetComponent<MeshRenderer>();


        theDissolveMat = new Material(material);

        Material[] materials = meshRenderer.materials;
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = theDissolveMat;
        }
        meshRenderer.materials = materials;
        theDissolveMat.color = color;

        StartCoroutine(DissolveEnemy());
    }

    IEnumerator DissolveEnemy()
    {
        theDissolveMat.SetFloat("_Dissolve", 0);
        float dissolveAmount = 0f;

        while (dissolveAmount < 1f)
        {
            dissolveAmount += dissolveSpeed * Time.deltaTime;

            theDissolveMat.SetFloat("_Dissolve", dissolveAmount);

            yield return null;
        }

        Destroy(gameObject);
    }
}
