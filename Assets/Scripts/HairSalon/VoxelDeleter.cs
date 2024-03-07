using UnityEngine;

public class VoxelDeleter : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Voxel")) return;
        other.gameObject.SetActive(false);
    }
}
