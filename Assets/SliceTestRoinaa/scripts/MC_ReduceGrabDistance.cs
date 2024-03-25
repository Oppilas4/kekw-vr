using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_ReduceGrabDistance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Find all objects with the name "Direct Interactor"
        GameObject[] directInteractors = GameObject.FindObjectsOfType<GameObject>();
        List<GameObject> directInteractorsWithName = new List<GameObject>();

        foreach (GameObject interactor in directInteractors)
        {
            if (interactor.name == "Direct Interactor")
            {
                directInteractorsWithName.Add(interactor);
            }
        }

        // Iterate through each object
        foreach (GameObject interactor in directInteractorsWithName)
        {
            // Find the SphereCollider component attached to the object
            SphereCollider sphereCollider = interactor.GetComponent<SphereCollider>();

            // If a SphereCollider is found, reduce its radius
            if (sphereCollider != null)
            {
                sphereCollider.radius = 0.07f;
            }
        }
    }
}
