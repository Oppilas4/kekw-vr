using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scissors : MonoBehaviour
{
    public bool isHeld;

    [SerializeField]
    private VoxelDeleter _deleterScript;
    [SerializeField]
    [Tooltip("Main scissor animation clip, needed to tell animation length")]
    private AnimationClip _animationClip;

    private XRIDefaultInputActions _actionMap;
    private InputAction _primaryAction;
    private Animator _anim;
    private float _animLength;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _actionMap = new();
        _animLength = _animationClip.length;
        _actionMap.XRIRightHandInteraction.Enable();
        _primaryAction = _actionMap.XRIRightHandInteraction.PrimaryAction;
        _deleterScript.active = false;

        _primaryAction.performed += (_) => {
            if (isHeld)
                Snip();
        };
    }

    private void Snip()
    {
        _deleterScript.active = true;
        _anim.SetTrigger("ScissorAnimShouldPlay");
        StartCoroutine(DisableDeleterAfterDelay(_animLength));
    }

    private IEnumerator DisableDeleterAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _deleterScript.active = false;
    }
}
