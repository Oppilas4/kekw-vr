using UnityEngine;

public class Tv_CloningMachine : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip cantClone;
    public AudioClip canClone;
    public Transform outputPlate; // The plate where the cloned object will appear.
    private GameObject cloneableObject = null; // Reference to the object that can be cloned.


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
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
        if (cloneableObject != null && cloneableObject.CompareTag("Tv_CopyableObject"))
        {
            GameObject clonedObject = Instantiate(cloneableObject, outputPlate.position, Quaternion.identity);
            clonedObject.transform.position = outputPlate.position;
            clonedObject.transform.rotation = outputPlate.rotation;

            Debug.Log("Object cloned!");
            audioSource.clip = canClone;
            audioSource.Play();
        }
        else
        {
            Debug.Log("No object to clone on the plate.");
            audioSource.clip = cantClone;
            audioSource.Play();
        }
    }
}
