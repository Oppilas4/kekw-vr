using System.Collections.Generic;
using UnityEngine;

namespace Gardening
{
    public class ForceForBrokenObject : MonoBehaviour
    {
        [SerializeField] private List<Rigidbody> _objectParts = new List<Rigidbody>();

        public void AddForceToAllParts(Vector3 velocity)
        {
            foreach (var part in _objectParts)
            {
                part.velocity = velocity;
            }
        }
    }
}
