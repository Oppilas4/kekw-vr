using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScissorsGrabInteractable : XRGrabInteractable
{
    [SerializeField]
    private Scissors _scissorScript;

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        _scissorScript.isHeld = true;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        _scissorScript.isHeld = false;
    }
}
