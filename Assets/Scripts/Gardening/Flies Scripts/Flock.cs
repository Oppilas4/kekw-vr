using System.Collections.Generic;
using UnityEngine;

namespace Gardening
{
    public class Flock : MonoBehaviour
    {
        [SerializeField] private FlockAgent _agentPrefab;
        private List<FlockAgent> _agents = new List<FlockAgent>();

        [SerializeField] private FlockBehaviour _flockBehavior;

        [Range(0.1f, 1f)]
        public float agentDensity;
        [Range(5f, 500f)]
        public float startingCount;
        [Range(0.1f, 10f)]
        public float neighbourRadius;

        [SerializeField] private float _avoidanceRadius;
        [SerializeField] private float _speed;
        [SerializeField] private float _maxSpeed;

        private float _squareMaxSpeed;
        public float SquareAvoidanceRadius { get; private set; }

        private void Start()
        {
            _squareMaxSpeed = _maxSpeed * _maxSpeed;
            SquareAvoidanceRadius = _avoidanceRadius * _avoidanceRadius;

            for (int i = 0; i < startingCount; i++)
            {
                FlockAgent newAgent = Instantiate(
                    _agentPrefab,
                    Random.insideUnitSphere * startingCount * agentDensity,
                    Quaternion.Euler(transform.forward * Random.Range(0f, 360f)),
                    transform);
                newAgent.name = "Agent " + i;
                _agents.Add(newAgent);
            }
        }
        private void Update()
        {
            foreach (var agent in _agents)
            {
                List<Transform> context = GetNearbyObjects(agent);
                //agent.GetComponentInChildren<MeshRenderer>().material.color = Color.Lerp(Color.white, Color.green, context.Count / 6f);

                Vector3 positionOffset = _flockBehavior.CalculateMove(agent, context, this);


                positionOffset *= _speed; //Adding speed

                if (positionOffset.sqrMagnitude > _squareMaxSpeed) // sqrMagnitude is more efficient than magnitude (no need to take square root)
                {
                    positionOffset = positionOffset.normalized * _maxSpeed;
                }
                agent.Move(positionOffset);

            }
        }
        private List<Transform> GetNearbyObjects(FlockAgent agent)
        {
            List<Transform> objects = new List<Transform>();
            Collider[] colliders = Physics.OverlapSphere(agent.transform.position, neighbourRadius);
            foreach (Collider collider in colliders)
            {
                if (collider == agent.AgentCollider)
                    continue;
                objects.Add(collider.transform);
            }
            return objects;
        }
    }
}

