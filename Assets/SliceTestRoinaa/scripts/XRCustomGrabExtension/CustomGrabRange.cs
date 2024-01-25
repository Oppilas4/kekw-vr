using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomGrabRange : XRGrabInteractable
{
    [SerializeField] private float customGrabRange = 0.5f;
    
    // Override the OnSelectEntered method
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Do your custom logic here
        base.OnSelectEntered(args);
    }
    
    // Override the IsSelectableBy method to manually check for grab range
    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        float distance = Vector3.Distance(interactor.transform.position, transform.position);
        return distance <= customGrabRange && base.IsSelectableBy(interactor);
    }

    // Draw a wire sphere around the object to visualize the custom grab range
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, customGrabRange);
    }
}
