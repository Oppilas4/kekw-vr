using UnityEngine;

namespace Kekw.Animation
{
    /// <summary>
    /// Select idle animatioin for character.
    /// </summary>
    public class CharacterAnimationSelector : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Animator for character")]
        Animator _animator;

        [SerializeField]
        [Tooltip("Animation index")]
        int _animationIndex;

        // Use this for initialization
        void Start()
        {
            _animator.SetInteger("AnimationIndex", _animationIndex);
        }
    }
}