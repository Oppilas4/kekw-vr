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
    public XRSocketInteractor RamiSocketInteractable;
    Animator Animator;
    public AudioSource GuitarSource;
    public AudioClip GuitarLoop,GuitarStart;
    void Start()
    
    {
        Animator = GetComponent<Animator>();
        RamiInteractable = GetComponent<XRBaseInteractable>();
        LookAtWhat = GameObject.Find("Main Camera");
        RamiSocketInteractable.onSelectExited.AddListener(MyLittleDancer);
        RamiSocketInteractable.onSelectEntered.AddListener(MyLittleGuitarHero);
    }

    private void MyLittleDancer(XRBaseInteractable arg0)
    {
        arg0.GetComponent<Collider>().isTrigger = false;
        Animator.SetTrigger("Dance");
        GuitarSource.loop = false;
        GuitarSource.Stop();
    }

    void Update()
    {
        Reye.transform.up = LookAtWhat.transform.position - transform.position;
        Leye.transform.up = LookAtWhat.transform.position - transform.position;
        if (RamiInteractable.isSelected)
        {
            var interactor = RamiInteractable.interactorsSelecting[0];
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
    public void MyLittleGuitarHero(XRBaseInteractable other)
    {
       if (other.tag == "Guitar")
        {
            other.GetComponent<Collider>().isTrigger = true;
            Animator.SetTrigger("Shred");
            GuitarSource.PlayOneShot(GuitarStart);
            StartCoroutine(WaitTillStartEnds(GuitarLoop));
        }
    }
    private IEnumerator WaitTillStartEnds(AudioClip Loop)
    {
        yield return new WaitUntil(() => GuitarSource.isPlaying == false);
        GuitarSource.loop = true;
        GuitarSource.clip = Loop;
        GuitarSource.Play();
    }
}
