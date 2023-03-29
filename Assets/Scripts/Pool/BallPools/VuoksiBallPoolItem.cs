using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Kekw.Pool
{
    /// <summary>
    /// Queueus new ball from pool to basket if hitting the floor.
    /// </summary>
    class VuoksiBallPoolItem: APoolMember
    {
        Coroutine _delay;

        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            if (collision.gameObject.CompareTag("Floor"))
            {
                if(_ownerPool.GetQueueuLength() > 0)
                {
                    APoolMember newBall = _ownerPool.GetFromPool();
                    newBall.gameObject.transform.position = _ownerPool.transform.position;
                }
               
                if(_delay == null)
                {
                   StartCoroutine(ReturnToPoolWithDelay());
                }
            }
        }

        IEnumerator ReturnToPoolWithDelay()
        {
            yield return new WaitForSeconds(2f);
            _ownerPool.ReturnToPool(this.gameObject);
            _delay = null;
        }
    }
}
