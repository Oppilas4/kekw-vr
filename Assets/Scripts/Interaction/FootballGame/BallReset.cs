using System;
using UnityEngine;

namespace Kekw.Interaction.Football
{
    /// <summary>
    /// Resets ball in football game
    /// </summary>
    class BallReset: MonoBehaviour, IIngameButtonLogic
    {
        Vector3 _orgPosition;
        Rigidbody _rb;

        private void Awake()
        {
            _orgPosition = this.transform.position;
            _rb = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// <seealso cref="IIngameButtonLogic"/>
        /// </summary>
        public void TriggerAction()
        {
            _rb.isKinematic = true;
            this.transform.position = _orgPosition;
            _rb.isKinematic = false;
        }
    }
}
