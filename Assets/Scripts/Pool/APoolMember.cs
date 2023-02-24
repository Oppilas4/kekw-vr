using UnityEngine;

namespace Kekw.Pool
{
    /// <summary>
    /// Pool memeber base class.
    /// </summary>
    public abstract class APoolMember : MonoBehaviour
    {

        protected APool _ownerPool;

        /// <summary>
        /// Set pool that owns this object.
        /// </summary>
        /// <param name="pool"></param>
        public void SetOwnerPool(APool pool)
        {
            _ownerPool = pool;
        }
    }
}