using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kekw.Interaction
{
    public class DisinfectionHolder : MonoBehaviour, IIngameButtonLogic
    {
        [SerializeField]
        [Tooltip("VFX effect to Instantiate when button is pressed")]
        GameObject _disinfectionVFX;

        [SerializeField]
        [Tooltip("Barrel where stuff flows out")]
        GameObject _barrel;

        GameObject _trackedObject;

        /// <summary>
        /// <seealso cref="IIngameButtonLogic"/>
        /// </summary>
        public void TriggerAction()
        {
            if(_trackedObject == null)
            {
                _trackedObject = Instantiate(_disinfectionVFX);
                _trackedObject.transform.position = _barrel.transform.position;
                _trackedObject.transform.rotation = _barrel.transform.rotation;
                Destroy(_trackedObject.gameObject, .8f);
            }
        }

        void Update()
        {
            if(_trackedObject != null)
            {
                _trackedObject.transform.position = _barrel.transform.position;
            }    
        }
    } 
}
