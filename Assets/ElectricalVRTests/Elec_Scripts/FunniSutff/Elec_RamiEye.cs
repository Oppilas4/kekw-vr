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
    public AudioClip GuitarLoop,GuitarStart,Squeek;
    public List <AudioClip> FunnySounds;
    public float RandomSound;
    [Obsolete]
    void Start()
    
    {
        Animator = GetComponentInChildren<Animator>();
        RamiInteractable = GetComponent<XRBaseInteractable>();
        LookAtWhat = GameObject.Find("Main Camera");
        RamiSocketInteractable.onSelectExited.AddListener(MyLittleDancer);
        RamiSocketInteractable.onSelectEntered.AddListener(MyLittleGuitarHero);
    }

    private void MyLittleDancer(XRBaseInteractable arg0)
    {
        if (arg0.GetComponent<Collider>() != null) { arg0.GetComponent<Collider>().isTrigger = false; }   
        Animator.SetBool("Shredding",false);
        GuitarSource.loop = false;
        GuitarSource.Stop();
        GuitarSource.enabled = false;
    }
    public void PlaySound()
    {
        RandomSound = UnityEngine.Random.Range(0f, 100f);
        if (RandomSound < 5f) GuitarSource.PlayOneShot(FunnySounds[UnityEngine.Random.Range(0, FunnySounds.Count)]);
        else GuitarSource.PlayOneShot(Squeek);
    }
    void Update()
    {
        Reye.transform.LookAt(LookAtWhat.transform);
        Leye.transform.LookAt(LookAtWhat.transform);
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
            GuitarSource.enabled = true;
            other.GetComponent<Collider>().isTrigger = true;
            Animator.SetBool("Shredding",true);
            GuitarSource.PlayOneShot(GuitarStart);
            StartCoroutine(WaitTillStartEnds());
        }
    }
    private IEnumerator WaitTillStartEnds()
    {
        yield return new WaitUntil(() => GuitarSource.isPlaying == false);
        Animator.SetTrigger("Yeap");
    }
    public void GoodGuitar()
    {
        GuitarSource.loop = true;
        GuitarSource.clip = GuitarLoop;
        GuitarSource.Play();
    }
}
