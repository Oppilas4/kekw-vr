using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TV_BlendShape : MonoBehaviour
{
    public GameObject valkokangas;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public string blendShapeName = "Open";
    public float blendShapeTargetValue = 0f;
    public float blendShapeStartValue = 100f;
    public float blendShapeTransitionTime = 2f;
    public VideoClip videoClip; // Assign the video clip in the Inspector

    private float blendShapeCurrentValue;
    private float blendShapeTransitionTimer;
    private bool videoPlayed = false;

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

            // Check if blend shape key has reached its peak
            if (blendShapeTransitionTimer <= 0f && !videoPlayed)
            {
                // Check if a video clip is assigned
                if (videoClip != null && valkokangas != null)
                {
                    // Play the video clip
                    PlayVideo();
                    videoPlayed = true;
                }
                else
                {
                    Debug.LogWarning("No video clip assigned.");
                }
            }
        }
    }

    void PlayVideo()
    {
        valkokangas.gameObject.SetActive(true);
        GetComponent<VideoPlayer>().Play();
    }
}