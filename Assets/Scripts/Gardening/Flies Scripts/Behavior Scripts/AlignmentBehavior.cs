using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Corrects the direction of the agent's movement
/// </summary>

namespace Gardening
{
    [CreateAssetMenu(fileName = "Alignment Behavior", menuName = "Flock/Behaviors/AlignmentBehavior")]
    public class AlignmentBehavior : FlockBehaviour
    {
        public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            Vector3 alignmentMove = agent.transform.forward;
            if (context.Count == 0)
                return alignmentMove;

            foreach (Transform transform in context)
            {
                alignmentMove += transform.forward;
            }
            alignmentMove /= context.Count;
            return alignmentMove;
        }
    }
}

