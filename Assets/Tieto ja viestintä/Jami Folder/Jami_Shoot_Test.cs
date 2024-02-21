using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.XR.Interaction.Toolkit;
public class Jami_Shoot_Test : MonoBehaviour
{
    public Transform barrel;
    public ParticleSystem muzzleFlash;
    public ParticleSystem bulletImpact;
    public AudioSource audioLaser;
    public GameObject laserPrefab;
    public int shotPower;
    // Start is called before the first frame update
    void Start()
    {

        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        //grabbable.activated.AddListener(FireBullet);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FireBullet(ActivateEventArgs arg)
    {
        RaycastHit hit;
        audioLaser.Play();
        Instantiate(laserPrefab, barrel.position, barrel.rotation).GetComponent<Rigidbody>().AddForce(barrel.forward * shotPower);

    }
}
