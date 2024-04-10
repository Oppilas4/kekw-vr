using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Smooth Cohesion Behavior
/// </summary>
namespace Gardening
{
    [CreateAssetMenu(fileName = "Steered Cohesion Behavior", menuName = "Flock/Behaviors/Steered CohesionBehavior")]
    public class SteeredCohesionBehavior : FlockBehaviour
    {
        [SerializeField] private float smoothTime;
        private Vector3 _currentVelocity = Vector3.zero; // vector3 that used for ref in SmoothDamp
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
            positionOffset = Vector3.SmoothDamp(agent.transform.position, positionOffset, ref _currentVelocity, smoothTime);
            positionOffset -= agent.transform.position;

            return positionOffset;
        }
    }

}
