using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_XRSocketTest : MonoBehaviour
{
    // Start is called before the first frame update
    public XRBaseInteractor interactor;
    public GameObject SelectedOne;
    void Start()
    {
        interactor.allowSelect = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(SelectedOne != null) SelectedOne = interactor.selectTarget.gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Elec_WireEnds>() != null)
        {
            interactor.allowSelect = true;
        }
    }
}
