using System.Collections.Generic;
using UnityEngine;

namespace Gardening
{
    [CreateAssetMenu(fileName = "StayInRadius Behavior", menuName = "Flock/Behaviors/StayInRadius Behavior")]
    public class StayInRadiusBehavior : FlockBehaviour
    {
        [SerializeField]
        private float _radius;
        private Vector3 _center = Vector3.zero;
        public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            Vector3 agentOffset = _center - agent.transform.position;
            float distanceFromCenter = agentOffset.sqrMagnitude / (_radius * _radius);
            if (distanceFromCenter < 0.9)
                return Vector3.zero;
            return agentOffset * distanceFromCenter;
        }
    }

}
