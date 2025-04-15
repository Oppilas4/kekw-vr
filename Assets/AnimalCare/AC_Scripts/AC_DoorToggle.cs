using UnityEngine;

public class AC_DoorToggle : MonoBehaviour
{
    public Animator animator;
    private bool isOpen = false;
    public bool checkOpening = false;
    public void ToggleDoor()
    {
        if (isOpen)
        {
            animator.SetTrigger("Close");
            checkOpening = false;
        }
        else
        {
            animator.SetTrigger("Open");
            checkOpening = true;
        }

        isOpen = !isOpen; // flip the state
    }
}
