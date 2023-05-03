using UnityEngine;

namespace Kekw.Interaction
{
    /// <summary>
    /// Edibte type definations. Edibles consist of 2 types.
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
        /// <summary>
        /// What type of edible current gameobject is.
        /// </summary>
        [Tooltip("Edible type")]
        public EdibleTypes ETYPE;
    }
}
