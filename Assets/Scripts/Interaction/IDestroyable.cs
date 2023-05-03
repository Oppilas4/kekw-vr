

namespace Kekw.Interaction
{
    /// <summary>
    /// If object needs to be destroyed use this interface with Destroyer
    /// <seealso cref="Destroyer"/>
    /// <see cref="Mug"/>
    /// </summary>
    public interface IDestroyable
    {
        public void OnDestroyRequested();
    }
}
