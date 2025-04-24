using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AC_HoseResetClicker : MonoBehaviour
{
    public AC_VacuumWire hoseSystem;

    private XRGrabInteractable interactable;

    void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
        interactable.activated.AddListener(OnClick);
    }

    public void OnClick(ActivateEventArgs args)
    {
        if (hoseSystem != null)
        {
            hoseSystem.ResetEntireHose();
        }
    }
}
