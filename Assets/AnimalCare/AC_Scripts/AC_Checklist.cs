using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AC_Checklist : MonoBehaviour
{
    public GameObject checklistUI; 

    private void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();

       
        grabInteractable.activated.AddListener(OnGrab); 
        grabInteractable.deactivated.AddListener(OnRelease); 
    }

   
    private void OnGrab(ActivateEventArgs arg)
    {
        
    }

    
    private void OnRelease(DeactivateEventArgs arg)
    {
       
    }
}
