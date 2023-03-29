using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kekw.Pool
{
    /// <summary>
    /// Queueus new ball from pool to basket if hitting the floor.
    /// </summary>
    class VuoksiBallPoolItem: APoolMember
    {
        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            if (collision.gameObject.CompareTag("Floor"))
            {
                APoolMember newBall = _ownerPool.GetFromPool();
                newBall.gameObject.transform.position = _ownerPool.transform.position;
                _ownerPool.ReturnToPool(this.gameObject);
            }
        }
    }
}
