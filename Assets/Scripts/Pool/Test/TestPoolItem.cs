using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kekw.Pool.Test
{
    /// <summary>
    /// Just for testing
    /// </summary>
    public class TestPoolItem: APoolMember
    {

        void OnEnable()
        {
            StartCoroutine(DestroyMe());
        }

        IEnumerator DestroyMe()
        {
            yield return new WaitForSeconds(.05f);
            APoolMember x = _ownerPool.GetFromPool();
            x.gameObject.transform.position = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), Random.Range(-2, 2));
            base._ownerPool.ReturnToPool(this.gameObject);
        }
    } 
}
