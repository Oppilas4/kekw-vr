using System.Collections;
using UnityEngine;

namespace Kekw.Pool.Test
{
    /// <summary>
    /// Just for testing
    /// </summary>
    public class TestPoolItem: APoolMember
    {

        [SerializeField]
        Material[] _materials;

        void OnEnable()
        {
            StartCoroutine(DestroyMe());
        }

        IEnumerator DestroyMe()
        {
            yield return new WaitForSeconds(.05f);
            APoolMember x = _ownerPool.GetFromPool();
            x.GetComponent<MeshRenderer>().material = _materials[Random.Range(0, _materials.Length)];
            x.gameObject.transform.position = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), Random.Range(-2, 2));
            base._ownerPool.ReturnToPool(this.gameObject);
        }
    } 
}
