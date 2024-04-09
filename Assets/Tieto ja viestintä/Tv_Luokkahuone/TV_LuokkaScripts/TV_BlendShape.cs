using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_BlendShape : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public string blendShapeName = "Open";
    public float blendShapeTargetValue = 0f;
    public float blendShapeStartValue = 100f;
    public float blendShapeTransitionTime = 2f;

    private float blendShapeCurrentValue;
    private float blendShapeTransitionTimer;

    void Start()
    {
        skinnedMeshRenderer.SetBlendShapeWeight(skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShapeName), 100f);
        blendShapeCurrentValue = blendShapeStartValue;
        blendShapeTransitionTimer = blendShapeTransitionTime;
    }

    void Update()
    {
        if (blendShapeTransitionTimer > 0f)
        {
            blendShapeTransitionTimer -= Time.deltaTime;
            float t = 1f - (blendShapeTransitionTimer / blendShapeTransitionTime);
            blendShapeCurrentValue = Mathf.Lerp(blendShapeStartValue, blendShapeTargetValue, t);
            skinnedMeshRenderer.SetBlendShapeWeight(skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShapeName), blendShapeCurrentValue);
        }
    }
}
