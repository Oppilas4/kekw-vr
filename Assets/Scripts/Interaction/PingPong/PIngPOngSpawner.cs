using Kekw.Manager;
using UnityEngine;

namespace Kekw.Interaction.PingPong
{
    public class PingPongSpawner : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Spawnable object")]
        GameObject _item;

        public GameObject Spawn(PingPongManager missionManager)
        {
            GameObject temp = Instantiate(_item);
            temp.transform.SetPositionAndRotation(this.gameObject.transform.position, this.transform.rotation);
            temp.GetComponent<PingPongItem>().SetMissionManager(missionManager);
            return temp;
        }
    } 
}
