using UnityEngine;
using UnityEngine.InputSystem;

public class Scissors : MonoBehaviour
{
    [SerializeField]
    private VoxelDeleter _deleterScript;
    private XRIDefaultInputActions _actionMap;
    private InputAction _primaryAction;

    private void Start()
    {
        _actionMap = new();
        _actionMap.XRIRightHandInteraction.Enable();
        _primaryAction = _actionMap.XRIRightHandInteraction.PrimaryAction;
        _deleterScript.active = false;

        _primaryAction.started += (_) =>
        {
            _deleterScript.active = true;
        };
        _primaryAction.canceled += (_) =>
        {
            _deleterScript.active = false;
        };
    }
}
