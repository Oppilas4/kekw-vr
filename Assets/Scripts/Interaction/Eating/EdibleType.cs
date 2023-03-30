using UnityEngine;

namespace Kekw.Interaction
{
    /// <summary>
    /// Edibte type definations
    /// </summary>
    public enum EdibleTypes
    {
        DRINK,
        EAT
    }

    /// <summary>
    /// Kinda edible what we are intetracting with.
    /// </summary>
    class EdibleType: MonoBehaviour
    {
        [Tooltip("Edible type")]
        public EdibleTypes ETYPE;
    }
}
