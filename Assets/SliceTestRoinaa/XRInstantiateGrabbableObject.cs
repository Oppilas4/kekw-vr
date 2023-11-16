using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class XRInstantiateGrabbableObject : XRBaseInteractable
{
    [SerializeField]
    private GameObject grabbableObject;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Get the transform of the hand that grabs the object
        Transform handTransform = args.interactorObject.transform;

        // Instantiate object at the hand's position and rotation
        GameObject newObject = Instantiate(grabbableObject, handTransform.position, handTransform.rotation);

        // Get grab interactable from prefab
        XRGrabInteractable objectInteractable = newObject.GetComponent<XRGrabInteractable>();

        // Select object into the same interactor (hand)
        interactionManager.SelectEnter(args.interactorObject, objectInteractable);

        base.OnSelectEntered(args);
    }
}
