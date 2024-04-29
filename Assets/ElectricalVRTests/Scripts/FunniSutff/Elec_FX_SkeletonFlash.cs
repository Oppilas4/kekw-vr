using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static Elec_Tero_AI;

public class Elec_FX_SkeletonFlash : MonoBehaviour
{
    public List<Renderer> NormalRenderers;
    public List<Renderer> SkellyRenderers;
    public int HowManyFlashes = 8;
    public float timeBetweenFlashes = 0.2f;
    AudioSource FlashSound;
    public AudioClip FlashSoundClip;
    GameObject Player;
    public GameObject DeathPosition;
    public ParticleSystem LHand, RHand;
    Elec_Haptics Haptics;
    public DeathKind EnumiWhatDeath;
    public enum DeathKind
    {
        DEATHBYPOWERISON,
        DEATHBYSCREWDRIVER,
        DEATHBYLIVEWIRES
        
    }
    private void Start()
    {
        Haptics = GetComponent<Elec_Haptics>();
        SkellyRenderers.Add(GameObject.Find("Bone_mesh.039").GetComponent<Renderer>());
        SkellyRenderers.Add(GameObject.Find("Bone_mesh.019").GetComponent<Renderer>());
        NormalRenderers.Add(GameObject.Find("asdMesh.002").GetComponent<Renderer>());
        NormalRenderers.Add(GameObject.Find("asdMesh.001").GetComponent<Renderer>());
        FlashSound = GameObject.Find("EatingSound").GetComponent<AudioSource>();
        Player = GameObject.Find("XR Origin");
    }
    public void Flash()
    {
        LHand.Play();
        RHand.Play();
        FlashSound.PlayOneShot(FlashSoundClip);
        StopAllCoroutines();
        StartCoroutine(FlashSkellyHands());
        Player.transform.position = DeathPosition.transform.position;
        Player.transform.rotation = DeathPosition.transform.rotation;
    }
    IEnumerator FlashSkellyHands()
    {
        Haptics.SendHaptics(1, 2);
        for (int i = 0; i < HowManyFlashes; i++)
        {
            foreach(Renderer foundRenderer in NormalRenderers)
            {
                foundRenderer.enabled = false;
            }
            foreach(Renderer foundRenderer in SkellyRenderers)
            {
                foundRenderer.enabled = true;
                LHand.transform.position = GameObject.FindWithTag("LeftHand").transform.position;
                RHand.transform.position = GameObject.FindWithTag("RightHand").transform.position;    
            }
            yield return null;
            yield return new WaitForSeconds(Mathf.Abs(timeBetweenFlashes));
                        foreach(Renderer foundRenderer in NormalRenderers)
            {
                foundRenderer.enabled = true;
            }
            foreach(Renderer foundRenderer in SkellyRenderers)
            {
                foundRenderer.enabled = false;
            }
            yield return null;
            yield return new WaitForSeconds(Mathf.Abs(timeBetweenFlashes));
        }
        switch (EnumiWhatDeath)
        {
            case DeathKind.DEATHBYPOWERISON:
                Elec_Tero_AI.Instance.kindaDed = Elec_Tero_AI.DeathKind.DEATHBYPOWERISON; break;
            case DeathKind.DEATHBYSCREWDRIVER:
                Elec_Tero_AI.Instance.kindaDed = Elec_Tero_AI.DeathKind.DEATHBYSCREWDRIVER; break;
            case DeathKind.DEATHBYLIVEWIRES:
                Elec_Tero_AI.Instance.kindaDed = Elec_Tero_AI.DeathKind.DEATHBYLIVEWIRES; break;
        }
        LHand.Stop();
        RHand.Stop();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ScrewDriver" || other.GetComponent<Elec_Multimeter>() != null || other.tag == "StickyMultiMeter")
        {
            if (other.gameObject.GetComponent<XRGrabInteractable>().isSelected && other.gameObject.GetComponent<XRGrabInteractable>().firstInteractorSelecting.transform.gameObject.GetComponent<XRDirectInteractor>() != null)
            { 
                Flash();
            }          
        }
        
    }
}
