using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class AC_Checklist : MonoBehaviour
{
    public GameObject[] tasks;
    private bool isExpanded = false;

    private void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(ToggleChecklist);  
    }

    private void ToggleChecklist(SelectEnterEventArgs args)  
    {
        isExpanded = !isExpanded;

        foreach (GameObject task in tasks)
        {
            task.SetActive(isExpanded);
        }
    }
}
