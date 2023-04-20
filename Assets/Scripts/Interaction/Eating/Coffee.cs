using Kekw.Manager;
using UnityEngine;
using UnityEngine.VFX;

namespace Kekw.Interaction
{
    /// <summary>
    /// Coffee pan trigger
    /// </summary>
    class Coffee: MonoBehaviour, ISpawnAble, IDestroyable
    {
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
