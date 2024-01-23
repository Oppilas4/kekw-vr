using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

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
    //Hello Henri!If you seeing this then you have to clean up quite some stuff here
    private void Start()
    {
        SkellyRenderers.Add(GameObject.Find("Bone_mesh.039").GetComponent<Renderer>());
        SkellyRenderers.Add(GameObject.Find("Bone_mesh.019").GetComponent<Renderer>());
        NormalRenderers.Add(GameObject.Find("asdMesh.002").GetComponent<Renderer>());
        NormalRenderers.Add(GameObject.Find("asdMesh.001").GetComponent<Renderer>());
        FlashSound = GameObject.Find("EatingSound").GetComponent<AudioSource>();
        Player = GameObject.Find("XrCharacterSetupWithHands(Clone)");
    }
    public void Flash()
    {
        FlashSound.PlayOneShot(FlashSoundClip);
        StopAllCoroutines();
        StartCoroutine(FlashSkellyHands());
        Player.transform.position = DeathPosition.transform.position;
    }

    IEnumerator FlashSkellyHands()
    {
        for (int i = 0; i < HowManyFlashes; i++)
        {
            foreach(Renderer foundRenderer in NormalRenderers)
            {
                foundRenderer.enabled = false;
            }
            foreach(Renderer foundRenderer in SkellyRenderers)
            {
                foundRenderer.enabled = true;
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
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ScrewDriver" || other.GetComponent<Elec_Multimeter>() != null || other.GetComponent<Elev_MultimeterSticky>() != null)
        {
            if (other.gameObject.GetComponent<XRGrabInteractable>().isSelected)
            {
                Flash();
            }          
        }
        
    }

}
