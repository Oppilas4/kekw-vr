using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Corrects movement of the agent to avoid colliding with other agents
/// </summary>

namespace Gardening 
{
    [CreateAssetMenu(fileName = "Avoidance Behavior", menuName = "Flock/Behaviors/AvoidanceBehavior")]
    public class AvoidanceBehavior : FlockBehaviour
    {
        public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            Vector3 avoidMove = Vector3.zero;
            if (context.Count > 0)
            {
                int avoidNumber = 0;
                foreach (Transform transform in context)
                {
                    if (Vector3.SqrMagnitude(agent.transform.position - transform.position) < flock.SquareAvoidanceRadius)
                    {
                        avoidNumber++;
                        avoidMove += agent.transform.position - transform.position;
                    }
                }
                if (avoidNumber > 0)
                    avoidMove /= avoidNumber;
            }
            return avoidMove;
        }
    }
}

