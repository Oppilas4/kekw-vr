using UnityEngine;

namespace Kekw.PhysicalThings
{
    /// <summary>
    /// This object can be picked up to player hand.
    /// </summary>
    public class PickUpObject : BPhysicalObject, IOnPickedUp
    {
        /// <summary>
        /// <seealso cref="IOnPickedUp.OnItemPickUp"/>
        /// </summary>
        public void OnItemPickUp(GameObject picker)
        {
            Debug.Log("Item should move to hand");
            // Get hand transform from picker parameter.

            // Set this objects parent to hand transform
        }
    }
}
