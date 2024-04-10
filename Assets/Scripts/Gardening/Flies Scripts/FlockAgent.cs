using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gardening
{
    [RequireComponent(typeof(Collider))]
    public class FlockAgent : MonoBehaviour
    {
        private Collider _agentCollider;
        public Collider AgentCollider { get { return _agentCollider; } }
        private void Start()
        {
            _agentCollider = GetComponent<Collider>();
            Debug.Log("worked");
        }
        public void Move(Vector3 newPosition)   // Moves agent to the new position we will get from FlockBehavior
        {
            transform.forward = newPosition;
            transform.position += newPosition;
        }
    }
}

