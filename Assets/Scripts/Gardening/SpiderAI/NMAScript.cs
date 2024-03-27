using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NMAScript : MonoBehaviour
{
    [SerializeField] private List<NavMeshData> data = new List<NavMeshData>();

    private NavMeshAgent agent = null;
    private Bounds bounds;
    private Vector3 moveto;
    private bool flag = false;

    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        bounds = data[Random.Range(0, data.Count)].sourceBounds;

        SetRandomDestination();
    }

    private void Update()
    {
        if (agent.hasPath == false && flag == false)
        {
            flag = true;
            SetRandomDestination();
        }
    }

    private void SetRandomDestination()
    {
        bounds = data[Random.Range(0, data.Count)].sourceBounds;
        //1. pick a point
        float rx = Random.Range(bounds.min.x, bounds.max.x);
        float ry = Random.Range(bounds.min.y, bounds.max.y);
        float rz = Random.Range(bounds.min.z, bounds.max.z);
        moveto = new Vector3(rx, ry, rz);
        agent.SetDestination(moveto); //figure out path, starts gameobject moving

        //Invoke("CheckPointOnPath", 0.2f);

        flag = false;
    }

    private void CheckPointOnPath()
    {
        //4. check
        if (agent.pathEndPosition != moveto)
        {
            SetRandomDestination();
        }
    }


}
