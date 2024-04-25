using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_ChairHeight : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public string blendShapeName = "HowHigh";

    private void Start()
    {
        int theHight = Random.Range(0, 100);
        skinnedMeshRenderer.SetBlendShapeWeight(skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShapeName), theHight);
    }
}
