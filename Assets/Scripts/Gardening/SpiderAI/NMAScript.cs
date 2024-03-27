using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NMAScript : MonoBehaviour
{
    [SerializeField] private List<NavMeshData> data = new List<NavMeshData>();

    private NavMeshAgent agent = null;
    private Bounds bounds;
    private Vector3 moveto;

    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();

        bounds = GetRandomBounds();

        SetRandomDestination();
    }

    private void Update()
    {
        if (agent.hasPath == false)
        {
            SetRandomDestination();
        }
    }

    private void SetRandomDestination()
    {
        bounds = GetRandomBounds();

        moveto = GetRandomPoint();
        agent.SetDestination(moveto);
    }

    private Bounds GetRandomBounds()
    {
        return data[Random.Range(0, data.Count)].sourceBounds;
    }

    private Vector3 GetRandomPoint()
    {
        float rx = Random.Range(bounds.min.x, bounds.max.x);
        float ry = Random.Range(bounds.min.y, bounds.max.y);
        float rz = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(rx, ry, rz);
    }
}
