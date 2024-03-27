using UnityEngine;
using UnityEngine.AI;

public class NavigationTest : MonoBehaviour
{
   [SerializeField] private NavMeshAgent _agent;
   [SerializeField] private GameObject _target;

    private void Start()
    {
        _agent.destination = _target.transform.position;
    }
}
