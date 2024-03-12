using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juho_MaterialTest : MonoBehaviour
{
    GameObject go;
    public Color color;
    public Material material;

    public float dissolveSpeed = 0.5f;

    private void Start()
    {
        go = this.gameObject;
        MeshRenderer meshRenderer;
        if (go.TryGetComponent(out meshRenderer))
        {
            material = meshRenderer.material;
            material.color = color;
        }
        material.color = color;
    }

    public void StartDissolve()
    {
        StartCoroutine(DissolveEnemy());
    }

    IEnumerator DissolveEnemy()
    {
        material.SetFloat("_Dissolve", 0);
        float dissolveAmount = 0f;

        while (dissolveAmount < 1f)
        {
            dissolveAmount += dissolveSpeed * Time.deltaTime;

            material.SetFloat("_Dissolve", dissolveAmount);

            yield return null;
        }

        Destroy(gameObject);
    }
}
