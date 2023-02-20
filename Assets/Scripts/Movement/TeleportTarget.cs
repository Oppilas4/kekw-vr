using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kekw.Teleport
{
    /// <summary>
    /// Component provides teleport route rendering and contains cursor target property.
    /// </summary>
    [RequireComponent(typeof(LineRenderer))]
    public class TeleportTarget : MonoBehaviour
    {
        /// <summary>
        /// Future world position where player will teleport.
        /// </summary>
        public Vector3 TeleportPosition { get; private set; }

        [SerializeField]
        [Tooltip("Ending gameobject")]
        GameObject _endingPosition;
    } 
}
