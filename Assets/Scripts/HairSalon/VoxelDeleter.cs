using UnityEngine;

public class VoxelDeleter : MonoBehaviour
{
    // enabled = false doesn't work for this script, so this variable is needed
    public bool active = true;

    private void OnCollisionEnter(Collision other)
    {
        if (!active || !other.gameObject.CompareTag("Voxel")) return;
        other.gameObject.SetActive(false);
    }
}
