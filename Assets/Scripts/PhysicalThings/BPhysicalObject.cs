using UnityEngine;

namespace Kekw.PhysicalThings
{
    /// <summary>
    /// Base class for all physical objects. Every object requires Rigidbody and collider.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class BPhysicalObject: MonoBehaviour
    {
        private void Awake()
        {
            // Check for collider if no found throw error.
            Collider collider = this.GetComponent<Collider>();
            if (collider == null)
            {
                throw new System.Exception($"This is a physical object and requires collider, please attach collider to {this.gameObject.name}!");
            }
        }
    }
}
