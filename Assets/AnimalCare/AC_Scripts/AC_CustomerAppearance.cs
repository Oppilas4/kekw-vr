using UnityEngine;

public class AC_CustomerAppearance : MonoBehaviour
{
    public Material[] clothesMaterials; // Drag all your materials here in the Inspector
    public Renderer clothingRenderer;   // Drag the specific Renderer that uses the clothing material

    void Start()
    {
        RandomizeClothing();
    }

    void RandomizeClothing()
    {
        if (clothesMaterials.Length == 0 || clothingRenderer == null)
        {
            return;
        }

        Material randomMat = clothesMaterials[Random.Range(0, clothesMaterials.Length)];

        clothingRenderer.material = clothesMaterials[Random.Range(0, clothesMaterials.Length)];
    }
}
