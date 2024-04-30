using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Elec_DecalGIF : MonoBehaviour
{
    public List<Material> materials;
    DecalProjector projector;
    void Start()
    {
        projector = GetComponent<DecalProjector>();
        StartCoroutine(CycleThroughPics()); 
    }
    IEnumerator CycleThroughPics()
    {
        int i = 0;
        while (true) 
        {               
                if (i == materials.Count) i = 0;
                projector.material = materials[i];
                i++;
                yield return new WaitForSeconds(0.1f);
        }     
    }
}
