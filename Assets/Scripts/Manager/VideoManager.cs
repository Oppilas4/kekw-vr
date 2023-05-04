using UnityEngine;
using UnityEngine.Video;

namespace Kekw.Manager
{
    /// <summary>
    /// Set video clips before building build.
    /// or playing game
    /// </summary>
    public class VideoManager : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("What video will be playing?")]
        VideoData _videoData;

        [SerializeField]
        [Tooltip("Video player")]
        VideoPlayer _videoPlayer;


        private void Awake()
        {
            if(_videoData._video == null)
            {
                Debug.LogWarning(
             
                "Video is missing, correct this before building in build machine!\n" +
                "Videos cannot be stored in version control. Import videos manually to project before build and set Video data container objects correctly in Assets/Video."
                );
                return;
            }
            _videoPlayer.clip = _videoData._video;
        }
    }
}
