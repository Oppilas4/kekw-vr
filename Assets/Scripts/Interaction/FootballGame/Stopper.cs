using System;
using UnityEngine;
namespace Kekw.Interaction.Football
{
    /// <summary>
    /// Controls football game stick movement.<br></br>
    /// Allows player to interact with stick by pushing and pulling.
    /// Limits movement with raycasts.
    /// IMPORTANT! Uses global axis instead of local axis.
    /// </summary>
    public class Stopper: MonoBehaviour
    {
        /// <summary>
        /// What rigidbody this script should stop.
        /// </summary>
        [SerializeField]
        [Tooltip("Rigidbody to stop")]
        Rigidbody _rb;

        /// <summary>
        /// Raylength to positive x direction.
        /// </summary>
        [SerializeField]
        [Tooltip("Ray length to positive X(Red line)")]
        float _rayLenPosX;

        /// <summary>
        /// Raylength to negative x direction.
        /// </summary>
        [SerializeField]
        [Tooltip("Ray length to negative X(Green Line)")]
        float _rayLenNegX;

        /// <summary>
        /// Distance to back up from detected collider.
        /// </summary>
        [SerializeField]
        [Tooltip("Backup distance when colliding")]
        float _backUp;

        /// <summary>
        /// Max error compared to original position after reached performs hard reset.
        /// </summary>
        [SerializeField]
        [Tooltip("Max x axis fallback error")]
        float _xMaxError = .3f;

        /// <summary>
        /// Should debug rays be drawn?
        /// </summary>
        [SerializeField]
        [Tooltip("Draw debug rays")]
        bool _drawDebugRays = false;

        Vector3 _originalPosition;

        private void Awake()
        {
            _originalPosition = _rb.gameObject.transform.position;
        }


        private void Update()
        {
            if (_drawDebugRays)
            {
                Debug.DrawRay(this.transform.position, Vector3.left * _rayLenNegX, Color.green);
                Debug.DrawRay(this.transform.position, -Vector3.left * _rayLenPosX, Color.red);
            }

            bool hitNegX = Physics.Raycast(this.transform.position, Vector3.left, _rayLenNegX);
            bool hitPosX = Physics.Raycast(this.transform.position, -Vector3.left, _rayLenPosX);

            if(hitNegX || hitPosX)
            {
                Vector3 angularVelocity = _rb.angularVelocity;
                _rb.isKinematic = true;

                // Stop movement                
                _rb.velocity = Vector3.zero;
               
                // back up from collision
                Vector3 position = _rb.gameObject.transform.position;
                if (hitNegX)
                {
                    position.x += _backUp;
                }
                if (hitPosX)
                {
                    position.x -= _backUp;
                }
                _rb.gameObject.transform.position = position;
                _rb.isKinematic = false;
                _rb.angularVelocity = angularVelocity;
            }

            // full hard reset
            if(MathF.Abs(MathF.Abs(_rb.gameObject.transform.position.x) - MathF.Abs(_originalPosition.x)) >= _xMaxError)
            {
                _rb.isKinematic = true;
                _rb.gameObject.transform.position = _originalPosition;
                _rb.isKinematic = false;
            }
        }
    }
}
