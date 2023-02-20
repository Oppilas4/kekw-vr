using UnityEngine;

namespace Kekw.Player
{
    /// <summary>
    /// Players ability to pick up items in world that implement IOnPickedUp interface.
    /// </summary>
    public class ItemPicker : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Players hand where items are attached")]
        GameObject _hand;
    } 
}
