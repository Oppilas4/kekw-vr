using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AC_Clicker : MonoBehaviour
{
    public AC_DogMovement dog;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable2 = GetComponent<XRGrabInteractable>();

        // Listen for activated event
        grabbable2.activated.AddListener(Press);

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Press(ActivateEventArgs arg)
    {
        animator.SetTrigger("Pressed");
        if(this.gameObject.name == "SinkClicker")
        {
            dog.movetoNearTub = true;
        }
        else if (this.gameObject.name == "TrimClicker")
        {
            dog.movetoTrim = true;
        }
    }
}
