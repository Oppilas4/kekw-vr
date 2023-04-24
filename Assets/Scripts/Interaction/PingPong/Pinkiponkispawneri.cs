using Kekw.Manager;
using UnityEngine;

public class Pinkiponkispawneri : MonoBehaviour, ISpawnAble, IDestroyable
{
    SimpleSpawn _simpleSpawn;

    /// <summary>
    /// <seealso cref="IDestroyable"/>
    /// </summary>
    public void OnDestroyRequested()
    {
        if (_simpleSpawn != null)
        {
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
}
