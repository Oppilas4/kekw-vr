using UnityEngine;
using UnityEngine.Video;

namespace Kekw.Manager
{
    /// <summary>
    /// Video data asset
    /// </summary>
    [CreateAssetMenu(fileName = "VideoData", menuName = "VideoData/videoContainer", order = 1)]
    public class VideoData : ScriptableObject
    {
        /// <summary>
        /// What video does this asset contain.
        /// </summary>
        public VideoClip _video;
    }
}
