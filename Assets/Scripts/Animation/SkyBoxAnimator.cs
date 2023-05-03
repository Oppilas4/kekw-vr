using UnityEngine;
using System;

namespace Kekw.Animation
{
    /// <summary>
    /// Class is deprecated should not be needed.
    /// </summary>
    [Obsolete("Deprecated", true)]
    public class SkyBoxAnimator : MonoBehaviour
    {
        /// <summary>
        /// Speed that skybox rotates.
        /// </summary>
        [SerializeField]
        [Tooltip("Rotation Speed")]
        float _speed;

        // Update is called once per frame
        void Update()
        {
            RenderSettings.skybox.SetFloat("_Rotation", Time.time * _speed);
        }
    } 
}
