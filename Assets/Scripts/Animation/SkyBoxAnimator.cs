using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kekw.Animation
{
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
