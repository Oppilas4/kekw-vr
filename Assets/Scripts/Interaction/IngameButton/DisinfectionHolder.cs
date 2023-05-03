using UnityEngine;

namespace Kekw.Interaction
{
    /// <summary>
    /// Hand sanitizer bottle thingy behaviour.<br></br><br></br>
    /// Instantiates vfx effect when button is pressed.
    /// </summary>
    public class DisinfectionHolder : MonoBehaviour, IIngameButtonLogic
    {
        /// <summary>
        /// Vfx to play
        /// </summary>
        [SerializeField]
        [Tooltip("VFX effect to Instantiate when button is pressed")]
        GameObject _disinfectionVFX;

        /// <summary>
        /// Gameobject where the vfx "flows" out.
        /// </summary>
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
