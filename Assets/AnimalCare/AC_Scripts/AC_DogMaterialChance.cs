using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AC_DogMaterialChance : MonoBehaviour
{
    public string childObjectName = "Plane";
    public Material wetMaterial; // material for indication when dog is wet
    public Material trimmedMaterial; // material for indication when dog is wet
    private Material originalMaterial;
    private Renderer objectRenderer;

    
    public AC_Soap soap;
    public AC_DogMovement dog;
    public AC_ChecklistManager checklistManager;

    public GameObject dripping;
    ParticleSystem waterDripping;

    public bool wet = false;
    public bool wet1 = false;
    public bool wet2 = false;

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
            if (other.CompareTag("Water"))
            {
                dripping.SetActive(true);
                waterDripping.Play();
                objectRenderer.material = wetMaterial; // chance wet material
                wet = true;
                if (!wet1)
                {

                    wet1 = true;
                }
                else if (!wet2 && soap.foamed)
                {

                    dog.AfterShower();
                    wet2 = true;
                }
            }
            else if (other.CompareTag("Trimmer"))
            {
                objectRenderer.material = trimmedMaterial;
                checklistManager.CompleteTask(1);
            }
        }
    }
    public void ChangeColorBack()
    {
        objectRenderer.material = originalMaterial;
        wet1 = false;
        wet2 = false;
        wet = false;
        soap.foamed = false;
        waterDripping.Stop();
        dripping.SetActive(false);
    }
}
