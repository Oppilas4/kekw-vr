using System.Collections.Generic;
using UnityEngine;

namespace Gardening
{
    [CreateAssetMenu(fileName = "Composite Behavior", menuName = "Flock/Composite Behavior")]
    public class CompositeBehavior : FlockBehaviour
    {
        [SerializeField] FlockBehaviour[] _behaviours;
        [SerializeField] float[] _weights;
        private void OnValidate()
        {
            if (_behaviours.Length != _weights.Length)
            {
                Debug.LogWarning("Behaviors length doesn't suit for the weights lenght");
            }
        }

        public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            Vector3 move = Vector3.zero;
            for (int i = 0; i < _behaviours.Length; i++)
            {
                Vector3 partialMove = _behaviours[i].CalculateMove(agent, context, flock) * _weights[i];
                if (partialMove != Vector3.zero)
                {
                    if (partialMove.sqrMagnitude > _weights[i] * _weights[i])
                    {
                        partialMove.Normalize();
                        partialMove *= _weights[i];
                    }
                    move += partialMove;
                }
            }
            return move;
        }
    }
}

