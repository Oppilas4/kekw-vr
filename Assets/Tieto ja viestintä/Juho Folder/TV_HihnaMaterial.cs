using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_HihnaMaterial : MonoBehaviour
{
    public bool usesY;
    public float scrollSpeed = 0.5f; // Adjust the speed of the scrolling

    void Update()
    {
        // Get the current material of the object
        Material material = GetComponent<Renderer>().material;

        // Calculate the offset to move the texture
        float offset = Time.time * scrollSpeed;

        if(usesY)
        {
            material.mainTextureOffset = new Vector2(0, offset);
        }
        else
        {
            material.mainTextureOffset = new Vector2(offset, 0);
        }
    }
}
