using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gardening {
    [CreateAssetMenu(fileName = "Cohesion Behavior", menuName = "Flock/Behaviors/CohesionBehavior")]
    public class CohesionBehavior : FlockBehaviour
    {
        public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            Vector3 positionOffset = Vector3.zero;
            if (context.Count == 0)
                return positionOffset;

            foreach (Transform transform in context)
            {
                positionOffset += transform.position;
            }
            positionOffset /= context.Count;
            positionOffset -= agent.transform.position;
            return positionOffset;
        }
    }
}

