namespace Kekw.Manager
{
    /// <summary>
    /// Interface for objects that can be spawned with <seealso cref="SimpleSpawn"/> o.
    /// </summary>
    interface ISpawnAble
    {
        /// <summary>
        /// Keep track of the spawner that has spawned this object.
        /// </summary>
        /// <param name="simpleSpawn">Spawn point that manages this object.</param>
        public void SetSpawner(SimpleSpawn simpleSpawn);
    }
}
