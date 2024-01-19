using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_MegaTool : MonoBehaviour
{
    public GameObject EndPrefab;
    public GameObject SpawnPos;
    public float shootingForce = 10;
    GameObject WirePiece;
    public Elec_ToolWireRenderer ToolWireREnderer;
    AudioSource StaplerAudio;
    Animator Animator;
    XRBaseInteractable Stapler;
    bool HasShoten;
    private void Start()
    {
        Stapler = GetComponent<XRBaseInteractable>();
        Animator = GetComponent<Animator>();
        Animator.speed = 0;
        StaplerAudio = GetComponent<AudioSource>();
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
            ToolWireREnderer.WireComponents.Add(WirePiece);
            HasShoten = true;
        }
    }
    private void Update()
    {
        if (Stapler.isSelected)
        {
            var interactor = Stapler.interactorsSelecting[0];
            Debug.Log(interactor.ToString());
            if (interactor.transform.gameObject.tag == "LeftHand")
            {
                Animator.Play("Cube_001_Down", 0, Input.GetAxis("XRI_Left_Trigger"));
            }
            if (interactor.transform.gameObject.tag == "RightHand")
            {
                Animator.Play("Cube_001_Down", 0, Input.GetAxis("XRI_Right_Trigger"));
            }           
        }

    }
    void HasShotenSetFalse()
    {
        HasShoten = false;
    }
}
