using UnityEngine;

public class Tv_Kopin_jointi : MonoBehaviour
{
    public string copyTag = "Tv_CopyableObject"; // Tag of the objects that can be copied
    public Transform platform1; // Transform of the first platform
    public Transform platform2; // Transform of the second platform



    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Tv_vastaus"))
        {
            print("balls");
            CopyObject();
        }
    }
    // Function to copy object from platform 1 to platform 2
    public void CopyObject()
    {
        // Iterate through all objects on platform 1
        foreach (Transform child in platform1)
        {
            // Check if the object has the correct tag
            if (child.CompareTag(copyTag))
            {
                // Copy the object by instantiating it on platform 2
                GameObject copiedObject = Instantiate(child.gameObject, platform2);
                // Reset position and rotation of the copied object (if needed)
                copiedObject.transform.localPosition = Vector3.zero;
                copiedObject.transform.localRotation = Quaternion.identity;
            }
        }
    }
}