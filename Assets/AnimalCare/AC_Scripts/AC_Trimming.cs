using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_Trimming : MonoBehaviour
{
    public Material trimmedMaterial; // Assign this in the Inspector
    public SkinnedMeshRenderer skinnedMeshRenderer;
    private bool isTrimmed = false;
    public AC_ChecklistManager checklistManager;
    void Start()
    {
        SkinnedMeshRenderer skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        if (trimmedMaterial == null)
        {
            Debug.LogWarning("Trimmed material not assigned.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trimmer") && !isTrimmed)
        {
            ApplyTrimmedMaterial();
        }
    }

    void ApplyTrimmedMaterial()
    {
        if (skinnedMeshRenderer != null && trimmedMaterial != null)
        {
            Material[] materials = skinnedMeshRenderer.materials;

            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = trimmedMaterial; // Replace all materials with the trimmed one
            }

            skinnedMeshRenderer.materials = materials;
            isTrimmed = true;
            checklistManager.CompleteTask(1);
        }
        else
        {
            Debug.LogWarning("SkinnedMeshRenderer or trimmedMaterial is missing.");
        }
    }
}
