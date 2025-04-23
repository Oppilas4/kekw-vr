using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AC_HoseResetClicker : MonoBehaviour
{
    public AC_VacuumWire vacuumWire;
    private Animator animator;

    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();

        if (grabbable != null)
        {
            grabbable.activated.AddListener(OnClick);
        }

        animator = GetComponent<Animator>();
    }

    void OnClick(ActivateEventArgs args)
    {
        if (animator != null)
            animator.SetTrigger("Pressed");

        if (vacuumWire != null)
            vacuumWire.ResetHose();
    }
    public void Press(ActivateEventArgs arg)
    {
        animator.SetTrigger("Pressed");

        if (vacuumWire != null)
        {
            vacuumWire.FullReset();
        }
        else
        {
            Debug.LogError("No AC_VacuumWire assigned to this reset button.");
        }
    }

}
