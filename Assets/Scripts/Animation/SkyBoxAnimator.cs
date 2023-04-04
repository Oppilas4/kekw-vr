using UnityEngine;
using System;

namespace Kekw.Animation
{
    [Obsolete("Deprecated", true)]
    public class SkyBoxAnimator : MonoBehaviour
    {
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
