
using UnityEngine;

namespace Kekw.PhysicalThings
{
    /// <summary>
    /// Interface to describe what happends when object is picked up.
    /// </summary>
    public interface IOnPickedUp
    {
        /// <summary>
        /// What happends when item is picked up to hand.
        /// </summary>
        /// <param name="newParent"></param>
        public void OnItemPickUp(GameObject picker);
    }
}
