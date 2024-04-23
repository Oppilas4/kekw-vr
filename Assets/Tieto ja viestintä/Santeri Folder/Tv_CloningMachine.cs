using UnityEngine;

public class Tv_CloningMachine : MonoBehaviour
{
    public Transform outputPlate; // The plate where the cloned object will appear.
    private GameObject cloneableObject = null; // Reference to the object that can be cloned.

    private void OnTriggerEnter(Collider other)
    {
        // Check if the trigger collider is triggered by an object.
        if (cloneableObject == null)
        {
            cloneableObject = other.gameObject; // Set the object that can be cloned.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset the cloneable object when it exits the trigger area.
        if (other.gameObject == cloneableObject)
        {
            cloneableObject = null;
        }
    }

    public void CloneObject()
    {
        if (cloneableObject != null)
        {
            // Instantiate a clone of the cloneableObject.
            GameObject clonedObject = Instantiate(cloneableObject, outputPlate.position, Quaternion.identity);

            // Set the cloned object's position and rotation to match the output plate.
            clonedObject.transform.position = outputPlate.position;
            clonedObject.transform.rotation = outputPlate.rotation;

            // Optionally, you can also copy other properties or components from the cloneableObject to the cloned object here.

            Debug.Log("Object cloned!");
        }
        else
        {
            Debug.Log("No object to clone on the plate.");
        }
    }
}
