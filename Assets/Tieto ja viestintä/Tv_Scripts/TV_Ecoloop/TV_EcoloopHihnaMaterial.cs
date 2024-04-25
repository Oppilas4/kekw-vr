using UnityEngine;

public class TV_EcoloopHihnaMaterial : MonoBehaviour
{
    public bool usesY;
    public float scrollSpeed = 0.5f;

    void Update()
    {
        Material material = GetComponent<Renderer>().material;
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
