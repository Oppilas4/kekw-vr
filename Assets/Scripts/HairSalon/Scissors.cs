using UnityEngine;
using UnityEngine.InputSystem;

public class Scissors : MonoBehaviour
{
    public bool isHeld;
    [SerializeField]
    private VoxelDeleter _deleterScript;
    private XRIDefaultInputActions _actionMap;
    private InputAction _primaryAction;

    private void Start()
    {
        _actionMap = new();
        _actionMap.XRIRightHandInteraction.Enable();
        _primaryAction = _actionMap.XRIRightHandInteraction.PrimaryAction;
        SetScissorState(working: false);

        _primaryAction.started += (_) => {
            if (isHeld)
                SetScissorState(working: true);
            else
                Debug.Log("Not held in action started");
        };
        _primaryAction.canceled += (_) => {
            if (isHeld)
                SetScissorState(working: false);
            else
                Debug.Log("Not held in action canceled");
        };
    }

    private void SetScissorState(bool working)
    {
        _deleterScript.active = working;
    }
}
