using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace Kekw.Interaction
{
    /// <summary>
    /// Class measures angles when shit should be coming out of can or some other source.
    /// </summary>
    public class Vatupassi : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Vfx effect to activate")]
        GameObject _visualEffect;

        [SerializeField]
        [Tooltip("'Barrel' where stuff flows out")]
        GameObject _barrel;

        Transform _upperMarker;
        Transform _bottomMarker;

        GameObject _trackedEffect;

        private void Awake()
        {
           _upperMarker = this.transform.GetChild(0);
           _bottomMarker = this.transform.GetChild(1);
        }

        private void Update()
        {
            if(_bottomMarker.transform.position.y >= _upperMarker.transform.position.y)
            {
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
                _trackedEffect.GetComponent<VisualEffect>().Stop();
                Destroy(_trackedEffect, 3f);
                _trackedEffect = null;
            }
        }
    } 
}
