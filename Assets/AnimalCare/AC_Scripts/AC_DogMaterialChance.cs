using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AC_DogMaterialChance : MonoBehaviour
{
    public string childObjectName = "Plane";
    public Material wetMaterial; // material for indication when dog is wet
    private Material originalMaterial;
    private Renderer objectRenderer;

    public GameObject dripping;

    ParticleSystem waterDripping;

    // Start is called before the first frame update
    void Start()
    {
        Transform child = transform.Find(childObjectName);
        if (child != null)
        {
            objectRenderer = child.GetComponent<Renderer>();
            if (objectRenderer != null)
            {
                originalMaterial = objectRenderer.material; // saves original material
                Debug.Log("original material saved");
            }
        }

       
        
        
        waterDripping = dripping.GetComponent<ParticleSystem>();
        

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("particle hit");
        if (objectRenderer != null && wetMaterial != null)
        {   
            dripping.SetActive(true);
            waterDripping.Play();
            objectRenderer.material = wetMaterial; // chance wet material
        }
    }
}
