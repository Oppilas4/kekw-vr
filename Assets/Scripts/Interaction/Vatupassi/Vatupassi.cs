using UnityEngine;
using UnityEngine.VFX;

namespace Kekw.Interaction
{
    /// <summary>
    /// Class measures angles when shit should be coming out of can or some other source.
    /// </summary>
    public class Vatupassi : MonoBehaviour
    {
        /// <summary>
        /// Visual effect to play when pouring.
        /// </summary>
        [SerializeField]
        [Tooltip("Vfx effect to activate")]
        GameObject _visualEffect;

        /// <summary>
        /// Where the vfx comes out 
        /// </summary>
        [SerializeField]
        [Tooltip("'Barrel' where stuff flows out")]
        GameObject _barrel;

        Transform _upperMarker;
        Transform _bottomMarker;

        GameObject _trackedEffect;

        /// <summary>
        /// To detect if this thing is pouring or not.
        /// </summary>
        public bool IsPouring { get; set; }

        private void Awake()
        {
           _upperMarker = this.transform.GetChild(0);
           _bottomMarker = this.transform.GetChild(1);
        }

        /// <summary>
        /// Destroy tracked pouring vfx effect.
        /// </summary>
        public void DestroyTrackedVFX()
        {
            Destroy(_trackedEffect);
            _trackedEffect = null;
        }

        private void Update()
        {
            if(_bottomMarker.transform.position.y >= _upperMarker.transform.position.y)
            {
                IsPouring = true;
                if (_trackedEffect == null)
                {
                    _trackedEffect = Instantiate(_visualEffect);
                    _trackedEffect.transform.position = _barrel.transform.position;
                    _trackedEffect.GetComponent<VisualEffect>().Play();
                }     

                if(_trackedEffect != null)
                {
                    _trackedEffect.transform.position = _barrel.transform.position;
                }
            }
            else if(_trackedEffect != null)
            {
                IsPouring = false;
                _trackedEffect.GetComponent<VisualEffect>().Stop();
                Destroy(_trackedEffect, 3f);
                _trackedEffect = null;
            }
        }
    } 
}
