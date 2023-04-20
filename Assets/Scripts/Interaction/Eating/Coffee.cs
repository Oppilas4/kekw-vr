using UnityEngine;
using UnityEngine.VFX;

namespace Kekw.Interaction
{
    /// <summary>
    /// Coffee pan trigger
    /// </summary>
    class Coffee: MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Coffee flow")]
        VisualEffect _vfxEffect;

        private void OnTriggerEnter(Collider other)
        {
            _vfxEffect.SendEvent("Pour");
            other.GetComponent<Mug>().StartFill();
        }

        private void OnTriggerExit(Collider other)
        {
            _vfxEffect.SendEvent("Stop");
            other.GetComponent<Mug>().StopFill();
        }
    }
}
