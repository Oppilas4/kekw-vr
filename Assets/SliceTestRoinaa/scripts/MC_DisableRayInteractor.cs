using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MC_DisableRayInteractor : MonoBehaviour
{

    private void Start()
    {
        DisableAllRayInteractors();
    }
    public void DisableAllRayInteractors()
    {
        XRRayInteractor[] rayInteractors = FindObjectsOfType<XRRayInteractor>();
        foreach (XRRayInteractor interactor in rayInteractors)
        {
            interactor.enabled = false;
        }
    }
}
