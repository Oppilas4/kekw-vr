using System;
using UnityEngine;
namespace Kekw.Interaction.Football
{
    /// <summary>
    /// Stops rigid body based on ray cast result
    /// </summary>
    class Stopper: MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Rigidbody to stop")]
        Rigidbody _rb;

        [SerializeField]
        [Tooltip("Ray length to positive X(Red line)")]
        float _rayLenPosX;

        [SerializeField]
        [Tooltip("Ray length to negative X(Green Line)")]
        float _rayLenNegX;

        [SerializeField]
        [Tooltip("Backup distance when colliding")]
        float _backUp;

        [SerializeField]
        [Tooltip("Max x axis fallback error")]
        float _xMaxError = .3f;

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
