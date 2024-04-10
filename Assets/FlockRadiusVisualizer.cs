using Gardening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Flock))]
public class FlockRadiusVisualizer : MonoBehaviour
{
    [Tooltip("Pay Attention to the real radius (In Behavior Object)")]
    public float radius;
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
