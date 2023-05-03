using UnityEngine;

namespace Kekw.Animation
{
    /// <summary>
    /// Select idle animation for character or another entity.
    /// Works by giving index of animation in Animator.
    /// </summary>
    public class CharacterAnimationSelector : MonoBehaviour
    {
        /// <summary>
        /// Animator where Stuff is
        /// </summary>
        [SerializeField]
        [Tooltip("Animator for character")]
        Animator _animator;

        /// <summary>
        /// Index of the animation
        /// </summary>
        [SerializeField]
        [Tooltip("Animation index")]
        int _animationIndex;


        void Start()
        {
            _animator.SetInteger("AnimationIndex", _animationIndex);
        }
    }
}