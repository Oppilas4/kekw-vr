using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_RamiEye : MonoBehaviour
{
    GameObject LookAtWhat;
    public GameObject Reye, Leye;
    XRBaseInteractable RamiInteractable;
    float ReyeScale;
    float LeyeScale;
    void Start()
    {
        RamiInteractable = GetComponent<XRBaseInteractable>();
        LookAtWhat = GameObject.Find("Main Camera");
    }
    void Update()
    {
        Reye.transform.up = LookAtWhat.transform.position - transform.position;
        Leye.transform.up = LookAtWhat.transform.position - transform.position;
        if (RamiInteractable.isSelected)
        {
            var interactor = RamiInteractable.interactorsSelecting[0];
            Debug.Log(interactor.ToString());
            if (interactor.transform.gameObject.tag == "LeftHand")
            {
                ReyeScale = Input.GetAxis("XRI_Left_Trigger") + 1;
                LeyeScale = Input.GetAxis("XRI_Left_Trigger") + 1;
                Reye.transform.localScale = new Vector3(ReyeScale,ReyeScale,ReyeScale);
                Leye.transform.localScale = new Vector3(LeyeScale, LeyeScale, LeyeScale);
            }
            if (interactor.transform.gameObject.tag == "RightHand")
            {
                ReyeScale = Input.GetAxis("XRI_Right_Trigger") + 1;
                LeyeScale = Input.GetAxis("XRI_Right_Trigger") + 1;
                Reye.transform.localScale = new Vector3(ReyeScale, ReyeScale, ReyeScale);
                Leye.transform.localScale = new Vector3(LeyeScale, LeyeScale, LeyeScale);
            }
        }
    }
}
