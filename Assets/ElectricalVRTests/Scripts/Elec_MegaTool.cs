using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_MegaTool : MonoBehaviour
{
    public bool IsFullAuto = false;
    public GameObject EndPrefab;
    public GameObject SpawnPos;
    public float shootingForce = 10;
    GameObject WirePiece;
    AudioSource StaplerAudio;
    Animator Animator;
    XRBaseInteractable Stapler;
    int spoolID = 0;
    public List<Elec_ToolWireRenderer> WireSpools = new List<Elec_ToolWireRenderer>();
    public List<Elec_ToolWireRenderer> WireSpoolsSaved;
    Elec_ToolWireRenderer CurrentWire;
    bool HasShoten;
    XRBaseInteractor InteractorSelecting;

    [System.Obsolete]
    private void Start()
    {
        CurrentWire = WireSpools[spoolID];
        Stapler = GetComponent<XRBaseInteractable>();
        Animator = GetComponent<Animator>();
        Animator.speed = 0;
        StaplerAudio = GetComponent<AudioSource>();
        Stapler.onSelectEntered.AddListener(OnSelected);
        Stapler.onSelectExited.AddListener(OnDeselected);
    }
    public void MakeWireEnd()
    {
        if (!HasShoten) 
        {
            WirePiece = Instantiate(EndPrefab, SpawnPos.transform.position, SpawnPos.transform.rotation);
            if (WirePiece.GetComponent<Rigidbody>() != null)
            {
                WirePiece.GetComponent<Rigidbody>().AddForce(-SpawnPos.transform.forward * shootingForce, ForceMode.Impulse);
            }
            StaplerAudio.Play();
            CurrentWire.WireComponents.Add(WirePiece);
            WirePiece.GetComponent<Elec_StapleMakeStick>().ListID = CurrentWire.WireComponents.Count - 1;
            WirePiece.GetComponent<Elec_StapleMakeStick>().SpoolItIsON = CurrentWire;
            WirePiece.GetComponent<Elec_StapleMakeStick>().currentVoltage = CurrentWire.Voltage_Send();
            if (!IsFullAuto) HasShoten = true; 
        }
    }
    private void Update()
    {
        if (InteractorSelecting != null)
        {         
            if (InteractorSelecting.transform.gameObject.tag == "RightHand" && Input.GetButtonDown("XRI_Right_PrimaryButton") || InteractorSelecting.transform.gameObject.tag == "LeftHand" && Input.GetButtonDown("XRI_Left_PrimaryButton"))
            {
                SwitchWire();
            }
            else if (InteractorSelecting.transform.gameObject.tag == "LeftHand")
            {
                Animator.Play("Cube_001_Down", 0, Input.GetAxis("XRI_Left_Trigger"));
            }
            else if (InteractorSelecting.transform.gameObject.tag == "RightHand")
            {
                Animator.Play("Cube_001_Down", 0, Input.GetAxis("XRI_Right_Trigger"));
            }
           
        }
    }
    private void OnSelected(XRBaseInteractor interactor)
    {
        InteractorSelecting = interactor;
    }
    private void OnDeselected(XRBaseInteractor interactor)
    {
        InteractorSelecting = null;
    }
    public void SwitchWire()
    {
        if (spoolID == WireSpools.Count -1 || spoolID == WireSpools.Count) 
        { 
            spoolID = 0;
            CurrentWire = WireSpools[spoolID];
            return;
        }           
        else
        {
            spoolID++;
            CurrentWire = WireSpools[spoolID];          
        }
    }
    public void ResetWireList()
    {
        WireSpools = WireSpoolsSaved;
    }
    void HasShotenSetFalse()
    {
        HasShoten = false;
    }
    public void TurnOnFullAuto()
    {
        IsFullAuto = true;
    }
}
