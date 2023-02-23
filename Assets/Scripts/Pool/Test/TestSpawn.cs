using UnityEngine;

namespace Kekw.Pool.Test
{
    /// <summary>
    /// Just for testing
    /// </summary>
    public class TestSpawn : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            APool x = FindAnyObjectByType<TestPool>();
            APoolMember y = x.GetFromPool();
            y.gameObject.transform.position = this.transform.position;
        }
    } 
}
