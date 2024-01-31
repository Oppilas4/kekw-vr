using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_LightBulb : MonoBehaviour
{
    ParticleSystem LightParticle;
    public Material EmissionGreen;
    public Material Glass;
    MeshRenderer LightMesh;
    AudioSource AudioSource;
    public Material Nothing;
    public bool Sandbox;
    public float NeededVoltage = 5;
    bool Broken = false;
    public int TimeToDestroy = 5;
    public ParticleSystem shards;
    Elec_SandNode ThisNode;

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        if (Sandbox)
        {
            XRBaseInteractable Interactable = GetComponent<XRBaseInteractable>();
            Interactable.onSelectEntered.AddListener(CheckVoltage);
            Interactable.onSelectExited.AddListener(DisableBulbXR);          
        }
        LightMesh = GetComponent<MeshRenderer>();
        LightParticle = GetComponentInChildren<ParticleSystem>();
        LightParticle.Stop();
        AudioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (ThisNode != null && ThisNode.currentVoltage == NeededVoltage)
        {
            BulbEnablee();
        }
    }
    public void BulbEnablee()
    {
        if(!Broken) 
        {
            LightMesh.material = EmissionGreen;
            if(!Sandbox) LightParticle.Play();
        }
        
    }
    public void BulbDisable()
    {
        if(!Broken)
        {
            LightMesh.material = Glass;
            if(!Sandbox) LightParticle.Stop();
        }
        
    }
    public void CheckVoltage(XRBaseInteractor Interactor)
    {
        ThisNode = Interactor.GetComponent<Elec_SandNode>();        
    }
    void DisableBulbXR(XRBaseInteractor Interactor)
    {
        BulbDisable();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" && !Broken)
        {
            LightMesh.material = Nothing;
            Broken = true;
            AudioSource.Play();
            shards.Play();
            StartCoroutine(DestroyAfterTime(TimeToDestroy));
        }
    }
    IEnumerator DestroyAfterTime(int time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
