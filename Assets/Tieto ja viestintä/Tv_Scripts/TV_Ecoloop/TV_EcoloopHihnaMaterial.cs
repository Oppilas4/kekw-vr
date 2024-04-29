using UnityEngine;

public class TV_EcoloopHihnaMaterial : MonoBehaviour
{
    public bool usesY;
    public float scrollSpeed = 0.5f;
    Material material;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
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
