using UnityEngine;

namespace HairSalon
{
    public class MaterialPainter : MonoBehaviour
    {
        [SerializeField] private Material material;
    
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Voxel"))
            {
                other.gameObject.GetComponent<Renderer>().material = material;
            }
        }
    }
}
