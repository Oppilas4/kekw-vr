using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Determines behavior that will be applied for some agent in the flock
/// </summary>
namespace Gardening
{
    public abstract class FlockBehaviour : ScriptableObject
    {
        public abstract Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock);
    }
}

