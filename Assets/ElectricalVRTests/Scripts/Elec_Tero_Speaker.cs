using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_Tero_Speaker : MonoBehaviour
{
    public bool Talking = false;
    Renderer ourRenderer;
    private MaterialPropertyBlock ourMaterialPropertyBlock;
    public Color currentEmissionColor = Color.black;
    public Color colorWhileTalking = Color.red;
    private void Update()
    {
        if (ourRenderer == null)
        {
            ourRenderer = GetComponent<Renderer>();
        }
        
        if (ourMaterialPropertyBlock == null)
        {
            ourMaterialPropertyBlock = new MaterialPropertyBlock();
        }

        if (Talking)
        {
            colorWhileTalking = Color.red * Random.Range(-2f, 3f);
            if (currentEmissionColor != colorWhileTalking)
            {
                currentEmissionColor = Color.Lerp(currentEmissionColor, colorWhileTalking, 6 * Time.deltaTime);
            }
            ourMaterialPropertyBlock.SetColor("_EmissionColor", currentEmissionColor);
            ourRenderer.SetPropertyBlock(ourMaterialPropertyBlock);
        }
        else if (currentEmissionColor != Color.black)
        {
            currentEmissionColor = Color.black;
            ourMaterialPropertyBlock.SetColor("_EmissionColor", currentEmissionColor);
            ourRenderer.SetPropertyBlock(ourMaterialPropertyBlock);
        }

    }
}
