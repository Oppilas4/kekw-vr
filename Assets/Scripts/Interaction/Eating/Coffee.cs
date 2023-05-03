using Kekw.Manager;
using UnityEngine;
using UnityEngine.VFX;

namespace Kekw.Interaction
{
    /// <summary>
    /// Coffee pan behaviour.
    /// When coffee pan is picked up and detects mug it starts to play pouring VFX.
    /// </summary>
    public class Coffee: MonoBehaviour, ISpawnAble, IDestroyable
    {
        /// <summary>
        /// Vfx effect to play when mug is detected.
        /// </summary>
        [SerializeField]
        [Tooltip("Coffee flow")]
        VisualEffect _vfxEffect;

        SimpleSpawn _simpleSpawn;

        /// <summary>
        /// <seealso cref="IDestroyable"/>
        /// </summary>
        public void OnDestroyRequested()
        {
            if(_simpleSpawn != null){
                _simpleSpawn.Spawn();
                Destroy(this.transform.parent.gameObject);
            }   
        }

        /// <summary>
        /// <seealso cref="ISpawnAble"/>
        /// </summary>
        /// <param name="simpleSpawn"></param>
        public void SetSpawner(SimpleSpawn simpleSpawn)
        {
            _simpleSpawn = simpleSpawn;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Mug"))
            {
                _vfxEffect.SendEvent("Pour");
                other.GetComponent<Mug>().StartFill();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Mug"))
            {
                _vfxEffect.SendEvent("Stop");
                other.GetComponent<Mug>().StopFill();
            }
        }
    }
}
