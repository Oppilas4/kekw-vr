using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Optimization
{
    /// <summary>
    /// Sets application target framerate on android builds
    /// </summary>
    public class FramerateTargeter : MonoBehaviour
    {
        [SerializeField] int _requestedFrameRate = 90;

        // Use this for initialization
        void Start()
        {
            // If compiling to android platform.
#if UNITY_ANDROID
            Application.targetFrameRate = _requestedFrameRate;
#endif
        }
    }
}