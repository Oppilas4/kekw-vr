namespace Kekw.Manager
{
    /// <summary>
    /// Interface for objects that can be spawned with <seealso cref="SimpleSpawn"/> o.
    /// </summary>
    interface ISpawnAble
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="simpleSpawn"></param>
        public void SetSpawner(SimpleSpawn simpleSpawn);
    }
}
