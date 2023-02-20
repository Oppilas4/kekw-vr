using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kekw.Common;

namespace Kekw.Teleport
{
    /// <summary>
    /// Component provides teleport abilities.
    /// </summary>
    [RequireComponent(typeof(TeleportTarget))]
    public class Teleport : MonoBehaviour, IButtonBinder
    {
        /// <summary>
        /// <seealso cref="IButtonBinder"/>
        /// </summary>
        public void BindInputs()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// <seealso cref="IButtonBinder"/>
        /// </summary>
        public void UnBindInputs()
        {
            throw new System.NotImplementedException();
        }
    }
}
