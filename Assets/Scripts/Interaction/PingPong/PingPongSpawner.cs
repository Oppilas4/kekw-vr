using UnityEngine;

namespace Kekw.Interaction.PingPong
{
    /// <summary>
    /// Spawns needed objects to ping pong game.
    /// </summary>
    public class PingPongSpawner : MonoBehaviour
    {
        /// <summary>
        /// What item should this spawn.
        /// </summary>
        [SerializeField]
        [Tooltip("Spawnable object")]
        GameObject _item;

        /// <summary>
        /// Spawn '_item' to gameworld and set owning manager.
        /// </summary>
        /// <param name="missionManager">Manager that controls this spawner</param>
        /// <returns></returns>
        public GameObject Spawn(PingPongManager missionManager)
        {
            GameObject temp = Instantiate(_item);
            temp.transform.SetPositionAndRotation(this.gameObject.transform.position, this.transform.rotation);
            temp.GetComponent<PingPongItem>().SetMissionManager(missionManager);
            return temp;
        }
    } 
}
