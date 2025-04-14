using UnityEngine;

public class AC_DoorToggle : MonoBehaviour
{
    public Animator animator;
    private bool isOpen = false;

    public void ToggleDoor()
    {
        if (isOpen)
        {
            animator.SetTrigger("Close");
        }
        else
        {
            animator.SetTrigger("Open");
        }

        isOpen = !isOpen; // flip the state
    }
}
