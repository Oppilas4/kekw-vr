using UnityEngine;
using UnityEngine.AI;
using Kekw.Common;

namespace Kekw.VuoksiBotti
{
    /// <summary>
    /// Handles bot moving and routing.
    /// </summary>
    public class Mover : MonoBehaviour, IPause
    {
        [SerializeField]
        [Tooltip("Baked navmesh plane data(nav mesh data) where we want random navigation points to occur.")]
        NavMeshData[] _navMeshDatas;

        NavMeshAgent _navMeshAgent;

        // Start is called before the first frame update
        void Start()
        {
            _navMeshAgent = this.GetComponent<NavMeshAgent>();
            _navMeshAgent.path = GetNewPath();
        }

        // Update is called once per frame
        void Update()
        {
            if(_navMeshAgent.remainingDistance < .1f)
            {
                _navMeshAgent.path = GetNewPath();
            }
        }

        /// <summary>
        /// <seealso cref="IPause"/>
        /// </summary>
        public void SetPause()
        {
            if (!_navMeshAgent.isStopped)
            {
                _navMeshAgent.isStopped = true;
            }
            else
            {
                UnPause();
            }
        }

        /// <summary>
        /// <seealso cref="IPause"/>
        /// </summary>
        public void UnPause()
        {
            if (_navMeshAgent.isStopped)
            {
                _navMeshAgent.isStopped = false;
            }
        }

        /// <summary>
        /// Gets random navigation area data and draw random points until path is valid.
        /// </summary>
        /// <returns></returns>
        private NavMeshPath GetNewPath()
        {
            int randomNavMeshDataIndex = Random.Range(0, _navMeshDatas.Length);
            // Get random points while they are not valid
            while (true)
            {
                Vector3 randomPoint = new Vector3(
                   Random.Range(_navMeshDatas[randomNavMeshDataIndex].sourceBounds.min.x, _navMeshDatas[randomNavMeshDataIndex].sourceBounds.max.x),
                   Random.Range(_navMeshDatas[randomNavMeshDataIndex].sourceBounds.min.y, _navMeshDatas[randomNavMeshDataIndex].sourceBounds.max.y),
                   Random.Range(_navMeshDatas[randomNavMeshDataIndex].sourceBounds.min.z, _navMeshDatas[randomNavMeshDataIndex].sourceBounds.max.z)
                   );
                // Check point is in mesh and can be navigated to
                NavMeshPath path = new NavMeshPath();
                _navMeshAgent.CalculatePath(randomPoint, path);
                if(path.status == NavMeshPathStatus.PathComplete)
                {
                    return path;
                }
            }
        }

       
    } 
}
