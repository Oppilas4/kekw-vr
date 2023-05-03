
namespace Kekw.Common
{
    /// <summary>
    /// Pausing interface
    /// </summary>
    public interface IPause
    {
        /// <summary>
        /// Pause gameobject.
        /// </summary>
        public void SetPause();


        /// <summary>
        /// Un pause game object so it continues normal behaviour.
        /// </summary>
        public void UnPause();
    }
}
